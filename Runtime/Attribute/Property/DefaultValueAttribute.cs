#region

using System;
using System.Diagnostics;

#endregion

namespace AIO
{
    /// <summary>[Editor-Conditional] Specifies the default value of a field and a secondary fallback.</summary>
    /// https://kybernetik.com.au/animancer/api/Animancer/DefaultValueAttribute
    [AttributeUsage(AttributeTargets.Field), Conditional(Strings.UnityEditor)]
    public partial class DefaultValueAttribute : Attribute
    {
        /// <summary>Creates a new <see cref="DefaultValueAttribute"/>.</summary>
        public DefaultValueAttribute(object primary, object secondary = null)
        {
            Primary   = primary;
            Secondary = secondary;
        }

        /// <summary>
        /// Creates a new <see cref="DefaultValueAttribute"/>.
        /// </summary>
        private DefaultValueAttribute() { }

        /// <summary>
        /// The main default value.
        /// </summary>
        public object Primary { get; protected set; }

        /// <summary>
        /// The fallback value to use if the target value was already equal to the <see cref="Primary"/>.
        /// </summary>
        public object Secondary { get; protected set; }
    }
}