namespace AIO
{
    using UnityEngine;

    public class CacheableLife : MonoBehaviour
    {
        public float Life { get; private set; } = 2.0f;

        public Cacheable Cacheable { get; private set; }

        public void SetPrefab(in Cacheable cacheable)
        {
            Cacheable = cacheable;
        }

        public void SetLife(in float life)
        {
            Life = life;
        }

        private void OnEnable()
        {
            Invoke(nameof(Recycle), Life);
        }

        private void OnDisable()
        {
            CancelInvoke();
        }

        private void Recycle()
        {
            Cacheable.Recycle();
        }

        private void OnValidate()
        {
            Cacheable = GetComponent<Cacheable>();
        }
    }
}
