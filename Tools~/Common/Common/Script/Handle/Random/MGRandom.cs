using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AIO
{
    /// <summary>
    /// 随机数单例
    /// </summary>
    public sealed class MGRandom : Singleton<MGRandom>
    {
        private Lazy<Dictionary<long, RandomItem>> lazy;

        /// <summary>
        /// 主随机数对象
        /// </summary>
        public static RandomItem Main => Instance.Get(0);

        /// <inheritdoc />
        protected override Task OnInitializeAsync()
        {
            lazy                = new Lazy<Dictionary<long, RandomItem>>(valueFactory);
            AutoDisposePriority = int.MinValue;
            return Task.CompletedTask;

            Dictionary<long, RandomItem> valueFactory() => new Dictionary<long, RandomItem>()
            {
                {
                    0, new RandomItem(Guid.NewGuid().GetHashCode())
                }
            };
        }

        /// <summary>
        /// 获取随机数对象
        /// </summary>
        public RandomItem Get(long id)
        {
            if (lazy.Value.TryGetValue(id, out var item)) return item;
            lazy.Value.Add(id, item = new RandomItem());
            item.SetSeed();
            return item;
        }

        /// <summary>
        /// 获取随机数对象
        /// </summary>
        public RandomItem Get(long id, int seed)
        {
            if (!lazy.Value.TryGetValue(id, out var item))
                lazy.Value.Add(id, item = new RandomItem());
            item.SetSeed(seed);
            return item;
        }

        /// <inheritdoc />
        protected override void OnDispose() { lazy.Value.Clear(); }
    }
}