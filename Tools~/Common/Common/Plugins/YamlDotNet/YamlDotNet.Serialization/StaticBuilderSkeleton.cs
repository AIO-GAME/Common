using System;
using System.Collections.Generic;
using YamlDotNet.Core;
using YamlDotNet.Serialization.Converters;
using YamlDotNet.Serialization.NamingConventions;

namespace YamlDotNet.Serialization
{
	internal abstract class StaticBuilderSkeleton<TBuilder> where TBuilder : StaticBuilderSkeleton<TBuilder>
	{
		internal INamingConvention namingConvention = NullNamingConvention.Instance;

		internal ITypeResolver typeResolver;

		internal readonly LazyComponentRegistrationList<Nothing, IYamlTypeConverter> typeConverterFactories;

		internal readonly LazyComponentRegistrationList<ITypeInspector, ITypeInspector> typeInspectorFactories;

		internal bool includeNonPublicProperties;

		internal Settings settings;

		protected abstract TBuilder Self { get; }

		internal StaticBuilderSkeleton(ITypeResolver typeResolver)
		{
			typeConverterFactories = new LazyComponentRegistrationList<Nothing, IYamlTypeConverter> {
			{
				typeof(GuidConverter),
				(Nothing _) => new GuidConverter(jsonCompatible: false)
			} };
			typeInspectorFactories = new LazyComponentRegistrationList<ITypeInspector, ITypeInspector>();
			this.typeResolver = typeResolver ?? throw new ArgumentNullException("typeResolver");
			settings = new Settings();
		}

		public TBuilder WithNamingConvention(INamingConvention namingConvention)
		{
			this.namingConvention = namingConvention ?? throw new ArgumentNullException("namingConvention");
			return Self;
		}

		public TBuilder WithTypeResolver(ITypeResolver typeResolver)
		{
			this.typeResolver = typeResolver ?? throw new ArgumentNullException("typeResolver");
			return Self;
		}

		internal abstract TBuilder WithTagMapping(TagName tag, Type type);

		public TBuilder WithTypeConverter(IYamlTypeConverter typeConverter)
		{
			return WithTypeConverter(typeConverter, delegate(IRegistrationLocationSelectionSyntax<IYamlTypeConverter> w)
			{
				w.OnTop();
			});
		}

		public TBuilder WithTypeConverter(IYamlTypeConverter typeConverter, Action<IRegistrationLocationSelectionSyntax<IYamlTypeConverter>> where)
		{
			IYamlTypeConverter typeConverter2 = typeConverter;
			if (typeConverter2 == null)
			{
				throw new ArgumentNullException("typeConverter");
			}
			if (where == null)
			{
				throw new ArgumentNullException("where");
			}
			where(typeConverterFactories.CreateRegistrationLocationSelector(typeConverter2.GetType(), (Nothing _) => typeConverter2));
			return Self;
		}

		public TBuilder WithTypeConverter<TYamlTypeConverter>(WrapperFactory<IYamlTypeConverter, IYamlTypeConverter> typeConverterFactory, Action<ITrackingRegistrationLocationSelectionSyntax<IYamlTypeConverter>> where) where TYamlTypeConverter : IYamlTypeConverter
		{
			WrapperFactory<IYamlTypeConverter, IYamlTypeConverter> typeConverterFactory2 = typeConverterFactory;
			if (typeConverterFactory2 == null)
			{
				throw new ArgumentNullException("typeConverterFactory");
			}
			if (where == null)
			{
				throw new ArgumentNullException("where");
			}
			where(typeConverterFactories.CreateTrackingRegistrationLocationSelector(typeof(TYamlTypeConverter), (IYamlTypeConverter wrapped, Nothing _) => typeConverterFactory2(wrapped)));
			return Self;
		}

		public TBuilder WithoutTypeConverter<TYamlTypeConverter>() where TYamlTypeConverter : IYamlTypeConverter
		{
			return WithoutTypeConverter(typeof(TYamlTypeConverter));
		}

		public TBuilder WithoutTypeConverter(Type converterType)
		{
			if (converterType == null)
			{
				throw new ArgumentNullException("converterType");
			}
			typeConverterFactories.Remove(converterType);
			return Self;
		}

		public TBuilder WithTypeInspector<TTypeInspector>(Func<ITypeInspector, TTypeInspector> typeInspectorFactory) where TTypeInspector : ITypeInspector
		{
			return WithTypeInspector(typeInspectorFactory, delegate(IRegistrationLocationSelectionSyntax<ITypeInspector> w)
			{
				w.OnTop();
			});
		}

		public TBuilder WithTypeInspector<TTypeInspector>(Func<ITypeInspector, TTypeInspector> typeInspectorFactory, Action<IRegistrationLocationSelectionSyntax<ITypeInspector>> where) where TTypeInspector : ITypeInspector
		{
			Func<ITypeInspector, TTypeInspector> typeInspectorFactory2 = typeInspectorFactory;
			if (typeInspectorFactory2 == null)
			{
				throw new ArgumentNullException("typeInspectorFactory");
			}
			if (where == null)
			{
				throw new ArgumentNullException("where");
			}
			where(typeInspectorFactories.CreateRegistrationLocationSelector(typeof(TTypeInspector), (ITypeInspector inner) => typeInspectorFactory2(inner)));
			return Self;
		}

		public TBuilder WithTypeInspector<TTypeInspector>(WrapperFactory<ITypeInspector, ITypeInspector, TTypeInspector> typeInspectorFactory, Action<ITrackingRegistrationLocationSelectionSyntax<ITypeInspector>> where) where TTypeInspector : ITypeInspector
		{
			WrapperFactory<ITypeInspector, ITypeInspector, TTypeInspector> typeInspectorFactory2 = typeInspectorFactory;
			if (typeInspectorFactory2 == null)
			{
				throw new ArgumentNullException("typeInspectorFactory");
			}
			if (where == null)
			{
				throw new ArgumentNullException("where");
			}
			where(typeInspectorFactories.CreateTrackingRegistrationLocationSelector(typeof(TTypeInspector), (ITypeInspector wrapped, ITypeInspector inner) => typeInspectorFactory2(wrapped, inner)));
			return Self;
		}

		public TBuilder WithoutTypeInspector<TTypeInspector>() where TTypeInspector : ITypeInspector
		{
			return WithoutTypeInspector(typeof(TTypeInspector));
		}

		public TBuilder WithoutTypeInspector(Type inspectorType)
		{
			if (inspectorType == null)
			{
				throw new ArgumentNullException("inspectorType");
			}
			typeInspectorFactories.Remove(inspectorType);
			return Self;
		}

		protected IEnumerable<IYamlTypeConverter> BuildTypeConverters()
		{
			return typeConverterFactories.BuildComponentList();
		}
	}
}
