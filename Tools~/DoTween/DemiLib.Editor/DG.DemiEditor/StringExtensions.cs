using System;
using System.Globalization;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace DG.DemiEditor
{
	/// <summary>
	/// String extensions
	/// </summary>
	public static class StringExtensions
	{
		private static readonly StringBuilder _Strb = new StringBuilder();

		/// <summary>
		/// Returns TRUE if the string is null or empty
		/// </summary>
		/// <param name="trimSpaces">If TRUE (default) and the string contains only spaces, considers it empty</param>
		public static bool IsNullOrEmpty(this string s, bool trimSpaces = true)
		{
			if (s == null)
			{
				return true;
			}
			if (!trimSpaces)
			{
				return s.Length == 0;
			}
			return s.Trim().Length == 0;
		}

		/// <summary>
		/// Compares a version string (in format #.#.###) with another of the same format,
		/// and return TRUE if this one is minor. Boths trings must have the same number of dot separators.
		/// </summary>
		public static bool VersionIsMinorThan(this string s, string version)
		{
			string[] array = s.Split('.');
			string[] array2 = version.Split('.');
			if (array.Length != array2.Length)
			{
				throw new ArgumentException("Invalid");
			}
			for (int i = 0; i < array.Length; i++)
			{
				int num = Convert.ToInt32(array[i]);
				int num2 = Convert.ToInt32(array2[i]);
				if (i == array.Length - 1)
				{
					return num < num2;
				}
				if (num != num2)
				{
					if (num < num2)
					{
						return true;
					}
					if (num > num2)
					{
						return false;
					}
				}
			}
			throw new ArgumentException("Invalid");
		}

		/// <summary>
		/// Converts a HEX color to a Unity Color and returns it
		/// </summary>
		/// <param name="hex">The HEX color, either with or without the initial # (accepts both regular and short format)</param>
		public static Color HexToColor(this string hex)
		{
			if (hex[0] == '#')
			{
				hex = hex.Substring(1);
			}
			int length = hex.Length;
			if (length < 6)
			{
				float r = ((float)HexToInt(hex[0]) + (float)HexToInt(hex[0]) * 16f) / 255f;
				float g = ((float)HexToInt(hex[1]) + (float)HexToInt(hex[1]) * 16f) / 255f;
				float b = ((float)HexToInt(hex[2]) + (float)HexToInt(hex[2]) * 16f) / 255f;
				float a = ((length == 4) ? (((float)HexToInt(hex[3]) + (float)HexToInt(hex[3]) * 16f) / 255f) : 1f);
				return new Color(r, g, b, a);
			}
			float r2 = ((float)HexToInt(hex[1]) + (float)HexToInt(hex[0]) * 16f) / 255f;
			float g2 = ((float)HexToInt(hex[3]) + (float)HexToInt(hex[2]) * 16f) / 255f;
			float b2 = ((float)HexToInt(hex[5]) + (float)HexToInt(hex[4]) * 16f) / 255f;
			float a2 = ((length == 8) ? (((float)HexToInt(hex[7]) + (float)HexToInt(hex[6]) * 16f) / 255f) : 1f);
			return new Color(r2, g2, b2, a2);
		}

		/// <summary>
		/// Nicifies a string, replacing underscores with spaces, and adding a space before Uppercase letters (except the first character)
		/// </summary>
		public static string Nicify(this string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return "";
			}
			_Strb.Length = 0;
			_Strb.Append(s[0].ToString().ToUpper());
			for (int i = 1; i < s.Length; i++)
			{
				char c = s[i];
				char c2 = s[i - 1];
				switch (c)
				{
				case '_':
					_Strb.Append(' ');
					continue;
				default:
					if ((char.IsUpper(c) && c2 != ' ' && c2 != '_') || (char.IsNumber(c) && c2 != ' ' && c2 != '_' && !char.IsNumber(c2)))
					{
						_Strb.Append(' ');
					}
					break;
				case ' ':
					break;
				}
				_Strb.Append(c);
			}
			return _Strb.ToString();
		}

		/// <summary>
		/// If the given string is a directory path, returns its parent
		/// with or without final slash depending on the original directory format
		/// </summary>
		public static string Parent(this string dir)
		{
			if (dir.Length <= 1)
			{
				return dir;
			}
			string value = ((dir.IndexOf("/") == -1) ? "\\" : "/");
			int num = dir.LastIndexOf(value);
			if (num == -1)
			{
				return dir;
			}
			if (num == dir.Length - 1)
			{
				num = dir.LastIndexOf(value, num - 1);
				if (num == -1)
				{
					return dir;
				}
				return dir.Substring(0, num + 1);
			}
			return dir.Substring(0, num);
		}

		/// <summary>
		/// If the string is a directory, returns the directory name,
		/// if instead it's a file returns its name without extension.
		/// Works better than Path.GetDirectoryName, which kind of sucks imho
		/// </summary>
		public static string FileOrDirectoryName(this string path)
		{
			if (path.Length <= 1)
			{
				return path;
			}
			int num = path.LastIndexOfAnySlash();
			int num2 = path.LastIndexOf('.');
			if (num2 != -1 && num2 > num)
			{
				path = path.Substring(0, num2);
			}
			if (num == -1)
			{
				return path;
			}
			if (num == path.Length - 1)
			{
				path = path.Substring(0, num);
				num = path.LastIndexOfAnySlash();
				if (num == -1)
				{
					return path;
				}
			}
			return path.Substring(num + 1);
		}

		/// <summary>
		/// Evaluates the string as a property or field and returns its value.
		/// </summary>
		/// <param name="obj">If NULL considers the string as a static property, otherwise uses obj as the starting instance</param>
		public static T EvalAsProperty<T>(this string s, object obj = null, bool logErrors = false)
		{
			try
			{
				Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
				string[] array = s.Split('.');
				if (obj == null)
				{
					string text = array[0];
					for (int i = 1; i < array.Length - 1; i++)
					{
						text = string.Concat(text, ".", array[i]);
					}
					Type type = null;
					for (int j = 0; j < assemblies.Length; j++)
					{
						type = assemblies[j].GetType(text);
						if (type != null)
						{
							break;
						}
					}
					if (type == null)
					{
						throw new NullReferenceException(string.Concat("Type ", text, " could not be found in any of the domain assemblies"));
					}
					PropertyInfo property = type.GetProperty(array[array.Length - 1]);
					if (property != null)
					{
						return (T)property.GetValue(null, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, null, CultureInfo.InvariantCulture);
					}
					return (T)type.GetField(array[array.Length - 1]).GetValue(null);
				}
				string[] array2 = array;
				foreach (string name in array2)
				{
					Type type2 = obj.GetType();
					PropertyInfo property2 = type2.GetProperty(name);
					obj = ((property2 == null) ? type2.GetField(name).GetValue(obj) : property2.GetValue(obj, null));
				}
				return (T)obj;
			}
			catch (Exception ex)
			{
				if (logErrors)
				{
					Debug.LogError(string.Concat("EvalAsProperty error (", ex.Message, ")\n", ex.StackTrace));
				}
				return default(T);
			}
		}

		private static int HexToInt(char hexVal)
		{
			return int.Parse(hexVal.ToString(), NumberStyles.HexNumber);
		}

		private static int LastIndexOfAnySlash(this string str)
		{
			int num = str.LastIndexOf('/');
			if (num == -1)
			{
				num = str.LastIndexOf('\\');
			}
			return num;
		}
	}
}
