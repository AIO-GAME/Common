using System;
using YamlDotNet.Serialization.ObjectFactories;

namespace YamlDotNet.Serialization
{
	internal abstract class StaticContext
	{
		public virtual StaticObjectFactory GetFactory()
		{
			throw new NotImplementedException();
		}

		public virtual ITypeInspector GetTypeInspector()
		{
			throw new NotImplementedException();
		}
	}
}
