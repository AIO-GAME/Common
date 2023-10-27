using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AIO.YamlDotNet.RepresentationModel
{
	internal sealed class YamlNodeIdentityEqualityComparer : IEqualityComparer<YamlNode>
	{
		public bool Equals([AllowNull] YamlNode x, [AllowNull] YamlNode y)
		{
			return x == y;
		}

		public int GetHashCode(YamlNode obj)
		{
			return obj.GetHashCode();
		}
	}
}
