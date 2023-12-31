﻿/* =================================================================================
 * File:   PListInteger.cs
 * Author: Christian Ecker
 *
 * Major Changes:
 * yyyy-mm-dd   Author               Description
 * ----------------------------------------------------------------
 * 2009-09-13   Christian Ecker      Created
 *
 * =================================================================================
 * Copyright (c) 2009, Christian Ecker
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification,
 * are permitted provided that the following conditions are met:
 *
 *  - Redistributions of source code must retain the above copyright notice,
 *    this list of conditions and the following disclaimer.
 *
 *  - Redistributions in binary form must reproduce the above copyright notice,
 *    this list of conditions and the following disclaimer in the documentation
 *    and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE
 * LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
 * CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF
 * THE POSSIBILITY OF SUCH DAMAGE.
 * =================================================================================
 */
using System;
using System.Collections.Generic;
using System.Text;


namespace AIO.PList
{
    /// <summary>
    /// Represents an string Value from a PList
    /// </summary>
    public class PListString : PListElement<String>
    {
        private static Byte[] s_UTF8Bytes = new Byte[] {
            0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F,
            0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F,
            0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28, 0x29, 0x2A, 0x2B, 0x2C, 0x2D, 0x2E, 0x2F,
            0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39, 0x3A, 0x3B, 0x3C, 0x3D, 0x3E, 0x3F,
            0x40, 0x41, 0x42, 0x43, 0x44, 0x45, 0x46, 0x47, 0x48, 0x49, 0x4A, 0x4B, 0x4C, 0x4D, 0x4E, 0x4F,
            0x50, 0x51, 0x52, 0x53, 0x54, 0x55, 0x56, 0x57, 0x58, 0x59, 0x5A, 0x5B, 0x5C, 0x5D, 0x5E, 0x5F,
            0x60, 0x61, 0x62, 0x63, 0x64, 0x65, 0x66, 0x67, 0x68, 0x69, 0x6A, 0x6B, 0x6C, 0x6D, 0x6E, 0x6F,
            0x70, 0x71, 0x72, 0x73, 0x74, 0x75, 0x76, 0x77, 0x78, 0x79, 0x7A, 0x7B, 0x7C, 0x7D, 0x7E, 0x7F,
            0x80, 0x81, 0x82, 0x83, 0x84, 0x85, 0x86, 0x87, 0x88, 0x89, 0x8A, 0x8B, 0x8C, 0x8D, 0x8E, 0x8F,
            0x90, 0x91, 0x92, 0x93, 0x94, 0x95, 0x96, 0x97, 0x98, 0x99, 0x9A, 0x9B, 0x9C, 0x9D, 0x9E, 0x9F,
            0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5, 0xA6, 0xA7, 0xA8, 0xA9, 0xAA, 0xAB, 0xAC, 0xAD, 0xAE, 0xAF,
            0xB0, 0xB1, 0xB2, 0xB3, 0xB4, 0xB5, 0xB6, 0xB7, 0xB8, 0xB9, 0xBA, 0xBB, 0xBC, 0xBD, 0xBE, 0xBF,
            0xC0, 0xC1, 0xC2, 0xC3, 0xC4, 0xC5, 0xC6, 0xC7, 0xC8, 0xC9, 0xCA, 0xCB, 0xCC, 0xCD, 0xCE, 0xCF,
            0xD0, 0xD1, 0xD2, 0xD3, 0xD4, 0xD5, 0xD6, 0xD7, 0xD8, 0xD9, 0xDA, 0xDB, 0xDC, 0xDD, 0xDE, 0xDF,
            0xE0, 0xE1, 0xE2, 0xE3, 0xE4, 0xE5, 0xE6, 0xE7, 0xE8, 0xE9, 0xEA, 0xEB, 0xEC, 0xED, 0xEE, 0xEF,
            0xF0, 0xF1, 0xF2, 0xF3, 0xF4, 0xF5, 0xF6, 0xF7, 0xF8, 0xF9, 0xFA, 0xFB, 0xFC, 0xFD, 0xFE, 0xFF};

        private static HashSet<char> s_UTF8Chars = new HashSet<char>(Encoding.UTF8.GetChars(s_UTF8Bytes));

        private String m_Value;
        private bool m_IsUTF16;

        /// <summary>
        /// Gets the Xml tag of this element.
        /// </summary>
        /// <value>The Xml tag of this element.</value>
        public override String Tag { get { return m_IsUTF16 ? "ustring" : "string"; } }

        /// <summary>
        /// Gets the binary typecode of this element.
        /// </summary>
        /// <value>The binary typecode of this element.</value>
        public override Byte TypeCode { get { return (Byte)(m_IsUTF16 ? 6 : 5); } }

        /// <summary>
        /// Initializes a new instance of the <see cref="PListString"/> class.
        /// </summary>
        public PListString() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PListString"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public PListString(String value) { Value = value; }

        /// <summary>
        /// Gets or sets the value of this element.
        /// </summary>
        /// <value>The value of this element.</value>
        public override String Value
        {
            get { return m_Value; }
            set
            {
                m_Value = value;
                //Detect Encoding
                foreach (char c in value)
                {
                    if (!s_UTF8Chars.Contains(c))
                    {
                        m_IsUTF16 = true;
                        return;
                    }
                }
                m_IsUTF16 = false;
            }
        }

        /// <summary>
        /// Parses the specified value from a given String, read from Xml.
        /// </summary>
        /// <param name="value">The String whis is parsed.</param>
        protected override void Parse(String value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets the XML String representation of the Value.
        /// </summary>
        /// <returns>
        /// The XML String representation of the Value.
        /// </returns>
        protected override String ToXmlString()
        {
            return Value;
        }

        /// <summary>
        /// Reads this element binary from the reader.
        /// </summary>
        /// <param name="reader">The <see cref="T:CE.iPhone.PListBinaryReader"/> from which the element is read.</param>
        /// <remarks>Provided for internal use only.</remarks>
        public override void ReadBinary(PListBinaryReader reader)
        {

            Byte[] buf = new Byte[reader.CurrentElementLength * (reader.CurrentElementTypeCode == 5 ? 1 : 2)];
            if (reader.BaseStream.Read(buf, 0, buf.Length) != buf.Length)
                throw new PListFormatException();


            Encoding enc = reader.CurrentElementTypeCode == 5 ? Encoding.UTF8 : Encoding.BigEndianUnicode;

            Value = enc.GetString(buf);
        }

        /// <summary>
        /// Gets the length of this PList element.
        /// </summary>
        /// <returns>The length of this PList element.</returns>
        /// <remarks>Provided for internal use only.</remarks>
        public override int GetPListElementLength()
        {
            return Value.Length;
        }

        /// <summary>
        /// Writes this element binary to the writer.
        /// </summary>
        /// <param name="writer">The <see cref="T:CE.iPhone.PListBinaryWriter"/> to which the element is written.</param>
        /// <remarks>Provided for internal use only.</remarks>
        public override void WriteBinary(PListBinaryWriter writer)
        {
            Encoding enc = m_IsUTF16 ? Encoding.BigEndianUnicode : Encoding.UTF8;
            Byte[] buf = enc.GetBytes(Value);
            writer.BaseStream.Write(buf, 0, buf.Length);
        }

        /// <summary>
        /// 获取哈希值
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return m_Value.GetHashCode();
        }

        /// <summary>
        /// 比较
        /// </summary>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// 相等
        /// </summary>
        public static bool operator ==(PListString a, PListString b)
        {
            return a.m_Value == b.m_Value;
        }

        /// <summary>
        /// 不相等
        /// </summary>
        public static bool operator !=(PListString a, PListString b)
        {
            return a.m_Value != b.m_Value;
        }

        /// <summary>
        /// 隐式转换
        /// </summary>
        public static implicit operator PListString(string value)
        {
            return new PListString(value);
        }
    }
}
