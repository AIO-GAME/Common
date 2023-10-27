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
	internal sealed class StaticSerializerBuilder : StaticBuilderSkeleton<StaticSerializerBuilder>
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

		private readonly StaticContext context;

		private readonly StaticObjectFactory factory;

		private ObjectGraphTraversalStrategyFactory objectGraphTraversalStrategyFactory;

		private readonly LazyComponentRegistrationList<IEnumerable<IYamlTypeConverter>, IObjectGraphVisitor<Nothing>> preProcessingPhaseObjectGraphVisitorFactories;

		private readonly LazyComponentRegistrationList<EmissionPhaseObjectGraphVisitorArgs, IObjectGraphVisitor<IEmitter>> emissionPhaseObjectGraphVisitorFactories;

		private readonly LazyComponentRegistrationList<IEventEmitter, IEventEmitter> eventEmitterFactories;

		private readonly IDictionary<Type, TagName> tagMappings = new Dictionary<Type, TagName>();

		private int maximumRecursion = 50;

		private EmitterSettings emitterSettings = EmitterSettings.Default;

		private DefaultValuesHandling defaultValuesHandlingConfiguration;

		private bool quoteNecessaryStrings;

		protected override StaticSerializerBuilder Self => this;

		public StaticSerializerBuilder(StaticContext context)
			: base((ITypeResolver)new DynamicTypeResolver())
		{
			this.context = context;
			factory = context.GetFactory();
			typeInspectorFactories.Add(typeof(CachedTypeInspector), (ITypeInspector inner) => new CachedTypeInspector(inner));
			typeInspectorFactories.Add(typeof(NamingConventionTypeInspector), (ITypeInspector inner) => (!(namingConvention is NullNamingConvention)) ? new NamingConventionTypeInspector(inner, namingConvention) : inner);
			typeInspectorFactories.Add(typeof(YamlAttributesTypeInspector), (ITypeInspector inner) => new YamlAttributesTypeInspector(inner));
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
					(EmissionPhaseObjectGraphVisitorArgs args) => new DefaultValuesObjectGraphVisitor(defaultValuesHandlingConfiguration, args.InnerVisitor, factory)
				},
				{
					typeof(CommentsObjectGraphVisitor),
					(EmissionPhaseObjectGraphVisitorArgs args) => new CommentsObjectGraphVisitor(args.InnerVisitor)
				}
			};
			eventEmitterFactories = new LazyComponentRegistrationList<IEventEmitter, IEventEmitter> {
			{
				typeof(TypeAssigningEventEmitter),
				(IEventEmitter inner) => new TypeAssigningEventEmitter(inner, requireTagWhenStaticAndActualTypesAreDifferent: false, tagMappings, quoteNecessaryStrings)
			} };
			objectGraphTraversalStrategyFactory = (ITypeInspector typeInspector, ITypeResolver typeResolver, IEnumerable<IYamlTypeConverter> typeConverters, int maximumRecursion) => new FullObjectGraphTraversalStrategy(typeInspector, typeResolver, maximumRecursion, namingConvention, factory);
		}

		public StaticSerializerBuilder WithQuotingNecessaryStrings()
		{
			quoteNecessaryStrings = true;
			return this;
		}

		public StaticSerializerBuilder WithMaximumRecursion(int maximumRecursion)
		{
			if (maximumRecursion <= 0)
			{
				throw new ArgumentOutOfRangeException("maximumRecursion", $"The maximum recursion specified ({maximumRecursion}) is invalid. It should be a positive integer.");
			}
			this.maximumRecursion = maximumRecursion;
			return this;
		}

		public StaticSerializerBuilder WithEventEmitter<TEventEmitter>(Func<IEventEmitter, TEventEmitter> eventEmitterFactory) where TEventEmitter : IEventEmitter
		{
			return WithEventEmitter(eventEmitterFactory, delegate(IRegistrationLocationSelectionSyntax<IEventEmitter> w)
			{
				w.OnTop();
			});
		}

		public StaticSerializerBuilder WithEventEmitter<TEventEmitter>(Func<IEventEmitter, TEventEmitter> eventEmitterFactory, Action<IRegistrationLocationSelectionSyntax<IEventEmitter>> where) where TEventEmitter : IEventEmitter
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

		public StaticSerializerBuilder WithEventEmitter<TEventEmitter>(WrapperFactory<IEventEmitter, IEventEmitter, TEventEmitter> eventEmitterFactory, Action<ITrackingRegistrationLocationSelectionSyntax<IEventEmitter>> where) where TEventEmitter : IEventEmitter
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

		public StaticSerializerBuilder WithoutEventEmitter<TEventEmitter>() where TEventEmitter : IEventEmitter
		{
			return WithoutEventEmitter(typeof(TEventEmitter));
		}

		public StaticSerializerBuilder WithoutEventEmitter(Type eventEmitterType)
		{
			if (eventEmitterType == null)
			{
				throw new ArgumentNullException("eventEmitterType");
			}
			eventEmitterFactories.Remove(eventEmitterType);
			return this;
		}

		internal override StaticSerializerBuilder WithTagMapping(TagName tag, Type type)
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

		public StaticSerializerBuilder WithoutTagMapping(Type type)
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

		public StaticSerializerBuilder EnsureRoundtrip()
		{
			objectGraphTraversalStrategyFactory = (ITypeInspector typeInspector, ITypeResolver typeResolver, IEnumerable<IYamlTypeConverter> typeConverters, int maximumRecursion) => new RoundtripObjectGraphTraversalStrategy(typeConverters, typeInspector, typeResolver, maximumRecursion, namingConvention, settings, factory);
			WithEventEmitter((IEventEmitter inner) => new TypeAssigningEventEmitter(inner, requireTagWhenStaticAndActualTypesAreDifferent: true, tagMappings, quoteNecessaryStrings), delegate(IRegistrationLocationSelectionSyntax<IEventEmitter> loc)
			{
				loc.InsteadOf<TypeAssigningEventEmitter>();
			});
			return WithTypeInspector((ITypeInspector inner) => new ReadableAndWritablePropertiesTypeInspector(inner), delegate(IRegistrationLocationSelectionSyntax<ITypeInspector> loc)
			{
				loc.OnBottom();
			});
		}

		public StaticSerializerBuilder DisableAliases()
		{
			preProcessingPhaseObjectGraphVisitorFactories.Remove(typeof(AnchorAssigner));
			emissionPhaseObjectGraphVisitorFactories.Remove(typeof(AnchorAssigningObjectGraphVisitor));
			return this;
		}

		[Obsolete("The default behavior is now to always emit default values, thefore calling this method has no effect. This behavior is now controlled by ConfigureDefaultValuesHandling.", true)]
		public StaticSerializerBuilder EmitDefaults()
		{
			return ConfigureDefaultValuesHandling(DefaultValuesHandling.Preserve);
		}

		public StaticSerializerBuilder ConfigureDefaultValuesHandling(DefaultValuesHandling configuration)
		{
			defaultValuesHandlingConfiguration = configuration;
			return this;
		}

		public StaticSerializerBuilder JsonCompatible()
		{
			emitterSettings = emitterSettings.WithMaxSimpleKeyLength(int.MaxValue).WithoutAnchorName();
			return WithTypeConverter(new GuidConverter(jsonCompatible: true), delegate(IRegistrationLocationSelectionSyntax<IYamlTypeConverter> w)
			{
				w.InsteadOf<GuidConverter>();
			}).WithEventEmitter((IEventEmitter inner) => new JsonEventEmitter(inner), delegate(IRegistrationLocationSelectionSyntax<IEventEmitter> loc)
			{
				loc.InsteadOf<TypeAssigningEventEmitter>();
			});
		}

		public StaticSerializerBuilder WithNewLine(string newLine)
		{
			emitterSettings = emitterSettings.WithNewLine(newLine);
			return this;
		}

		public StaticSerializerBuilder WithPreProcessingPhaseObjectGraphVisitor<TObjectGraphVisitor>(TObjectGraphVisitor objectGraphVisitor) where TObjectGraphVisitor : IObjectGraphVisitor<Nothing>
		{
			return WithPreProcessingPhaseObjectGraphVisitor(objectGraphVisitor, delegate(IRegistrationLocationSelectionSyntax<IObjectGraphVisitor<Nothing>> w)
			{
				w.OnTop();
			});
		}

		public StaticSerializerBuilder WithPreProcessingPhaseObjectGraphVisitor<TObjectGraphVisitor>(TObjectGraphVisitor objectGraphVisitor, Action<IRegistrationLocationSelectionSyntax<IObjectGraphVisitor<Nothing>>> where) where TObjectGraphVisitor : IObjectGraphVisitor<Nothing>
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

		public StaticSerializerBuilder WithPreProcessingPhaseObjectGraphVisitor<TObjectGraphVisitor>(WrapperFactory<IObjectGraphVisitor<Nothing>, TObjectGraphVisitor> objectGraphVisitorFactory, Action<ITrackingRegistrationLocationSelectionSyntax<IObjectGraphVisitor<Nothing>>> where) where TObjectGraphVisitor : IObjectGraphVisitor<Nothing>
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

		public StaticSerializerBuilder WithoutPreProcessingPhaseObjectGraphVisitor<TObjectGraphVisitor>() where TObjectGraphVisitor : IObjectGraphVisitor<Nothing>
		{
			return WithoutPreProcessingPhaseObjectGraphVisitor(typeof(TObjectGraphVisitor));
		}

		public StaticSerializerBuilder WithoutPreProcessingPhaseObjectGraphVisitor(Type objectGraphVisitorType)
		{
			if (objectGraphVisitorType == null)
			{
				throw new ArgumentNullException("objectGraphVisitorType");
			}
			preProcessingPhaseObjectGraphVisitorFactories.Remove(objectGraphVisitorType);
			return this;
		}

		public StaticSerializerBuilder WithObjectGraphTraversalStrategyFactory(ObjectGraphTraversalStrategyFactory objectGraphTraversalStrategyFactory)
		{
			this.objectGraphTraversalStrategyFactory = objectGraphTraversalStrategyFactory;
			return this;
		}

		public StaticSerializerBuilder WithEmissionPhaseObjectGraphVisitor<TObjectGraphVisitor>(Func<EmissionPhaseObjectGraphVisitorArgs, TObjectGraphVisitor> objectGraphVisitorFactory) where TObjectGraphVisitor : IObjectGraphVisitor<IEmitter>
		{
			return WithEmissionPhaseObjectGraphVisitor(objectGraphVisitorFactory, delegate(IRegistrationLocationSelectionSyntax<IObjectGraphVisitor<IEmitter>> w)
			{
				w.OnTop();
			});
		}

		public StaticSerializerBuilder WithEmissionPhaseObjectGraphVisitor<TObjectGraphVisitor>(Func<EmissionPhaseObjectGraphVisitorArgs, TObjectGraphVisitor> objectGraphVisitorFactory, Action<IRegistrationLocationSelectionSyntax<IObjectGraphVisitor<IEmitter>>> where) where TObjectGraphVisitor : IObjectGraphVisitor<IEmitter>
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

		public StaticSerializerBuilder WithEmissionPhaseObjectGraphVisitor<TObjectGraphVisitor>(WrapperFactory<EmissionPhaseObjectGraphVisitorArgs, IObjectGraphVisitor<IEmitter>, TObjectGraphVisitor> objectGraphVisitorFactory, Action<ITrackingRegistrationLocationSelectionSyntax<IObjectGraphVisitor<IEmitter>>> where) where TObjectGraphVisitor : IObjectGraphVisitor<IEmitter>
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

		public StaticSerializerBuilder WithoutEmissionPhaseObjectGraphVisitor<TObjectGraphVisitor>() where TObjectGraphVisitor : IObjectGraphVisitor<IEmitter>
		{
			return WithoutEmissionPhaseObjectGraphVisitor(typeof(TObjectGraphVisitor));
		}

		public StaticSerializerBuilder WithoutEmissionPhaseObjectGraphVisitor(Type objectGraphVisitorType)
		{
			if (objectGraphVisitorType == null)
			{
				throw new ArgumentNullException("objectGraphVisitorType");
			}
			emissionPhaseObjectGraphVisitorFactories.Remove(objectGraphVisitorType);
			return this;
		}

		public StaticSerializerBuilder WithIndentedSequences()
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
			ITypeInspector typeInspector = context.GetTypeInspector();
			return typeInspectorFactories.BuildComponentChain(typeInspector);
		}
	}
}
