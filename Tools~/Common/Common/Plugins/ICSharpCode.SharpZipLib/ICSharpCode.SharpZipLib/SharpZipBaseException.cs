using System;

namespace ICSharpCode.SharpZipLib
{
	internal class SharpZipBaseException : Exception
	{
		public SharpZipBaseException()
		{
		}

		public SharpZipBaseException(string message)
			: base(message)
		{
		}

		public SharpZipBaseException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
