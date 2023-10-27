using System.IO;

namespace AIO.ICSharpCode.SharpZipLib.Zip
{
	internal interface IDynamicDataSource
	{
		Stream GetSource(ZipEntry entry, string name);
	}
}
