#nullable enable
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace AIO.YamlDotNet.Serialization.Utilities
{
	internal static class TypeConverter
	{
		public static T ChangeType<T>(object? value)
		{
			return (T)ChangeType(value, typeof(T));
		}

		public static T ChangeType<T>(object? value, IFormatProvider provider)
		{
			return (T)ChangeType(value, typeof(T), provider);
		}

		public static T ChangeType<T>(object? value, CultureInfo culture)
		{
			return (T)ChangeType(value, typeof(T), culture);
		}

		public static object? ChangeType(object? value, Type destinationType)
		{
			return ChangeType(value, destinationType, CultureInfo.InvariantCulture);
		}

		public static object? ChangeType(object? value, Type destinationType, IFormatProvider provider)
		{
			return ChangeType(value, destinationType, new CultureInfoAdapter(CultureInfo.CurrentCulture, provider));
		}

		public static object? ChangeType(object? value, Type destinationType, CultureInfo culture)
		{
			if (value == null || value.IsDbNull())
			{
				if (!destinationType.IsValueType())
				{
					return null;
				}
				return Activator.CreateInstance(destinationType);
			}
			Type type = value.GetType();
			if (destinationType == type || destinationType.IsAssignableFrom(type))
			{
				return value;
			}
			if (destinationType.IsGenericType() && destinationType.GetGenericTypeDefinition() == typeof(Nullable<>))
			{
				Type destinationType2 = destinationType.GetGenericArguments()[0];
				object obj = ChangeType(value, destinationType2, culture);
				return Activator.CreateInstance(destinationType, obj);
			}
			if (destinationType.IsEnum())
			{
				if (!(value is string value2))
				{
					return value;
				}
				return Enum.Parse(destinationType, value2, ignoreCase: true);
			}
			if (destinationType == typeof(bool))
			{
				if ("0".Equals(value))
				{
					return false;
				}
				if ("1".Equals(value))
				{
					return true;
				}
			}
			System.ComponentModel.TypeConverter converter = TypeDescriptor.GetConverter(type);
			if (converter != null && converter.CanConvertTo(destinationType))
			{
				return converter.ConvertTo(null, culture, value, destinationType);
			}
			System.ComponentModel.TypeConverter converter2 = TypeDescriptor.GetConverter(destinationType);
			if (converter2 != null && converter2.CanConvertFrom(type))
			{
				return converter2.ConvertFrom(null, culture, value);
			}
			Type[] array = new Type[2] { type, destinationType };
			for (int i = 0; i < array.Length; i++)
			{
				foreach (MethodInfo publicStaticMethod2 in array[i].GetPublicStaticMethods())
				{
					if (!publicStaticMethod2.IsSpecialName || (!(publicStaticMethod2.Name == "op_Implicit") && !(publicStaticMethod2.Name == "op_Explicit")) || !destinationType.IsAssignableFrom(publicStaticMethod2.ReturnParameter.ParameterType))
					{
						continue;
					}
					ParameterInfo[] parameters = publicStaticMethod2.GetParameters();
					if (parameters.Length == 1 && parameters[0].ParameterType.IsAssignableFrom(type))
					{
						try
						{
							return publicStaticMethod2.Invoke(null, new object[1] { value });
						}
						catch (TargetInvocationException ex)
						{
							throw ex.Unwrap();
						}
					}
				}
			}
			if (type == typeof(string))
			{
				try
				{
					MethodInfo publicStaticMethod = destinationType.GetPublicStaticMethod("Parse", typeof(string), typeof(IFormatProvider));
					if (publicStaticMethod != null)
					{
						return publicStaticMethod.Invoke(null, new object[2] { value, culture });
					}
					publicStaticMethod = destinationType.GetPublicStaticMethod("Parse", typeof(string));
					if (publicStaticMethod != null)
					{
						return publicStaticMethod.Invoke(null, new object[1] { value });
					}
				}
				catch (TargetInvocationException ex2)
				{
					throw ex2.Unwrap();
				}
			}
			if (destinationType == typeof(TimeSpan))
			{
				return TimeSpan.Parse((string)ChangeType(value, typeof(string), CultureInfo.InvariantCulture));
			}
			return Convert.ChangeType(value, destinationType, CultureInfo.InvariantCulture);
		}

		public static void RegisterTypeConverter<TConvertible, TConverter>() where TConverter : System.ComponentModel.TypeConverter
		{
			if (!TypeDescriptor.GetAttributes(typeof(TConvertible)).OfType<TypeConverterAttribute>().Any((TypeConverterAttribute a) => a.ConverterTypeName == typeof(TConverter).AssemblyQualifiedName))
			{
				TypeDescriptor.AddAttributes(typeof(TConvertible), new TypeConverterAttribute(typeof(TConverter)));
			}
		}
	}
}
