﻿// <auto-generated> This code was generated by a tool. </auto-generated>
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

using System;

namespace AIO.Security
{
    partial struct NByte
    { 
        /// <param name="a"> <see cref="NByte"/> </param>
        /// <param name="b"> <see cref="NByte"/> </param>
        public static bool operator ==(NByte a, NByte b) => a.Value == b.Value;

        /// <param name="a"> <see cref="NByte"/> </param>
        /// <param name="b"> <see cref="byte"/> </param>
        public static bool operator ==(NByte a, byte b) => a.Value == b;

        /// <param name="a"> <see cref="NByte"/> </param>
        /// <param name="b"> <see cref="NByte"/> </param>
        public static bool operator !=(NByte a, NByte b) => a.Value != b.Value;

        /// <param name="a"> <see cref="NByte"/> </param>
        /// <param name="b"> <see cref="byte"/> </param>
        public static bool operator !=(NByte a, byte b) => a.Value != b;
        /// <param name="a"> <see cref="NByte"/> </param>
        public static NByte operator --(NByte a)
        {
            a.Value--;
            return a;
        }

        /// <param name="a"> <see cref="NByte"/> </param>
        public static NByte operator ++(NByte a)
        {
            a.Value++;
            return a;
        }

        /// <param name="a"> <see cref="NByte"/> </param>
        /// <param name="b"> <see cref="NByte"/> </param>
        public static NByte operator +(NByte a, NByte b) => new NByte(a.Value + b.Value);

        /// <param name="a"> <see cref="NByte"/> </param>
        /// <param name="b"> <see cref="byte"/> </param>
        public static NByte operator +(NByte a, byte b) => new NByte(a.Value + b);

        /// <param name="a"> <see cref="NByte"/> </param>
        /// <param name="b"> <see cref="NByte"/> </param>
        public static NByte operator -(NByte a, NByte b) => new NByte(a.Value - b.Value);

        /// <param name="a"> <see cref="NByte"/> </param>
        /// <param name="b"> <see cref="byte"/> </param>
        public static NByte operator -(NByte a, byte b) => new NByte(a.Value - b);
    }
}