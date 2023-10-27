using System;
using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Serialization.BufferedDeserialization;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization.NodeDeserializers;
using YamlDotNet.Serialization.NodeTypeResolvers;
using YamlDotNet.Serialization.ObjectFactories;
using YamlDotNet.Serialization.Schemas;
using YamlDotNet.Serialization.TypeInspectors;
using YamlDotNet.Serialization.TypeResolvers;
using YamlDotNet.Serialization.Utilities;
using YamlDotNet.Serialization.ValueDeserializers;

namespace YamlDotNet.Serialization
{
	internal sealed class StaticDeserializerBuilder : StaticBuilderSkeleton<StaticDeserializerBuilder>
	{
		private readonly StaticContext context;

		private readonly StaticObjectFactory factory;

		private readonly LazyComponentRegistrationList<Nothing, INodeDeserializer> nodeDeserializerFactories;

		private readonly LazyComponentRegistrationList<Nothing, INodeTypeResolver> nodeTypeResolverFactories;

		private readonly Dictionary<TagName, Type> tagMappings;

		private readonly ITypeConverter typeConverter;

		private readonly Dictionary<Type, Type> typeMappings;

		private bool ignoreUnmatched;

		private bool duplicateKeyChecking;

		private bool attemptUnknownTypeDeserialization;

		protected override StaticDeserializerBuilder Self => this;

		public StaticDeserializerBuilder(StaticContext context)
			: base((ITypeResolver)new StaticTypeResolver())
		{
			this.context = context;
			factory = context.GetFactory();
			typeMappings = new Dictionary<Type, Type>();
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
			nodeDeserializerFactories = new LazyComponentRegistrationList<Nothing, INodeDeserializer>
			{
				{
					typeof(YamlConvertibleNodeDeserializer),
					(Nothing _) => new YamlConvertibleNodeDeserializer(factory)
				},
				{
					typeof(YamlSerializableNodeDeserializer),
					(Nothing _) => new YamlSerializableNodeDeserializer(factory)
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
					typeof(StaticArrayNodeDeserializer),
					(Nothing _) => new StaticArrayNodeDeserializer(factory)
				},
				{
					typeof(StaticDictionaryNodeDeserializer),
					(Nothing _) => new StaticDictionaryNodeDeserializer(factory, duplicateKeyChecking)
				},
				{
					typeof(StaticCollectionNodeDeserializer),
					(Nothing _) => new StaticCollectionNodeDeserializer(factory)
				},
				{
					typeof(ObjectNodeDeserializer),
					(Nothing _) => new ObjectNodeDeserializer(factory, BuildTypeInspector(), ignoreUnmatched, duplicateKeyChecking, typeConverter)
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
			typeConverter = new NullTypeConverter();
		}

		internal ITypeInspector BuildTypeInspector()
		{
			ITypeInspector typeInspector = context.GetTypeInspector();
			return typeInspectorFactories.BuildComponentChain(typeInspector);
		}

		public StaticDeserializerBuilder WithAttemptingUnquotedStringTypeDeserialization()
		{
			attemptUnknownTypeDeserialization = true;
			return this;
		}

		public StaticDeserializerBuilder WithNodeDeserializer(INodeDeserializer nodeDeserializer)
		{
			return WithNodeDeserializer(nodeDeserializer, delegate(IRegistrationLocationSelectionSyntax<INodeDeserializer> w)
			{
				w.OnTop();
			});
		}

		public StaticDeserializerBuilder WithNodeDeserializer(INodeDeserializer nodeDeserializer, Action<IRegistrationLocationSelectionSyntax<INodeDeserializer>> where)
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

		public StaticDeserializerBuilder WithNodeDeserializer<TNodeDeserializer>(WrapperFactory<INodeDeserializer, TNodeDeserializer> nodeDeserializerFactory, Action<ITrackingRegistrationLocationSelectionSyntax<INodeDeserializer>> where) where TNodeDeserializer : INodeDeserializer
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

		public StaticDeserializerBuilder WithoutNodeDeserializer<TNodeDeserializer>() where TNodeDeserializer : INodeDeserializer
		{
			return WithoutNodeDeserializer(typeof(TNodeDeserializer));
		}

		public StaticDeserializerBuilder WithoutNodeDeserializer(Type nodeDeserializerType)
		{
			if (nodeDeserializerType == null)
			{
				throw new ArgumentNullException("nodeDeserializerType");
			}
			nodeDeserializerFactories.Remove(nodeDeserializerType);
			return this;
		}

		public StaticDeserializerBuilder WithTypeDiscriminatingNodeDeserializer(Action<ITypeDiscriminatingNodeDeserializerOptions> configureTypeDiscriminatingNodeDeserializerOptions, int maxDepth = -1, int maxLength = -1)
		{
			TypeDiscriminatingNodeDeserializerOptions typeDiscriminatingNodeDeserializerOptions = new TypeDiscriminatingNodeDeserializerOptions();
			configureTypeDiscriminatingNodeDeserializerOptions(typeDiscriminatingNodeDeserializerOptions);
			TypeDiscriminatingNodeDeserializer nodeDeserializer = new TypeDiscriminatingNodeDeserializer(nodeDeserializerFactories.BuildComponentList(), typeDiscriminatingNodeDeserializerOptions.discriminators, maxDepth, maxLength);
			return WithNodeDeserializer(nodeDeserializer, delegate(IRegistrationLocationSelectionSyntax<INodeDeserializer> s)
			{
				s.Before<DictionaryNodeDeserializer>();
			});
		}

		public StaticDeserializerBuilder WithNodeTypeResolver(INodeTypeResolver nodeTypeResolver)
		{
			return WithNodeTypeResolver(nodeTypeResolver, delegate(IRegistrationLocationSelectionSyntax<INodeTypeResolver> w)
			{
				w.OnTop();
			});
		}

		public StaticDeserializerBuilder WithNodeTypeResolver(INodeTypeResolver nodeTypeResolver, Action<IRegistrationLocationSelectionSyntax<INodeTypeResolver>> where)
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

		public StaticDeserializerBuilder WithNodeTypeResolver<TNodeTypeResolver>(WrapperFactory<INodeTypeResolver, TNodeTypeResolver> nodeTypeResolverFactory, Action<ITrackingRegistrationLocationSelectionSyntax<INodeTypeResolver>> where) where TNodeTypeResolver : INodeTypeResolver
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

		public StaticDeserializerBuilder WithoutNodeTypeResolver<TNodeTypeResolver>() where TNodeTypeResolver : INodeTypeResolver
		{
			return WithoutNodeTypeResolver(typeof(TNodeTypeResolver));
		}

		public StaticDeserializerBuilder WithoutNodeTypeResolver(Type nodeTypeResolverType)
		{
			if (nodeTypeResolverType == null)
			{
				throw new ArgumentNullException("nodeTypeResolverType");
			}
			nodeTypeResolverFactories.Remove(nodeTypeResolverType);
			return this;
		}

		internal override StaticDeserializerBuilder WithTagMapping(TagName tag, Type type)
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

		public StaticDeserializerBuilder WithTypeMapping<TInterface, TConcrete>() where TConcrete : TInterface
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

		public StaticDeserializerBuilder WithoutTagMapping(TagName tag)
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

		public StaticDeserializerBuilder IgnoreUnmatchedProperties()
		{
			ignoreUnmatched = true;
			return this;
		}

		public StaticDeserializerBuilder WithDuplicateKeyChecking()
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
