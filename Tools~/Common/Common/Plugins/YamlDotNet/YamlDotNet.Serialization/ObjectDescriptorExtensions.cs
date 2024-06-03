using System;

namespace AIO.YamlDotNet.Serialization
{
	internal static class ObjectDescriptorExtensions
	{
		public static object NonNullValue(this IObjectDescriptor objectDescriptor)
		{
			return objectDescriptor.Value ?? throw new InvalidOperationException("Attempted to use a IObjectDescriptor of type '" + objectDescriptor.Type.FullName + "' whose Value is null at a point whete it is invalid to do so. This may indicate a bug in YamlDotNet.");
		}
	}
}
