using System;
using System.ComponentModel;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization.ObjectGraphVisitors
{
	internal sealed class DefaultExclusiveObjectGraphVisitor : ChainedObjectGraphVisitor
	{
		public DefaultExclusiveObjectGraphVisitor(IObjectGraphVisitor<IEmitter> nextVisitor)
			: base(nextVisitor)
		{
		}

		private static object? GetDefault(Type type)
		{
			if (!type.IsValueType())
			{
				return null;
			}
			return Activator.CreateInstance(type);
		}

		public override bool EnterMapping(IObjectDescriptor key, IObjectDescriptor value, IEmitter context)
		{
			if (!object.Equals(value.Value, GetDefault(value.Type)))
			{
				return base.EnterMapping(key, value, context);
			}
			return false;
		}

		public override bool EnterMapping(IPropertyDescriptor key, IObjectDescriptor value, IEmitter context)
		{
			DefaultValueAttribute customAttribute = key.GetCustomAttribute<DefaultValueAttribute>();
			object objB = ((customAttribute != null) ? customAttribute.Value : GetDefault(key.Type));
			if (!object.Equals(value.Value, objB))
			{
				return base.EnterMapping(key, value, context);
			}
			return false;
		}
	}
}
