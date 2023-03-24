namespace AIO
{
    using System.Reflection;

    public sealed class ReflectionFieldAccessor : IOptimizedAccessor
    {
        public ReflectionFieldAccessor(FieldInfo fieldInfo)
        {
            if (OptimizedReflection.safeMode)
            {
                Ensure.That(nameof(fieldInfo)).IsNotNull(fieldInfo);
            }

            this.fieldInfo = fieldInfo;
        }

        private readonly FieldInfo fieldInfo;

        public void Compile()
        {
        }

        public object GetValue(in object target)
        {
            return fieldInfo.GetValue(target);
        }

        public void SetValue(in object target, in object value)
        {
            fieldInfo.SetValue(target, value);
        }
    }
}