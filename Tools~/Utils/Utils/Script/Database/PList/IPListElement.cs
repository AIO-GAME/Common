/* =================================================================================
 * File:   IPListElement.cs
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

namespace AIO.PList
{
    using System;
    using System.Diagnostics;
    using System.Xml.Serialization;

    /// <summary>
    /// A .Net representation of a  PList element exten
    /// </summary>
    public static class IPListElementExten
    {
        /// <summary>
        /// 转换为 String
        /// </summary>
        public static PListString AsString(this IPListElement element)
        {
            Debug.Assert(element.TypeCode == 5, "PListElement Conversion Failure");
            return (PListString)element;
        }

        /// <summary>
        /// 转换为 Real
        /// </summary>
        public static PListReal AsReal(this IPListElement element)
        {
            Debug.Assert(element.TypeCode == 2, "PListElement Conversion Failure");
            return (PListReal)element;
        }

        /// <summary>
        /// 转换为 Bool
        /// </summary>
        public static PListBool AsBool(this IPListElement element)
        {
            Debug.Assert(element.TypeCode == 0, "PListElement Conversion Failure");
            return (PListBool)element;
        }

        /// <summary>
        /// 转换为 Dict
        /// </summary>
        public static PListDict AsDict(this IPListElement element)
        {
            Debug.Assert(element.TypeCode == 13, "PListElement Conversion Failure");
            return (PListDict)element;
        }

        /// <summary>
        /// 转换为 Integer
        /// </summary>
        public static PListInteger AsInteger(this IPListElement element)
        {
            Debug.Assert(element.TypeCode == 1, "PListElement Conversion Failure");
            return (PListInteger)element;
        }

        /// <summary>
        /// 转换为 Date
        /// </summary>
        public static PListDate AsDate(this IPListElement element)
        {
            Debug.Assert(element.TypeCode == 3, "PListElement Conversion Failure");
            return (PListDate)element;
        }

        /// <summary>
        /// 转换为 Data
        /// </summary>
        public static PListData AsData(this IPListElement element)
        {
            Debug.Assert(element.TypeCode == 4, "PListElement Conversion Failure");
            return (PListData)element;
        }

        /// <summary>
        /// 转换为 Null
        /// </summary>
        public static PListArray AsArray(this IPListElement element)
        {
            Debug.Assert(element.TypeCode == 0x0A, "PListElement Conversion Failure");
            return (PListArray)element;
        }
    }

    /// <summary>
    /// A .Net representation of a  PList element
    /// </summary>
    public interface IPListElement : IXmlSerializable
    {
        /// <summary>
        /// Gets the Xml tag of this element.
        /// </summary>
        /// <value>The Xml tag of this element.</value>
        String Tag { get; }

        /// <summary>
        /// Gets the binary typecode of this element.
        /// </summary>
        /// <value>The binary typecode of this element.</value>
        Byte TypeCode { get; }

        /// <summary>
        /// Gets the length of this PList element.
        /// </summary>
        /// <returns>The length of this PList element.</returns>
        /// <remarks>Provided for internal use only.</remarks>
        int GetPListElementLength();

        /// <summary>
        /// Gets the count of PList elements in this element.
        /// </summary>
        /// <returns>The count of PList elements in this element.</returns>
        /// <remarks>Provided for internal use only.</remarks>
        int GetPListElementCount();

        /// <summary>
        /// Gets a value indicating whether this instance is written only once in binary mode.
        /// </summary>
        /// <value>
        /// 	<c>true</c> this instance is written only once in binary mode; otherwise, <c>false</c>.
        /// </value>
        bool IsBinaryUnique { get; }

        /// <summary>
        /// Writes this element binary to the writer.
        /// </summary>
        /// <param name="writer">The <see cref="T:CE.iPhone.PListBinaryWriter"/> to which the element is written.</param>
        /// <remarks>Provided for internal use only.</remarks>
        void WriteBinary(PListBinaryWriter writer);

        /// <summary>
        /// Reads this element binary from the reader.
        /// </summary>
        /// <param name="reader">The <see cref="T:CE.iPhone.PListBinaryReader"/> from which the element is read.</param>
        /// <remarks>Provided for internal use only.</remarks>
        void ReadBinary(PListBinaryReader reader);
    }
}

