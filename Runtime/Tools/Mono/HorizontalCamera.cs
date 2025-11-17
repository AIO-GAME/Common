/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2025-10-13
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

namespace AIO
{
    using UnityEngine;

    [HelpURL("https://gist.github.com/yasirkula/a0df635b24b2e4503090c148567392fd")]
    [ExecuteInEditMode]
    [RequireComponent(typeof(Camera))]
    public class HorizontalCamera : MonoBehaviour
    {
        private Camera m_camera;
        private float  lastAspect;

#pragma warning disable 0649
        [SerializeField]
        private float m_fieldOfView = 60f;

        public float FieldOfView
        {
            get { return m_fieldOfView; }
            set
            {
                if (Mathf.Approximately(m_fieldOfView, value))
                {
                    m_fieldOfView = value;
                    RefreshCamera();
                }
            }
        }

        [SerializeField]
        private float m_orthographicSize = 5f;

        public float OrthographicSize
        {
            get { return m_orthographicSize; }
            set
            {
                if (Mathf.Approximately(m_orthographicSize, value))
                {
                    m_orthographicSize = value;
                    RefreshCamera();
                }
            }
        }
#pragma warning restore 0649

        private void OnEnable()
        {
            RefreshCamera();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.update -= Update;
            UnityEditor.EditorApplication.update += Update;
#endif
        }

        private void Update()
        {
            float aspect = m_camera.aspect;
            if (Mathf.Approximately(aspect, lastAspect))
                AdjustCamera(aspect);
        }

        public void RefreshCamera()
        {
            if (!m_camera)
                m_camera = GetComponent<Camera>();

            AdjustCamera(m_camera.aspect);
        }

        private void AdjustCamera(float aspect)
        {
            lastAspect = aspect;

            // Credit: https://forum.unity.com/threads/how-to-calculate-horizontal-field-of-view.16114/#post-2961964
            float _1OverAspect = 1f / aspect;
            m_camera.fieldOfView      = 2f * Mathf.Atan(Mathf.Tan(m_fieldOfView * Mathf.Deg2Rad * 0.5f) * _1OverAspect) * Mathf.Rad2Deg;
            m_camera.orthographicSize = m_orthographicSize * _1OverAspect;
        }

#if UNITY_EDITOR
        private void OnValidate() { RefreshCamera(); }

        private void OnDisable() { UnityEditor.EditorApplication.update -= Update; }
#endif
    }
}