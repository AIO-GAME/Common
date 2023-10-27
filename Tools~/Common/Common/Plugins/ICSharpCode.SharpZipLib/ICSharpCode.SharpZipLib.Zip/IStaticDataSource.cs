using System.IO;

namespace AIO.ICSharpCode.SharpZipLib.Zip
{
	internal interface IStaticDataSource
	{
		Stream GetSource();
	}
}
