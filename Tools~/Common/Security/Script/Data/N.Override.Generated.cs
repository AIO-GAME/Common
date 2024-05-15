using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Globalization;

namespace AIO.Security
{
    #region NInt

    partial struct NInt
    { 
        /// <inheritdoc/>
        public override bool Equals(object obj) => obj switch
        {
            NInt a => Value.Equals(a.Value),
            int b  => Value.Equals(b),
            _       => false
        };

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => Value.ToString(CultureInfo.CurrentCulture);

        /// <param name="provider"> 格式化提供者 </param>
        /// <typeparam name="T"> 格式化提供者类型 </typeparam>
        /// <returns> <see cref="string"/> </returns>
        public string ToString<T>(T provider) where T : IFormatProvider => Value.ToString(provider);

        /// <param name="format"> 格式化字符串 </param>
        /// <returns> <see cref="string"/> </returns>
        public string ToString(string format) => Value.ToString(format, CultureInfo.CurrentCulture);

        /// <param name="format"> 格式化字符串 </param>
        /// <param name="provider"> 格式化提供者 </param>
        /// <typeparam name="T"> 格式化提供者类型 </typeparam>
        /// <returns> <see cref="string"/> </returns>
        public string ToString<T>(string format, T provider) where T : IFormatProvider => Value.ToString(format, provider);

    }

    #endregion

    #region NLong

    partial struct NLong
    { 
        /// <inheritdoc/>
        public override bool Equals(object obj) => obj switch
        {
            NLong a => Value.Equals(a.Value),
            long b  => Value.Equals(b),
            _       => false
        };

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => Value.ToString(CultureInfo.CurrentCulture);

        /// <param name="provider"> 格式化提供者 </param>
        /// <typeparam name="T"> 格式化提供者类型 </typeparam>
        /// <returns> <see cref="string"/> </returns>
        public string ToString<T>(T provider) where T : IFormatProvider => Value.ToString(provider);

        /// <param name="format"> 格式化字符串 </param>
        /// <returns> <see cref="string"/> </returns>
        public string ToString(string format) => Value.ToString(format, CultureInfo.CurrentCulture);

        /// <param name="format"> 格式化字符串 </param>
        /// <param name="provider"> 格式化提供者 </param>
        /// <typeparam name="T"> 格式化提供者类型 </typeparam>
        /// <returns> <see cref="string"/> </returns>
        public string ToString<T>(string format, T provider) where T : IFormatProvider => Value.ToString(format, provider);

    }

    #endregion

    #region NShort

    partial struct NShort
    { 
        /// <inheritdoc/>
        public override bool Equals(object obj) => obj switch
        {
            NShort a => Value.Equals(a.Value),
            short b  => Value.Equals(b),
            _       => false
        };

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => Value.ToString(CultureInfo.CurrentCulture);

        /// <param name="provider"> 格式化提供者 </param>
        /// <typeparam name="T"> 格式化提供者类型 </typeparam>
        /// <returns> <see cref="string"/> </returns>
        public string ToString<T>(T provider) where T : IFormatProvider => Value.ToString(provider);

        /// <param name="format"> 格式化字符串 </param>
        /// <returns> <see cref="string"/> </returns>
        public string ToString(string format) => Value.ToString(format, CultureInfo.CurrentCulture);

        /// <param name="format"> 格式化字符串 </param>
        /// <param name="provider"> 格式化提供者 </param>
        /// <typeparam name="T"> 格式化提供者类型 </typeparam>
        /// <returns> <see cref="string"/> </returns>
        public string ToString<T>(string format, T provider) where T : IFormatProvider => Value.ToString(format, provider);

    }

    #endregion

    #region NUInt

    partial struct NUInt
    { 
        /// <inheritdoc/>
        public override bool Equals(object obj) => obj switch
        {
            NUInt a => Value.Equals(a.Value),
            uint b  => Value.Equals(b),
            _       => false
        };

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => Value.ToString(CultureInfo.CurrentCulture);

        /// <param name="provider"> 格式化提供者 </param>
        /// <typeparam name="T"> 格式化提供者类型 </typeparam>
        /// <returns> <see cref="string"/> </returns>
        public string ToString<T>(T provider) where T : IFormatProvider => Value.ToString(provider);

        /// <param name="format"> 格式化字符串 </param>
        /// <returns> <see cref="string"/> </returns>
        public string ToString(string format) => Value.ToString(format, CultureInfo.CurrentCulture);

        /// <param name="format"> 格式化字符串 </param>
        /// <param name="provider"> 格式化提供者 </param>
        /// <typeparam name="T"> 格式化提供者类型 </typeparam>
        /// <returns> <see cref="string"/> </returns>
        public string ToString<T>(string format, T provider) where T : IFormatProvider => Value.ToString(format, provider);

    }

    #endregion

    #region NULong

    partial struct NULong
    { 
        /// <inheritdoc/>
        public override bool Equals(object obj) => obj switch
        {
            NULong a => Value.Equals(a.Value),
            ulong b  => Value.Equals(b),
            _       => false
        };

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => Value.ToString(CultureInfo.CurrentCulture);

        /// <param name="provider"> 格式化提供者 </param>
        /// <typeparam name="T"> 格式化提供者类型 </typeparam>
        /// <returns> <see cref="string"/> </returns>
        public string ToString<T>(T provider) where T : IFormatProvider => Value.ToString(provider);

        /// <param name="format"> 格式化字符串 </param>
        /// <returns> <see cref="string"/> </returns>
        public string ToString(string format) => Value.ToString(format, CultureInfo.CurrentCulture);

        /// <param name="format"> 格式化字符串 </param>
        /// <param name="provider"> 格式化提供者 </param>
        /// <typeparam name="T"> 格式化提供者类型 </typeparam>
        /// <returns> <see cref="string"/> </returns>
        public string ToString<T>(string format, T provider) where T : IFormatProvider => Value.ToString(format, provider);

    }

    #endregion

    #region NUShort

    partial struct NUShort
    { 
        /// <inheritdoc/>
        public override bool Equals(object obj) => obj switch
        {
            NUShort a => Value.Equals(a.Value),
            ushort b  => Value.Equals(b),
            _       => false
        };

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => Value.ToString(CultureInfo.CurrentCulture);

        /// <param name="provider"> 格式化提供者 </param>
        /// <typeparam name="T"> 格式化提供者类型 </typeparam>
        /// <returns> <see cref="string"/> </returns>
        public string ToString<T>(T provider) where T : IFormatProvider => Value.ToString(provider);

        /// <param name="format"> 格式化字符串 </param>
        /// <returns> <see cref="string"/> </returns>
        public string ToString(string format) => Value.ToString(format, CultureInfo.CurrentCulture);

        /// <param name="format"> 格式化字符串 </param>
        /// <param name="provider"> 格式化提供者 </param>
        /// <typeparam name="T"> 格式化提供者类型 </typeparam>
        /// <returns> <see cref="string"/> </returns>
        public string ToString<T>(string format, T provider) where T : IFormatProvider => Value.ToString(format, provider);

    }

    #endregion

    #region NBool

    partial struct NBool
    { 
        /// <inheritdoc/>
        public override bool Equals(object obj) => obj switch
        {
            NBool a => Value.Equals(a.Value),
            bool b  => Value.Equals(b),
            _       => false
        };

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => Value.ToString(CultureInfo.CurrentCulture);

        /// <param name="provider"> 格式化提供者 </param>
        /// <typeparam name="T"> 格式化提供者类型 </typeparam>
        /// <returns> <see cref="string"/> </returns>
        public string ToString<T>(T provider) where T : IFormatProvider => Value.ToString(provider);

    }

    #endregion

    #region NByte

    partial struct NByte
    { 
        /// <inheritdoc/>
        public override bool Equals(object obj) => obj switch
        {
            NByte a => Value.Equals(a.Value),
            byte b  => Value.Equals(b),
            _       => false
        };

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => Value.ToString(CultureInfo.CurrentCulture);

        /// <param name="provider"> 格式化提供者 </param>
        /// <typeparam name="T"> 格式化提供者类型 </typeparam>
        /// <returns> <see cref="string"/> </returns>
        public string ToString<T>(T provider) where T : IFormatProvider => Value.ToString(provider);

        /// <param name="format"> 格式化字符串 </param>
        /// <returns> <see cref="string"/> </returns>
        public string ToString(string format) => Value.ToString(format, CultureInfo.CurrentCulture);

        /// <param name="format"> 格式化字符串 </param>
        /// <param name="provider"> 格式化提供者 </param>
        /// <typeparam name="T"> 格式化提供者类型 </typeparam>
        /// <returns> <see cref="string"/> </returns>
        public string ToString<T>(string format, T provider) where T : IFormatProvider => Value.ToString(format, provider);

    }

    #endregion

    #region NChar

    partial struct NChar
    { 
        /// <inheritdoc/>
        public override bool Equals(object obj) => obj switch
        {
            NChar a => Value.Equals(a.Value),
            char b  => Value.Equals(b),
            _       => false
        };

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => Value.ToString(CultureInfo.CurrentCulture);

        /// <param name="provider"> 格式化提供者 </param>
        /// <typeparam name="T"> 格式化提供者类型 </typeparam>
        /// <returns> <see cref="string"/> </returns>
        public string ToString<T>(T provider) where T : IFormatProvider => Value.ToString(provider);

    }

    #endregion

    #region NSByte

    partial struct NSByte
    { 
        /// <inheritdoc/>
        public override bool Equals(object obj) => obj switch
        {
            NSByte a => Value.Equals(a.Value),
            sbyte b  => Value.Equals(b),
            _       => false
        };

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => Value.ToString(CultureInfo.CurrentCulture);

        /// <param name="provider"> 格式化提供者 </param>
        /// <typeparam name="T"> 格式化提供者类型 </typeparam>
        /// <returns> <see cref="string"/> </returns>
        public string ToString<T>(T provider) where T : IFormatProvider => Value.ToString(provider);

        /// <param name="format"> 格式化字符串 </param>
        /// <returns> <see cref="string"/> </returns>
        public string ToString(string format) => Value.ToString(format, CultureInfo.CurrentCulture);

        /// <param name="format"> 格式化字符串 </param>
        /// <param name="provider"> 格式化提供者 </param>
        /// <typeparam name="T"> 格式化提供者类型 </typeparam>
        /// <returns> <see cref="string"/> </returns>
        public string ToString<T>(string format, T provider) where T : IFormatProvider => Value.ToString(format, provider);

    }

    #endregion

    #region NString

    partial struct NString
    { 
        /// <inheritdoc/>
        public override bool Equals(object obj) => obj switch
        {
            NString a => Value.Equals(a.Value),
            string b  => Value.Equals(b),
            _       => false
        };

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => Value.ToString(CultureInfo.CurrentCulture);

        /// <param name="provider"> 格式化提供者 </param>
        /// <typeparam name="T"> 格式化提供者类型 </typeparam>
        /// <returns> <see cref="string"/> </returns>
        public string ToString<T>(T provider) where T : IFormatProvider => Value.ToString(provider);

    }

    #endregion

    #region NFloat

    partial struct NFloat
    { 
        /// <inheritdoc/>
        public override bool Equals(object obj) => obj switch
        {
            NFloat a => Value.Equals(a.Value),
            float b  => Value.Equals(b),
            _       => false
        };

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => Value.ToString(CultureInfo.CurrentCulture);

        /// <param name="provider"> 格式化提供者 </param>
        /// <typeparam name="T"> 格式化提供者类型 </typeparam>
        /// <returns> <see cref="string"/> </returns>
        public string ToString<T>(T provider) where T : IFormatProvider => Value.ToString(provider);

        /// <param name="format"> 格式化字符串 </param>
        /// <returns> <see cref="string"/> </returns>
        public string ToString(string format) => Value.ToString(format, CultureInfo.CurrentCulture);

        /// <param name="format"> 格式化字符串 </param>
        /// <param name="provider"> 格式化提供者 </param>
        /// <typeparam name="T"> 格式化提供者类型 </typeparam>
        /// <returns> <see cref="string"/> </returns>
        public string ToString<T>(string format, T provider) where T : IFormatProvider => Value.ToString(format, provider);

    }

    #endregion

    #region NDouble

    partial struct NDouble
    { 
        /// <inheritdoc/>
        public override bool Equals(object obj) => obj switch
        {
            NDouble a => Value.Equals(a.Value),
            double b  => Value.Equals(b),
            _       => false
        };

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => Value.ToString(CultureInfo.CurrentCulture);

        /// <param name="provider"> 格式化提供者 </param>
        /// <typeparam name="T"> 格式化提供者类型 </typeparam>
        /// <returns> <see cref="string"/> </returns>
        public string ToString<T>(T provider) where T : IFormatProvider => Value.ToString(provider);

        /// <param name="format"> 格式化字符串 </param>
        /// <returns> <see cref="string"/> </returns>
        public string ToString(string format) => Value.ToString(format, CultureInfo.CurrentCulture);

        /// <param name="format"> 格式化字符串 </param>
        /// <param name="provider"> 格式化提供者 </param>
        /// <typeparam name="T"> 格式化提供者类型 </typeparam>
        /// <returns> <see cref="string"/> </returns>
        public string ToString<T>(string format, T provider) where T : IFormatProvider => Value.ToString(format, provider);

    }

    #endregion

    #region NDecimal

    partial struct NDecimal
    { 
        /// <inheritdoc/>
        public override bool Equals(object obj) => obj switch
        {
            NDecimal a => Value.Equals(a.Value),
            decimal b  => Value.Equals(b),
            _       => false
        };

        /// <inheritdoc/>
        public override int GetHashCode() => Value.GetHashCode();

        /// <inheritdoc/>
        public override string ToString() => Value.ToString(CultureInfo.CurrentCulture);

        /// <param name="provider"> 格式化提供者 </param>
        /// <typeparam name="T"> 格式化提供者类型 </typeparam>
        /// <returns> <see cref="string"/> </returns>
        public string ToString<T>(T provider) where T : IFormatProvider => Value.ToString(provider);

        /// <param name="format"> 格式化字符串 </param>
        /// <returns> <see cref="string"/> </returns>
        public string ToString(string format) => Value.ToString(format, CultureInfo.CurrentCulture);

        /// <param name="format"> 格式化字符串 </param>
        /// <param name="provider"> 格式化提供者 </param>
        /// <typeparam name="T"> 格式化提供者类型 </typeparam>
        /// <returns> <see cref="string"/> </returns>
        public string ToString<T>(string format, T provider) where T : IFormatProvider => Value.ToString(format, provider);

    }

    #endregion

}