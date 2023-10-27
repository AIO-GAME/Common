namespace AIO.YamlDotNet.Core.Tokens
{
	internal class Error : Token
	{
		public string Value { get; }

		public Error(string value, Mark start, Mark end)
			: base(in start, in end)
		{
			Value = value;
		}
	}
}
