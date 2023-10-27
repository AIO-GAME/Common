using System;
using AIO.YamlDotNet.Core;
using AIO.YamlDotNet.Core.Events;

namespace AIO.YamlDotNet.Serialization.Converters
{
	internal class SystemTypeConverter : IYamlTypeConverter
	{
		public bool Accepts(Type type)
		{
			return typeof(Type).IsAssignableFrom(type);
		}

		public object ReadYaml(IParser parser, Type type)
		{
			return Type.GetType(parser.Consume<Scalar>().Value, throwOnError: true);
		}

		public void WriteYaml(IEmitter emitter, object? value, Type type)
		{
			Type type2 = (Type)value;
			emitter.Emit(new Scalar(AnchorName.Empty, TagName.Empty, type2.AssemblyQualifiedName, ScalarStyle.Any, isPlainImplicit: true, isQuotedImplicit: false));
		}
	}
}
