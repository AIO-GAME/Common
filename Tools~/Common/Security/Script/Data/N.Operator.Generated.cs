using System;

namespace AIO.Security
{
    #region NInt

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

    #endregion

    #region NLong

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

    #endregion

    #region NShort

    partial struct NShort
    { 
        /// <param name="a"> <see cref="NShort"/> </param>
        /// <param name="b"> <see cref="NShort"/> </param>
        public static bool operator ==(NShort a, NShort b) => a.Value == b.Value;

        /// <param name="a"> <see cref="NShort"/> </param>
        /// <param name="b"> <see cref="short"/> </param>
        public static bool operator ==(NShort a, short b) => a.Value == b;

        /// <param name="a"> <see cref="NShort"/> </param>
        /// <param name="b"> <see cref="NShort"/> </param>
        public static bool operator !=(NShort a, NShort b) => a.Value != b.Value;

        /// <param name="a"> <see cref="NShort"/> </param>
        /// <param name="b"> <see cref="short"/> </param>
        public static bool operator !=(NShort a, short b) => a.Value != b;

        /// <param name="a"> <see cref="NShort"/> </param>
        /// <param name="b"> <see cref="NShort"/> </param>
        public static NShort operator +(NShort a, NShort b) => new NShort(a.Value + b.Value);

        /// <param name="a"> <see cref="NShort"/> </param>
        /// <param name="b"> <see cref="short"/> </param>
        public static NShort operator +(NShort a, short b) => new NShort(a.Value + b);

        /// <param name="a"> <see cref="NShort"/> </param>
        /// <param name="b"> <see cref="NShort"/> </param>
        public static NShort operator -(NShort a, NShort b) => new NShort(a.Value - b.Value);

        /// <param name="a"> <see cref="NShort"/> </param>
        /// <param name="b"> <see cref="short"/> </param>
        public static NShort operator -(NShort a, short b) => new NShort(a.Value - b);

    }

    #endregion

    #region NUInt

    partial struct NUInt
    { 
        /// <param name="a"> <see cref="NUInt"/> </param>
        /// <param name="b"> <see cref="NUInt"/> </param>
        public static bool operator ==(NUInt a, NUInt b) => a.Value == b.Value;

        /// <param name="a"> <see cref="NUInt"/> </param>
        /// <param name="b"> <see cref="uint"/> </param>
        public static bool operator ==(NUInt a, uint b) => a.Value == b;

        /// <param name="a"> <see cref="NUInt"/> </param>
        /// <param name="b"> <see cref="NUInt"/> </param>
        public static bool operator !=(NUInt a, NUInt b) => a.Value != b.Value;

        /// <param name="a"> <see cref="NUInt"/> </param>
        /// <param name="b"> <see cref="uint"/> </param>
        public static bool operator !=(NUInt a, uint b) => a.Value != b;

        /// <param name="a"> <see cref="NUInt"/> </param>
        /// <param name="b"> <see cref="NUInt"/> </param>
        public static NUInt operator +(NUInt a, NUInt b) => new NUInt(a.Value + b.Value);

        /// <param name="a"> <see cref="NUInt"/> </param>
        /// <param name="b"> <see cref="uint"/> </param>
        public static NUInt operator +(NUInt a, uint b) => new NUInt(a.Value + b);

        /// <param name="a"> <see cref="NUInt"/> </param>
        /// <param name="b"> <see cref="NUInt"/> </param>
        public static NUInt operator -(NUInt a, NUInt b) => new NUInt(a.Value - b.Value);

        /// <param name="a"> <see cref="NUInt"/> </param>
        /// <param name="b"> <see cref="uint"/> </param>
        public static NUInt operator -(NUInt a, uint b) => new NUInt(a.Value - b);

    }

    #endregion

    #region NULong

    partial struct NULong
    { 
        /// <param name="a"> <see cref="NULong"/> </param>
        /// <param name="b"> <see cref="NULong"/> </param>
        public static bool operator ==(NULong a, NULong b) => a.Value == b.Value;

        /// <param name="a"> <see cref="NULong"/> </param>
        /// <param name="b"> <see cref="ulong"/> </param>
        public static bool operator ==(NULong a, ulong b) => a.Value == b;

        /// <param name="a"> <see cref="NULong"/> </param>
        /// <param name="b"> <see cref="NULong"/> </param>
        public static bool operator !=(NULong a, NULong b) => a.Value != b.Value;

        /// <param name="a"> <see cref="NULong"/> </param>
        /// <param name="b"> <see cref="ulong"/> </param>
        public static bool operator !=(NULong a, ulong b) => a.Value != b;

        /// <param name="a"> <see cref="NULong"/> </param>
        /// <param name="b"> <see cref="NULong"/> </param>
        public static NULong operator +(NULong a, NULong b) => new NULong(a.Value + b.Value);

        /// <param name="a"> <see cref="NULong"/> </param>
        /// <param name="b"> <see cref="ulong"/> </param>
        public static NULong operator +(NULong a, ulong b) => new NULong(a.Value + b);

        /// <param name="a"> <see cref="NULong"/> </param>
        /// <param name="b"> <see cref="NULong"/> </param>
        public static NULong operator -(NULong a, NULong b) => new NULong(a.Value - b.Value);

        /// <param name="a"> <see cref="NULong"/> </param>
        /// <param name="b"> <see cref="ulong"/> </param>
        public static NULong operator -(NULong a, ulong b) => new NULong(a.Value - b);

    }

    #endregion

    #region NUShort

    partial struct NUShort
    { 
        /// <param name="a"> <see cref="NUShort"/> </param>
        /// <param name="b"> <see cref="NUShort"/> </param>
        public static bool operator ==(NUShort a, NUShort b) => a.Value == b.Value;

        /// <param name="a"> <see cref="NUShort"/> </param>
        /// <param name="b"> <see cref="ushort"/> </param>
        public static bool operator ==(NUShort a, ushort b) => a.Value == b;

        /// <param name="a"> <see cref="NUShort"/> </param>
        /// <param name="b"> <see cref="NUShort"/> </param>
        public static bool operator !=(NUShort a, NUShort b) => a.Value != b.Value;

        /// <param name="a"> <see cref="NUShort"/> </param>
        /// <param name="b"> <see cref="ushort"/> </param>
        public static bool operator !=(NUShort a, ushort b) => a.Value != b;

        /// <param name="a"> <see cref="NUShort"/> </param>
        /// <param name="b"> <see cref="NUShort"/> </param>
        public static NUShort operator +(NUShort a, NUShort b) => new NUShort(a.Value + b.Value);

        /// <param name="a"> <see cref="NUShort"/> </param>
        /// <param name="b"> <see cref="ushort"/> </param>
        public static NUShort operator +(NUShort a, ushort b) => new NUShort(a.Value + b);

        /// <param name="a"> <see cref="NUShort"/> </param>
        /// <param name="b"> <see cref="NUShort"/> </param>
        public static NUShort operator -(NUShort a, NUShort b) => new NUShort(a.Value - b.Value);

        /// <param name="a"> <see cref="NUShort"/> </param>
        /// <param name="b"> <see cref="ushort"/> </param>
        public static NUShort operator -(NUShort a, ushort b) => new NUShort(a.Value - b);

    }

    #endregion

    #region NBool

    partial struct NBool
    { 
        /// <param name="a"> <see cref="NBool"/> </param>
        /// <param name="b"> <see cref="NBool"/> </param>
        public static bool operator ==(NBool a, NBool b) => a.Value == b.Value;

        /// <param name="a"> <see cref="NBool"/> </param>
        /// <param name="b"> <see cref="bool"/> </param>
        public static bool operator ==(NBool a, bool b) => a.Value == b;

        /// <param name="a"> <see cref="NBool"/> </param>
        /// <param name="b"> <see cref="NBool"/> </param>
        public static bool operator !=(NBool a, NBool b) => a.Value != b.Value;

        /// <param name="a"> <see cref="NBool"/> </param>
        /// <param name="b"> <see cref="bool"/> </param>
        public static bool operator !=(NBool a, bool b) => a.Value != b;

    }

    #endregion

    #region NByte

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

    #endregion

    #region NChar

    partial struct NChar
    { 
        /// <param name="a"> <see cref="NChar"/> </param>
        /// <param name="b"> <see cref="NChar"/> </param>
        public static bool operator ==(NChar a, NChar b) => a.Value == b.Value;

        /// <param name="a"> <see cref="NChar"/> </param>
        /// <param name="b"> <see cref="char"/> </param>
        public static bool operator ==(NChar a, char b) => a.Value == b;

        /// <param name="a"> <see cref="NChar"/> </param>
        /// <param name="b"> <see cref="NChar"/> </param>
        public static bool operator !=(NChar a, NChar b) => a.Value != b.Value;

        /// <param name="a"> <see cref="NChar"/> </param>
        /// <param name="b"> <see cref="char"/> </param>
        public static bool operator !=(NChar a, char b) => a.Value != b;

        /// <param name="a"> <see cref="NChar"/> </param>
        /// <param name="b"> <see cref="NChar"/> </param>
        public static NChar operator +(NChar a, NChar b) => new NChar(a.Value + b.Value);

        /// <param name="a"> <see cref="NChar"/> </param>
        /// <param name="b"> <see cref="char"/> </param>
        public static NChar operator +(NChar a, char b) => new NChar(a.Value + b);

        /// <param name="a"> <see cref="NChar"/> </param>
        /// <param name="b"> <see cref="NChar"/> </param>
        public static NChar operator -(NChar a, NChar b) => new NChar(a.Value - b.Value);

        /// <param name="a"> <see cref="NChar"/> </param>
        /// <param name="b"> <see cref="char"/> </param>
        public static NChar operator -(NChar a, char b) => new NChar(a.Value - b);

    }

    #endregion

    #region NSByte

    partial struct NSByte
    { 
        /// <param name="a"> <see cref="NSByte"/> </param>
        /// <param name="b"> <see cref="NSByte"/> </param>
        public static bool operator ==(NSByte a, NSByte b) => a.Value == b.Value;

        /// <param name="a"> <see cref="NSByte"/> </param>
        /// <param name="b"> <see cref="sbyte"/> </param>
        public static bool operator ==(NSByte a, sbyte b) => a.Value == b;

        /// <param name="a"> <see cref="NSByte"/> </param>
        /// <param name="b"> <see cref="NSByte"/> </param>
        public static bool operator !=(NSByte a, NSByte b) => a.Value != b.Value;

        /// <param name="a"> <see cref="NSByte"/> </param>
        /// <param name="b"> <see cref="sbyte"/> </param>
        public static bool operator !=(NSByte a, sbyte b) => a.Value != b;

        /// <param name="a"> <see cref="NSByte"/> </param>
        /// <param name="b"> <see cref="NSByte"/> </param>
        public static NSByte operator +(NSByte a, NSByte b) => new NSByte(a.Value + b.Value);

        /// <param name="a"> <see cref="NSByte"/> </param>
        /// <param name="b"> <see cref="sbyte"/> </param>
        public static NSByte operator +(NSByte a, sbyte b) => new NSByte(a.Value + b);

        /// <param name="a"> <see cref="NSByte"/> </param>
        /// <param name="b"> <see cref="NSByte"/> </param>
        public static NSByte operator -(NSByte a, NSByte b) => new NSByte(a.Value - b.Value);

        /// <param name="a"> <see cref="NSByte"/> </param>
        /// <param name="b"> <see cref="sbyte"/> </param>
        public static NSByte operator -(NSByte a, sbyte b) => new NSByte(a.Value - b);

    }

    #endregion

    #region NString

    partial struct NString
    { 
        /// <param name="a"> <see cref="NString"/> </param>
        /// <param name="b"> <see cref="NString"/> </param>
        public static bool operator ==(NString a, NString b) => a.Value == b.Value;

        /// <param name="a"> <see cref="NString"/> </param>
        /// <param name="b"> <see cref="string"/> </param>
        public static bool operator ==(NString a, string b) => a.Value == b;

        /// <param name="a"> <see cref="NString"/> </param>
        /// <param name="b"> <see cref="NString"/> </param>
        public static bool operator !=(NString a, NString b) => a.Value != b.Value;

        /// <param name="a"> <see cref="NString"/> </param>
        /// <param name="b"> <see cref="string"/> </param>
        public static bool operator !=(NString a, string b) => a.Value != b;

    }

    #endregion

    #region NFloat

    partial struct NFloat
    { 
        /// <param name="a"> <see cref="NFloat"/> </param>
        /// <param name="b"> <see cref="NFloat"/> </param>
        public static bool operator ==(NFloat a, NFloat b) => Math.Abs(a.Value - b.Value) <= float.Epsilon;

        /// <param name="a"> <see cref="NFloat"/> </param>
        /// <param name="b"> <see cref="float"/> </param>
        public static bool operator ==(NFloat a, float b) => Math.Abs(a.Value - b) <= float.Epsilon;

        /// <param name="a"> <see cref="NFloat"/> </param>
        /// <param name="b"> <see cref="NFloat"/> </param>
        public static bool operator !=(NFloat a, NFloat b) => Math.Abs(a.Value - b.Value) > float.Epsilon;

        /// <param name="a"> <see cref="NFloat"/> </param>
        /// <param name="b"> <see cref="float"/> </param>
        public static bool operator !=(NFloat a, float b) => Math.Abs(a.Value - b) > float.Epsilon;

        /// <param name="a"> <see cref="NFloat"/> </param>
        /// <param name="b"> <see cref="NFloat"/> </param>
        public static NFloat operator +(NFloat a, NFloat b) => new NFloat(a.Value + b.Value);

        /// <param name="a"> <see cref="NFloat"/> </param>
        /// <param name="b"> <see cref="float"/> </param>
        public static NFloat operator +(NFloat a, float b) => new NFloat(a.Value + b);

        /// <param name="a"> <see cref="NFloat"/> </param>
        /// <param name="b"> <see cref="NFloat"/> </param>
        public static NFloat operator -(NFloat a, NFloat b) => new NFloat(a.Value - b.Value);

        /// <param name="a"> <see cref="NFloat"/> </param>
        /// <param name="b"> <see cref="float"/> </param>
        public static NFloat operator -(NFloat a, float b) => new NFloat(a.Value - b);

    }

    #endregion

    #region NDouble

    partial struct NDouble
    { 
        /// <param name="a"> <see cref="NDouble"/> </param>
        /// <param name="b"> <see cref="NDouble"/> </param>
        public static bool operator ==(NDouble a, NDouble b) => Math.Abs(a.Value - b.Value) <= double.Epsilon;

        /// <param name="a"> <see cref="NDouble"/> </param>
        /// <param name="b"> <see cref="double"/> </param>
        public static bool operator ==(NDouble a, double b) => Math.Abs(a.Value - b) <= double.Epsilon;

        /// <param name="a"> <see cref="NDouble"/> </param>
        /// <param name="b"> <see cref="NDouble"/> </param>
        public static bool operator !=(NDouble a, NDouble b) => Math.Abs(a.Value - b.Value) > double.Epsilon;

        /// <param name="a"> <see cref="NDouble"/> </param>
        /// <param name="b"> <see cref="double"/> </param>
        public static bool operator !=(NDouble a, double b) => Math.Abs(a.Value - b) > double.Epsilon;

        /// <param name="a"> <see cref="NDouble"/> </param>
        /// <param name="b"> <see cref="NDouble"/> </param>
        public static NDouble operator +(NDouble a, NDouble b) => new NDouble(a.Value + b.Value);

        /// <param name="a"> <see cref="NDouble"/> </param>
        /// <param name="b"> <see cref="double"/> </param>
        public static NDouble operator +(NDouble a, double b) => new NDouble(a.Value + b);

        /// <param name="a"> <see cref="NDouble"/> </param>
        /// <param name="b"> <see cref="NDouble"/> </param>
        public static NDouble operator -(NDouble a, NDouble b) => new NDouble(a.Value - b.Value);

        /// <param name="a"> <see cref="NDouble"/> </param>
        /// <param name="b"> <see cref="double"/> </param>
        public static NDouble operator -(NDouble a, double b) => new NDouble(a.Value - b);

    }

    #endregion

    #region NDecimal

    partial struct NDecimal
    { 
        /// <param name="a"> <see cref="NDecimal"/> </param>
        /// <param name="b"> <see cref="NDecimal"/> </param>
        public static bool operator ==(NDecimal a, NDecimal b) => Math.Abs(a.Value - b.Value) <= decimal.Zero;

        /// <param name="a"> <see cref="NDecimal"/> </param>
        /// <param name="b"> <see cref="decimal"/> </param>
        public static bool operator ==(NDecimal a, decimal b) => Math.Abs(a.Value - b) <= decimal.Zero;

        /// <param name="a"> <see cref="NDecimal"/> </param>
        /// <param name="b"> <see cref="NDecimal"/> </param>
        public static bool operator !=(NDecimal a, NDecimal b) => Math.Abs(a.Value - b.Value) > decimal.Zero;

        /// <param name="a"> <see cref="NDecimal"/> </param>
        /// <param name="b"> <see cref="decimal"/> </param>
        public static bool operator !=(NDecimal a, decimal b) => Math.Abs(a.Value - b) > decimal.Zero;

        /// <param name="a"> <see cref="NDecimal"/> </param>
        /// <param name="b"> <see cref="NDecimal"/> </param>
        public static NDecimal operator +(NDecimal a, NDecimal b) => new NDecimal(a.Value + b.Value);

        /// <param name="a"> <see cref="NDecimal"/> </param>
        /// <param name="b"> <see cref="decimal"/> </param>
        public static NDecimal operator +(NDecimal a, decimal b) => new NDecimal(a.Value + b);

        /// <param name="a"> <see cref="NDecimal"/> </param>
        /// <param name="b"> <see cref="NDecimal"/> </param>
        public static NDecimal operator -(NDecimal a, NDecimal b) => new NDecimal(a.Value - b.Value);

        /// <param name="a"> <see cref="NDecimal"/> </param>
        /// <param name="b"> <see cref="decimal"/> </param>
        public static NDecimal operator -(NDecimal a, decimal b) => new NDecimal(a.Value - b);

    }

    #endregion

}