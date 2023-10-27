namespace AIO.YamlDotNet.Serialization
{
	internal interface IObjectGraphTraversalStrategy
	{
		void Traverse<TContext>(IObjectDescriptor graph, IObjectGraphVisitor<TContext> visitor, TContext context);
	}
}
