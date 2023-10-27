using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	internal interface IDynamicDataSource
	{
		Stream GetSource(ZipEntry entry, string name);
	}
}
