#if UNITY_EDITOR
namespace AIO
{
    /// <summary>
    /// Draws the GUI for a <see cref="SelfDrawerAttribute"/> field.
    /// </summary>
    internal sealed partial class SelfDrawerDrawer
    {
        /// <summary>Casts the <see cref="UnityEditor.PropertyDrawer.attribute"/>.</summary>
        public SelfDrawerAttribute Attribute => (SelfDrawerAttribute)attribute;
    }
}
#endif