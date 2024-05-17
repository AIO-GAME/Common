
/* auto-generated */
using System;
using System.Runtime.InteropServices;

namespace AIO.Security
{
    /// <summary>
    /// NDecimal is <see cref="decimal"/>
    /// </summary>
    [Serializable, ComVisible(true)]
    partial struct NDecimal
    {
        /// <param name="value"> <see cref="decimal"/> </param>
        public static implicit operator NDecimal(decimal value) => new NDecimal(value);

        /// <param name="value"> <see cref="NDecimal"/> </param>
        public static implicit operator decimal(NDecimal value) => value.Value;
      
        /// <param name="value"> <see cref="byte"/> </param>
        public static implicit operator NDecimal(byte value) => new NDecimal(value);
      
        /// <param name="value"> <see cref="short"/> </param>
        public static implicit operator NDecimal(short value) => new NDecimal(value);
      
        /// <param name="value"> <see cref="ushort"/> </param>
        public static implicit operator NDecimal(ushort value) => new NDecimal(value);
      
        /// <param name="value"> <see cref="int"/> </param>
        public static implicit operator NDecimal(int value) => new NDecimal(value);
      
        /// <param name="value"> <see cref="uint"/> </param>
        public static implicit operator NDecimal(uint value) => new NDecimal(value);
      
        /// <param name="value"> <see cref="long"/> </param>
        public static implicit operator NDecimal(long value) => new NDecimal(value);
      
        /// <param name="value"> <see cref="ulong"/> </param>
        public static implicit operator NDecimal(ulong value) => new NDecimal(value);
      
        /// <param name="value"> <see cref="float"/> </param>
        public static implicit operator NDecimal(float value) => new NDecimal(value);
      
        /// <param name="value"> <see cref="double"/> </param>
        public static implicit operator NDecimal(double value) => new NDecimal(value);
      
        /// <param name="value"> <see cref="NByte"/> </param>
        public static implicit operator NDecimal(NByte value) => new NDecimal(value);
      
        /// <param name="value"> <see cref="NShort"/> </param>
        public static implicit operator NDecimal(NShort value) => new NDecimal(value);
      
        /// <param name="value"> <see cref="NUShort"/> </param>
        public static implicit operator NDecimal(NUShort value) => new NDecimal(value);
      
        /// <param name="value"> <see cref="NInt"/> </param>
        public static implicit operator NDecimal(NInt value) => new NDecimal(value);
      
        /// <param name="value"> <see cref="NUInt"/> </param>
        public static implicit operator NDecimal(NUInt value) => new NDecimal(value);
      
        /// <param name="value"> <see cref="NLong"/> </param>
        public static implicit operator NDecimal(NLong value) => new NDecimal(value);
      
        /// <param name="value"> <see cref="NULong"/> </param>
        public static implicit operator NDecimal(NULong value) => new NDecimal(value);
      
        /// <param name="value"> <see cref="NFloat"/> </param>
        public static implicit operator NDecimal(NFloat value) => new NDecimal(value);
      
        /// <param name="value"> <see cref="NDouble"/> </param>
        public static implicit operator NDecimal(NDouble value) => new NDecimal(value);

    }
}
