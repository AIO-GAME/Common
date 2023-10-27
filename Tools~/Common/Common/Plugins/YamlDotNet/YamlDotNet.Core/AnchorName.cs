using System;
using System.Text.RegularExpressions;
#nullable enable
namespace YamlDotNet.Core
{
	internal struct AnchorName : IEquatable<AnchorName>
	{
		public static readonly AnchorName Empty = default(AnchorName);

		private static readonly Regex AnchorPattern = new Regex("^[^\\[\\]\\{\\},]+$", RegexOptions.Compiled);

		private readonly string? value;

		public string Value => value ?? throw new InvalidOperationException("Cannot read the Value of an empty anchor");

		public bool IsEmpty => value == null;

		public AnchorName(string value)
		{
			this.value = value ?? throw new ArgumentNullException("value");
			if (!AnchorPattern.IsMatch(value))
			{
				throw new ArgumentException("Anchor cannot be empty or contain disallowed characters: []{},\nThe value was '" + value + "'.", "value");
			}
		}

		public override string ToString()
		{
			return value ?? "[empty]";
		}

		public bool Equals(AnchorName other)
		{
			return object.Equals(value, other.value);
		}

		public override bool Equals(object? obj)
		{
			if (obj is AnchorName other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return value?.GetHashCode() ?? 0;
		}

		public static bool operator ==(AnchorName left, AnchorName right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(AnchorName left, AnchorName right)
		{
			return !(left == right);
		}

		public static implicit operator AnchorName(string? value)
		{
			if (value != null)
			{
				return new AnchorName(value);
			}
			return Empty;
		}
	}
}
