using System;

namespace AIO.ICSharpCode.SharpZipLib.GZip
{
	internal class GZipException : SharpZipBaseException
	{
		public GZipException()
		{
		}

		public GZipException(string message)
			: base(message)
		{
		}

		public GZipException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
