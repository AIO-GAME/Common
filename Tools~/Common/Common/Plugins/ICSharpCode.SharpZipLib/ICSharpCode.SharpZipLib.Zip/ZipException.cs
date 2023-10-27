using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	internal class ZipException : SharpZipBaseException
	{
		public ZipException()
		{
		}

		public ZipException(string message)
			: base(message)
		{
		}

		public ZipException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
