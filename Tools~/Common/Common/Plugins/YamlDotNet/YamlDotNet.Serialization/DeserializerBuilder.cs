using System;
using System.Collections.Generic;
using AIO.YamlDotNet.Core;
using AIO.YamlDotNet.Serialization.BufferedDeserialization;
using AIO.YamlDotNet.Serialization.NamingConventions;
using AIO.YamlDotNet.Serialization.NodeDeserializers;
using AIO.YamlDotNet.Serialization.NodeTypeResolvers;
using AIO.YamlDotNet.Serialization.ObjectFactories;
using AIO.YamlDotNet.Serialization.Schemas;
using AIO.YamlDotNet.Serialization.TypeInspectors;
using AIO.YamlDotNet.Serialization.TypeResolvers;
using AIO.YamlDotNet.Serialization.Utilities;
using AIO.YamlDotNet.Serialization.ValueDeserializers;

namespace AIO.YamlDotNet.Serialization
{
	internal sealed class DeserializerBuilder : BuilderSkeleton<DeserializerBuilder>
	{
		private Lazy<IObjectFactory> objectFactory;

		private readonly LazyComponentRegistrationList<Nothing, INodeDeserializer> nodeDeserializerFactories;

		private readonly LazyComponentRegistrationList<Nothing, INodeTypeResolver> nodeTypeResolverFactories;

		private readonly Dictionary<TagName, Type> tagMappings;

		private readonly Dictionary<Type, Type> typeMappings;

		private readonly ITypeConverter typeConverter;

		private bool ignoreUnmatched;

		private bool duplicateKeyChecking;

		private bool attemptUnknownTypeDeserialization;

		protected override DeserializerBuilder Self => this;

		public DeserializerBuilder()
			: base((ITypeResolver)new StaticTypeResolver())
		{
			typeMappings = new Dictionary<Type, Type>();
			objectFactory = new Lazy<IObjectFactory>(() => new DefaultObjectFactory(typeMappings, settings), isThreadSafe: true);
			tagMappings = new Dictionary<TagName, Type>
			{
				{
					FailsafeSchema.Tags.Map,
					typeof(Dictionary<object, object>)
				},
				{
					FailsafeSchema.Tags.Str,
					typeof(string)
				},
				{
					JsonSchema.Tags.Bool,
					typeof(bool)
				},
				{
					JsonSchema.Tags.Float,
					typeof(double)
				},
				{
					JsonSchema.Tags.Int,
					typeof(int)
				},
				{
					DefaultSchema.Tags.Timestamp,
					typeof(DateTime)
				}
			};
			typeInspectorFactories.Add(typeof(CachedTypeInspector), (ITypeInspector inner) => new CachedTypeInspector(inner));
			typeInspectorFactories.Add(typeof(NamingConventionTypeInspector), (ITypeInspector inner) => (!(namingConvention is NullNamingConvention)) ? new NamingConventionTypeInspector(inner, namingConvention) : inner);
			typeInspectorFactories.Add(typeof(YamlAttributesTypeInspector), (ITypeInspector inner) => new YamlAttributesTypeInspector(inner));
			typeInspectorFactories.Add(typeof(YamlAttributeOverridesInspector), (ITypeInspector inner) => (overrides == null) ? inner : new YamlAttributeOverridesInspector(inner, overrides.Clone()));
			typeInspectorFactories.Add(typeof(ReadableAndWritablePropertiesTypeInspector), (ITypeInspector inner) => new ReadableAndWritablePropertiesTypeInspector(inner));
			nodeDeserializerFactories = new LazyComponentRegistrationList<Nothing, INodeDeserializer>
			{
				{
					typeof(YamlConvertibleNodeDeserializer),
					(Nothing _) => new YamlConvertibleNodeDeserializer(objectFactory.Value)
				},
				{
					typeof(YamlSerializableNodeDeserializer),
					(Nothing _) => new YamlSerializableNodeDeserializer(objectFactory.Value)
				},
				{
					typeof(TypeConverterNodeDeserializer),
					(Nothing _) => new TypeConverterNodeDeserializer(BuildTypeConverters())
				},
				{
					typeof(NullNodeDeserializer),
					(Nothing _) => new NullNodeDeserializer()
				},
				{
					typeof(ScalarNodeDeserializer),
					(Nothing _) => new ScalarNodeDeserializer(attemptUnknownTypeDeserialization, typeConverter)
				},
				{
					typeof(ArrayNodeDeserializer),
					(Nothing _) => new ArrayNodeDeserializer()
				},
				{
					typeof(DictionaryNodeDeserializer),
					(Nothing _) => new DictionaryNodeDeserializer(objectFactory.Value, duplicateKeyChecking)
				},
				{
					typeof(CollectionNodeDeserializer),
					(Nothing _) => new CollectionNodeDeserializer(objectFactory.Value)
				},
				{
					typeof(EnumerableNodeDeserializer),
					(Nothing _) => new EnumerableNodeDeserializer()
				},
				{
					typeof(ObjectNodeDeserializer),
					(Nothing _) => new ObjectNodeDeserializer(objectFactory.Value, BuildTypeInspector(), ignoreUnmatched, duplicateKeyChecking, typeConverter)
				}
			};
			nodeTypeResolverFactories = new LazyComponentRegistrationList<Nothing, INodeTypeResolver>
			{
				{
					typeof(MappingNodeTypeResolver),
					(Nothing _) => new MappingNodeTypeResolver(typeMappings)
				},
				{
					typeof(YamlConvertibleTypeResolver),
					(Nothing _) => new YamlConvertibleTypeResolver()
				},
				{
					typeof(YamlSerializableTypeResolver),
					(Nothing _) => new YamlSerializableTypeResolver()
				},
				{
					typeof(TagNodeTypeResolver),
					(Nothing _) => new TagNodeTypeResolver(tagMappings)
				},
				{
					typeof(PreventUnknownTagsNodeTypeResolver),
					(Nothing _) => new PreventUnknownTagsNodeTypeResolver()
				},
				{
					typeof(DefaultContainersNodeTypeResolver),
					(Nothing _) => new DefaultContainersNodeTypeResolver()
				}
			};
			typeConverter = new ReflectionTypeConverter();
		}

		internal ITypeInspector BuildTypeInspector()
		{
			ITypeInspector typeInspector = new WritablePropertiesTypeInspector(typeResolver, includeNonPublicProperties);
			if (!ignoreFields)
			{
				typeInspector = new CompositeTypeInspector(new ReadableFieldsTypeInspector(typeResolver), typeInspector);
			}
			return typeInspectorFactories.BuildComponentChain(typeInspector);
		}

		public DeserializerBuilder WithAttemptingUnquotedStringTypeDeserialization()
		{
			attemptUnknownTypeDeserialization = true;
			return this;
		}

		public DeserializerBuilder WithObjectFactory(IObjectFactory objectFactory)
		{
			IObjectFactory objectFactory2 = objectFactory;
			if (objectFactory2 == null)
			{
				throw new ArgumentNullException("objectFactory");
			}
			this.objectFactory = new Lazy<IObjectFactory>(() => objectFactory2, isThreadSafe: true);
			return this;
		}

		public DeserializerBuilder WithObjectFactory(Func<Type, object> objectFactory)
		{
			if (objectFactory == null)
			{
				throw new ArgumentNullException("objectFactory");
			}
			return WithObjectFactory(new LambdaObjectFactory(objectFactory));
		}

		public DeserializerBuilder WithNodeDeserializer(INodeDeserializer nodeDeserializer)
		{
			return WithNodeDeserializer(nodeDeserializer, delegate(IRegistrationLocationSelectionSyntax<INodeDeserializer> w)
			{
				w.OnTop();
			});
		}

		public DeserializerBuilder WithNodeDeserializer(INodeDeserializer nodeDeserializer, Action<IRegistrationLocationSelectionSyntax<INodeDeserializer>> where)
		{
			INodeDeserializer nodeDeserializer2 = nodeDeserializer;
			if (nodeDeserializer2 == null)
			{
				throw new ArgumentNullException("nodeDeserializer");
			}
			if (where == null)
			{
				throw new ArgumentNullException("where");
			}
			where(nodeDeserializerFactories.CreateRegistrationLocationSelector(nodeDeserializer2.GetType(), (Nothing _) => nodeDeserializer2));
			return this;
		}

		public DeserializerBuilder WithNodeDeserializer<TNodeDeserializer>(WrapperFactory<INodeDeserializer, TNodeDeserializer> nodeDeserializerFactory, Action<ITrackingRegistrationLocationSelectionSyntax<INodeDeserializer>> where) where TNodeDeserializer : INodeDeserializer
		{
			WrapperFactory<INodeDeserializer, TNodeDeserializer> nodeDeserializerFactory2 = nodeDeserializerFactory;
			if (nodeDeserializerFactory2 == null)
			{
				throw new ArgumentNullException("nodeDeserializerFactory");
			}
			if (where == null)
			{
				throw new ArgumentNullException("where");
			}
			where(nodeDeserializerFactories.CreateTrackingRegistrationLocationSelector(typeof(TNodeDeserializer), (INodeDeserializer wrapped, Nothing _) => nodeDeserializerFactory2(wrapped)));
			return this;
		}

		public DeserializerBuilder WithoutNodeDeserializer<TNodeDeserializer>() where TNodeDeserializer : INodeDeserializer
		{
			return WithoutNodeDeserializer(typeof(TNodeDeserializer));
		}

		public DeserializerBuilder WithoutNodeDeserializer(Type nodeDeserializerType)
		{
			if (nodeDeserializerType == null)
			{
				throw new ArgumentNullException("nodeDeserializerType");
			}
			nodeDeserializerFactories.Remove(nodeDeserializerType);
			return this;
		}

		public DeserializerBuilder WithTypeDiscriminatingNodeDeserializer(Action<ITypeDiscriminatingNodeDeserializerOptions> configureTypeDiscriminatingNodeDeserializerOptions, int maxDepth = -1, int maxLength = -1)
		{
			TypeDiscriminatingNodeDeserializerOptions typeDiscriminatingNodeDeserializerOptions = new TypeDiscriminatingNodeDeserializerOptions();
			configureTypeDiscriminatingNodeDeserializerOptions(typeDiscriminatingNodeDeserializerOptions);
			TypeDiscriminatingNodeDeserializer nodeDeserializer = new TypeDiscriminatingNodeDeserializer(nodeDeserializerFactories.BuildComponentList(), typeDiscriminatingNodeDeserializerOptions.discriminators, maxDepth, maxLength);
			return WithNodeDeserializer(nodeDeserializer, delegate(IRegistrationLocationSelectionSyntax<INodeDeserializer> s)
			{
				s.Before<DictionaryNodeDeserializer>();
			});
		}

		public DeserializerBuilder WithNodeTypeResolver(INodeTypeResolver nodeTypeResolver)
		{
			return WithNodeTypeResolver(nodeTypeResolver, delegate(IRegistrationLocationSelectionSyntax<INodeTypeResolver> w)
			{
				w.OnTop();
			});
		}

		public DeserializerBuilder WithNodeTypeResolver(INodeTypeResolver nodeTypeResolver, Action<IRegistrationLocationSelectionSyntax<INodeTypeResolver>> where)
		{
			INodeTypeResolver nodeTypeResolver2 = nodeTypeResolver;
			if (nodeTypeResolver2 == null)
			{
				throw new ArgumentNullException("nodeTypeResolver");
			}
			if (where == null)
			{
				throw new ArgumentNullException("where");
			}
			where(nodeTypeResolverFactories.CreateRegistrationLocationSelector(nodeTypeResolver2.GetType(), (Nothing _) => nodeTypeResolver2));
			return this;
		}

		public DeserializerBuilder WithNodeTypeResolver<TNodeTypeResolver>(WrapperFactory<INodeTypeResolver, TNodeTypeResolver> nodeTypeResolverFactory, Action<ITrackingRegistrationLocationSelectionSyntax<INodeTypeResolver>> where) where TNodeTypeResolver : INodeTypeResolver
		{
			WrapperFactory<INodeTypeResolver, TNodeTypeResolver> nodeTypeResolverFactory2 = nodeTypeResolverFactory;
			if (nodeTypeResolverFactory2 == null)
			{
				throw new ArgumentNullException("nodeTypeResolverFactory");
			}
			if (where == null)
			{
				throw new ArgumentNullException("where");
			}
			where(nodeTypeResolverFactories.CreateTrackingRegistrationLocationSelector(typeof(TNodeTypeResolver), (INodeTypeResolver wrapped, Nothing _) => nodeTypeResolverFactory2(wrapped)));
			return this;
		}

		public DeserializerBuilder WithoutNodeTypeResolver<TNodeTypeResolver>() where TNodeTypeResolver : INodeTypeResolver
		{
			return WithoutNodeTypeResolver(typeof(TNodeTypeResolver));
		}

		public DeserializerBuilder WithoutNodeTypeResolver(Type nodeTypeResolverType)
		{
			if (nodeTypeResolverType == null)
			{
				throw new ArgumentNullException("nodeTypeResolverType");
			}
			nodeTypeResolverFactories.Remove(nodeTypeResolverType);
			return this;
		}

		internal override DeserializerBuilder WithTagMapping(TagName tag, Type type)
		{
			if (tag.IsEmpty)
			{
				throw new ArgumentException("Non-specific tags cannot be maped");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (tagMappings.TryGetValue(tag, out var value))
			{
				throw new ArgumentException($"Type already has a registered type '{value.FullName}' for tag '{tag}'", "tag");
			}
			tagMappings.Add(tag, type);
			return this;
		}

		public DeserializerBuilder WithTypeMapping<TInterface, TConcrete>() where TConcrete : TInterface
		{
			Type typeFromHandle = typeof(TInterface);
			Type typeFromHandle2 = typeof(TConcrete);
			if (!typeFromHandle.IsAssignableFrom(typeFromHandle2))
			{
				throw new InvalidOperationException("The type '" + typeFromHandle2.Name + "' does not implement interface '" + typeFromHandle.Name + "'.");
			}
			if (typeMappings.ContainsKey(typeFromHandle))
			{
				typeMappings[typeFromHandle] = typeFromHandle2;
			}
			else
			{
				typeMappings.Add(typeFromHandle, typeFromHandle2);
			}
			return this;
		}

		public DeserializerBuilder WithoutTagMapping(TagName tag)
		{
			if (tag.IsEmpty)
			{
				throw new ArgumentException("Non-specific tags cannot be maped");
			}
			if (!tagMappings.Remove(tag))
			{
				throw new KeyNotFoundException($"Tag '{tag}' is not registered");
			}
			return this;
		}

		public DeserializerBuilder IgnoreUnmatchedProperties()
		{
			ignoreUnmatched = true;
			return this;
		}

		public DeserializerBuilder WithDuplicateKeyChecking()
		{
			duplicateKeyChecking = true;
			return this;
		}

		public IDeserializer Build()
		{
			return Deserializer.FromValueDeserializer(BuildValueDeserializer());
		}

		public IValueDeserializer BuildValueDeserializer()
		{
			return new AliasValueDeserializer(new NodeValueDeserializer(nodeDeserializerFactories.BuildComponentList(), nodeTypeResolverFactories.BuildComponentList(), typeConverter));
		}
	}
}
