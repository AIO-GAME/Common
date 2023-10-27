using System;
using System.Collections.Generic;
using System.Linq;

namespace AIO.YamlDotNet.Serialization.ObjectGraphTraversalStrategies
{
	internal class RoundtripObjectGraphTraversalStrategy : FullObjectGraphTraversalStrategy
	{
		private readonly IEnumerable<IYamlTypeConverter> converters;

		private readonly Settings settings;

		public RoundtripObjectGraphTraversalStrategy(IEnumerable<IYamlTypeConverter> converters, ITypeInspector typeDescriptor, ITypeResolver typeResolver, int maxRecursion, INamingConvention namingConvention, Settings settings, IObjectFactory factory)
			: base(typeDescriptor, typeResolver, maxRecursion, namingConvention, factory)
		{
			this.converters = converters;
			this.settings = settings;
		}

		protected override void TraverseProperties<TContext>(IObjectDescriptor value, IObjectGraphVisitor<TContext> visitor, TContext context, Stack<ObjectPathSegment> path)
		{
			IObjectDescriptor value2 = value;
			if (!value2.Type.HasDefaultConstructor(settings.AllowPrivateConstructors) && !converters.Any((IYamlTypeConverter c) => c.Accepts(value2.Type)))
			{
				throw new InvalidOperationException($"Type '{value2.Type}' cannot be deserialized because it does not have a default constructor or a type converter.");
			}
			base.TraverseProperties(value2, visitor, context, path);
		}
	}
}
