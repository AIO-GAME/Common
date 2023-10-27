using System;
using System.IO;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization.Utilities;
#nullable enable
namespace YamlDotNet.Serialization
{
	internal sealed class Deserializer : IDeserializer
	{
		private readonly IValueDeserializer valueDeserializer;

		public Deserializer()
			: this(new DeserializerBuilder().BuildValueDeserializer())
		{
		}

		private Deserializer(IValueDeserializer valueDeserializer)
		{
			this.valueDeserializer = valueDeserializer ?? throw new ArgumentNullException("valueDeserializer");
		}

		public static Deserializer FromValueDeserializer(IValueDeserializer valueDeserializer)
		{
			return new Deserializer(valueDeserializer);
		}

		public T Deserialize<T>(string input)
		{
			using StringReader input2 = new StringReader(input);
			return Deserialize<T>(input2);
		}

		public T Deserialize<T>(TextReader input)
		{
			return Deserialize<T>(new Parser(input));
		}

		public object? Deserialize(TextReader input)
		{
			return Deserialize(input, typeof(object));
		}

		public object? Deserialize(string input, Type type)
		{
			using StringReader input2 = new StringReader(input);
			return Deserialize(input2, type);
		}

		public object? Deserialize(TextReader input, Type type)
		{
			return Deserialize(new Parser(input), type);
		}

		public T Deserialize<T>(IParser parser)
		{
			return (T)Deserialize(parser, typeof(T));
		}

		public object? Deserialize(IParser parser)
		{
			return Deserialize(parser, typeof(object));
		}

		public object? Deserialize(IParser parser, Type type)
		{
			if (parser == null)
			{
				throw new ArgumentNullException("parser");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			StreamStart @event;
			bool flag = parser.TryConsume<StreamStart>(out @event);
			DocumentStart event2;
			bool flag2 = parser.TryConsume<DocumentStart>(out event2);
			object result = null;
			if (!parser.Accept<DocumentEnd>(out var _) && !parser.Accept<StreamEnd>(out var _))
			{
				using SerializerState serializerState = new SerializerState();
				result = valueDeserializer.DeserializeValue(parser, type, serializerState, valueDeserializer);
				serializerState.OnDeserialization();
			}
			if (flag2)
			{
				parser.Consume<DocumentEnd>();
			}
			if (flag)
			{
				parser.Consume<StreamEnd>();
			}
			return result;
		}
	}
}
