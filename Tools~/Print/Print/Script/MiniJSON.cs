﻿#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

#endregion

namespace AIO.Internal
{
    /// <summary>
    /// Mini Json
    /// </summary>
    internal partial class MiniJSON
    {
        private const int TOKEN_NONE          = 0;
        private const int TOKEN_CURLY_OPEN    = 1;
        private const int TOKEN_CURLY_CLOSE   = 2;
        private const int TOKEN_SQUARED_OPEN  = 3;
        private const int TOKEN_SQUARED_CLOSE = 4;
        private const int TOKEN_COLON         = 5;
        private const int TOKEN_COMMA         = 6;
        private const int TOKEN_STRING        = 7;
        private const int TOKEN_NUMBER        = 8;
        private const int TOKEN_TRUE          = 9;
        private const int TOKEN_FALSE         = 10;
        private const int TOKEN_NULL          = 11;
        private const int BUILDER_CAPACITY    = 2000;

        /// <summary>
        /// On decoding, this value holds the position at which the parse failed (-1 = no error).
        /// </summary>
        protected static int lastErrorIndex = -1;

        protected static string lastDecode = "";


        /// <summary>
        /// Parses the string json into a value
        /// </summary>
        /// <param name="json">A JSON string.</param>
        /// <returns>An ArrayList, a Hashtable, a double, a string, null, true, or false</returns>
        public static object Decode(string json)
        {
            // save the string for debug information
            lastDecode = json;

            if (json != null)
            {
                var charArray = json.ToCharArray();
                var index = 0;
                var success = true;
                var value = parseValue(charArray, ref index, ref success);

                if (success)
                    lastErrorIndex = -1;
                else
                    lastErrorIndex = index;

                return value;
            }

            return null;
        }


        /// <summary>
        /// Converts a Hashtable / ArrayList / Dictionary(string,string) object into a JSON string
        /// </summary>
        /// <param name="json">A Hashtable / ArrayList</param>
        /// <returns>A JSON encoded string, or null if object 'json' is not serializable</returns>
        public static string Encode(object json)
        {
            var builder = new StringBuilder(BUILDER_CAPACITY);
            var success = serializeValue(json, builder);

            return success ? builder.ToString() : null;
        }


        /// <summary>
        /// On decoding, this function returns the position at which the parse failed (-1 = no error).
        /// </summary>
        /// <returns></returns>
        public static bool LastDecodeSuccessful()
        {
            return lastErrorIndex == -1;
        }


        /// <summary>
        /// On decoding, this function returns the position at which the parse failed (-1 = no error).
        /// </summary>
        /// <returns></returns>
        public static int GetLastErrorIndex()
        {
            return lastErrorIndex;
        }


        /// <summary>
        /// If a decoding error occurred, this function returns a piece of the JSON string
        /// at which the error took place. To ease debugging.
        /// </summary>
        /// <returns></returns>
        public static string GetLastErrorSnippet()
        {
            if (lastErrorIndex == -1) return "";

            var startIndex = lastErrorIndex - 5;
            var endIndex = lastErrorIndex + 15;
            if (startIndex < 0)
                startIndex = 0;

            if (endIndex >= lastDecode.Length)
                endIndex = lastDecode.Length - 1;

            return lastDecode.Substring(startIndex, endIndex - startIndex + 1);
        }


        #region Parsing

        protected static Hashtable parseObject(char[] json, ref int index)
        {
            var table = new Hashtable();
            int token;

            // {
            nextToken(json, ref index);

            var done = false;
            while (!done)
            {
                token = lookAhead(json, index);
                if (token == TOKEN_NONE) return null;

                if (token == TOKEN_COMMA)
                {
                    nextToken(json, ref index);
                }
                else if (token == TOKEN_CURLY_CLOSE)
                {
                    nextToken(json, ref index);
                    return table;
                }
                else
                {
                    // name
                    var name = parseString(json, ref index);
                    if (name == null) return null;

                    // :
                    token = nextToken(json, ref index);
                    if (token != TOKEN_COLON)
                        return null;

                    // value
                    var success = true;
                    var value = parseValue(json, ref index, ref success);
                    if (!success)
                        return null;

                    table[name] = value;
                }
            }

            return table;
        }


        protected static ArrayList parseArray(char[] json, ref int index)
        {
            var array = new ArrayList();

            // [
            nextToken(json, ref index);

            var done = false;
            while (!done)
            {
                var token = lookAhead(json, index);
                if (token == TOKEN_NONE) return null;

                if (token == TOKEN_COMMA)
                {
                    nextToken(json, ref index);
                }
                else if (token == TOKEN_SQUARED_CLOSE)
                {
                    nextToken(json, ref index);
                    break;
                }
                else
                {
                    var success = true;
                    var value = parseValue(json, ref index, ref success);
                    if (!success)
                        return null;

                    array.Add(value);
                }
            }

            return array;
        }


        protected static object parseValue(char[] json, ref int index, ref bool success)
        {
            switch (lookAhead(json, index))
            {
                case TOKEN_STRING:
                    return parseString(json, ref index);
                case TOKEN_NUMBER:
                    return parseNumber(json, ref index);
                case TOKEN_CURLY_OPEN:
                    return parseObject(json, ref index);
                case TOKEN_SQUARED_OPEN:
                    return parseArray(json, ref index);
                case TOKEN_TRUE:
                    nextToken(json, ref index);
                    return bool.Parse("TRUE");
                case TOKEN_FALSE:
                    nextToken(json, ref index);
                    return bool.Parse("FALSE");
                case TOKEN_NULL:
                    nextToken(json, ref index);
                    return null;
                case TOKEN_NONE:
                    break;
            }

            success = false;
            return null;
        }


        protected static string parseString(char[] json, ref int index)
        {
            var s = "";
            char c;

            eatWhitespace(json, ref index);

            // "
            c = json[index++];

            var complete = false;
            while (!complete)
            {
                if (index == json.Length)
                    break;

                c = json[index++];
                if (c == '"')
                {
                    complete = true;
                    break;
                }

                if (c == '\\')
                {
                    if (index == json.Length)
                        break;

                    c = json[index++];
                    if (c == '"')
                    {
                        s += '"';
                    }
                    else if (c == '\\')
                    {
                        s += '\\';
                    }
                    else if (c == '/')
                    {
                        s += '/';
                    }
                    else if (c == 'b')
                    {
                        s += '\b';
                    }
                    else if (c == 'f')
                    {
                        s += '\f';
                    }
                    else if (c == 'n')
                    {
                        s += '\n';
                    }
                    else if (c == 'r')
                    {
                        s += '\r';
                    }
                    else if (c == 't')
                    {
                        s += '\t';
                    }
                    else if (c == 'u')
                    {
                        var remainingLength = json.Length - index;
                        if (remainingLength >= 4)
                        {
                            var unicodeCharArray = new char[4];
                            Array.Copy(json, index, unicodeCharArray, 0, 4);

                            var codePoint = uint.Parse(new string(unicodeCharArray),
                                                       NumberStyles.HexNumber);

                            // convert the integer codepoint to a unicode char and add to string
                            s += char.ConvertFromUtf32((int)codePoint);

                            // skip 4 chars
                            index += 4;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    s += c;
                }
            }

            if (!complete)
                return null;

            return s;
        }


        protected static double parseNumber(char[] json, ref int index)
        {
            eatWhitespace(json, ref index);

            var lastIndex = getLastIndexOfNumber(json, index);
            var charLength = lastIndex - index + 1;
            var numberCharArray = new char[charLength];

            Array.Copy(json, index, numberCharArray, 0, charLength);
            index = lastIndex + 1;
            return double.Parse(new string(numberCharArray)); // , CultureInfo.InvariantCulture);
        }


        protected static int getLastIndexOfNumber(char[] json, int index)
        {
            int lastIndex;
            for (lastIndex = index; lastIndex < json.Length; lastIndex++)
                if ("0123456789+-.eE".IndexOf(json[lastIndex]) == -1)
                    break;

            return lastIndex - 1;
        }


        protected static void eatWhitespace(char[] json, ref int index)
        {
            for (; index < json.Length; index++)
                if (" \t\n\r".IndexOf(json[index]) == -1)
                    break;
        }


        protected static int lookAhead(char[] json, int index)
        {
            var saveIndex = index;
            return nextToken(json, ref saveIndex);
        }


        protected static int nextToken(char[] json, ref int index)
        {
            eatWhitespace(json, ref index);

            if (index == json.Length) return TOKEN_NONE;

            var c = json[index];
            index++;
            switch (c)
            {
                case '{':
                    return TOKEN_CURLY_OPEN;
                case '}':
                    return TOKEN_CURLY_CLOSE;
                case '[':
                    return TOKEN_SQUARED_OPEN;
                case ']':
                    return TOKEN_SQUARED_CLOSE;
                case ',':
                    return TOKEN_COMMA;
                case '"':
                    return TOKEN_STRING;
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case '-':
                    return TOKEN_NUMBER;
                case ':':
                    return TOKEN_COLON;
            }

            index--;

            var remainingLength = json.Length - index;

            // false
            if (remainingLength >= 5)
                if (json[index] == 'f' &&
                    json[index + 1] == 'a' &&
                    json[index + 2] == 'l' &&
                    json[index + 3] == 's' &&
                    json[index + 4] == 'e')
                {
                    index += 5;
                    return TOKEN_FALSE;
                }

            // true
            if (remainingLength >= 4)
                if (json[index] == 't' &&
                    json[index + 1] == 'r' &&
                    json[index + 2] == 'u' &&
                    json[index + 3] == 'e')
                {
                    index += 4;
                    return TOKEN_TRUE;
                }

            // null
            if (remainingLength >= 4)
                if (json[index] == 'n' &&
                    json[index + 1] == 'u' &&
                    json[index + 2] == 'l' &&
                    json[index + 3] == 'l')
                {
                    index += 4;
                    return TOKEN_NULL;
                }

            return TOKEN_NONE;
        }

        #endregion


        #region Serialization

        protected static bool serializeObjectOrArray(object objectOrArray, StringBuilder builder)
        {
            if (objectOrArray is Hashtable)
                return serializeObject((Hashtable)objectOrArray, builder);
            if (objectOrArray is ArrayList)
                return serializeArray((ArrayList)objectOrArray, builder);
            return false;
        }


        protected static bool serializeObject(Hashtable anObject, StringBuilder builder)
        {
            builder.Append("{");

            var e = anObject.GetEnumerator();
            var first = true;
            while (e.MoveNext())
            {
                var key = e.Key.ToString();
                var value = e.Value;

                if (!first) builder.Append(", ");

                serializeString(key, builder);
                builder.Append(":");
                if (!serializeValue(value, builder)) return false;

                first = false;
            }

            builder.Append("}");
            return true;
        }


        protected static bool serializeDictionary(Dictionary<string, string> dict, StringBuilder builder)
        {
            builder.Append("{");

            var first = true;
            foreach (var kv in dict)
            {
                if (!first)
                    builder.Append(", ");

                serializeString(kv.Key, builder);
                builder.Append(":");
                serializeString(kv.Value, builder);

                first = false;
            }

            builder.Append("}");
            return true;
        }


        protected static bool serializeArray(ArrayList anArray, StringBuilder builder)
        {
            builder.Append("[");

            var first = true;
            for (var i = 0; i < anArray.Count; i++)
            {
                var value = anArray[i];

                if (!first) builder.Append(", ");

                if (!serializeValue(value, builder)) return false;

                first = false;
            }

            builder.Append("]");
            return true;
        }


        protected static bool serializeValue(object value, StringBuilder builder)
        {
            //Type t = value.GetType();
            //UnityEngine.Debug.Log("type: " + t.ToString() + " isArray: " + t.IsArray);

            if (value == null)
                builder.Append("null");
            else if (value.GetType().IsArray)
                serializeArray(new ArrayList((ICollection)value), builder);
            else if (value is string)
                serializeString((string)value, builder);
            else if (value is char)
                serializeString(Convert.ToString((char)value), builder);
            else if (value is decimal)
                serializeString(Convert.ToString((decimal)value), builder);
            else if (value is Hashtable)
                serializeObject((Hashtable)value, builder);
            else if (value is Dictionary<string, string>)
                serializeDictionary((Dictionary<string, string>)value, builder);
            else if (value is ArrayList)
                serializeArray((ArrayList)value, builder);
            else if (value is bool && (bool)value)
                builder.Append("true");
            else if (value is bool && (bool)value == false)
                builder.Append("false");
            else if (value.GetType().IsPrimitive)
                serializeNumber(Convert.ToDouble(value), builder);
            else
                return false;

            return true;
        }


        protected static void serializeString(string aString, StringBuilder builder)
        {
            builder.Append("\"");

            var charArray = aString.ToCharArray();
            for (var i = 0; i < charArray.Length; i++)
            {
                var c = charArray[i];
                if (c == '"')
                {
                    builder.Append("\\\"");
                }
                else if (c == '\\')
                {
                    builder.Append("\\\\");
                }
                else if (c == '\b')
                {
                    builder.Append("\\b");
                }
                else if (c == '\f')
                {
                    builder.Append("\\f");
                }
                else if (c == '\n')
                {
                    builder.Append("\\n");
                }
                else if (c == '\r')
                {
                    builder.Append("\\r");
                }
                else if (c == '\t')
                {
                    builder.Append("\\t");
                }
                else
                {
                    var codepoint = Convert.ToInt32(c);
                    if (codepoint >= 32 && codepoint <= 126)
                        builder.Append(c);
                    else
                        builder.Append("\\u" + Convert.ToString(codepoint, 16).PadLeft(4, '0'));
                }
            }

            builder.Append("\"");
        }


        protected static void serializeNumber(double number, StringBuilder builder)
        {
            builder.Append(Convert.ToString(number)); // , CultureInfo.InvariantCulture));
        }

        #endregion
    }

    #region Extension methods

    internal partial class MiniJSON
    {
        public static T Decode<T>(string json)
        where T : class
        {
            return Decode(json) as T;
        }

        public static T Decode<T>(StringBuilder json)
        where T : class
        {
            return Decode(json.ToString()) as T;
        }
    }

    internal static class MiniJsonExtensions
    {
        public static string ToJson(this Hashtable obj)
        {
            return MiniJSON.Encode(obj);
        }


        public static string ToJson(this Dictionary<string, string> obj)
        {
            return MiniJSON.Encode(obj);
        }
    }

    #endregion
}