using UnityEngine;

namespace AIO
{
    /// <summary>
    /// Draws the GUI for a <see cref="SelfDrawerAttribute"/> field.
    /// </summary>
    internal sealed partial class SelfDrawerDrawer
    {
#if UNITY_EDITOR
        /// <summary>Casts the <see cref="UnityEditor.PropertyDrawer.attribute"/>.</summary>
#endif
        public SelfDrawerAttribute Attribute => (SelfDrawerAttribute)attribute;
    }
}