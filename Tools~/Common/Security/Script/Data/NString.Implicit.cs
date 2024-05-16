    #region NString

    /// <summary>
    /// NString is <see cref="string"/>
    /// </summary>
    [Serializable, ComVisible(true)]
    partial struct NString
    { 
        /// <param name="value"> <see cref="string"/> </param>
        public static implicit operator NString(string value) => new NString(value);

        /// <param name="value"> <see cref="NString"/> </param>
        public static implicit operator string(NString value) => value.Value;

    }

    #endregion

