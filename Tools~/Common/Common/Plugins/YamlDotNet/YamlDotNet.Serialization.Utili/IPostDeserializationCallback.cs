#nullable enable
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace YamlDotNet.Serialization.Utilities
{
	internal interface IPostDeserializationCallback
	{
		void OnDeserialization();
	}
}
