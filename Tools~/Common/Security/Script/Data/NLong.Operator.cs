﻿// <auto-generated> This code was generated by a tool. </auto-generated>
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

using System;

namespace AIO.Security
{
    partial struct NLong
    { 
        /// <param name="a"> <see cref="NLong"/> </param>
        /// <param name="b"> <see cref="NLong"/> </param>
        public static bool operator ==(NLong a, NLong b) => a.Value == b.Value;

        /// <param name="a"> <see cref="NLong"/> </param>
        /// <param name="b"> <see cref="long"/> </param>
        public static bool operator ==(NLong a, long b) => a.Value == b;

        /// <param name="a"> <see cref="NLong"/> </param>
        /// <param name="b"> <see cref="NLong"/> </param>
        public static bool operator !=(NLong a, NLong b) => a.Value != b.Value;

        /// <param name="a"> <see cref="NLong"/> </param>
        /// <param name="b"> <see cref="long"/> </param>
        public static bool operator !=(NLong a, long b) => a.Value != b;
        /// <param name="a"> <see cref="NLong"/> </param>
        public static NLong operator --(NLong a)
        {
            a.Value--;
            return a;
        }

        /// <param name="a"> <see cref="NLong"/> </param>
        public static NLong operator ++(NLong a)
        {
            a.Value++;
            return a;
        }

        /// <param name="a"> <see cref="NLong"/> </param>
        /// <param name="b"> <see cref="NLong"/> </param>
        public static NLong operator +(NLong a, NLong b) => new NLong(a.Value + b.Value);

        /// <param name="a"> <see cref="NLong"/> </param>
        /// <param name="b"> <see cref="long"/> </param>
        public static NLong operator +(NLong a, long b) => new NLong(a.Value + b);

        /// <param name="a"> <see cref="NLong"/> </param>
        /// <param name="b"> <see cref="NLong"/> </param>
        public static NLong operator -(NLong a, NLong b) => new NLong(a.Value - b.Value);

        /// <param name="a"> <see cref="NLong"/> </param>
        /// <param name="b"> <see cref="long"/> </param>
        public static NLong operator -(NLong a, long b) => new NLong(a.Value - b);
    }
}
