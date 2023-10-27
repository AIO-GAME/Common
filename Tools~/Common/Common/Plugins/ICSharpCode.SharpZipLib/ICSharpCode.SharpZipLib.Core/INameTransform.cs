namespace ICSharpCode.SharpZipLib.Core
{
	internal interface INameTransform
	{
		string TransformFile(string name);

		string TransformDirectory(string name);
	}
}
