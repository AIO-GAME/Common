using System;

namespace YamlDotNet.Serialization
{
	internal interface IValuePromise
	{
		event Action<object?> ValueAvailable;
	}
}
