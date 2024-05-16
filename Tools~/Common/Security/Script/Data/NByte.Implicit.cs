    #region NByte

    /// <summary>
    /// NByte is <see cref="byte"/>
    /// </summary>
    [Serializable, ComVisible(true)]
    partial struct NByte
    { 
        /// <param name="value"> <see cref="byte"/> </param>
        public static implicit operator NByte(byte value) => new NByte(value);

        /// <param name="value"> <see cref="NByte"/> </param>
        public static implicit operator byte(NByte value) => value.Value;

    }

    #endregion

