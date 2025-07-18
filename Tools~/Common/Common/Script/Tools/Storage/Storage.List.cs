// #region
//
// using System.Collections.Generic;
//
// #endregion
//
// namespace AIO
// {
//     public partial class Storage
//     {
//         private readonly List<IJsonData> Collection;
//
//         /// <summary>
//         /// 数据有效长度 需要调动序列化 Serialize
//         /// </summary>
//         public int Count => Buffer.Count;
//
//         /// <summary>
//         /// 添加数据bin
//         /// </summary>
//         public void AddBin(IJsonData item)
//         {
//             Collection.Add(item);
//         }
//
//         /// <summary>
//         /// 清空数据Bin
//         /// </summary>
//         public void ClearBin()
//         {
//             Collection.Clear();
//         }
//
//         /// <summary>
//         /// 判断是否存在数据
//         /// </summary>
//         /// <param name="item">数据</param>
//         /// <returns>Ture存在 False不存在</returns>
//         public bool ContainBin(IJsonData item)
//         {
//             return Collection.Contains(item);
//         }
//
//         /// <summary>
//         /// 获取当前数据的下标
//         /// </summary>
//         public int IndexOfBin(IJsonData item)
//         {
//             return Collection.IndexOf(item);
//         }
//
//         /// <summary>
//         /// 插入
//         /// </summary>
//         public void InsertBin(int index, IJsonData item)
//         {
//             Collection.Insert(index, item);
//         }
//
//         /// <summary>
//         /// 移除指定数据
//         /// </summary>
//         public bool RemoveBin(IJsonData item)
//         {
//             return Collection.Remove(item);
//         }
//     }
// }