using System.Diagnostics;

namespace YamlDotNet.Core
{
	[DebuggerStepThrough]
	internal sealed class Cursor
	{
		public int Index { get; private set; }

		public int Line { get; private set; }

		public int LineOffset { get; private set; }

		public Cursor()
		{
			Line = 1;
		}

		public Cursor(Cursor cursor)
		{
			Index = cursor.Index;
			Line = cursor.Line;
			LineOffset = cursor.LineOffset;
		}

		public Mark Mark()
		{
			return new Mark(Index, Line, LineOffset + 1);
		}

		public void Skip()
		{
			Index++;
			LineOffset++;
		}

		public void SkipLineByOffset(int offset)
		{
			Index += offset;
			Line++;
			LineOffset = 0;
		}

		public void ForceSkipLineAfterNonBreak()
		{
			if (LineOffset != 0)
			{
				Line++;
				LineOffset = 0;
			}
		}
	}
}
