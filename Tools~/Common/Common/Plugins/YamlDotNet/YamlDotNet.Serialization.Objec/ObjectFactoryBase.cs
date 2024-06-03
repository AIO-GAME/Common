using System;
using System.Collections;
using System.Collections.Generic;
using AIO.YamlDotNet.Helpers;
using AIO.YamlDotNet.Serialization.Utilities;

namespace AIO.YamlDotNet.Serialization.ObjectFactories
{
	internal abstract class ObjectFactoryBase : IObjectFactory
	{
		public abstract object Create(Type type);

		public virtual object? CreatePrimitive(Type type)
		{
			if (!type.IsValueType())
			{
				return null;
			}
			return Activator.CreateInstance(type);
		}

		public virtual bool GetDictionary(IObjectDescriptor descriptor, out IDictionary? dictionary, out Type[]? genericArguments)
		{
			Type implementedGenericInterface = ReflectionUtility.GetImplementedGenericInterface(descriptor.Type, typeof(IDictionary<, >));
			if (implementedGenericInterface != null)
			{
				genericArguments = implementedGenericInterface.GetGenericArguments();
				object obj = Activator.CreateInstance(typeof(GenericDictionaryToNonGenericAdapter<, >).MakeGenericType(genericArguments), descriptor.Value);
				dictionary = obj as IDictionary;
				return true;
			}
			genericArguments = null;
			dictionary = null;
			return false;
		}

		public virtual Type GetValueType(Type type)
		{
			Type implementedGenericInterface = ReflectionUtility.GetImplementedGenericInterface(type, typeof(IEnumerable<>));
			if (!(implementedGenericInterface != null))
			{
				return typeof(object);
			}
			return implementedGenericInterface.GetGenericArguments()[0];
		}
	}
}
