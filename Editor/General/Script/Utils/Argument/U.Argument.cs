/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-26
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using System.Reflection;

namespace AIO.UEditor
{
    public static partial class EHelper
    {
        /// <summary>
        /// 参数帮助类
        /// </summary>
        public static class Argument
        {
            /// <summary>
            /// 解析自定义命令
            /// </summary>
            public static T ResolverCustomCur<T>() where T : ArgumentCustom, new()
            {
                var cmd = Activator.CreateInstance<T>();
                cmd.Resolver(string.Join(" ", Environment.GetCommandLineArgs()));
                return cmd;
            }

            /// <summary>
            /// 解析自定义命令
            /// </summary>
            public static T ResolverCustom<T>(in string args) where T : ArgumentCustom, new()
            {
                var cmd = Activator.CreateInstance<T>();
                cmd.Resolver(args);
                return cmd;
            }

            /// <summary>
            /// 解析自定义命令
            /// </summary>
            public static T ResolverCustom<T>(in IEnumerable<string> args) where T : ArgumentCustom, new()
            {
                var cmd = Activator.CreateInstance<T>();
                cmd.Resolver(string.Join(" ", args));
                return cmd;
            }

            /// <summary>
            /// 解析默认命令
            /// </summary>
            public static T ResolverCur<T>() where T : new()
            {
                return Resolver(Activator.CreateInstance<T>(), string.Join(" ", Environment.GetCommandLineArgs()));
            }

            /// <summary>
            /// 解析默认命令
            /// </summary>
            public static T Resolver<T>(in IEnumerable<string> args) where T : new()
            {
                return Resolver(Activator.CreateInstance<T>(), string.Join(" ", args));
            }

            /// <summary>
            /// 解析默认命令
            /// </summary>
            public static T Resolver<T>(in string args) where T : new()
            {
                return Resolver(Activator.CreateInstance<T>(), args);
            }

            /// <summary>
            /// 解析
            /// </summary>
            internal static T Resolver<T>(T value, in string cmd)
            {
                if (value == null) value = Activator.CreateInstance<T>();
                var fields = value.GetType().GetFields();
                foreach (var field in fields)
                {
                    if (!(field.IsPublic && !field.IsStatic)) continue;
                    foreach (var item in field.GetCustomAttributes())
                    {
                        if (!(item is ArgumentAttribute argLabel)) continue;
                        switch (argLabel.Type)
                        {
                            case EArgLabel.Integer:
                                field.SetValue(value, GetInteger(cmd, argLabel.Label));
                                break;
                            case EArgLabel.IntegerArray:
                                field.SetValue(value, GetIntegerArray(cmd, argLabel.Label));
                                break;
                            case EArgLabel.Bool:
                                field.SetValue(value, GetBool(cmd, argLabel.Label));
                                break;
                            case EArgLabel.String:
                                field.SetValue(value, GetString(cmd, argLabel.Label));
                                break;
                            case EArgLabel.StringArray:
                                field.SetValue(value, GetStringArray(cmd, argLabel.Label));
                                break;
                            case EArgLabel.Enum:
                                field.SetValue(value, GetEnum(cmd, field.FieldType, argLabel.Label));
                                break;
                            default:
                                field.SetValue(value, null);
                                break;
                        }

                        break;
                    }
                }

                return value;
            }

            /// <summary>
            /// 获取 Bool
            /// </summary>
            internal static bool GetBool(in string command, in string key)
            {
                return command.Contains(key);
            }

            /// <summary>
            /// 获取 String
            /// </summary>
            internal static string GetString(in string command, in string key)
            {
                if (!command.Contains(key)) return null;
                var start = command.IndexOf(key, StringComparison.CurrentCulture); //获取命令开始下标
                var space = key.Length + 1;
                var temp = command.Substring(start + space, command.Length - start - space);
                var end = temp.IndexOf(' ');
                if (end <= 0) return temp;
                return temp.Substring(0, end);
            }

            /// <summary>
            /// 获取 String Array
            /// </summary>
            internal static string[] GetStringArray(in string command, in string key)
            {
                if (!command.Contains(key)) return Array.Empty<string>();
                var start = command.IndexOf(key, StringComparison.CurrentCulture); //获取命令开始下标
                var space = key.Length + 1;
                var temp = command.Substring(start + space, command.Length - start - space);
                var end = temp.IndexOf('|');
                if (end <= 0) return temp.Split(' ');
                return temp.Substring(0, end).Split(' ');
            }

            /// <summary>
            /// 获取 String Array
            /// </summary>
            internal static object GetEnum(string command, Type type, string key)
            {
                if (!command.Contains(key)) return null;
                var start = command.IndexOf(key, StringComparison.CurrentCulture); //获取命令开始下标
                var space = key.Length + 1;
                var temp = command.Substring(start + space, command.Length - start - space);
                var end = temp.IndexOf(' ');
                var strValue = end <= 0 ? temp : temp.Substring(0, end);
                if (string.IsNullOrEmpty(strValue)) return null;
                if (int.TryParse(strValue, out var intValue)) return Enum.Parse(type, intValue.ToString());
                return Enum.Parse(type, strValue);
            }

            /// <summary>
            /// 获取 String Array
            /// </summary>
            internal static int[] GetIntegerArray(in string command, in string key)
            {
                if (!command.Contains(key)) return Array.Empty<int>();
                var start = command.IndexOf(key, StringComparison.CurrentCulture); //获取命令开始下标
                var space = key.Length + 1;
                var temp = command.Substring(start + space, command.Length - start - space);
                var end = temp.IndexOf('|');
                if (end > 0) _ = temp.Substring(0, end);
                var values = temp.Split(' ');
                var list = new int[values.Length];
                for (var i = 0; i < list.Length; i++)
                {
                    list[i] = int.Parse(values[i]);
                }

                return list;
            }

            /// <summary>
            /// 获取 Int
            /// </summary>
            /// 如果为-1 则说明没有使用
            internal static int GetInteger(in string command, in string key)
            {
                if (!command.Contains(key)) return -1;
                var start = command.IndexOf(key, StringComparison.CurrentCulture); //获取命令开始下标
                var space = key.Length + 1;
                var temp = command.Substring(start + space, command.Length - start - space);
                var end = temp.IndexOf(' ');
                if (end <= 0) return int.Parse(temp);
                return int.Parse(temp.Substring(0, end));
            }
        }
    }
}
