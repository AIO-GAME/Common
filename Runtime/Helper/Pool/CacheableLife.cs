#region

using UnityEngine;

#endregion

namespace AIO
{
    /// <summary>
    /// 快捷缓存
    /// </summary>
    public class CacheableLife : MonoBehaviour
    {
        /// <summary>
        ///
        /// </summary>
        public float Life { get; private set; } = 2.0f;

        /// <summary>
        ///
        /// </summary>
        public Cacheable Cacheable { get; private set; }

        private void OnEnable()
        {
            Invoke(nameof(Recycle), Life);
        }

        private void OnDisable()
        {
            CancelInvoke();
        }

        private void OnValidate()
        {
            Cacheable = GetComponent<Cacheable>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cacheable"></param>
        public void SetPrefab(in Cacheable cacheable)
        {
            Cacheable = cacheable;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="life"></param>
        public void SetLife(in float life)
        {
            Life = life;
        }

        private void Recycle()
        {
            Cacheable.Recycle();
        }
    }
}