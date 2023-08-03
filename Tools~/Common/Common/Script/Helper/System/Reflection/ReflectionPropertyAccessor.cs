using System.Reflection;

namespace AIO
{
    public sealed class ReflectionPropertyAccessor : IOptimizedAccessor
    {
        public ReflectionPropertyAccessor(PropertyInfo propertyInfo)
        {
            if (OptimizedReflection.safeMode)
            {
                Ensure.That(nameof(propertyInfo)).IsNotNull(propertyInfo);
            }

            this.propertyInfo = propertyInfo;
        }

        private readonly PropertyInfo propertyInfo;

        public void Compile()
        {
        }

        public object GetValue(in object target)
        {
            return propertyInfo.GetValue(target, null);
        }

        public void SetValue(in object target, in object value)
        {
            propertyInfo.SetValue(target, value, null);
        }
    }
}