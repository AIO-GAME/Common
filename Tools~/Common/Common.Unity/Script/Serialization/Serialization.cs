namespace AIO.Unity
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;

#if UNITY_EDITOR
using UnityEditor;
#endif
    using Debug = UnityEngine.Debug;
    using UnityObject = UnityEngine.Object;

    /// <summary>
    /// 序列化
    /// </summary>
    public static class Serialization
    {
        static Serialization()
        {
            freeOperations = new HashSet<SerializationOperation>();
            busyOperations = new HashSet<SerializationOperation>();
        }

        /// <summary>
        /// 构造函数警告
        /// </summary>
        public const string ConstructorWarning = "This parameterless constructor is only made public for serialization. Use another constructor instead.";

        private static readonly HashSet<SerializationOperation> freeOperations;
        private static readonly HashSet<SerializationOperation> busyOperations;

        private static readonly object @lock = new object();

        /// <summary>
        /// Unity序列化
        /// </summary>
        public static bool isUnitySerializing { get; set; }

        /// <summary>
        /// 自定义序列化
        /// </summary>
        public static bool isCustomSerializing => busyOperations.Count > 0;

        /// <summary>
        /// 是否序列化
        /// </summary>
        public static bool isSerializing => isUnitySerializing || isCustomSerializing;

        private static SerializationOperation StartOperation()
        {
            lock (@lock)
            {
                if (freeOperations.Count == 0)
                {
                    freeOperations.Add(new SerializationOperation());
                }

                var operation = freeOperations.First();
                freeOperations.Remove(operation);
                busyOperations.Add(operation);
                return operation;
            }
        }

        private static void EndOperation(SerializationOperation operation)
        {
            lock (@lock)
            {
                if (!busyOperations.Contains(operation))
                {
                    throw new InvalidOperationException("Trying to finish an operation that isn't started.");
                }

                operation.Reset();
                busyOperations.Remove(operation);
                freeOperations.Add(operation);
            }
        }

        /// <summary>
        /// 复制序列化
        /// </summary>
        /// <returns></returns>
        public static T CloneViaSerialization<T>(this T value, in bool forceReflected = false)
        {
            return (T)Deserialize(Serialize(value, forceReflected), forceReflected);
        }

        /// <summary>
        /// 复制序列化
        /// </summary>
        public static void CloneViaSerializationInto<TSource, TDestination>(this TSource value, ref TDestination instance, in bool forceReflected = false)
            where TDestination : TSource
        {
            object _instance = instance;
            DeserializeInto(Serialize(value, forceReflected), ref _instance, forceReflected);
        }

        public static SerializationData Serialize(this object value, in bool forceReflected = false)
        {
            var operation = StartOperation();
            try
            {
                var json = SerializeJson(operation.serializer, value, forceReflected);
                var objectReferences = operation.objectReferences.ToArray();
                var data = new SerializationData(json, objectReferences);

#if DEBUG_SERIALIZATION
                Debug.Log(data.ToString($"<color=#88FF00>Serialized: <b>{value?.GetType().Name ?? "null"} [{value?.GetHashCode().ToString() ?? "N/A"}]</b></color>"));
#endif

                return data;
            }
            catch (Exception ex)
            {
                throw new SerializationException($"Serialization of '{value?.GetType().ToString() ?? "null"}' failed.",
                    ex);
            }
            finally
            {
                EndOperation(operation);
            }
        }

        public static void DeserializeInto(this SerializationData data, ref object instance, in bool forceReflected = false)
        {
            try
            {
                if (string.IsNullOrEmpty(data.json))
                {
                    instance = null;
                    return;
                }

#if DEBUG_SERIALIZATION
                Debug.Log(data.ToString($"<color=#3388FF>Deserializing into: <b>{instance?.GetType().Name ?? "null"} [{instance?.GetHashCode().ToString() ?? "N/A"}]</b></color>"));
#endif

                var operation = StartOperation();
                try
                {
                    operation.objectReferences.AddRange(data.objectReferences);
                    DeserializeJson(operation.serializer, data.json, ref instance, forceReflected);
                }
                finally
                {
                    EndOperation(operation);
                }
            }
            catch (Exception ex)
            {
                try
                {
                    Debug.LogWarning(data.ToString("Deserialization Failure Data"), instance as UnityObject);
                }
                catch (Exception ex2)
                {
                    Debug.LogWarning("Failed to log deserialization failure data:\n" + ex2, instance as UnityObject);
                }

                throw new SerializationException($"Deserialization into '{instance?.GetType().ToString() ?? "null"}' failed.", ex);
            }
        }

        public static object Deserialize(this SerializationData data, in bool forceReflected = false)
        {
            object instance = null;
            DeserializeInto(data, ref instance, forceReflected);
            return instance;
        }

        private static string SerializeJson(in fsSerializer serializer, in object instance, in bool forceReflected)
        {
            using (ProfilingUtils.SampleBlock("SerializeJson"))
            {
                fsData data;

                fsResult result;

                if (forceReflected)
                {
                    result = serializer.TrySerialize(instance.GetType(), typeof(fsReflectedConverter), instance, out data);
                }
                else
                {
                    result = serializer.TrySerialize(instance, out data);
                }

                HandleResult("Serialization", result, instance as UnityObject);

                return fsJsonPrinter.CompressedJson(data);
            }
        }

        private static fsResult DeserializeJsonUtil(in fsSerializer serializer, in string json, ref object instance, in bool forceReflected)
        {
            var fsData = fsJsonParser.Parse(json);

            fsResult result;

            if (forceReflected)
            {
                result = serializer.TryDeserialize(fsData, instance.GetType(), typeof(fsReflectedConverter), ref instance);
            }
            else
            {
                result = serializer.TryDeserialize(fsData, ref instance);
            }

            return result;
        }

        private static void DeserializeJson(in fsSerializer serializer, in string json, ref object instance, in bool forceReflected)
        {
            using (ProfilingUtils.SampleBlock("DeserializeJson"))
            {
                fsResult result = DeserializeJsonUtil(serializer, json, ref instance, forceReflected);

                HandleResult("Deserialization", result, instance as UnityObject);
            }
        }

        private static void HandleResult(in string label, in fsResult result, in UnityObject context = null)
        {
            result.AssertSuccess();

            if (result.HasWarnings)
            {
                foreach (var warning in result.RawMessages)
                {
                    Debug.LogWarning($"[{label}] {warning}\n", context);
                }
            }
        }

        /// <summary>
        /// 美化打印
        /// </summary>
        public static string PrettyPrint(in string json)
        {
            return fsJsonPrinter.PrettyJson(fsJsonParser.Parse(json));
        }

        #region Dependencies

        private static readonly HashSet<ISerializationDepender> awaitingDependers = new HashSet<ISerializationDepender>();

        private static readonly HashSet<object> availableDependencies = new HashSet<object>();

        private static object WeakenSerializationDependencyReference(ISerializationDependency dependency)
        {
            /*
             We need to use weak references to UnityEngine.Object dependencies, because otherwise
             Unity will consider them to be always used, and never call OnDisable() on them when unloading,
             which in turn will never unregister them from our available dependencies.
             Using WeakReference is overkill and not actually helpful. We don't need to access the actual dependency
             object later on, and anyway the only way we could immmutably compare for equality in the hash set is based on the
             original hash code, which is not guaranteed to be unique... Except for UnityEngine.Objects,
             in which case the hash code is the native instance ID, which is guaranteed to be unique within a given assembly session.
             https://support.ludiq.io/communities/5/topics/4150-flowmachine-memory-leak#comment-11626
            */
            if (dependency is UnityObject uo)
            {
                return uo.GetHashCode();
            }
            else
            {
                // In this case, we can't actually weaken the reference.
                // But it shouldn't be a problem, because in non-UO cases, the
                // dependency should be able to unregister itself as it doesn't rely
                // on the native unload callbacks of unused assets.
                return dependency;
            }
        }

        /// <summary>
        /// 等待依赖关系
        /// </summary>
        public static void AwaitDependencies(in ISerializationDepender depender)
        {
            awaitingDependers.Add(depender);

            CheckIfDependenciesMet(depender);
        }

        /// <summary>
        /// 通知依赖项反序列化
        /// </summary>
        public static void NotifyDependencyDeserializing(in ISerializationDependency dependency)
        {
            NotifyDependencyUnavailable(dependency);
        }

        /// <summary>
        /// 通知依赖项反序列化
        /// </summary>
        public static void NotifyDependencyDeserialized(in ISerializationDependency dependency)
        {
            NotifyDependencyAvailable(dependency);
        }

        /// <summary>
        /// 通知依赖项不可用
        /// </summary>
        public static void NotifyDependencyUnavailable(in ISerializationDependency dependency)
        {
            availableDependencies.Remove(WeakenSerializationDependencyReference(dependency));
        }

        /// <summary>
        /// 通知依赖项可用
        /// </summary>
        public static void NotifyDependencyAvailable(in ISerializationDependency dependency)
        {
            availableDependencies.Add(WeakenSerializationDependencyReference(dependency));

            foreach (var awaitingDepender in awaitingDependers.ToArray())
            {
                if (!awaitingDependers.Contains(awaitingDepender))
                {
                    // In case the depender was already handled by a recursive
                    // dependency via OnAfterDependenciesDeserialized,
                    // we skip it. This is necessary because we duplicated
                    // the set to safely iterate over it with removal.
                    //
                    // This should prevent OnAfterDependenciesDeserialized from
                    // running twice on any given depender in a single deserialization
                    // operation.
                    continue;
                }

                CheckIfDependenciesMet(awaitingDepender);
            }
        }

        private static void CheckIfDependenciesMet(in ISerializationDepender depender)
        {
            var areDependenciesMet = true;

            foreach (var requiredDependency in depender.DeserializationDependencies)
            {
                var weakRequiredDependency = WeakenSerializationDependencyReference(requiredDependency);

                if (!availableDependencies.Contains(weakRequiredDependency))
                {
                    areDependenciesMet = false;
                    break;
                }
            }

            if (areDependenciesMet)
            {
                awaitingDependers.Remove(depender);

                depender.OnAfterDependenciesDeserialized();
            }
        }

        /// <summary>
        /// 日志
        /// </summary>
        public static void LogStuckDependers()
        {
            if (awaitingDependers.Any())
            {
                var message = awaitingDependers.Count + " awaiting dependers: \n";

                foreach (var depender in awaitingDependers)
                {
                    HashSet<object> missingDependencies = new HashSet<object>();

                    foreach (var requiredDependency in depender.DeserializationDependencies)
                    {
                        var weakRequiredDependency = WeakenSerializationDependencyReference(requiredDependency);

                        if (!availableDependencies.Contains(weakRequiredDependency))
                        {
                            missingDependencies.Add(requiredDependency);
                            break;
                        }
                    }

                    message += $"{depender} is missing {missingDependencies.ToCommaSeparatedString()}\n";
                }

                Debug.LogWarning(message);
            }
            else
            {
                Debug.Log("No stuck awaiting depender.");
            }
        }

        #endregion
    }
}
