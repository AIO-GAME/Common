namespace YamlDotNet.Core
{
	internal sealed class SimpleKey
	{
		private readonly Cursor cursor;

		public bool IsPossible { get; private set; }

		public bool IsRequired { get; }

		public int TokenNumber { get; }

		public int Index => cursor.Index;

		public int Line => cursor.Line;

		public int LineOffset => cursor.LineOffset;

		public Mark Mark => cursor.Mark();

		public void MarkAsImpossible()
		{
			IsPossible = false;
		}

		public SimpleKey()
		{
			cursor = new Cursor();
		}

		public SimpleKey(bool isRequired, int tokenNumber, Cursor cursor)
		{
			IsPossible = true;
			IsRequired = isRequired;
			TokenNumber = tokenNumber;
			this.cursor = new Cursor(cursor);
		}
	}
}
