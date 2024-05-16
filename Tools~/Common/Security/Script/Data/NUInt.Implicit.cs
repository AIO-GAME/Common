    #region NUInt

    /// <summary>
    /// NUInt is <see cref="uint"/>
    /// </summary>
    [Serializable, ComVisible(true)]
    partial struct NUInt
    { 
        /// <param name="value"> <see cref="uint"/> </param>
        public static implicit operator NUInt(uint value) => new NUInt(value);

        /// <param name="value"> <see cref="NUInt"/> </param>
        public static implicit operator uint(NUInt value) => value.Value;

        /// <param name="value"> <see cref="byte"/> </param>
        public static implicit operator NUInt(byte value) => new NUInt(value);

        /// <param name="value"> <see cref="char"/> </param>
        public static implicit operator NUInt(char value) => new NUInt(value);

        /// <param name="value"> <see cref="ushort"/> </param>
        public static implicit operator NUInt(ushort value) => new NUInt(value);

        /// <param name="value"> <see cref="NByte"/> </param>
        public static implicit operator NUInt(NByte value) => new NUInt(value);

        /// <param name="value"> <see cref="NChar"/> </param>
        public static implicit operator NUInt(NChar value) => new NUInt(value);

        /// <param name="value"> <see cref="NUShort"/> </param>
        public static implicit operator NUInt(NUShort value) => new NUInt(value);

    }

    #endregion

