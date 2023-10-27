using System;

namespace ICSharpCode.SharpZipLib.Tar
{
	internal class InvalidHeaderException : TarException
	{
		public InvalidHeaderException()
		{
		}

		public InvalidHeaderException(string message)
			: base(message)
		{
		}

		public InvalidHeaderException(string message, Exception exception)
			: base(message, exception)
		{
		}
	}
}
