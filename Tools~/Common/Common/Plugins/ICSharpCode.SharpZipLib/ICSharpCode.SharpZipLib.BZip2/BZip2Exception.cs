using System;

namespace ICSharpCode.SharpZipLib.BZip2
{
	internal class BZip2Exception : SharpZipBaseException
	{
		public BZip2Exception()
		{
		}

		public BZip2Exception(string message)
			: base(message)
		{
		}

		public BZip2Exception(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}
