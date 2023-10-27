using System;
using AIO.YamlDotNet.Serialization.ObjectFactories;

namespace AIO.YamlDotNet.Serialization
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
