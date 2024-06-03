using AIO.ICSharpCode.SharpZipLib.Core;

namespace AIO.ICSharpCode.SharpZipLib.Zip
{
	internal interface IEntryFactory
	{
		INameTransform NameTransform { get; set; }

		ZipEntry MakeFileEntry(string fileName);

		ZipEntry MakeFileEntry(string fileName, bool useFileSystem);

		ZipEntry MakeFileEntry(string fileName, string entryName, bool useFileSystem);

		ZipEntry MakeDirectoryEntry(string directoryName);

		ZipEntry MakeDirectoryEntry(string directoryName, bool useFileSystem);
	}
}
