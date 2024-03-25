/*|============|*|
|*|Author:     |*| Star fire
|*|Date:       |*| 2024-03-25
|*|E-Mail:     |*| xinansky99@foxmail.com
|*|============|*/

using System;
using System.Text;

namespace AIO
{
    /// <summary>
    /// nameof(Pool_StringBuilder)
    /// </summary>
    public sealed class PoolStringBuilder : PoolSystem<StringBuilder>
    {
        static PoolStringBuilder()
        {
            CreateInstance<PoolStringBuilder>();
        }

        /// <inheritdoc />
        public PoolStringBuilder()
        {
            Capacity = 100;
        }

        /// <inheritdoc />
        protected override StringBuilder CreateEntity()
        {
            return new StringBuilder();
        }

        /// <inheritdoc />
        protected override int GetEID(StringBuilder entity)
        {
            return entity.GetHashCode();
        }
    }
}