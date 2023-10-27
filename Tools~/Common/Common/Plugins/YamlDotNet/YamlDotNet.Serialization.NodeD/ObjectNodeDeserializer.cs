using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using AIO.YamlDotNet.Core;
using AIO.YamlDotNet.Core.Events;
using AIO.YamlDotNet.Serialization.Utilities;

#nullable enable
namespace AIO.YamlDotNet.Serialization.NodeDeserializers
{
	internal sealed class ObjectNodeDeserializer : INodeDeserializer
	{
		private readonly IObjectFactory objectFactory;

		private readonly ITypeInspector typeDescriptor;

		private readonly bool ignoreUnmatched;

		private readonly bool duplicateKeyChecking;

		private readonly ITypeConverter typeConverter;

		public ObjectNodeDeserializer(IObjectFactory objectFactory, ITypeInspector typeDescriptor, bool ignoreUnmatched, bool duplicateKeyChecking, ITypeConverter typeConverter)
		{
			this.objectFactory = objectFactory ?? throw new ArgumentNullException("objectFactory");
			this.typeDescriptor = typeDescriptor ?? throw new ArgumentNullException("typeDescriptor");
			this.ignoreUnmatched = ignoreUnmatched;
			this.duplicateKeyChecking = duplicateKeyChecking;
			this.typeConverter = typeConverter ?? throw new ArgumentNullException("typeConverter");
		}

		public bool Deserialize(IParser parser, Type expectedType, Func<IParser, Type, object?> nestedObjectDeserializer, out object? value)
		{
			if (!parser.TryConsume<MappingStart>(out var _))
			{
				value = null;
				return false;
			}
			Type type = Nullable.GetUnderlyingType(expectedType) ?? expectedType;
			value = objectFactory.Create(type);
			HashSet<string> hashSet = new HashSet<string>(StringComparer.Ordinal);
			MappingEnd event2;
			IPropertyDescriptor property;
			object valueRef;
			while (!parser.TryConsume<MappingEnd>(out event2))
			{
				Scalar scalar = parser.Consume<Scalar>();
				if (duplicateKeyChecking && !hashSet.Add(scalar.Value))
				{
					Mark start = scalar.Start;
					Mark end = scalar.End;
					throw new YamlException(in start, in end, "Encountered duplicate key " + scalar.Value);
				}
				try
				{
					property = typeDescriptor.GetProperty(type, null, scalar.Value, ignoreUnmatched);
					if (property == null)
					{
						parser.SkipThisAndNestedEvents();
						continue;
					}
					object obj = nestedObjectDeserializer(parser, property.Type);
					if (obj is IValuePromise valuePromise)
					{
						valueRef = value;
						valuePromise.ValueAvailable += delegate(object? v)
						{
							object value3 = typeConverter.ChangeType(v, property.Type);
							property.Write(valueRef, value3);
						};
					}
					else
					{
						object value2 = typeConverter.ChangeType(obj, property.Type);
						property.Write(value, value2);
					}
				}
				catch (SerializationException ex)
				{
					Mark start = scalar.Start;
					Mark end = scalar.End;
					throw new YamlException(in start, in end, ex.Message);
				}
				catch (YamlException)
				{
					throw;
				}
				catch (Exception innerException)
				{
					Mark start = scalar.Start;
					Mark end = scalar.End;
					throw new YamlException(in start, in end, "Exception during deserialization", innerException);
				}
			}
			return true;
		}
	}
}
