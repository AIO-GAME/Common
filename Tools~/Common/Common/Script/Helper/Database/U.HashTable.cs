using System.Collections;
using System.Collections.Generic;

namespace AIO
{
    public partial class AHelper
    {
        /// <summary>
        /// HashTabel 工具类
        /// </summary>
        public class HashTable
        {
            /// <summary>
            /// 创建
            /// </summary>
            public static Hashtable Create(params object[] p)
            {
                var inst = new Hashtable();
                if (p != null)
                {
                    for (int i = 0; i < p.Length;)
                    {
                        inst.Add(p[i], p[i + 1]);
                        i += 2;
                    }
                }

                return inst;
            }

            /// <summary>
            /// 创建
            /// </summary>
            public static Hashtable Create(IDictionary<object, object> dic)
            {
                var inst = new Hashtable();
                if (dic != null)
                {
                    foreach (var item in dic)
                    {
                        inst.Add(item.Key, inst.Values);
                    }
                }

                return inst;
            }
        }
    }
}