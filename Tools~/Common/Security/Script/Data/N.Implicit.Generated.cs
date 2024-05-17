

/* auto-generated */
using System;
using System.Runtime.InteropServices;

namespace AIO.Security
{
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
}

namespace AIO.Security
{
    /// <summary>
    /// NSByte is <see cref="sbyte"/>
    /// </summary>
    [Serializable, ComVisible(true)]
    partial struct NSByte
    {
        /// <param name="value"> <see cref="sbyte"/> </param>
        public static implicit operator NSByte(sbyte value) => new NSByte(value);

        /// <param name="value"> <see cref="NSByte"/> </param>
        public static implicit operator sbyte(NSByte value) => value.Value;

    }
}

namespace AIO.Security
{
    /// <summary>
    /// NShort is <see cref="short"/>
    /// </summary>
    [Serializable, ComVisible(true)]
    partial struct NShort
    {
        /// <param name="value"> <see cref="short"/> </param>
        public static implicit operator NShort(short value) => new NShort(value);

        /// <param name="value"> <see cref="NShort"/> </param>
        public static implicit operator short(NShort value) => value.Value;
      
        /// <param name="value"> <see cref="byte"/> </param>
        public static implicit operator NShort(byte value) => new NShort(value);
      
        /// <param name="value"> <see cref="sbyte"/> </param>
        public static implicit operator NShort(sbyte value) => new NShort(value);
      
        /// <param name="value"> <see cref="char"/> </param>
        public static implicit operator NShort(char value) => new NShort(value);
      
        /// <param name="value"> <see cref="NByte"/> </param>
        public static implicit operator NShort(NByte value) => new NShort(value);
      
        /// <param name="value"> <see cref="NSByte"/> </param>
        public static implicit operator NShort(NSByte value) => new NShort(value);
      
        /// <param name="value"> <see cref="NChar"/> </param>
        public static implicit operator NShort(NChar value) => new NShort(value);

    }
}

namespace AIO.Security
{
    /// <summary>
    /// NInt is <see cref="int"/>
    /// </summary>
    [Serializable, ComVisible(true)]
    partial struct NInt
    {
        /// <param name="value"> <see cref="int"/> </param>
        public static implicit operator NInt(int value) => new NInt(value);

        /// <param name="value"> <see cref="NInt"/> </param>
        public static implicit operator int(NInt value) => value.Value;
 
        /// <param name="value"> <see cref="NInt"/> </param>
        public static implicit operator char(NInt value) => (char)value.Value;
 
        /// <param name="value"> <see cref="NInt"/> </param>
        public static implicit operator long(NInt value) => (long)value.Value;

        /// <param name="value"> <see cref="NChar"/> </param>
        public static implicit operator NChar(NInt value) => new NChar(value.Value);

        /// <param name="value"> <see cref="NLong"/> </param>
        public static implicit operator NLong(NInt value) => new NLong(value.Value);
      
        /// <param name="value"> <see cref="byte"/> </param>
        public static implicit operator NInt(byte value) => new NInt(value);
      
        /// <param name="value"> <see cref="sbyte"/> </param>
        public static implicit operator NInt(sbyte value) => new NInt(value);
      
        /// <param name="value"> <see cref="char"/> </param>
        public static implicit operator NInt(char value) => new NInt(value);
      
        /// <param name="value"> <see cref="short"/> </param>
        public static implicit operator NInt(short value) => new NInt(value);
      
        /// <param name="value"> <see cref="ushort"/> </param>
        public static implicit operator NInt(ushort value) => new NInt(value);
      
        /// <param name="value"> <see cref="NByte"/> </param>
        public static implicit operator NInt(NByte value) => new NInt(value);
      
        /// <param name="value"> <see cref="NSByte"/> </param>
        public static implicit operator NInt(NSByte value) => new NInt(value);
      
        /// <param name="value"> <see cref="NChar"/> </param>
        public static implicit operator NInt(NChar value) => new NInt(value);
      
        /// <param name="value"> <see cref="NShort"/> </param>
        public static implicit operator NInt(NShort value) => new NInt(value);
      
        /// <param name="value"> <see cref="NUShort"/> </param>
        public static implicit operator NInt(NUShort value) => new NInt(value);

    }
}

namespace AIO.Security
{
    /// <summary>
    /// NLong is <see cref="long"/>
    /// </summary>
    [Serializable, ComVisible(true)]
    partial struct NLong
    {
        /// <param name="value"> <see cref="long"/> </param>
        public static implicit operator NLong(long value) => new NLong(value);

        /// <param name="value"> <see cref="NLong"/> </param>
        public static implicit operator long(NLong value) => value.Value;
      
        /// <param name="value"> <see cref="byte"/> </param>
        public static implicit operator NLong(byte value) => new NLong(value);
      
        /// <param name="value"> <see cref="sbyte"/> </param>
        public static implicit operator NLong(sbyte value) => new NLong(value);
      
        /// <param name="value"> <see cref="char"/> </param>
        public static implicit operator NLong(char value) => new NLong(value);
      
        /// <param name="value"> <see cref="short"/> </param>
        public static implicit operator NLong(short value) => new NLong(value);
      
        /// <param name="value"> <see cref="ushort"/> </param>
        public static implicit operator NLong(ushort value) => new NLong(value);
      
        /// <param name="value"> <see cref="int"/> </param>
        public static implicit operator NLong(int value) => new NLong(value);
      
        /// <param name="value"> <see cref="uint"/> </param>
        public static implicit operator NLong(uint value) => new NLong(value);
      
        /// <param name="value"> <see cref="NByte"/> </param>
        public static implicit operator NLong(NByte value) => new NLong(value);
      
        /// <param name="value"> <see cref="NSByte"/> </param>
        public static implicit operator NLong(NSByte value) => new NLong(value);
      
        /// <param name="value"> <see cref="NChar"/> </param>
        public static implicit operator NLong(NChar value) => new NLong(value);
      
        /// <param name="value"> <see cref="NShort"/> </param>
        public static implicit operator NLong(NShort value) => new NLong(value);
      
        /// <param name="value"> <see cref="NUShort"/> </param>
        public static implicit operator NLong(NUShort value) => new NLong(value);
      
        /// <param name="value"> <see cref="NInt"/> </param>
        public static implicit operator NLong(NInt value) => new NLong(value);
      
        /// <param name="value"> <see cref="NUInt"/> </param>
        public static implicit operator NLong(NUInt value) => new NLong(value);

    }
}

namespace AIO.Security
{
    /// <summary>
    /// NByte is <see cref="byte"/>
    /// </summary>
    [Serializable, ComVisible(true)]
    partial struct NByte
    {
        /// <param name="value"> <see cref="byte"/> </param>
        public static implicit operator NByte(byte value) => new NByte(value);

        /// <param name="value"> <see cref="NByte"/> </param>
        public static implicit operator byte(NByte value) => value.Value;

    }
}

namespace AIO.Security
{
    /// <summary>
    /// NUShort is <see cref="ushort"/>
    /// </summary>
    [Serializable, ComVisible(true)]
    partial struct NUShort
    {
        /// <param name="value"> <see cref="ushort"/> </param>
        public static implicit operator NUShort(ushort value) => new NUShort(value);

        /// <param name="value"> <see cref="NUShort"/> </param>
        public static implicit operator ushort(NUShort value) => value.Value;
      
        /// <param name="value"> <see cref="byte"/> </param>
        public static implicit operator NUShort(byte value) => new NUShort(value);
      
        /// <param name="value"> <see cref="char"/> </param>
        public static implicit operator NUShort(char value) => new NUShort(value);
      
        /// <param name="value"> <see cref="NByte"/> </param>
        public static implicit operator NUShort(NByte value) => new NUShort(value);
      
        /// <param name="value"> <see cref="NChar"/> </param>
        public static implicit operator NUShort(NChar value) => new NUShort(value);

    }
}

namespace AIO.Security
{
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
}

namespace AIO.Security
{
    /// <summary>
    /// NULong is <see cref="ulong"/>
    /// </summary>
    [Serializable, ComVisible(true)]
    partial struct NULong
    {
        /// <param name="value"> <see cref="ulong"/> </param>
        public static implicit operator NULong(ulong value) => new NULong(value);

        /// <param name="value"> <see cref="NULong"/> </param>
        public static implicit operator ulong(NULong value) => value.Value;
      
        /// <param name="value"> <see cref="byte"/> </param>
        public static implicit operator NULong(byte value) => new NULong(value);
      
        /// <param name="value"> <see cref="char"/> </param>
        public static implicit operator NULong(char value) => new NULong(value);
      
        /// <param name="value"> <see cref="ushort"/> </param>
        public static implicit operator NULong(ushort value) => new NULong(value);
      
        /// <param name="value"> <see cref="uint"/> </param>
        public static implicit operator NULong(uint value) => new NULong(value);
      
        /// <param name="value"> <see cref="NByte"/> </param>
        public static implicit operator NULong(NByte value) => new NULong(value);
      
        /// <param name="value"> <see cref="NChar"/> </param>
        public static implicit operator NULong(NChar value) => new NULong(value);
      
        /// <param name="value"> <see cref="NUShort"/> </param>
        public static implicit operator NULong(NUShort value) => new NULong(value);
      
        /// <param name="value"> <see cref="NUInt"/> </param>
        public static implicit operator NULong(NUInt value) => new NULong(value);

    }
}

namespace AIO.Security
{
    /// <summary>
    /// NChar is <see cref="char"/>
    /// </summary>
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

namespace AIO.Security
{
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
}

namespace AIO.Security
{
    /// <summary>
    /// NFloat is <see cref="float"/>
    /// </summary>
    [Serializable, ComVisible(true)]
    partial struct NFloat
    {
        /// <param name="value"> <see cref="float"/> </param>
        public static implicit operator NFloat(float value) => new NFloat(value);

        /// <param name="value"> <see cref="NFloat"/> </param>
        public static implicit operator float(NFloat value) => value.Value;
      
        /// <param name="value"> <see cref="byte"/> </param>
        public static implicit operator NFloat(byte value) => new NFloat(value);
      
        /// <param name="value"> <see cref="short"/> </param>
        public static implicit operator NFloat(short value) => new NFloat(value);
      
        /// <param name="value"> <see cref="ushort"/> </param>
        public static implicit operator NFloat(ushort value) => new NFloat(value);
      
        /// <param name="value"> <see cref="int"/> </param>
        public static implicit operator NFloat(int value) => new NFloat(value);
      
        /// <param name="value"> <see cref="uint"/> </param>
        public static implicit operator NFloat(uint value) => new NFloat(value);
      
        /// <param name="value"> <see cref="long"/> </param>
        public static implicit operator NFloat(long value) => new NFloat(value);
      
        /// <param name="value"> <see cref="ulong"/> </param>
        public static implicit operator NFloat(ulong value) => new NFloat(value);
      
        /// <param name="value"> <see cref="NByte"/> </param>
        public static implicit operator NFloat(NByte value) => new NFloat(value);
      
        /// <param name="value"> <see cref="NShort"/> </param>
        public static implicit operator NFloat(NShort value) => new NFloat(value);
      
        /// <param name="value"> <see cref="NUShort"/> </param>
        public static implicit operator NFloat(NUShort value) => new NFloat(value);
      
        /// <param name="value"> <see cref="NInt"/> </param>
        public static implicit operator NFloat(NInt value) => new NFloat(value);
      
        /// <param name="value"> <see cref="NUInt"/> </param>
        public static implicit operator NFloat(NUInt value) => new NFloat(value);
      
        /// <param name="value"> <see cref="NLong"/> </param>
        public static implicit operator NFloat(NLong value) => new NFloat(value);
      
        /// <param name="value"> <see cref="NULong"/> </param>
        public static implicit operator NFloat(NULong value) => new NFloat(value);

    }
}

namespace AIO.Security
{
    /// <summary>
    /// NDouble is <see cref="double"/>
    /// </summary>
    [Serializable, ComVisible(true)]
    partial struct NDouble
    {
        /// <param name="value"> <see cref="double"/> </param>
        public static implicit operator NDouble(double value) => new NDouble(value);

        /// <param name="value"> <see cref="NDouble"/> </param>
        public static implicit operator double(NDouble value) => value.Value;
      
        /// <param name="value"> <see cref="byte"/> </param>
        public static implicit operator NDouble(byte value) => new NDouble(value);
      
        /// <param name="value"> <see cref="short"/> </param>
        public static implicit operator NDouble(short value) => new NDouble(value);
      
        /// <param name="value"> <see cref="ushort"/> </param>
        public static implicit operator NDouble(ushort value) => new NDouble(value);
      
        /// <param name="value"> <see cref="int"/> </param>
        public static implicit operator NDouble(int value) => new NDouble(value);
      
        /// <param name="value"> <see cref="uint"/> </param>
        public static implicit operator NDouble(uint value) => new NDouble(value);
      
        /// <param name="value"> <see cref="long"/> </param>
        public static implicit operator NDouble(long value) => new NDouble(value);
      
        /// <param name="value"> <see cref="ulong"/> </param>
        public static implicit operator NDouble(ulong value) => new NDouble(value);
      
        /// <param name="value"> <see cref="float"/> </param>
        public static implicit operator NDouble(float value) => new NDouble(value);
      
        /// <param name="value"> <see cref="NByte"/> </param>
        public static implicit operator NDouble(NByte value) => new NDouble(value);
      
        /// <param name="value"> <see cref="NShort"/> </param>
        public static implicit operator NDouble(NShort value) => new NDouble(value);
      
        /// <param name="value"> <see cref="NUShort"/> </param>
        public static implicit operator NDouble(NUShort value) => new NDouble(value);
      
        /// <param name="value"> <see cref="NInt"/> </param>
        public static implicit operator NDouble(NInt value) => new NDouble(value);
      
        /// <param name="value"> <see cref="NUInt"/> </param>
        public static implicit operator NDouble(NUInt value) => new NDouble(value);
      
        /// <param name="value"> <see cref="NLong"/> </param>
        public static implicit operator NDouble(NLong value) => new NDouble(value);
      
        /// <param name="value"> <see cref="NULong"/> </param>
        public static implicit operator NDouble(NULong value) => new NDouble(value);
      
        /// <param name="value"> <see cref="NFloat"/> </param>
        public static implicit operator NDouble(NFloat value) => new NDouble(value);

    }
}

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
