using System;

namespace AIO.YamlDotNet.Serialization
{
	internal interface IValuePromise
	{
		event Action<object?> ValueAvailable;
	}
}
