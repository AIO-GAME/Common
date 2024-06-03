using System.Collections.Generic;

namespace AIO.YamlDotNet.Serialization
{
	internal delegate IObjectGraphTraversalStrategy ObjectGraphTraversalStrategyFactory(ITypeInspector typeInspector, ITypeResolver typeResolver, IEnumerable<IYamlTypeConverter> typeConverters, int maximumRecursion);
}
