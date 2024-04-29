#region

using System;
using System.Collections.Generic;
using System.Reflection;

#endregion

namespace AIO
{
    internal static partial class UnitHelper
    {
        public const BindingFlags
            InstanceBindings = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

        private const string
            ArrayDataPrefix = ".Array.data[",
            ArrayDataSuffix = "]";

        private static PropertyInfo _GradientValue;

        private static readonly Dictionary<Type, Dictionary<string, PropertyAccessor>>
            TypeToPathToAccessor = new Dictionary<Type, Dictionary<string, PropertyAccessor>>();

        /// <summary>Returns a field with the specified `name` in the `declaringType` or any of its base types.</summary>
        /// <remarks>Uses the <see cref="InstanceBindings"/>.</remarks>
        internal static FieldInfo GetField(Type declaringType, string name)
        {
            while (declaringType != null)
            {
                var field = declaringType.GetField(name, InstanceBindings);
                if (field != null)
                    return field;

                declaringType = declaringType.BaseType;
            }

            return null;
        }
    }
}