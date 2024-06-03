namespace AIO.YamlDotNet.Core.Tokens
{
	internal sealed class VersionDirective : Token
	{
		public Version Version { get; }

		public VersionDirective(Version version)
			: this(version, Mark.Empty, Mark.Empty)
		{
		}

		public VersionDirective(Version version, Mark start, Mark end)
			: base(in start, in end)
		{
			Version = version;
		}

		public override bool Equals(object? obj)
		{
			if (obj is VersionDirective versionDirective)
			{
				return Version.Equals(versionDirective.Version);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return Version.GetHashCode();
		}
	}
}
