using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Helpers;
using YamlDotNet.Serialization.Utilities;

namespace YamlDotNet.Serialization.NodeDeserializers
{
	internal sealed class ScalarNodeDeserializer : INodeDeserializer
	{
		private const string BooleanTruePattern = "^(true|y|yes|on)$";

		private const string BooleanFalsePattern = "^(false|n|no|off)$";

		private readonly bool attemptUnknownTypeDeserialization;

		private readonly ITypeConverter typeConverter;

		public ScalarNodeDeserializer(bool attemptUnknownTypeDeserialization, ITypeConverter typeConverter)
		{
			this.attemptUnknownTypeDeserialization = attemptUnknownTypeDeserialization;
			this.typeConverter = typeConverter ?? throw new ArgumentNullException("typeConverter");
		}

		public bool Deserialize(IParser parser, Type expectedType, Func<IParser, Type, object?> nestedObjectDeserializer, out object? value)
		{
			if (!parser.TryConsume<Scalar>(out var @event))
			{
				value = null;
				return false;
			}
			Type type = Nullable.GetUnderlyingType(expectedType) ?? expectedType;
			if (type.IsEnum())
			{
				value = Enum.Parse(type, @event.Value, ignoreCase: true);
				return true;
			}
			TypeCode typeCode = type.GetTypeCode();
			switch (typeCode)
			{
			case TypeCode.Boolean:
				value = DeserializeBooleanHelper(@event.Value);
				break;
			case TypeCode.SByte:
			case TypeCode.Byte:
			case TypeCode.Int16:
			case TypeCode.UInt16:
			case TypeCode.Int32:
			case TypeCode.UInt32:
			case TypeCode.Int64:
			case TypeCode.UInt64:
				value = DeserializeIntegerHelper(typeCode, @event.Value);
				break;
			case TypeCode.Single:
				value = float.Parse(@event.Value, YamlFormatter.NumberFormat);
				break;
			case TypeCode.Double:
				value = double.Parse(@event.Value, YamlFormatter.NumberFormat);
				break;
			case TypeCode.Decimal:
				value = decimal.Parse(@event.Value, YamlFormatter.NumberFormat);
				break;
			case TypeCode.String:
				value = @event.Value;
				break;
			case TypeCode.Char:
				value = @event.Value[0];
				break;
			case TypeCode.DateTime:
				value = DateTime.Parse(@event.Value, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
				break;
			default:
				if (expectedType == typeof(object))
				{
					if (!@event.IsKey && attemptUnknownTypeDeserialization)
					{
						value = AttemptUnknownTypeDeserialization(@event);
					}
					else
					{
						value = @event.Value;
					}
				}
				else
				{
					value = typeConverter.ChangeType(@event.Value, expectedType);
				}
				break;
			}
			return true;
		}

		private object DeserializeBooleanHelper(string value)
		{
			bool flag;
			if (Regex.IsMatch(value, "^(true|y|yes|on)$", RegexOptions.IgnoreCase))
			{
				flag = true;
			}
			else
			{
				if (!Regex.IsMatch(value, "^(false|n|no|off)$", RegexOptions.IgnoreCase))
				{
					throw new FormatException("The value \"" + value + "\" is not a valid YAML Boolean");
				}
				flag = false;
			}
			return flag;
		}

		private static object DeserializeIntegerHelper(TypeCode typeCode, string value)
		{
			StringBuilderPool.BuilderWrapper builderWrapper = StringBuilderPool.Rent();
			try
			{
				StringBuilder builder = builderWrapper.Builder;
				int i = 0;
				bool flag = false;
				ulong num = 0uL;
				if (value[0] == '-')
				{
					i++;
					flag = true;
				}
				else if (value[0] == '+')
				{
					i++;
				}
				if (value[i] == '0')
				{
					int num2;
					if (i == value.Length - 1)
					{
						num2 = 10;
						num = 0uL;
					}
					else
					{
						i++;
						if (value[i] == 'b')
						{
							num2 = 2;
							i++;
						}
						else if (value[i] == 'x')
						{
							num2 = 16;
							i++;
						}
						else
						{
							num2 = 8;
						}
					}
					for (; i < value.Length; i++)
					{
						if (value[i] != '_')
						{
							builder.Append(value[i]);
						}
					}
					switch (num2)
					{
					case 2:
					case 8:
						num = Convert.ToUInt64(builder.ToString(), num2);
						break;
					case 16:
						num = ulong.Parse(builder.ToString(), NumberStyles.HexNumber, YamlFormatter.NumberFormat);
						break;
					}
				}
				else
				{
					string[] array = value.Substring(i).Split(':');
					num = 0uL;
					for (int j = 0; j < array.Length; j++)
					{
						num *= 60;
						num += ulong.Parse(array[j].Replace("_", ""));
					}
				}
				if (!flag)
				{
					return CastInteger(num, typeCode);
				}
				long number = ((num != 9223372036854775808uL) ? checked(-(long)num) : long.MinValue);
				return CastInteger(number, typeCode);
			}
			finally
			{
				((IDisposable)builderWrapper).Dispose();
			}
		}

		private static object CastInteger(long number, TypeCode typeCode)
		{
			return checked(typeCode switch
			{
				TypeCode.Byte => (byte)number,
				TypeCode.Int16 => (short)number,
				TypeCode.Int32 => (int)number,
				TypeCode.Int64 => number,
				TypeCode.SByte => (sbyte)number,
				TypeCode.UInt16 => (ushort)number,
				TypeCode.UInt32 => (uint)number,
				TypeCode.UInt64 => (ulong)number,
				_ => number,
			});
		}

		private static object CastInteger(ulong number, TypeCode typeCode)
		{
			return checked(typeCode switch
			{
				TypeCode.Byte => (byte)number,
				TypeCode.Int16 => (short)number,
				TypeCode.Int32 => (int)number,
				TypeCode.Int64 => (long)number,
				TypeCode.SByte => (sbyte)number,
				TypeCode.UInt16 => (ushort)number,
				TypeCode.UInt32 => (uint)number,
				TypeCode.UInt64 => number,
				_ => number,
			});
		}

		private static object? AttemptUnknownTypeDeserialization(Scalar value)
		{
			if (value.Style == ScalarStyle.SingleQuoted || value.Style == ScalarStyle.DoubleQuoted || value.Style == ScalarStyle.Folded)
			{
				return value.Value;
			}
			string v = value.Value;
			switch (v)
			{
			case "":
			case "~":
			case "null":
			case "Null":
			case "NULL":
				return null;
			case "true":
			case "True":
			case "TRUE":
				return true;
			case "false":
			case "False":
			case "FALSE":
				return false;
			default:
			{
				object value2;
				if (Regex.IsMatch(v, "0x[0-9a-fA-F]+"))
				{
					if (!TryAndSwallow(() => Convert.ToByte(v, 16), out value2) && !TryAndSwallow(() => Convert.ToInt16(v, 16), out value2) && !TryAndSwallow(() => Convert.ToInt32(v, 16), out value2) && !TryAndSwallow(() => Convert.ToInt64(v, 16), out value2) && !TryAndSwallow(() => Convert.ToUInt64(v, 16), out value2))
					{
						return v;
					}
				}
				else if (Regex.IsMatch(v, "0o[0-9a-fA-F]+"))
				{
					if (!TryAndSwallow(() => Convert.ToByte(v, 8), out value2) && !TryAndSwallow(() => Convert.ToInt16(v, 8), out value2) && !TryAndSwallow(() => Convert.ToInt32(v, 8), out value2) && !TryAndSwallow(() => Convert.ToInt64(v, 8), out value2) && !TryAndSwallow(() => Convert.ToUInt64(v, 8), out value2))
					{
						return v;
					}
				}
				else
				{
					if (!Regex.IsMatch(v, "[-+]?(\\.[0-9]+|[0-9]+(\\.[0-9]*)?)([eE][-+]?[0-9]+)?"))
					{
						if (Regex.IsMatch(v, "^[-+]?(\\.inf|\\.Inf|\\.INF)$"))
						{
							if (v.StartsWith("-"))
							{
								return float.NegativeInfinity;
							}
							return float.PositiveInfinity;
						}
						if (Regex.IsMatch(v, "^(\\.nan|\\.NaN|\\.NAN)$"))
						{
							return float.NaN;
						}
						return v;
					}
					if (!TryAndSwallow(() => byte.Parse(v), out value2) && !TryAndSwallow(() => short.Parse(v), out value2) && !TryAndSwallow(() => int.Parse(v), out value2) && !TryAndSwallow(() => long.Parse(v), out value2) && !TryAndSwallow(() => ulong.Parse(v), out value2) && !TryAndSwallow(() => float.Parse(v), out value2) && !TryAndSwallow(() => double.Parse(v), out value2))
					{
						return v;
					}
				}
				return value2;
			}
			}
		}

		private static bool TryAndSwallow(Func<object> attempt, out object? value)
		{
			try
			{
				value = attempt();
				return true;
			}
			catch
			{
				value = null;
				return false;
			}
		}
	}
}
