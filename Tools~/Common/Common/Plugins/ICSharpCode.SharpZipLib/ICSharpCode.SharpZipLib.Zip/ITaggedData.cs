namespace ICSharpCode.SharpZipLib.Zip
{
	internal interface ITaggedData
	{
		short TagID { get; }

		void SetData(byte[] data, int offset, int count);

		byte[] GetData();
	}
}
