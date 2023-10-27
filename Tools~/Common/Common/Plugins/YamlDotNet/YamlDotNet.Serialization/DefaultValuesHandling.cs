using System;

namespace AIO.YamlDotNet.Serialization
{
	[Flags]
	internal enum DefaultValuesHandling
	{
		Preserve = 0,
		OmitNull = 1,
		OmitDefaults = 2,
		OmitEmptyCollections = 4
	}
}
