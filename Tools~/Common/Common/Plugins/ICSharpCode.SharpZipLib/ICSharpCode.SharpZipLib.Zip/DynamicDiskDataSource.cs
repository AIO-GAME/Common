using System.IO;

namespace AIO.ICSharpCode.SharpZipLib.Zip
{
	internal class DynamicDiskDataSource : IDynamicDataSource
	{
		public Stream GetSource(ZipEntry entry, string name)
		{
			Stream result = null;
			if (name != null)
			{
				result = File.Open(name, FileMode.Open, FileAccess.Read, FileShare.Read);
			}
			return result;
		}
	}
}
