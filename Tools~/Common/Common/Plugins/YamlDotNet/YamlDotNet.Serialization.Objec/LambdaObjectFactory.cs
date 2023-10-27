using System;

namespace YamlDotNet.Serialization.ObjectFactories
{
	internal sealed class LambdaObjectFactory : ObjectFactoryBase
	{
		private readonly Func<Type, object> factory;

		public LambdaObjectFactory(Func<Type, object> factory)
		{
			this.factory = factory ?? throw new ArgumentNullException("factory");
		}

		public override object Create(Type type)
		{
			return factory(type);
		}
	}
}
