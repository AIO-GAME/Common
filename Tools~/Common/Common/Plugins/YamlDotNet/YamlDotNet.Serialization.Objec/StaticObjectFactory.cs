using System;
using System.Collections;

namespace YamlDotNet.Serialization.ObjectFactories
{
	internal abstract class StaticObjectFactory : IObjectFactory
	{
		public abstract object Create(Type type);

		internal abstract Array CreateArray(Type type, int count);

		internal abstract bool IsDictionary(Type type);

		internal abstract bool IsArray(Type type);

		internal abstract bool IsList(Type type);

		internal abstract Type GetKeyType(Type type);

		public abstract Type GetValueType(Type type);

		public virtual object? CreatePrimitive(Type type)
		{
			return Type.GetTypeCode(type) switch
			{
				TypeCode.Boolean => false,
				TypeCode.Byte => (byte)0,
				TypeCode.Int16 => (short)0,
				TypeCode.Int32 => 0,
				TypeCode.Int64 => 0L,
				TypeCode.SByte => (sbyte)0,
				TypeCode.UInt16 => (ushort)0,
				TypeCode.UInt32 => 0u,
				TypeCode.UInt64 => 0uL,
				TypeCode.Single => 0f,
				TypeCode.Double => 0.0,
				TypeCode.Decimal => 0m,
				TypeCode.Char => '\0',
				TypeCode.DateTime => default(DateTime),
				_ => null,
			};
		}

		public bool GetDictionary(IObjectDescriptor descriptor, out IDictionary? dictionary, out Type[]? genericArguments)
		{
			dictionary = null;
			genericArguments = null;
			return false;
		}
	}
}
