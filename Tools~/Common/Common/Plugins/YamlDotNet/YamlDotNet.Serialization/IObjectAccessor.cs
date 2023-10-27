namespace YamlDotNet.Serialization
{
	internal interface IObjectAccessor
	{
		void Set(string name, object target, object value);

		object? Read(string name, object target);
	}
}
