#nullable enable
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Core
{
	internal static class ParserExtensions
	{
		public static T Consume<T>(this IParser parser) where T : ParsingEvent
		{
			T result = parser.Require<T>();
			parser.MoveNext();
			return result;
		}

		public static bool TryConsume<T>(this IParser parser, [MaybeNullWhen(false)] out T @event) where T : ParsingEvent
		{
			if (parser.Accept<T>(out @event))
			{
				parser.MoveNext();
				return true;
			}
			return false;
		}

		public static T Require<T>(this IParser parser) where T : ParsingEvent
		{
			if (!parser.Accept<T>(out var @event))
			{
				ParsingEvent current = parser.Current;
				if (current == null)
				{
					throw new YamlException("Expected '" + typeof(T).Name + "', got nothing.");
				}
				Mark start = current.Start;
				Mark end = current.End;
				throw new YamlException(in start, in end, $"Expected '{typeof(T).Name}', got '{current.GetType().Name}' (at {current.Start}).");
			}
			return @event;
		}

		public static bool Accept<T>(this IParser parser, [MaybeNullWhen(false)] out T @event) where T : ParsingEvent
		{
			if (parser.Current == null && !parser.MoveNext())
			{
				throw new EndOfStreamException();
			}
			if (parser.Current is T val)
			{
				@event = val;
				return true;
			}
			@event = null;
			return false;
		}

		public static void SkipThisAndNestedEvents(this IParser parser)
		{
			int num = 0;
			do
			{
				ParsingEvent parsingEvent = parser.Consume<ParsingEvent>();
				num += parsingEvent.NestingIncrease;
			}
			while (num > 0);
		}

		[Obsolete("Please use Consume<T>() instead")]
		public static T Expect<T>(this IParser parser) where T : ParsingEvent
		{
			return parser.Consume<T>();
		}

		[Obsolete("Please use TryConsume<T>(out var evt) instead")]
		[return: MaybeNull]
		public static T? Allow<T>(this IParser parser) where T : ParsingEvent
		{
			if (!parser.TryConsume<T>(out var @event))
			{
				return null;
			}
			return @event;
		}

		[Obsolete("Please use Accept<T>(out var evt) instead")]
		[return: MaybeNull]
		public static T? Peek<T>(this IParser parser) where T : ParsingEvent
		{
			if (!parser.Accept<T>(out var @event))
			{
				return null;
			}
			return @event;
		}

		[Obsolete("Please use TryConsume<T>(out var evt) or Accept<T>(out var evt) instead")]
		public static bool Accept<T>(this IParser parser) where T : ParsingEvent
		{
			T @event;
			return parser.Accept<T>(out @event);
		}

		public static bool TryFindMappingEntry(this IParser parser, Func<Scalar, bool> selector, [MaybeNullWhen(false)] out Scalar? key, [MaybeNullWhen(false)] out ParsingEvent? value)
		{
			if (parser.TryConsume<MappingStart>(out var _))
			{
				while (parser.Current != null)
				{
					ParsingEvent current = parser.Current;
					if (!(current is Scalar scalar))
					{
						if (current is MappingStart || current is SequenceStart)
						{
							parser.SkipThisAndNestedEvents();
						}
						else
						{
							parser.MoveNext();
						}
						continue;
					}
					bool num = selector(scalar);
					parser.MoveNext();
					if (num)
					{
						value = parser.Current;
						key = scalar;
						return true;
					}
					parser.SkipThisAndNestedEvents();
				}
			}
			key = null;
			value = null;
			return false;
		}
	}
}
