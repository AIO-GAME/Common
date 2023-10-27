using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	internal interface IStaticDataSource
	{
		Stream GetSource();
	}
}
