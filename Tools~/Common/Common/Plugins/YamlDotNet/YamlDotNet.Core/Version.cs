using System;

namespace AIO.YamlDotNet.Core
{
	internal sealed class Version
	{
		public int Major { get; }

		public int Minor { get; }

		public Version(int major, int minor)
		{
			if (major < 0)
			{
				throw new ArgumentOutOfRangeException("major", $"{major} should be >= 0");
			}
			Major = major;
			if (minor < 0)
			{
				throw new ArgumentOutOfRangeException("minor", $"{minor} should be >= 0");
			}
			Minor = minor;
		}

		public override bool Equals(object? obj)
		{
			if (obj is Version version && Major == version.Major)
			{
				return Minor == version.Minor;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.CombineHashCodes(Major.GetHashCode(), Minor.GetHashCode());
		}
	}
}
