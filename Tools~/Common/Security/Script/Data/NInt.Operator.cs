﻿// <auto-generated> This code was generated by a tool. </auto-generated>
// Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.

using System;

namespace AIO.Security
{
    partial struct NInt
    { 
        /// <param name="a"> <see cref="NInt"/> </param>
        /// <param name="b"> <see cref="NInt"/> </param>
        public static bool operator ==(NInt a, NInt b) => a.Value == b.Value;

        /// <param name="a"> <see cref="NInt"/> </param>
        /// <param name="b"> <see cref="int"/> </param>
        public static bool operator ==(NInt a, int b) => a.Value == b;

        /// <param name="a"> <see cref="NInt"/> </param>
        /// <param name="b"> <see cref="NInt"/> </param>
        public static bool operator !=(NInt a, NInt b) => a.Value != b.Value;

        /// <param name="a"> <see cref="NInt"/> </param>
        /// <param name="b"> <see cref="int"/> </param>
        public static bool operator !=(NInt a, int b) => a.Value != b;
        /// <param name="a"> <see cref="NInt"/> </param>
        public static NInt operator --(NInt a)
        {
            a.Value--;
            return a;
        }

        /// <param name="a"> <see cref="NInt"/> </param>
        public static NInt operator ++(NInt a)
        {
            a.Value++;
            return a;
        }

        /// <param name="a"> <see cref="NInt"/> </param>
        /// <param name="b"> <see cref="NInt"/> </param>
        public static NInt operator +(NInt a, NInt b) => new NInt(a.Value + b.Value);

        /// <param name="a"> <see cref="NInt"/> </param>
        /// <param name="b"> <see cref="int"/> </param>
        public static NInt operator +(NInt a, int b) => new NInt(a.Value + b);

        /// <param name="a"> <see cref="NInt"/> </param>
        /// <param name="b"> <see cref="NInt"/> </param>
        public static NInt operator -(NInt a, NInt b) => new NInt(a.Value - b.Value);

        /// <param name="a"> <see cref="NInt"/> </param>
        /// <param name="b"> <see cref="int"/> </param>
        public static NInt operator -(NInt a, int b) => new NInt(a.Value - b);
    }
}
