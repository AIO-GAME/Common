using System;
using System.Collections.Generic;
using AIO.YamlDotNet.Core;
using AIO.YamlDotNet.Serialization.Converters;
using AIO.YamlDotNet.Serialization.EventEmitters;
using AIO.YamlDotNet.Serialization.NamingConventions;
using AIO.YamlDotNet.Serialization.ObjectFactories;
using AIO.YamlDotNet.Serialization.ObjectGraphTraversalStrategies;
using AIO.YamlDotNet.Serialization.ObjectGraphVisitors;
using AIO.YamlDotNet.Serialization.TypeInspectors;
using AIO.YamlDotNet.Serialization.TypeResolvers;

namespace AIO.YamlDotNet.Serialization
{
	internal sealed class SerializerBuilder : BuilderSkeleton<SerializerBuilder>
	{
		private class ValueSerializer : IValueSerializer
		{
			private readonly IObjectGraphTraversalStrategy traversalStrategy;

			private readonly IEventEmitter eventEmitter;

			private readonly IEnumerable<IYamlTypeConverter> typeConverters;

			private readonly LazyComponentRegistrationList<IEnumerable<IYamlTypeConverter>, IObjectGraphVisitor<Nothing>> preProcessingPhaseObjectGraphVisitorFactories;

			private readonly LazyComponentRegistrationList<EmissionPhaseObjectGraphVisitorArgs, IObjectGraphVisitor<IEmitter>> emissionPhaseObjectGraphVisitorFactories;

			public ValueSerializer(IObjectGraphTraversalStrategy traversalStrategy, IEventEmitter eventEmitter, IEnumerable<IYamlTypeConverter> typeConverters, LazyComponentRegistrationList<IEnumerable<IYamlTypeConverter>, IObjectGraphVisitor<Nothing>> preProcessingPhaseObjectGraphVisitorFactories, LazyComponentRegistrationList<EmissionPhaseObjectGraphVisitorArgs, IObjectGraphVisitor<IEmitter>> emissionPhaseObjectGraphVisitorFactories)
			{
				this.traversalStrategy = traversalStrategy;
				this.eventEmitter = eventEmitter;
				this.typeConverters = typeConverters;
				this.preProcessingPhaseObjectGraphVisitorFactories = preProcessingPhaseObjectGraphVisitorFactories;
				this.emissionPhaseObjectGraphVisitorFactories = emissionPhaseObjectGraphVisitorFactories;
			}

			public void SerializeValue(IEmitter emitter, object? value, Type? type)
			{
				IEmitter emitter2 = emitter;
				Type type2 = type ?? ((value != null) ? value.GetType() : typeof(object));
				Type staticType = type ?? typeof(object);
				ObjectDescriptor graph = new ObjectDescriptor(value, type2, staticType);
				List<IObjectGraphVisitor<Nothing>> preProcessingPhaseObjectGraphVisitors = preProcessingPhaseObjectGraphVisitorFactories.BuildComponentList(typeConverters);
				foreach (IObjectGraphVisitor<Nothing> item in preProcessingPhaseObjectGraphVisitors)
				{
					traversalStrategy.Traverse(graph, item, default(Nothing));
				}
				IObjectGraphVisitor<IEmitter> visitor = emissionPhaseObjectGraphVisitorFactories.BuildComponentChain<EmissionPhaseObjectGraphVisitorArgs, IObjectGraphVisitor<IEmitter>>(new EmittingObjectGraphVisitor(eventEmitter), (IObjectGraphVisitor<IEmitter> inner) => new EmissionPhaseObjectGraphVisitorArgs(inner, eventEmitter, preProcessingPhaseObjectGraphVisitors, typeConverters, NestedObjectSerializer));
				traversalStrategy.Traverse(graph, visitor, emitter2);
				void NestedObjectSerializer(object? v, Type? t)
				{
					SerializeValue(emitter2, v, t);
				}
			}
		}

		private ObjectGraphTraversalStrategyFactory objectGraphTraversalStrategyFactory;

		private readonly LazyComponentRegistrationList<IEnumerable<IYamlTypeConverter>, IObjectGraphVisitor<Nothing>> preProcessingPhaseObjectGraphVisitorFactories;

		private readonly LazyComponentRegistrationList<EmissionPhaseObjectGraphVisitorArgs, IObjectGraphVisitor<IEmitter>> emissionPhaseObjectGraphVisitorFactories;

		private readonly LazyComponentRegistrationList<IEventEmitter, IEventEmitter> eventEmitterFactories;

		private readonly IDictionary<Type, TagName> tagMappings = new Dictionary<Type, TagName>();

		private readonly IObjectFactory objectFactory;

		private int maximumRecursion = 50;

		private EmitterSettings emitterSettings = EmitterSettings.Default;

		private DefaultValuesHandling defaultValuesHandlingConfiguration;

		private bool quoteNecessaryStrings;

		private bool quoteYaml1_1Strings;

		protected override SerializerBuilder Self => this;

		public SerializerBuilder()
			: base((ITypeResolver)new DynamicTypeResolver())
		{
			typeInspectorFactories.Add(typeof(CachedTypeInspector), (ITypeInspector inner) => new CachedTypeInspector(inner));
			typeInspectorFactories.Add(typeof(NamingConventionTypeInspector), (ITypeInspector inner) => (!(namingConvention is NullNamingConvention)) ? new NamingConventionTypeInspector(inner, namingConvention) : inner);
			typeInspectorFactories.Add(typeof(YamlAttributesTypeInspector), (ITypeInspector inner) => new YamlAttributesTypeInspector(inner));
			typeInspectorFactories.Add(typeof(YamlAttributeOverridesInspector), (ITypeInspector inner) => (overrides == null) ? inner : new YamlAttributeOverridesInspector(inner, overrides.Clone()));
			preProcessingPhaseObjectGraphVisitorFactories = new LazyComponentRegistrationList<IEnumerable<IYamlTypeConverter>, IObjectGraphVisitor<Nothing>> {
			{
				typeof(AnchorAssigner),
				(IEnumerable<IYamlTypeConverter> typeConverters) => new AnchorAssigner(typeConverters)
			} };
			emissionPhaseObjectGraphVisitorFactories = new LazyComponentRegistrationList<EmissionPhaseObjectGraphVisitorArgs, IObjectGraphVisitor<IEmitter>>
			{
				{
					typeof(CustomSerializationObjectGraphVisitor),
					(EmissionPhaseObjectGraphVisitorArgs args) => new CustomSerializationObjectGraphVisitor(args.InnerVisitor, args.TypeConverters, args.NestedObjectSerializer)
				},
				{
					typeof(AnchorAssigningObjectGraphVisitor),
					(EmissionPhaseObjectGraphVisitorArgs args) => new AnchorAssigningObjectGraphVisitor(args.InnerVisitor, args.EventEmitter, args.GetPreProcessingPhaseObjectGraphVisitor<AnchorAssigner>())
				},
				{
					typeof(DefaultValuesObjectGraphVisitor),
					(EmissionPhaseObjectGraphVisitorArgs args) => new DefaultValuesObjectGraphVisitor(defaultValuesHandlingConfiguration, args.InnerVisitor, new DefaultObjectFactory())
				},
				{
					typeof(CommentsObjectGraphVisitor),
					(EmissionPhaseObjectGraphVisitorArgs args) => new CommentsObjectGraphVisitor(args.InnerVisitor)
				}
			};
			eventEmitterFactories = new LazyComponentRegistrationList<IEventEmitter, IEventEmitter> {
			{
				typeof(TypeAssigningEventEmitter),
				(IEventEmitter inner) => new TypeAssigningEventEmitter(inner, requireTagWhenStaticAndActualTypesAreDifferent: false, tagMappings, quoteNecessaryStrings, quoteYaml1_1Strings)
			} };
			objectFactory = new DefaultObjectFactory();
			objectGraphTraversalStrategyFactory = (ITypeInspector typeInspector, ITypeResolver typeResolver, IEnumerable<IYamlTypeConverter> typeConverters, int maximumRecursion) => new FullObjectGraphTraversalStrategy(typeInspector, typeResolver, maximumRecursion, namingConvention, objectFactory);
		}

		public SerializerBuilder WithQuotingNecessaryStrings(bool quoteYaml1_1Strings = false)
		{
			quoteNecessaryStrings = true;
			this.quoteYaml1_1Strings = quoteYaml1_1Strings;
			return this;
		}

		public SerializerBuilder WithMaximumRecursion(int maximumRecursion)
		{
			if (maximumRecursion <= 0)
			{
				throw new ArgumentOutOfRangeException("maximumRecursion", $"The maximum recursion specified ({maximumRecursion}) is invalid. It should be a positive integer.");
			}
			this.maximumRecursion = maximumRecursion;
			return this;
		}

		public SerializerBuilder WithEventEmitter<TEventEmitter>(Func<IEventEmitter, TEventEmitter> eventEmitterFactory) where TEventEmitter : IEventEmitter
		{
			return WithEventEmitter(eventEmitterFactory, delegate(IRegistrationLocationSelectionSyntax<IEventEmitter> w)
			{
				w.OnTop();
			});
		}

		public SerializerBuilder WithEventEmitter<TEventEmitter>(Func<IEventEmitter, TEventEmitter> eventEmitterFactory, Action<IRegistrationLocationSelectionSyntax<IEventEmitter>> where) where TEventEmitter : IEventEmitter
		{
			Func<IEventEmitter, TEventEmitter> eventEmitterFactory2 = eventEmitterFactory;
			if (eventEmitterFactory2 == null)
			{
				throw new ArgumentNullException("eventEmitterFactory");
			}
			if (where == null)
			{
				throw new ArgumentNullException("where");
			}
			where(eventEmitterFactories.CreateRegistrationLocationSelector(typeof(TEventEmitter), (IEventEmitter inner) => eventEmitterFactory2(inner)));
			return Self;
		}

		public SerializerBuilder WithEventEmitter<TEventEmitter>(WrapperFactory<IEventEmitter, IEventEmitter, TEventEmitter> eventEmitterFactory, Action<ITrackingRegistrationLocationSelectionSyntax<IEventEmitter>> where) where TEventEmitter : IEventEmitter
		{
			WrapperFactory<IEventEmitter, IEventEmitter, TEventEmitter> eventEmitterFactory2 = eventEmitterFactory;
			if (eventEmitterFactory2 == null)
			{
				throw new ArgumentNullException("eventEmitterFactory");
			}
			if (where == null)
			{
				throw new ArgumentNullException("where");
			}
			where(eventEmitterFactories.CreateTrackingRegistrationLocationSelector(typeof(TEventEmitter), (IEventEmitter wrapped, IEventEmitter inner) => eventEmitterFactory2(wrapped, inner)));
			return Self;
		}

		public SerializerBuilder WithoutEventEmitter<TEventEmitter>() where TEventEmitter : IEventEmitter
		{
			return WithoutEventEmitter(typeof(TEventEmitter));
		}

		public SerializerBuilder WithoutEventEmitter(Type eventEmitterType)
		{
			if (eventEmitterType == null)
			{
				throw new ArgumentNullException("eventEmitterType");
			}
			eventEmitterFactories.Remove(eventEmitterType);
			return this;
		}

		internal override SerializerBuilder WithTagMapping(TagName tag, Type type)
		{
			if (tag.IsEmpty)
			{
				throw new ArgumentException("Non-specific tags cannot be maped");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (tagMappings.TryGetValue(type, out var value))
			{
				throw new ArgumentException($"Type already has a registered tag '{value}' for type '{type.FullName}'", "type");
			}
			tagMappings.Add(type, tag);
			return this;
		}

		public SerializerBuilder WithoutTagMapping(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!tagMappings.Remove(type))
			{
				throw new KeyNotFoundException("Tag for type '" + type.FullName + "' is not registered");
			}
			return this;
		}

		public SerializerBuilder EnsureRoundtrip()
		{
			objectGraphTraversalStrategyFactory = (ITypeInspector typeInspector, ITypeResolver typeResolver, IEnumerable<IYamlTypeConverter> typeConverters, int maximumRecursion) => new RoundtripObjectGraphTraversalStrategy(typeConverters, typeInspector, typeResolver, maximumRecursion, namingConvention, settings, objectFactory);
			WithEventEmitter((IEventEmitter inner) => new TypeAssigningEventEmitter(inner, requireTagWhenStaticAndActualTypesAreDifferent: true, tagMappings, quoteNecessaryStrings, quoteYaml1_1Strings), delegate(IRegistrationLocationSelectionSyntax<IEventEmitter> loc)
			{
				loc.InsteadOf<TypeAssigningEventEmitter>();
			});
			return WithTypeInspector((ITypeInspector inner) => new ReadableAndWritablePropertiesTypeInspector(inner), delegate(IRegistrationLocationSelectionSyntax<ITypeInspector> loc)
			{
				loc.OnBottom();
			});
		}

		public SerializerBuilder DisableAliases()
		{
			preProcessingPhaseObjectGraphVisitorFactories.Remove(typeof(AnchorAssigner));
			emissionPhaseObjectGraphVisitorFactories.Remove(typeof(AnchorAssigningObjectGraphVisitor));
			return this;
		}

		[Obsolete("The default behavior is now to always emit default values, thefore calling this method has no effect. This behavior is now controlled by ConfigureDefaultValuesHandling.", true)]
		public SerializerBuilder EmitDefaults()
		{
			return ConfigureDefaultValuesHandling(DefaultValuesHandling.Preserve);
		}

		public SerializerBuilder ConfigureDefaultValuesHandling(DefaultValuesHandling configuration)
		{
			defaultValuesHandlingConfiguration = configuration;
			return this;
		}

		public SerializerBuilder JsonCompatible()
		{
			emitterSettings = emitterSettings.WithMaxSimpleKeyLength(int.MaxValue).WithoutAnchorName();
			return WithTypeConverter(new GuidConverter(jsonCompatible: true), delegate(IRegistrationLocationSelectionSyntax<IYamlTypeConverter> w)
			{
				w.InsteadOf<GuidConverter>();
			}).WithTypeConverter(new DateTimeConverter(DateTimeKind.Utc, null, true)).WithEventEmitter((IEventEmitter inner) => new JsonEventEmitter(inner), delegate(IRegistrationLocationSelectionSyntax<IEventEmitter> loc)
			{
				loc.InsteadOf<TypeAssigningEventEmitter>();
			});
		}

		public SerializerBuilder WithNewLine(string newLine)
		{
			emitterSettings = emitterSettings.WithNewLine(newLine);
			return this;
		}

		public SerializerBuilder WithPreProcessingPhaseObjectGraphVisitor<TObjectGraphVisitor>(TObjectGraphVisitor objectGraphVisitor) where TObjectGraphVisitor : IObjectGraphVisitor<Nothing>
		{
			return WithPreProcessingPhaseObjectGraphVisitor(objectGraphVisitor, delegate(IRegistrationLocationSelectionSyntax<IObjectGraphVisitor<Nothing>> w)
			{
				w.OnTop();
			});
		}

		public SerializerBuilder WithPreProcessingPhaseObjectGraphVisitor<TObjectGraphVisitor>(TObjectGraphVisitor objectGraphVisitor, Action<IRegistrationLocationSelectionSyntax<IObjectGraphVisitor<Nothing>>> where) where TObjectGraphVisitor : IObjectGraphVisitor<Nothing>
		{
			TObjectGraphVisitor objectGraphVisitor2 = objectGraphVisitor;
			if (objectGraphVisitor2 == null)
			{
				throw new ArgumentNullException("objectGraphVisitor");
			}
			if (where == null)
			{
				throw new ArgumentNullException("where");
			}
			where(preProcessingPhaseObjectGraphVisitorFactories.CreateRegistrationLocationSelector(typeof(TObjectGraphVisitor), (IEnumerable<IYamlTypeConverter> _) => objectGraphVisitor2));
			return this;
		}

		public SerializerBuilder WithPreProcessingPhaseObjectGraphVisitor<TObjectGraphVisitor>(WrapperFactory<IObjectGraphVisitor<Nothing>, TObjectGraphVisitor> objectGraphVisitorFactory, Action<ITrackingRegistrationLocationSelectionSyntax<IObjectGraphVisitor<Nothing>>> where) where TObjectGraphVisitor : IObjectGraphVisitor<Nothing>
		{
			WrapperFactory<IObjectGraphVisitor<Nothing>, TObjectGraphVisitor> objectGraphVisitorFactory2 = objectGraphVisitorFactory;
			if (objectGraphVisitorFactory2 == null)
			{
				throw new ArgumentNullException("objectGraphVisitorFactory");
			}
			if (where == null)
			{
				throw new ArgumentNullException("where");
			}
			where(preProcessingPhaseObjectGraphVisitorFactories.CreateTrackingRegistrationLocationSelector(typeof(TObjectGraphVisitor), (IObjectGraphVisitor<Nothing> wrapped, IEnumerable<IYamlTypeConverter> _) => objectGraphVisitorFactory2(wrapped)));
			return this;
		}

		public SerializerBuilder WithoutPreProcessingPhaseObjectGraphVisitor<TObjectGraphVisitor>() where TObjectGraphVisitor : IObjectGraphVisitor<Nothing>
		{
			return WithoutPreProcessingPhaseObjectGraphVisitor(typeof(TObjectGraphVisitor));
		}

		public SerializerBuilder WithoutPreProcessingPhaseObjectGraphVisitor(Type objectGraphVisitorType)
		{
			if (objectGraphVisitorType == null)
			{
				throw new ArgumentNullException("objectGraphVisitorType");
			}
			preProcessingPhaseObjectGraphVisitorFactories.Remove(objectGraphVisitorType);
			return this;
		}

		public SerializerBuilder WithObjectGraphTraversalStrategyFactory(ObjectGraphTraversalStrategyFactory objectGraphTraversalStrategyFactory)
		{
			this.objectGraphTraversalStrategyFactory = objectGraphTraversalStrategyFactory;
			return this;
		}

		public SerializerBuilder WithEmissionPhaseObjectGraphVisitor<TObjectGraphVisitor>(Func<EmissionPhaseObjectGraphVisitorArgs, TObjectGraphVisitor> objectGraphVisitorFactory) where TObjectGraphVisitor : IObjectGraphVisitor<IEmitter>
		{
			return WithEmissionPhaseObjectGraphVisitor(objectGraphVisitorFactory, delegate(IRegistrationLocationSelectionSyntax<IObjectGraphVisitor<IEmitter>> w)
			{
				w.OnTop();
			});
		}

		public SerializerBuilder WithEmissionPhaseObjectGraphVisitor<TObjectGraphVisitor>(Func<EmissionPhaseObjectGraphVisitorArgs, TObjectGraphVisitor> objectGraphVisitorFactory, Action<IRegistrationLocationSelectionSyntax<IObjectGraphVisitor<IEmitter>>> where) where TObjectGraphVisitor : IObjectGraphVisitor<IEmitter>
		{
			Func<EmissionPhaseObjectGraphVisitorArgs, TObjectGraphVisitor> objectGraphVisitorFactory2 = objectGraphVisitorFactory;
			if (objectGraphVisitorFactory2 == null)
			{
				throw new ArgumentNullException("objectGraphVisitorFactory");
			}
			if (where == null)
			{
				throw new ArgumentNullException("where");
			}
			where(emissionPhaseObjectGraphVisitorFactories.CreateRegistrationLocationSelector(typeof(TObjectGraphVisitor), (EmissionPhaseObjectGraphVisitorArgs args) => objectGraphVisitorFactory2(args)));
			return this;
		}

		public SerializerBuilder WithEmissionPhaseObjectGraphVisitor<TObjectGraphVisitor>(WrapperFactory<EmissionPhaseObjectGraphVisitorArgs, IObjectGraphVisitor<IEmitter>, TObjectGraphVisitor> objectGraphVisitorFactory, Action<ITrackingRegistrationLocationSelectionSyntax<IObjectGraphVisitor<IEmitter>>> where) where TObjectGraphVisitor : IObjectGraphVisitor<IEmitter>
		{
			WrapperFactory<EmissionPhaseObjectGraphVisitorArgs, IObjectGraphVisitor<IEmitter>, TObjectGraphVisitor> objectGraphVisitorFactory2 = objectGraphVisitorFactory;
			if (objectGraphVisitorFactory2 == null)
			{
				throw new ArgumentNullException("objectGraphVisitorFactory");
			}
			if (where == null)
			{
				throw new ArgumentNullException("where");
			}
			where(emissionPhaseObjectGraphVisitorFactories.CreateTrackingRegistrationLocationSelector(typeof(TObjectGraphVisitor), (IObjectGraphVisitor<IEmitter> wrapped, EmissionPhaseObjectGraphVisitorArgs args) => objectGraphVisitorFactory2(wrapped, args)));
			return this;
		}

		public SerializerBuilder WithoutEmissionPhaseObjectGraphVisitor<TObjectGraphVisitor>() where TObjectGraphVisitor : IObjectGraphVisitor<IEmitter>
		{
			return WithoutEmissionPhaseObjectGraphVisitor(typeof(TObjectGraphVisitor));
		}

		public SerializerBuilder WithoutEmissionPhaseObjectGraphVisitor(Type objectGraphVisitorType)
		{
			if (objectGraphVisitorType == null)
			{
				throw new ArgumentNullException("objectGraphVisitorType");
			}
			emissionPhaseObjectGraphVisitorFactories.Remove(objectGraphVisitorType);
			return this;
		}

		public SerializerBuilder WithIndentedSequences()
		{
			emitterSettings = emitterSettings.WithIndentedSequences();
			return this;
		}

		public ISerializer Build()
		{
			return Serializer.FromValueSerializer(BuildValueSerializer(), emitterSettings);
		}

		public IValueSerializer BuildValueSerializer()
		{
			IEnumerable<IYamlTypeConverter> typeConverters = BuildTypeConverters();
			ITypeInspector typeInspector = BuildTypeInspector();
			IObjectGraphTraversalStrategy traversalStrategy = objectGraphTraversalStrategyFactory(typeInspector, typeResolver, typeConverters, maximumRecursion);
			IEventEmitter eventEmitter = eventEmitterFactories.BuildComponentChain(new WriterEventEmitter());
			return new ValueSerializer(traversalStrategy, eventEmitter, typeConverters, preProcessingPhaseObjectGraphVisitorFactories.Clone(), emissionPhaseObjectGraphVisitorFactories.Clone());
		}

		internal ITypeInspector BuildTypeInspector()
		{
			ITypeInspector typeInspector = new ReadablePropertiesTypeInspector(typeResolver, includeNonPublicProperties);
			if (!ignoreFields)
			{
				typeInspector = new CompositeTypeInspector(new ReadableFieldsTypeInspector(typeResolver), typeInspector);
			}
			return typeInspectorFactories.BuildComponentChain(typeInspector);
		}
	}
}
