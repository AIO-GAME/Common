#region

using System;

#endregion

namespace AIO
{
    /// <summary>
    /// 游戏数据注册
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class GBeanRegisterAttribute : Attribute
    {
        /// <summary>
        /// 游戏数据注册
        /// </summary>
        /// <param name="id">唯一ID</param>
        public GBeanRegisterAttribute(int id)
        {
            ID = id;
        }

        /// <summary>
        /// 唯一ID
        /// </summary>
        public int ID { get; }
    }
}