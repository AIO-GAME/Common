using System;
using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Serialization.BufferedDeserialization
{
	internal class ParserBuffer : IParser
	{
		private readonly LinkedList<ParsingEvent> buffer;

		private LinkedListNode<ParsingEvent>? current;

		public ParsingEvent? Current => current?.Value;

		public ParserBuffer(IParser parserToBuffer, int maxDepth, int maxLength)
		{
			buffer = new LinkedList<ParsingEvent>();
			buffer.AddLast(parserToBuffer.Consume<MappingStart>());
			int num = 0;
			do
			{
				ParsingEvent parsingEvent = parserToBuffer.Consume<ParsingEvent>();
				num += parsingEvent.NestingIncrease;
				buffer.AddLast(parsingEvent);
				if (maxDepth > -1 && num > maxDepth)
				{
					throw new ArgumentOutOfRangeException("parserToBuffer", "Parser buffer exceeded max depth");
				}
				if (maxLength > -1 && buffer.Count > maxLength)
				{
					throw new ArgumentOutOfRangeException("parserToBuffer", "Parser buffer exceeded max length");
				}
			}
			while (num >= 0);
			current = buffer.First;
		}

		public bool MoveNext()
		{
			current = current?.Next;
			return current != null;
		}

		public void Reset()
		{
			current = buffer.First;
		}
	}
}
