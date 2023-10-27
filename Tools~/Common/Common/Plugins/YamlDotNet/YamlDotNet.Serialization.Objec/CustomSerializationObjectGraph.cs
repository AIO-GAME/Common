using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization.ObjectGraphVisitors
{
	internal sealed class CustomSerializationObjectGraphVisitor : ChainedObjectGraphVisitor
	{
		private readonly IEnumerable<IYamlTypeConverter> typeConverters;

		private readonly ObjectSerializer nestedObjectSerializer;

		public CustomSerializationObjectGraphVisitor(IObjectGraphVisitor<IEmitter> nextVisitor, IEnumerable<IYamlTypeConverter> typeConverters, ObjectSerializer nestedObjectSerializer)
			: base(nextVisitor)
		{
			IEnumerable<IYamlTypeConverter> enumerable;
			if (typeConverters == null)
			{
				enumerable = Enumerable.Empty<IYamlTypeConverter>();
			}
			else
			{
				IEnumerable<IYamlTypeConverter> enumerable2 = typeConverters.ToList();
				enumerable = enumerable2;
			}
			this.typeConverters = enumerable;
			this.nestedObjectSerializer = nestedObjectSerializer;
		}

		public override bool Enter(IObjectDescriptor value, IEmitter context)
		{
			IObjectDescriptor value2 = value;
			IYamlTypeConverter yamlTypeConverter = typeConverters.FirstOrDefault((IYamlTypeConverter t) => t.Accepts(value2.Type));
			if (yamlTypeConverter != null)
			{
				yamlTypeConverter.WriteYaml(context, value2.Value, value2.Type);
				return false;
			}
			if (value2.Value is IYamlConvertible yamlConvertible)
			{
				yamlConvertible.Write(context, nestedObjectSerializer);
				return false;
			}
			if (value2.Value is IYamlSerializable yamlSerializable)
			{
				yamlSerializable.WriteYaml(context);
				return false;
			}
			return base.Enter(value2, context);
		}
	}
}
