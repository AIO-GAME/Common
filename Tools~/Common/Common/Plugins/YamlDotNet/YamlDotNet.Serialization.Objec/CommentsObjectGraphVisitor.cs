using AIO.YamlDotNet.Core;
using AIO.YamlDotNet.Core.Events;

namespace AIO.YamlDotNet.Serialization.ObjectGraphVisitors
{
	internal sealed class CommentsObjectGraphVisitor : ChainedObjectGraphVisitor
	{
		public CommentsObjectGraphVisitor(IObjectGraphVisitor<IEmitter> nextVisitor)
			: base(nextVisitor)
		{
		}

		public override bool EnterMapping(IPropertyDescriptor key, IObjectDescriptor value, IEmitter context)
		{
			YamlMemberAttribute customAttribute = key.GetCustomAttribute<YamlMemberAttribute>();
			if (customAttribute != null && customAttribute.Description != null)
			{
				context.Emit(new Comment(customAttribute.Description, isInline: false));
			}
			return base.EnterMapping(key, value, context);
		}
	}
}
