﻿using System;

namespace AIO
{
    /*
 * Copyright (c) 2013 Calvin Rien
 *
 * Based on the JSON parser by Patrick van Bergen
 * http://techblog.procurios.nl/k/618/news/view/14605/14863/How-do-I-write-my-own-parser-for-JSON.html
 *
 * Simplified it so that it doesn't throw exceptions
 * and can be used in Unity iPhone with maximum code stripping.
 *
 * Permission is hereby granted, free of charge, to any person obtaining
 * a copy of this software and associated documentation files (the
 * "Software"), to deal in the Software without restriction, including
 * without limitation the rights to use, copy, modify, merge, publish,
 * distribute, sublicense, and/or sell copies of the Software, and to
 * permit persons to whom the Software is furnished to do so, subject to
 * the following conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
 * MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
 * IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
 * CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
 * TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
 * SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using UnityEngine;

    namespace HUMiniJSON
    {
        // Example usage:
        //
        //  using UnityEngine;
        //  using System.Collections;
        //  using System.Collections.Generic;
        //  using MiniJSON;
        //
        //  public class MiniJSONTest : MonoBehaviour {
        //      void Start () {
        //          var jsonString = "{ \"array\": [1.44,2,3], " +
        //                          "\"object\": {\"key1\":\"value1\", \"key2\":256}, " +
        //                          "\"string\": \"The quick brown fox \\\"jumps\\\" over the lazy dog \", " +
        //                          "\"unicode\": \"\\u3041 Men\u00fa sesi\u00f3n\", " +
        //                          "\"int\": 65536, " +
        //                          "\"float\": 3.1415926, " +
        //                          "\"bool\": true, " +
        //                          "\"null\": null }";
        //
        //          var dict = Json.Deserialize(jsonString) as Dictionary<string,object>;
        //
        //          Debug.Log("deserialized: " + dict.GetType());
        //          Debug.Log("dict['array'][0]: " + ((List<object>) dict["array"])[0]);
        //          Debug.Log("dict['string']: " + (string) dict["string"]);
        //          Debug.Log("dict['float']: " + (double) dict["float"]); // floats come out as doubles
        //          Debug.Log("dict['int']: " + (long) dict["int"]); // ints come out as longs
        //          Debug.Log("dict['unicode']: " + (string) dict["unicode"]);
        //
        //          var str = Json.Serialize(dict);
        //
        //          Debug.Log("serialized: " + str);
        //      }
        //  }

        /// <summary>
        /// This class encodes and decodes JSON strings.
        /// Spec. details, see http://www.json.org/
        ///
        /// JSON uses Arrays and Objects. These correspond here to the datatypes IList and IDictionary.
        /// All numbers are parsed to doubles.
        /// </summary>
        public static class Json
        {
            /// <summary>
            /// Parses the string json into a value
            /// </summary>
            /// <param name="json">A JSON string.</param>
            /// <returns>An List&lt;object&gt;, a Dictionary&lt;string, object&gt;, a double, an integer,a string, null, true, or false</returns>
            public static object Deserialize(string json)
            {
                // save the string for debug information
                if (string.IsNullOrEmpty(json))
                {
                    return null;
                }

                return Parser.Parse(json);
            }

            sealed class Parser : IDisposable
            {
                const string WORD_BREAK = "{}[],:\"";

                public static bool IsWordBreak(char c)
                {
                    return Char.IsWhiteSpace(c) || WORD_BREAK.IndexOf(c) != -1;
                }

                enum TOKEN
                {
                    NONE,
                    CURLY_OPEN,
                    CURLY_CLOSE,
                    SQUARED_OPEN,
                    SQUARED_CLOSE,
                    COLON,
                    COMMA,
                    STRING,
                    NUMBER,
                    TRUE,
                    FALSE,
                    NULL,
                };

                StringReader json;

                Parser(string jsonString)
                {
                    json = new StringReader(jsonString);
                }

                public static object Parse(string jsonString)
                {
                    using (var instance = new Parser(jsonString))
                    {
                        return instance.ParseValue();
                    }
                }

                public void Dispose()
                {
                    json.Dispose();
                    json = null;
                }

                Dictionary<string, object> ParseObject()
                {
                    Dictionary<string, object> table = new Dictionary<string, object>();

                    // ditch opening brace
                    json.Read();

                    // {
                    while (true)
                    {
                        switch (NextToken)
                        {
                            case TOKEN.NONE:
                                return null;
                            case TOKEN.COMMA:
                                continue;
                            case TOKEN.CURLY_CLOSE:
                                return table;
                            default:
                                // name
                                string name = ParseString();
                                if (name == null)
                                {
                                    return null;
                                }

                                // :
                                if (NextToken != TOKEN.COLON)
                                {
                                    return null;
                                }

                                // ditch the colon
                                json.Read();

                                // value
                                table[name] = ParseValue();
                                break;
                        }
                    }
                }

                List<object> ParseArray()
                {
                    List<object> array = new List<object>();

                    // ditch opening bracket
                    json.Read();

                    // [
                    var parsing = true;
                    while (parsing)
                    {
                        TOKEN nextToken = NextToken;

                        switch (nextToken)
                        {
                            case TOKEN.NONE:
                                return null;
                            case TOKEN.COMMA:
                                continue;
                            case TOKEN.SQUARED_CLOSE:
                                parsing = false;
                                break;
                            default:
                                object value = ParseByToken(nextToken);

                                array.Add(value);
                                break;
                        }
                    }

                    return array;
                }

                object ParseValue()
                {
                    TOKEN nextToken = NextToken;
                    return ParseByToken(nextToken);
                }

                object ParseByToken(TOKEN token)
                {
                    switch (token)
                    {
                        case TOKEN.STRING:
                            return ParseString();
                        case TOKEN.NUMBER:
                            return ParseNumber();
                        case TOKEN.CURLY_OPEN:
                            return ParseObject();
                        case TOKEN.SQUARED_OPEN:
                            return ParseArray();
                        case TOKEN.TRUE:
                            return true;
                        case TOKEN.FALSE:
                            return false;
                        case TOKEN.NULL:
                            return null;
                        default:
                            return null;
                    }
                }

                string ParseString()
                {
                    StringBuilder s = new StringBuilder();
                    char c;

                    // ditch opening quote
                    json.Read();

                    bool parsing = true;
                    while (parsing)
                    {
                        if (json.Peek() == -1)
                        {
                            parsing = false;
                            break;
                        }

                        c = NextChar;
                        switch (c)
                        {
                            case '"':
                                parsing = false;
                                break;
                            case '\\':
                                if (json.Peek() == -1)
                                {
                                    parsing = false;
                                    break;
                                }

                                c = NextChar;
                                switch (c)
                                {
                                    case '"':
                                    case '\\':
                                    case '/':
                                        s.Append(c);
                                        break;
                                    case 'b':
                                        s.Append('\b');
                                        break;
                                    case 'f':
                                        s.Append('\f');
                                        break;
                                    case 'n':
                                        s.Append('\n');
                                        break;
                                    case 'r':
                                        s.Append('\r');
                                        break;
                                    case 't':
                                        s.Append('\t');
                                        break;
                                    case 'u':
                                        var hex = new char[4];

                                        for (int i = 0; i < 4; i++)
                                        {
                                            hex[i] = NextChar;
                                        }

                                        s.Append((char)Convert.ToInt32(new string(hex), 16));
                                        break;
                                }

                                break;
                            default:
                                s.Append(c);
                                break;
                        }
                    }

                    return s.ToString();
                }

                object ParseNumber()
                {
                    string number = NextWord;

                    // llm 修改，简化转换，并判断是否正常转换，不正常返回原始值
                    long l;
                    double d;
                    if (Int64.TryParse(number, out l))
                    {
                        return l;
                    }
                    else if (Double.TryParse(number, out d))
                    {
                        return d;
                    }

                    return number;

                    //if (number.IndexOf('.') == -1) {
                    //	long parsedInt;
                    //	Int64.TryParse(number, out parsedInt);
                    //	return parsedInt;
                    //}

                    //double parsedDouble;
                    //Double.TryParse(number, out parsedDouble);
                    //return parsedDouble;
                }

                void EatWhitespace()
                {
                    while (Char.IsWhiteSpace(PeekChar))
                    {
                        json.Read();

                        if (json.Peek() == -1)
                        {
                            break;
                        }
                    }
                }

                char PeekChar
                {
                    get { return Convert.ToChar(json.Peek()); }
                }

                char NextChar
                {
                    get { return Convert.ToChar(json.Read()); }
                }

                string NextWord
                {
                    get
                    {
                        StringBuilder word = new StringBuilder();

                        while (!IsWordBreak(PeekChar))
                        {
                            word.Append(NextChar);

                            if (json.Peek() == -1)
                            {
                                break;
                            }
                        }

                        return word.ToString();
                    }
                }

                TOKEN NextToken
                {
                    get
                    {
                        EatWhitespace();

                        if (json.Peek() == -1)
                        {
                            return TOKEN.NONE;
                        }

                        switch (PeekChar)
                        {
                            case '{':
                                return TOKEN.CURLY_OPEN;
                            case '}':
                                json.Read();
                                return TOKEN.CURLY_CLOSE;
                            case '[':
                                return TOKEN.SQUARED_OPEN;
                            case ']':
                                json.Read();
                                return TOKEN.SQUARED_CLOSE;
                            case ',':
                                json.Read();
                                return TOKEN.COMMA;
                            case '"':
                                return TOKEN.STRING;
                            case ':':
                                return TOKEN.COLON;
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
                                return TOKEN.NUMBER;
                        }

                        string nextWord = NextWord;
                        switch (nextWord)
                        {
                            case "false":
                                return TOKEN.FALSE;
                            case "true":
                                return TOKEN.TRUE;
                            case "null":
                                return TOKEN.NULL;
                            // llm add，解析到注释，要去掉这一行
                            default:
                                if (nextWord.Substring(0, 2) == "//")
                                {
                                    // 删除这一行
                                    json.ReadLine();

                                    // 获取下一个token
                                    return NextToken;
                                }

                                break;
                            // llm add
                        }

                        return TOKEN.NONE;
                    }
                }
            }

            /// <summary>
            /// Converts a IDictionary / IList object or a simple type (string, int, etc.) into a JSON string
            /// </summary>
            /// <param name="json">A Dictionary&lt;string, object&gt; / List&lt;object&gt;</param>
            /// <returns>A JSON encoded string, or null if object 'json' is not serializable</returns>
            public static string Serialize(object obj)
            {
                return Serializer.Serialize(obj);
            }

            sealed class Serializer
            {
                StringBuilder builder;

                Serializer()
                {
                    builder = new StringBuilder();
                }

                public static string Serialize(object obj)
                {
                    var instance = new Serializer();

                    instance.SerializeValue(obj);

                    return instance.builder.ToString();
                }

                void SerializeValue(object value)
                {
                    IList asList;
                    IDictionary asDict;
                    string asStr;

                    if (value == null)
                    {
                        builder.Append("null");
                    }
                    else if ((asStr = value as string) != null)
                    {
                        SerializeString(asStr);
                    }
                    else if (value is bool)
                    {
                        builder.Append((bool)value ? "true" : "false");
                    }
                    else if ((asList = value as IList) != null)
                    {
                        SerializeArray(asList);
                    }
                    else if ((asDict = value as IDictionary) != null)
                    {
                        SerializeObject(asDict);
                    }
                    else if (value is char)
                    {
                        SerializeString(new string((char)value, 1));
                    }
                    else
                    {
                        SerializeOther(value);
                    }
                }

                void SerializeObject(IDictionary obj)
                {
                    bool first = true;

                    builder.Append('{');

                    foreach (object e in obj.Keys)
                    {
                        if (!first)
                        {
                            builder.Append(',');
                        }

                        SerializeString(e.ToString());
                        builder.Append(':');

                        SerializeValue(obj[e]);

                        first = false;
                    }

                    builder.Append('}');
                }

                void SerializeArray(IList anArray)
                {
                    builder.Append('[');

                    bool first = true;

                    foreach (object obj in anArray)
                    {
                        if (!first)
                        {
                            builder.Append(',');
                        }

                        SerializeValue(obj);

                        first = false;
                    }

                    builder.Append(']');
                }

                void SerializeString(string str)
                {
                    builder.Append('\"');

                    char[] charArray = str.ToCharArray();
                    foreach (var c in charArray)
                    {
                        switch (c)
                        {
                            case '"':
                                builder.Append("\\\"");
                                break;
                            case '\\':
                                builder.Append("\\\\");
                                break;
                            case '\b':
                                builder.Append("\\b");
                                break;
                            case '\f':
                                builder.Append("\\f");
                                break;
                            case '\n':
                                builder.Append("\\n");
                                break;
                            case '\r':
                                builder.Append("\\r");
                                break;
                            case '\t':
                                builder.Append("\\t");
                                break;
                            default:
                                int codepoint = Convert.ToInt32(c);
                                if ((codepoint >= 32) && (codepoint <= 126))
                                {
                                    builder.Append(c);
                                }
                                else
                                {
                                    builder.Append("\\u");
                                    builder.Append(codepoint.ToString("x4"));
                                }

                                break;
                        }
                    }

                    builder.Append('\"');
                }

                void SerializeOther(object value)
                {
                    // NOTE: decimals lose precision during serialization.
                    // They always have, I'm just letting you know.
                    // Previously floats and doubles lost precision too.
                    if (value is float)
                    {
                        builder.Append(((float)value).ToString("R"));
                    }
                    else if (value is int
                             || value is uint
                             || value is long
                             || value is sbyte
                             || value is byte
                             || value is short
                             || value is ushort
                             || value is ulong)
                    {
                        builder.Append(value);
                    }
                    else if (value is double
                             || value is decimal)
                    {
                        builder.Append(Convert.ToDouble(value).ToString("R"));
                    }
                    else
                    {
                        SerializeString(value.ToString());
                    }
                }
            }

            ////////////////////////////////////////////////////////////////////
            //
            //llm add
            //

            /// <summary>
            ///    把对象类型转化为指定类型，转化失败时返回指定的默认值
            /// </summary>
            /// <typeparam name="T"> 动态类型 </typeparam>
            /// <param name="value"> 要转化的源对象 </param>
            /// <param name="defaultValue"> 转化失败返回的指定默认值 </param>
            /// <returns> 转化后的指定类型对象，转化失败时返回指定的默认值 </returns>
            public static T CastTo<T>(this object value, T defaultValue)
            {
                if (value == null)
                {
                    return defaultValue;
                }

                object result;
                Type type = typeof(T);
                try
                {
                    result = type.IsEnum ? Enum.Parse(type, value.ToString()) : Convert.ChangeType(value, type);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);

                    result = defaultValue;
                }

                return (T)result;
            }

            public static T ConvertTo<T>(Dictionary<string, object> dict, string key, T def)
            {
                if (dict.ContainsKey(key))
                {
                    try
                    {
                        return CastTo<T>(dict[key], def);
                    }
                    catch
                    {
                        Console.WriteLine("Convert boolean error! key：" + key);
                        return def;
                    }
                }

                return def;
            }

            public static T To<T>(Dictionary<string, object> dict, string key, T def = default(T))
            {
                return ConvertTo<T>(dict, key, def);
            }

            public static bool ToBool(Dictionary<string, object> dict, string key, bool def = false)
            {
                return ConvertTo<bool>(dict, key, def);
            }

            public static int ToInt(Dictionary<string, object> dict, string key, int def = 0)
            {
                return ConvertTo<int>(dict, key, def);
            }

            public static long ToLong(Dictionary<string, object> dict, string key, long def = 0)
            {
                return ConvertTo<long>(dict, key, def);
            }

            public static float ToFloat(Dictionary<string, object> dict, string key, float def = 0.0f)
            {
                return ConvertTo<float>(dict, key, def);
            }

            public static double ToDouble(Dictionary<string, object> dict, string key, double def = 0.0f)
            {
                return ConvertTo<Double>(dict, key, def);
            }

            public static string ToString(Dictionary<string, object> dict, string key, string def = null)
            {
                return ConvertTo<string>(dict, key, def);
            }

            //
            //llm add
            //
            ////////////////////////////////////////////////////////////////////
        }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            Test1();
        }

        public static void Test1()
        {
            Internal.Print.Show(EPrint.Log);
            try
            {
                Console.WriteLine("--------------------------------- server");
                var context = File.ReadAllText(@"G:\checksum.server.json");
                Dictionary<string, object> json = HUMiniJSON.Json.Deserialize(context) as Dictionary<string, object>;
                CPrint.Log(json);

                Console.WriteLine("--------------------------------- client");
                context = File.ReadAllText(@"G:\checksum.client.json");
                json = HUMiniJSON.Json.Deserialize(context) as Dictionary<string, object>;
                CPrint.Log(json);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}