using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	internal class StaticDiskDataSource : IStaticDataSource
	{
		private readonly string fileName_;

		public StaticDiskDataSource(string fileName)
		{
			fileName_ = fileName;
		}

		public Stream GetSource()
		{
			return File.Open(fileName_, FileMode.Open, FileAccess.Read, FileShare.Read);
		}
	}
}
