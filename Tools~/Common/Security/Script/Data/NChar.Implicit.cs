﻿// <auto-generated> This code was generated by a tool. </auto-generated>
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

using System;
using System.Runtime.InteropServices;

namespace AIO.Security
{
    [Serializable, ComVisible(true)]
    partial struct NChar
    {
        /// <param name="value"> <see cref="char"/> </param>
        public static implicit operator NChar(char value) => new NChar(value);

        /// <param name="value"> <see cref="NChar"/> </param>
        public static implicit operator char(NChar value) => value.Value;
 
        /// <param name="value"> <see cref="NChar"/> </param>
        public static implicit operator long(NChar value) => (long)value.Value;
 
        /// <param name="value"> <see cref="NChar"/> </param>
        public static implicit operator int(NChar value) => (int)value.Value;
 
        /// <param name="value"> <see cref="NChar"/> </param>
        public static implicit operator uint(NChar value) => (uint)value.Value;
 
        /// <param name="value"> <see cref="NChar"/> </param>
        public static implicit operator ulong(NChar value) => (ulong)value.Value;
        /// <param name="value"> <see cref="NLong"/> </param>
        public static implicit operator NLong(NChar value) => new NLong(value.Value);
        /// <param name="value"> <see cref="NInt"/> </param>
        public static implicit operator NInt(NChar value) => new NInt(value.Value);
        /// <param name="value"> <see cref="NUInt"/> </param>
        public static implicit operator NUInt(NChar value) => new NUInt(value.Value);
        /// <param name="value"> <see cref="NULong"/> </param>
        public static implicit operator NULong(NChar value) => new NULong(value.Value);
      
        /// <param name="value"> <see cref="byte"/> </param>
        public static implicit operator NChar(byte value) => new NChar(value);
      
        /// <param name="value"> <see cref="ushort"/> </param>
        public static implicit operator NChar(ushort value) => new NChar(value);
      
        /// <param name="value"> <see cref="NByte"/> </param>
        public static implicit operator NChar(NByte value) => new NChar(value);
      
        /// <param name="value"> <see cref="NUShort"/> </param>
        public static implicit operator NChar(NUShort value) => new NChar(value);
    }
}
