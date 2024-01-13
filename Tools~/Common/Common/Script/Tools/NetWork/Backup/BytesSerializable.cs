// /*|✩ - - - - - |||
// |||✩ Author:   ||| -> xi nan
// |||✩ Date:     ||| -> 2023-06-29
//
// |||✩ - - - - - |*/
//
// using System.Collections;
//
// namespace AIO
// {
//     using System;
//     using System.Collections;
//
//     /// <summary>
//     /// 数据序列化
//     /// </summary>
//     [Serializable]
//     public class BytesSerializable
//     {
//         [NonSerialized] private Hashtable data;
//
//         /// <summary>
//         /// 构造函数
//         /// </summary>
//         public BytesSerializable()
//         {
//         }
//
//         /// <summary>
//         /// 设置数据
//         /// </summary>
//         /// <param name="key"></param>
//         /// <param name="value"></param>
//         public void DataValueSet(object key, object value)
//         {
//             if (data == null)
//                 data = new Hashtable();
//             if (data.ContainsKey(key))
//                 data[key] = value;
//             else
//                 data.Add(key, value);
//         }
//
//         private bool DataValueIsExist(object key)
//         {
//             if (data == null || !data.ContainsKey(key))
//                 return false;
//             return true;
//         }
//
//         /// <summary>
//         /// 获取数据
//         /// </summary>
//         /// <param name="key"></param>
//         /// <returns></returns>
//         public object DataValueGet(object key)
//         {
//             if (!DataValueIsExist(key))
//                 return null;
//             return data[key];
//         }
//
//         /// <summary>
//         /// 移除数据
//         /// </summary>
//         /// <param name="key"></param>
//         /// <returns></returns>
//         public bool DataValueRemove(object key)
//         {
//             if (!DataValueIsExist(key)) return false;
//             data.Remove(key);
//             return true;
//         }
//
//         /// <summary>
//         /// 提供数据读取存储方法
//         /// </summary>
//         /// <param name="data"></param>
//         public virtual void BytesRead(BufferByte data)
//         {
//         }
//
//         /// <summary>
//         /// 提供数据读取存储方法
//         /// </summary>
//         /// <param name="data"></param>
//         public virtual void BytesWrite(BufferByte data)
//         {
//         }
//     }
// }
