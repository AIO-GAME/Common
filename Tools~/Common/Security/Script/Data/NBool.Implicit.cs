    #region NBool

    /// <summary>
    /// NBool is <see cref="bool"/>
    /// </summary>
    [Serializable, ComVisible(true)]
    partial struct NBool
    { 
        /// <param name="value"> <see cref="bool"/> </param>
        public static implicit operator NBool(bool value) => new NBool(value);

        /// <param name="value"> <see cref="NBool"/> </param>
        public static implicit operator bool(NBool value) => value.Value;

    }

    #endregion

