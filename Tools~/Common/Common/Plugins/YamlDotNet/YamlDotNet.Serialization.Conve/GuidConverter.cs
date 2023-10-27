using System;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace YamlDotNet.Serialization.Converters
{
	internal class GuidConverter : IYamlTypeConverter
	{
		private readonly bool jsonCompatible;

		public GuidConverter(bool jsonCompatible)
		{
			this.jsonCompatible = jsonCompatible;
		}

		public bool Accepts(Type type)
		{
			return type == typeof(Guid);
		}

		public object ReadYaml(IParser parser, Type type)
		{
			return new Guid(parser.Consume<Scalar>().Value);
		}

		public void WriteYaml(IEmitter emitter, object? value, Type type)
		{
			emitter.Emit(new Scalar(value: ((Guid)value).ToString("D"), anchor: AnchorName.Empty, tag: TagName.Empty, style: jsonCompatible ? ScalarStyle.DoubleQuoted : ScalarStyle.Any, isPlainImplicit: true, isQuotedImplicit: false));
		}
	}
}
