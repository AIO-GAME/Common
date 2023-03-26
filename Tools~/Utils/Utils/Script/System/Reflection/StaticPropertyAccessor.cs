using System;
using System.Linq.Expressions;
using System.Reflection;

namespace AIO
{
    public class StaticPropertyAccessor<TProperty> : IOptimizedAccessor
    {
        public StaticPropertyAccessor(in PropertyInfo propertyInfo)
        {
            if (OptimizedReflection.safeMode)
            {
                if (propertyInfo == null)
                {
                    throw new ArgumentNullException(nameof(propertyInfo));
                }

                if (propertyInfo.PropertyType != typeof(TProperty))
                {
                    throw new ArgumentException("The property type of the property info doesn't match the generic type.", nameof(propertyInfo));
                }

                if (!propertyInfo.IsStatic())
                {
                    throw new ArgumentException("The property isn't static.", nameof(propertyInfo));
                }
            }

            this.propertyInfo = propertyInfo;
            targetType = propertyInfo.DeclaringType;
        }

        private readonly PropertyInfo propertyInfo;
        private Func<TProperty> getter;
        private Action<TProperty> setter;
        private Type targetType;

        public void Compile()
        {
            var getterInfo = propertyInfo.GetGetMethod(true);
            var setterInfo = propertyInfo.GetSetMethod(true);

            if (getterInfo != null)
            {
                getter = (Func<TProperty>)getterInfo.CreateDelegate(typeof(Func<TProperty>));
            }

            if (setterInfo != null)
            {
                setter = (Action<TProperty>)setterInfo.CreateDelegate(typeof(Action<TProperty>));
            }
        }

        public object GetValue(in object target)
        {
            if (OptimizedReflection.safeMode)
            {
                OptimizedReflection.VerifyStaticTarget(targetType, target);

                if (getter == null)
                {
                    throw new TargetException($"The property '{targetType}.{propertyInfo.Name}' has no get accessor.");
                }

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

        public void SetValue(in object target,in  object value)
        {
            if (OptimizedReflection.safeMode)
            {
                OptimizedReflection.VerifyStaticTarget(targetType, target);

                if (setter == null)
                {
                    throw new TargetException($"The property '{targetType}.{propertyInfo.Name}' has no set accessor.");
                }

                if (!typeof(TProperty).IsAssignableFrom(value))
                {
                    throw new ArgumentException(
                        $"The provided value for '{targetType}.{propertyInfo.Name}' does not match the property type.\nProvided: {value?.GetType()?.ToString() ?? "null"}\nExpected: {typeof(TProperty)}");
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
            setter.Invoke((TProperty)value);
        }
    }
}