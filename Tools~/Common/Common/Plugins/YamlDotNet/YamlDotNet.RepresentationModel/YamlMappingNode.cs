using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AIO.YamlDotNet.Core;
using AIO.YamlDotNet.Core.Events;
using AIO.YamlDotNet.Helpers;
using AIO.YamlDotNet.Serialization;

namespace AIO.YamlDotNet.RepresentationModel
{
	internal sealed class YamlMappingNode : YamlNode, IEnumerable<KeyValuePair<YamlNode, YamlNode>>, IEnumerable, IYamlConvertible
	{
		private readonly OrderedDictionary<YamlNode, YamlNode> children = new OrderedDictionary<YamlNode, YamlNode>();

		public IOrderedDictionary<YamlNode, YamlNode> Children => children;

		public MappingStyle Style { get; set; }

		internal override YamlNodeType NodeType => YamlNodeType.Mapping;

		internal YamlMappingNode(IParser parser, DocumentLoadingState state)
		{
			Load(parser, state);
		}

		private void Load(IParser parser, DocumentLoadingState state)
		{
			MappingStart mappingStart = parser.Consume<MappingStart>();
			Load(mappingStart, state);
			Style = mappingStart.Style;
			bool flag = false;
			MappingEnd @event;
			while (!parser.TryConsume<MappingEnd>(out @event))
			{
				YamlNode yamlNode = YamlNode.ParseNode(parser, state);
				YamlNode yamlNode2 = YamlNode.ParseNode(parser, state);
				try
				{
					children.Add(yamlNode, yamlNode2);
				}
				catch (ArgumentException innerException)
				{
					Mark start = yamlNode.Start;
					Mark end = yamlNode.End;
					throw new YamlException(in start, in end, "Duplicate key", innerException);
				}
				flag = flag || yamlNode is YamlAliasNode || yamlNode2 is YamlAliasNode;
			}
			if (flag)
			{
				state.AddNodeWithUnresolvedAliases(this);
			}
		}

		public YamlMappingNode()
		{
		}

		public YamlMappingNode(params KeyValuePair<YamlNode, YamlNode>[] children)
			: this((IEnumerable<KeyValuePair<YamlNode, YamlNode>>)children)
		{
		}

		public YamlMappingNode(IEnumerable<KeyValuePair<YamlNode, YamlNode>> children)
		{
			foreach (KeyValuePair<YamlNode, YamlNode> child in children)
			{
				this.children.Add(child);
			}
		}

		public YamlMappingNode(params YamlNode[] children)
			: this((IEnumerable<YamlNode>)children)
		{
		}

		public YamlMappingNode(IEnumerable<YamlNode> children)
		{
			using IEnumerator<YamlNode> enumerator = children.GetEnumerator();
			while (enumerator.MoveNext())
			{
				YamlNode current = enumerator.Current;
				if (!enumerator.MoveNext())
				{
					throw new ArgumentException("When constructing a mapping node with a sequence, the number of elements of the sequence must be even.");
				}
				Add(current, enumerator.Current);
			}
		}

		public void Add(YamlNode key, YamlNode value)
		{
			children.Add(key, value);
		}

		public void Add(string key, YamlNode value)
		{
			children.Add(new YamlScalarNode(key), value);
		}

		public void Add(YamlNode key, string value)
		{
			children.Add(key, new YamlScalarNode(value));
		}

		public void Add(string key, string value)
		{
			children.Add(new YamlScalarNode(key), new YamlScalarNode(value));
		}

		internal override void ResolveAliases(DocumentLoadingState state)
		{
			Dictionary<YamlNode, YamlNode> dictionary = null;
			Dictionary<YamlNode, YamlNode> dictionary2 = null;
			foreach (KeyValuePair<YamlNode, YamlNode> child in children)
			{
				if (child.Key is YamlAliasNode)
				{
					if (dictionary == null)
					{
						dictionary = new Dictionary<YamlNode, YamlNode>();
					}
					dictionary.Add(child.Key, state.GetNode(child.Key.Anchor, child.Key.Start, child.Key.End));
				}
				if (child.Value is YamlAliasNode)
				{
					if (dictionary2 == null)
					{
						dictionary2 = new Dictionary<YamlNode, YamlNode>();
					}
					dictionary2.Add(child.Key, state.GetNode(child.Value.Anchor, child.Value.Start, child.Value.End));
				}
			}
			if (dictionary2 != null)
			{
				foreach (KeyValuePair<YamlNode, YamlNode> item in dictionary2)
				{
					children[item.Key] = item.Value;
				}
			}
			if (dictionary == null)
			{
				return;
			}
			foreach (KeyValuePair<YamlNode, YamlNode> item2 in dictionary)
			{
				YamlNode value = children[item2.Key];
				children.Remove(item2.Key);
				children.Add(item2.Value, value);
			}
		}

		internal override void Emit(IEmitter emitter, EmitterState state)
		{
			emitter.Emit(new MappingStart(base.Anchor, base.Tag, isImplicit: true, Style));
			foreach (KeyValuePair<YamlNode, YamlNode> child in children)
			{
				child.Key.Save(emitter, state);
				child.Value.Save(emitter, state);
			}
			emitter.Emit(new MappingEnd());
		}

		internal override void Accept(IYamlVisitor visitor)
		{
			visitor.Visit(this);
		}

		public override bool Equals(object? obj)
		{
			if (!(obj is YamlMappingNode yamlMappingNode) || !object.Equals(base.Tag, yamlMappingNode.Tag) || children.Count != yamlMappingNode.children.Count)
			{
				return false;
			}
			foreach (KeyValuePair<YamlNode, YamlNode> child in children)
			{
				if (!yamlMappingNode.children.TryGetValue(child.Key, out var value) || !object.Equals(child.Value, value))
				{
					return false;
				}
			}
			return true;
		}

		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			foreach (KeyValuePair<YamlNode, YamlNode> child in children)
			{
				num = Core.HashCode.CombineHashCodes(num, child.Key);
				num = Core.HashCode.CombineHashCodes(num, child.Value);
			}
			return num;
		}

		internal override IEnumerable<YamlNode> SafeAllNodes(RecursionLevel level)
		{
			level.Increment();
			yield return this;
			foreach (KeyValuePair<YamlNode, YamlNode> child in children)
			{
				foreach (YamlNode item in child.Key.SafeAllNodes(level))
				{
					yield return item;
				}
				foreach (YamlNode item2 in child.Value.SafeAllNodes(level))
				{
					yield return item2;
				}
			}
			level.Decrement();
		}

		internal override string ToString(RecursionLevel level)
		{
			if (!level.TryIncrement())
			{
				return "WARNING! INFINITE RECURSION!";
			}
			StringBuilderPool.BuilderWrapper builderWrapper = StringBuilderPool.Rent();
			try
			{
				StringBuilder builder = builderWrapper.Builder;
				builder.Append("{ ");
				foreach (KeyValuePair<YamlNode, YamlNode> child in children)
				{
					if (builder.Length > 2)
					{
						builder.Append(", ");
					}
					builder.Append("{ ").Append(child.Key.ToString(level)).Append(", ")
						.Append(child.Value.ToString(level))
						.Append(" }");
				}
				builder.Append(" }");
				level.Decrement();
				return builder.ToString();
			}
			finally
			{
				((IDisposable)builderWrapper).Dispose();
			}
		}

		public IEnumerator<KeyValuePair<YamlNode, YamlNode>> GetEnumerator()
		{
			return children.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		void IYamlConvertible.Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
		{
			Load(parser, new DocumentLoadingState());
		}

		void IYamlConvertible.Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
		{
			Emit(emitter, new EmitterState());
		}

		public static YamlMappingNode FromObject(object mapping)
		{
			if (mapping == null)
			{
				throw new ArgumentNullException("mapping");
			}
			YamlMappingNode yamlMappingNode = new YamlMappingNode();
			foreach (PropertyInfo publicProperty in mapping.GetType().GetPublicProperties())
			{
				if (publicProperty.CanRead && publicProperty.GetGetMethod(nonPublic: false).GetParameters().Length == 0)
				{
					object value = publicProperty.GetValue(mapping, null);
					YamlNode yamlNode = value as YamlNode;
					if (yamlNode == null)
					{
						yamlNode = Convert.ToString(value) ?? string.Empty;
					}
					yamlMappingNode.Add(publicProperty.Name, yamlNode);
				}
			}
			return yamlMappingNode;
		}
	}
}
