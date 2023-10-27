using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using YamlDotNet.Core;

namespace YamlDotNet.Serialization.ObjectGraphTraversalStrategies
{
	internal class FullObjectGraphTraversalStrategy : IObjectGraphTraversalStrategy
	{
		protected struct ObjectPathSegment
		{
			public readonly object Name;

			public readonly IObjectDescriptor Value;

			public ObjectPathSegment(object name, IObjectDescriptor value)
			{
				Name = name;
				Value = value;
			}
		}

		private readonly int maxRecursion;

		private readonly ITypeInspector typeDescriptor;

		private readonly ITypeResolver typeResolver;

		private readonly INamingConvention namingConvention;

		private readonly IObjectFactory objectFactory;

		public FullObjectGraphTraversalStrategy(ITypeInspector typeDescriptor, ITypeResolver typeResolver, int maxRecursion, INamingConvention namingConvention, IObjectFactory objectFactory)
		{
			if (maxRecursion <= 0)
			{
				throw new ArgumentOutOfRangeException("maxRecursion", maxRecursion, "maxRecursion must be greater than 1");
			}
			this.typeDescriptor = typeDescriptor ?? throw new ArgumentNullException("typeDescriptor");
			this.typeResolver = typeResolver ?? throw new ArgumentNullException("typeResolver");
			this.maxRecursion = maxRecursion;
			this.namingConvention = namingConvention ?? throw new ArgumentNullException("namingConvention");
			this.objectFactory = objectFactory ?? throw new ArgumentNullException("objectFactory");
		}

		void IObjectGraphTraversalStrategy.Traverse<TContext>(IObjectDescriptor graph, IObjectGraphVisitor<TContext> visitor, TContext context)
		{
			Traverse("<root>", graph, visitor, context, new Stack<ObjectPathSegment>(maxRecursion));
		}

		protected virtual void Traverse<TContext>(object name, IObjectDescriptor value, IObjectGraphVisitor<TContext> visitor, TContext context, Stack<ObjectPathSegment> path)
		{
			if (path.Count >= maxRecursion)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendLine("Too much recursion when traversing the object graph.");
				stringBuilder.AppendLine("The path to reach this recursion was:");
				Stack<KeyValuePair<string, string>> stack = new Stack<KeyValuePair<string, string>>(path.Count);
				int num = 0;
				foreach (ObjectPathSegment item in path)
				{
					string text = item.Name?.ToString() ?? string.Empty;
					num = Math.Max(num, text.Length);
					stack.Push(new KeyValuePair<string, string>(text, item.Value.Type.FullName));
				}
				foreach (KeyValuePair<string, string> item2 in stack)
				{
					stringBuilder.Append(" -> ").Append(item2.Key.PadRight(num)).Append("  [")
						.Append(item2.Value)
						.AppendLine("]");
				}
				throw new MaximumRecursionLevelReachedException(stringBuilder.ToString());
			}
			if (!visitor.Enter(value, context))
			{
				return;
			}
			path.Push(new ObjectPathSegment(name, value));
			try
			{
				TypeCode typeCode = value.Type.GetTypeCode();
				switch (typeCode)
				{
				case TypeCode.Boolean:
				case TypeCode.Char:
				case TypeCode.SByte:
				case TypeCode.Byte:
				case TypeCode.Int16:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Int64:
				case TypeCode.UInt64:
				case TypeCode.Single:
				case TypeCode.Double:
				case TypeCode.Decimal:
				case TypeCode.DateTime:
				case TypeCode.String:
					visitor.VisitScalar(value, context);
					return;
				case TypeCode.Empty:
					throw new NotSupportedException($"TypeCode.{typeCode} is not supported.");
				}
				if (value.IsDbNull())
				{
					visitor.VisitScalar(new ObjectDescriptor(null, typeof(object), typeof(object)), context);
				}
				if (value.Value == null || value.Type == typeof(TimeSpan))
				{
					visitor.VisitScalar(value, context);
					return;
				}
				Type underlyingType = Nullable.GetUnderlyingType(value.Type);
				if (underlyingType != null)
				{
					Traverse("Value", new ObjectDescriptor(value.Value, underlyingType, value.Type, value.ScalarStyle), visitor, context, path);
				}
				else
				{
					TraverseObject(value, visitor, context, path);
				}
			}
			finally
			{
				path.Pop();
			}
		}

		protected virtual void TraverseObject<TContext>(IObjectDescriptor value, IObjectGraphVisitor<TContext> visitor, TContext context, Stack<ObjectPathSegment> path)
		{
			IDictionary dictionary;
			Type[] genericArguments;
			if (typeof(IDictionary).IsAssignableFrom(value.Type))
			{
				TraverseDictionary(value, visitor, typeof(object), typeof(object), context, path);
			}
			else if (objectFactory.GetDictionary(value, out dictionary, out genericArguments))
			{
				TraverseDictionary(new ObjectDescriptor(dictionary, value.Type, value.StaticType, value.ScalarStyle), visitor, genericArguments[0], genericArguments[1], context, path);
			}
			else if (typeof(IEnumerable).IsAssignableFrom(value.Type))
			{
				TraverseList(value, visitor, context, path);
			}
			else
			{
				TraverseProperties(value, visitor, context, path);
			}
		}

		protected virtual void TraverseDictionary<TContext>(IObjectDescriptor dictionary, IObjectGraphVisitor<TContext> visitor, Type keyType, Type valueType, TContext context, Stack<ObjectPathSegment> path)
		{
			visitor.VisitMappingStart(dictionary, keyType, valueType, context);
			bool flag = dictionary.Type.FullName.Equals("System.Dynamic.ExpandoObject");
			foreach (DictionaryEntry? item in (IDictionary)dictionary.NonNullValue())
			{
				DictionaryEntry value = item.Value;
				object obj = (flag ? namingConvention.Apply(value.Key.ToString()) : value.Key);
				IObjectDescriptor objectDescriptor = GetObjectDescriptor(obj, keyType);
				IObjectDescriptor objectDescriptor2 = GetObjectDescriptor(value.Value, valueType);
				if (visitor.EnterMapping(objectDescriptor, objectDescriptor2, context))
				{
					Traverse(obj, objectDescriptor, visitor, context, path);
					Traverse(obj, objectDescriptor2, visitor, context, path);
				}
			}
			visitor.VisitMappingEnd(dictionary, context);
		}

		private void TraverseList<TContext>(IObjectDescriptor value, IObjectGraphVisitor<TContext> visitor, TContext context, Stack<ObjectPathSegment> path)
		{
			Type valueType = objectFactory.GetValueType(value.Type);
			visitor.VisitSequenceStart(value, valueType, context);
			int num = 0;
			foreach (object item in (IEnumerable)value.NonNullValue())
			{
				Traverse(num, GetObjectDescriptor(item, valueType), visitor, context, path);
				num++;
			}
			visitor.VisitSequenceEnd(value, context);
		}

		protected virtual void TraverseProperties<TContext>(IObjectDescriptor value, IObjectGraphVisitor<TContext> visitor, TContext context, Stack<ObjectPathSegment> path)
		{
			visitor.VisitMappingStart(value, typeof(string), typeof(object), context);
			object obj = value.NonNullValue();
			foreach (IPropertyDescriptor property in typeDescriptor.GetProperties(value.Type, obj))
			{
				IObjectDescriptor value2 = property.Read(obj);
				if (visitor.EnterMapping(property, value2, context))
				{
					Traverse(property.Name, new ObjectDescriptor(property.Name, typeof(string), typeof(string), ScalarStyle.Plain), visitor, context, path);
					Traverse(property.Name, value2, visitor, context, path);
				}
			}
			visitor.VisitMappingEnd(value, context);
		}

		private IObjectDescriptor GetObjectDescriptor(object? value, Type staticType)
		{
			return new ObjectDescriptor(value, typeResolver.Resolve(staticType, value), staticType);
		}
	}
}
