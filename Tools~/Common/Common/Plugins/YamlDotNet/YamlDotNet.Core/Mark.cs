using System;
using AIO.YamlDotNet.Helpers;

namespace AIO.YamlDotNet.Core
{
	internal readonly struct Mark : IEquatable<Mark>, IComparable<Mark>, IComparable
	{
		public static readonly Mark Empty = new Mark(0, 1, 1);

		public int Index { get; }

		public int Line { get; }

		public int Column { get; }

		public Mark(int index, int line, int column)
		{
			if (index < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException("index", "Index must be greater than or equal to zero.");
			}
			if (line < 1)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException("line", "Line must be greater than or equal to 1.");
			}
			if (column < 1)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException("column", "Column must be greater than or equal to 1.");
			}
			Index = index;
			Line = line;
			Column = column;
		}

		public override string ToString()
		{
			return $"Line: {Line}, Col: {Column}, Idx: {Index}";
		}

		public override bool Equals(object? obj)
		{
			return Equals((Mark)(obj ?? ((object)Empty)));
		}

		public bool Equals(Mark other)
		{
			if (Index == other.Index && Line == other.Line)
			{
				return Column == other.Column;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return HashCode.CombineHashCodes(Index.GetHashCode(), HashCode.CombineHashCodes(Line.GetHashCode(), Column.GetHashCode()));
		}

		public int CompareTo(object? obj)
		{
			return CompareTo((Mark)(obj ?? ((object)Empty)));
		}

		public int CompareTo(Mark other)
		{
			int num = Line.CompareTo(other.Line);
			if (num == 0)
			{
				num = Column.CompareTo(other.Column);
			}
			return num;
		}
	}
}
