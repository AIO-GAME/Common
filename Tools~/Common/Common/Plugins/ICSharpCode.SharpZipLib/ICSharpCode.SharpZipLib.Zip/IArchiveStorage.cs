using System.IO;

namespace AIO.ICSharpCode.SharpZipLib.Zip
{
	internal interface IArchiveStorage
	{
		FileUpdateMode UpdateMode { get; }

		Stream GetTemporaryOutput();

		Stream ConvertTemporaryToFinal();

		Stream MakeTemporaryCopy(Stream stream);

		Stream OpenForDirectUpdate(Stream stream);

		void Dispose();
	}
}
