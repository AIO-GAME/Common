using System;

namespace AIO.ICSharpCode.SharpZipLib.Lzw
{
	internal class LzwException : SharpZipBaseException
	{
		public LzwException()
		{
		}

		public LzwException(string message)
			: base(message)
		{
		}

		public LzwException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
