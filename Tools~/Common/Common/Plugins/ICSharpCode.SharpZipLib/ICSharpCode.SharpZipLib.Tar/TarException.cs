using System;

namespace AIO.ICSharpCode.SharpZipLib.Tar
{
	internal class TarException : SharpZipBaseException
	{
		public TarException()
		{
		}

		public TarException(string message)
			: base(message)
		{
		}

		public TarException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
