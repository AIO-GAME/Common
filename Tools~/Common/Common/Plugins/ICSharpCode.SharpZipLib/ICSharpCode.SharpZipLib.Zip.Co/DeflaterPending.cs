namespace AIO.ICSharpCode.SharpZipLib.Zip.Compression
{
	internal class DeflaterPending : PendingBuffer
	{
		public DeflaterPending()
			: base(65536)
		{
		}
	}
}
