using System;
using System.Collections.Generic;
using System.Diagnostics;
using AIO.YamlDotNet.Core;
using AIO.YamlDotNet.Core.Events;
using AIO.YamlDotNet.Serialization;

#nullable enable
namespace AIO.YamlDotNet.RepresentationModel
{
	[DebuggerDisplay("{Value}")]
	internal sealed class YamlScalarNode : YamlNode, IYamlConvertible
	{
		public string? Value { get; set; }

		public ScalarStyle Style { get; set; }

		internal override YamlNodeType NodeType => YamlNodeType.Scalar;

		internal YamlScalarNode(IParser parser, DocumentLoadingState state)
		{
			Load(parser, state);
		}

		private void Load(IParser parser, DocumentLoadingState state)
		{
			Scalar scalar = parser.Consume<Scalar>();
			Load(scalar, state);
			Value = scalar.Value;
			Style = scalar.Style;
		}

		public YamlScalarNode()
		{
		}

		public YamlScalarNode(string? value)
		{
			Value = value;
		}

		internal override void ResolveAliases(DocumentLoadingState state)
		{
			throw new NotSupportedException("Resolving an alias on a scalar node does not make sense");
		}

		internal override void Emit(IEmitter emitter, EmitterState state)
		{
			emitter.Emit(new Scalar(base.Anchor, base.Tag, Value ?? string.Empty, Style, base.Tag.IsEmpty, isQuotedImplicit: false));
		}

		internal override void Accept(IYamlVisitor visitor)
		{
			visitor.Visit(this);
		}

		public override bool Equals(object? obj)
		{
			if (obj is YamlScalarNode yamlScalarNode && object.Equals(base.Tag, yamlScalarNode.Tag))
			{
				return object.Equals(Value, yamlScalarNode.Value);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return Core.HashCode.CombineHashCodes(base.Tag.GetHashCode(), Value);
		}

		public static explicit operator string?(YamlScalarNode value)
		{
			return value.Value;
		}

		internal override string ToString(RecursionLevel level)
		{
			return Value ?? string.Empty;
		}

		internal override IEnumerable<YamlNode> SafeAllNodes(RecursionLevel level)
		{
			yield return this;
		}

		void IYamlConvertible.Read(IParser parser, Type expectedType, ObjectDeserializer nestedObjectDeserializer)
		{
			Load(parser, new DocumentLoadingState());
		}

		void IYamlConvertible.Write(IEmitter emitter, ObjectSerializer nestedObjectSerializer)
		{
			Emit(emitter, new EmitterState());
		}
	}
}
