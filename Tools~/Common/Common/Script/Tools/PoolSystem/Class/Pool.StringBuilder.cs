﻿#region

using System.Text;

#endregion

namespace AIO
{
    /// <summary>
    /// nameof(Pool_StringBuilder)
    /// </summary>
    public sealed class PoolStringBuilder : PoolSystem<StringBuilder>
    {
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