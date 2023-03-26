using System;
using System.Linq.Expressions;
using System.Reflection;

namespace AIO
{
    public class StaticFieldAccessor<TField> : IOptimizedAccessor
    {
        public StaticFieldAccessor(FieldInfo fieldInfo)
        {
            if (OptimizedReflection.safeMode)
            {
                if (fieldInfo == null)
                {
                    throw new ArgumentNullException(nameof(fieldInfo));
                }

                if (fieldInfo.FieldType != typeof(TField))
                {
                    throw new ArgumentException("Field type of field info doesn't match generic type.", nameof(fieldInfo));
                }

                if (!fieldInfo.IsStatic)
                {
                    throw new ArgumentException("The field isn't static.", nameof(fieldInfo));
                }
            }

            this.fieldInfo = fieldInfo;
            targetType = fieldInfo.DeclaringType;
        }

        private readonly FieldInfo fieldInfo;
        private Func<TField> getter;
        private Action<TField> setter;
        private Type targetType;

        public void Compile()
        {
            if (fieldInfo.IsLiteral)
            {
                var constant = (TField)fieldInfo.GetValue(null);
                getter = () => constant;
            }
            else
            {
                // If no JIT is available, we can only use reflection.
                getter = () => (TField)fieldInfo.GetValue(null);

                if (fieldInfo.CanWrite())
                {
                    setter = (value) => fieldInfo.SetValue(null, value);
                }
            }
        }

        public object GetValue(in object target)
        {
            if (OptimizedReflection.safeMode)
            {
                OptimizedReflection.VerifyStaticTarget(targetType, target);

                try
                {
                    return GetValueUnsafe(target);
                }
                catch (TargetInvocationException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new TargetInvocationException(ex);
                }
            }
            else
            {
                return GetValueUnsafe(target);
            }
        }

        private object GetValueUnsafe(in object target)
        {
            return getter.Invoke();
        }

        public void SetValue(in object target, in object value)
        {
            if (OptimizedReflection.safeMode)
            {
                OptimizedReflection.VerifyStaticTarget(targetType, target);

                if (setter == null)
                {
                    throw new TargetException($"The field '{targetType}.{fieldInfo.Name}' cannot be assigned.");
                }

                if (!typeof(TField).IsAssignableFrom(value))
                {
                    throw new ArgumentException(
                        $"The provided value for '{targetType}.{fieldInfo.Name}' does not match the field type.\nProvided: {value?.GetType()?.ToString() ?? "null"}\nExpected: {typeof(TField)}");
                }

                try
                {
                    SetValueUnsafe(target, value);
                }
                catch (TargetInvocationException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new TargetInvocationException(ex);
                }
            }
            else
            {
                SetValueUnsafe(target, value);
            }
        }

        private void SetValueUnsafe(object target, object value)
        {
            setter.Invoke((TField)value);
        }
    }
}