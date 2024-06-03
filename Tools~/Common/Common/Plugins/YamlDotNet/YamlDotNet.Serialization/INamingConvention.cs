namespace AIO.YamlDotNet.Serialization
{
	internal interface INamingConvention
	{
		string Apply(string value);
	}
}
