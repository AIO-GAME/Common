namespace AIO
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBase"></typeparam>
    /// <typeparam name="TImplementation"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class VariantKeyedCollection<TBase, TImplementation, TKey> :
        VariantCollection<TBase, TImplementation>,
        IKeyedCollection<TKey, TBase>
        where TImplementation : TBase
    {
        /// <inheritdoc />
        public VariantKeyedCollection(in IKeyedCollection<TKey, TImplementation> implementation) : base(implementation)
        {
            this.implementation = implementation;
        }

        /// <inheritdoc />
        public TBase this[TKey key] => implementation[key];


        /// <summary>
        /// 
        /// </summary>
        public IKeyedCollection<TKey, TImplementation> implementation { get; private set; }

        /// <inheritdoc />
        public bool TryGetValue(TKey key, out TBase value)
        {
            var result = implementation.TryGetValue(key, out var implementationValue);
            value = implementationValue;
            return result;
        }

        /// <inheritdoc />
        public bool Contains(TKey key)
        {
            return implementation.Contains(key);
        }

        /// <inheritdoc />
        public bool Remove(TKey key)
        {
            return implementation.Remove(key);
        }

        TBase IKeyedCollection<TKey, TBase>.this[int index] => implementation[index];
    }
}