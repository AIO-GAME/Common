﻿namespace AIO
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;

    /// <summary>
    /// 自定义参数解析
    /// </summary>
    public abstract class ArgumentCustom
    {
        /// <summary>
        /// 命令
        /// </summary>
        public string Command { get; private set; }

        /// <summary>
        /// 字段解析
        /// </summary>
        /// <param name="field">字段</param>
        /// <param name="attribute">属性</param>
        /// <param name="command">命令</param>
        /// <returns>Ture:解析成功 False:解析失败</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual bool SubResolver(ref FieldInfo field, in Attribute attribute, in string command)
        {
            if (!(attribute is ArgumentAttribute attrib)) return false;
            switch (attrib.Type)
            {
                case EArgLabel.Integer:
                    field.SetValue(this, GetInteger(command, attrib.Label));
                    break;
                case EArgLabel.IntegerArray:
                    field.SetValue(this, GetIntegerArray(command, attrib.Label));
                    break;
                case EArgLabel.Bool:
                    field.SetValue(this, GetBool(command, attrib.Label));
                    break;
                case EArgLabel.String:
                    field.SetValue(this, GetString(command, attrib.Label));
                    break;
                case EArgLabel.StringArray:
                    field.SetValue(this, GetStringArray(command, attrib.Label));
                    break;
                case EArgLabel.Enum:
                    field.SetValue(this, GetEnum(command, field.FieldType, attrib.Label));
                    break;
                default:
                    field.SetValue(this, null);
                    break;
            }
            return true;
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="cmd"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Resolver(in string cmd)
        {
            Command = cmd;
            var fields = GetType().GetFields();
            for (int i = 0; i < fields.Length; i++)
            {
                var index = i;
                if (!(fields[i].IsPublic && !fields[i].IsStatic)) continue;
                foreach (var item in fields[index].GetCustomAttributes())
                {
                    if (SubResolver(ref fields[index], item, cmd)) break;
                }
            }
        }

        /// <summary>
        /// 重写转化 String
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string ToString()
        {
            var str = new StringBuilder().Append("Command:").AppendLine();
            var fileds = GetType().GetFields();
            foreach (var field in fileds)
            {
                if (field.IsPublic && !field.IsStatic)
                    str.Append(field.Name).Append('=').Append(field.GetValue(this)).AppendLine();
            }

            return str.ToString();
        }

        /// <summary>
        /// 获取 Bool
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual bool GetBool(in string command, in string key)
        {
            return ArgumentUtils.GetBool(command, key);
        }

        /// <summary>
        /// 获取 String
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual string GetString(in string command, in string key)
        {
            return ArgumentUtils.GetString(command, key);
        }

        /// <summary>
        /// 获取 String Array
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual string[] GetStringArray(in string command, in string key)
        {
            return ArgumentUtils.GetStringArray(command, key);
        }

        /// <summary>
        /// 获取 String Array
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual object GetEnum(in string command, in Type type, in string key)
        {
            return ArgumentUtils.GetEnum(command, type, key);
        }

        /// <summary>
        /// 获取 String Array
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual int[] GetIntegerArray(in string command, in string key)
        {
            return ArgumentUtils.GetIntegerArray(command, key);
        }

        /// <summary>
        /// 获取 Int
        /// </summary>
        /// 如果为-1 则说明没有使用
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected virtual int GetInteger(in string command, in string key)
        {
            return ArgumentUtils.GetInteger(command, key);
        }
    }
}
