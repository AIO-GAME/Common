/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-08-03
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

public partial class UtilsGen
{
    /// <summary>
    /// Unity File ID MD4 算法
    /// </summary>
    public static partial class FileID
    {
        /// <summary>
        /// 计算类型的 FileID
        /// </summary>
        /// <param name="v">类型</param>
        /// <returns>fileid</returns>
        public static int Compute<T>(T v) 
        {
            var type = v.GetType();
            var toBeHashed = string.Concat("s\0\0\0", type.Namespace, type.Name);
            using (var hash = new MD4())
            {
                var hashed = hash.ComputeHash(Encoding.UTF8.GetBytes(toBeHashed));
                var result = 0;
                for (var i = 3; i >= 0; --i)
                {
                    result <<= 8;
                    result |= hashed[i];
                }

                return result;
            }
        }

        /// <summary>
        /// 计算类型的 FileID
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>fileid</returns>
        public static int Compute(Type type)
        {
            var toBeHashed = string.Concat("s\0\0\0", type.Namespace, type.Name);
            using (var hash = new MD4())
            {
                var hashed = hash.ComputeHash(Encoding.UTF8.GetBytes(toBeHashed));
                var result = 0;
                for (var i = 3; i >= 0; --i)
                {
                    result <<= 8;
                    result |= hashed[i];
                }

                return result;
            }
        }


        /// <summary>
        /// 计算类型的 FileID
        /// </summary>
        /// <returns>fileid</returns>
        public static int Compute<T>()
        {
            var type = typeof(T);
            var toBeHashed = string.Concat("s\0\0\0", type.Namespace, type.Name);
            using (HashAlgorithm hash = new MD4())
            {
                hash.Initialize();
                var hashed = hash.ComputeHash(Encoding.UTF8.GetBytes(toBeHashed));
                var result = 0;
                for (var i = 3; i >= 0; --i)
                {
                    result <<= 8;
                    result |= hashed[i];
                }

                return result;
            }
        }
    }
}