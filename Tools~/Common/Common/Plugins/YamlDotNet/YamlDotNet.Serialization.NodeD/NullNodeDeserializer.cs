using System;
using AIO.YamlDotNet.Core;
using AIO.YamlDotNet.Core.Events;

namespace AIO.YamlDotNet.Serialization.NodeDeserializers
{
	internal sealed class NullNodeDeserializer : INodeDeserializer
	{
		public bool Deserialize(IParser parser, Type expectedType, Func<IParser, Type, object?> nestedObjectDeserializer, out object? value)
		{
			value = null;
			if (parser.Accept<NodeEvent>(out var @event) && NodeIsNull(@event))
			{
				parser.SkipThisAndNestedEvents();
				return true;
			}
			return false;
		}

		private bool NodeIsNull(NodeEvent nodeEvent)
		{
			if (nodeEvent.Tag == "tag:yaml.org,2002:null")
			{
				return true;
			}
			if (nodeEvent is Scalar scalar && scalar.Style == ScalarStyle.Plain && !scalar.IsKey)
			{
				string value = scalar.Value;
				switch (value)
				{
				default:
					return value == "NULL";
				case "":
				case "~":
				case "null":
				case "Null":
					return true;
				}
			}
			return false;
		}
	}
}
