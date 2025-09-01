#region using

using System;
using System.Collections.Generic;
using AIO.UEngine;
using UnityEngine;
using UnityEngine.Rendering;
#if UNITY_EDITOR
using UnityEditor;
#endif

#endregion

namespace AIO
{
    #region Eidtor

#if UNITY_EDITOR
    [CustomEditor(typeof(LightingSettingData))]
    public class LightingSettingDataEditor : Editor
    {
        private LightingSettingData m_Target;

        private void OnEnable() { m_Target = target as LightingSettingData; }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("提取"))
            {
                var so = new SerializedObject(m_Target);
                so.Update();
                var prop = so.GetIterator();
                while (prop.NextVisible(true))
                {
                    if (prop.name == "m_Script") continue;
                    if (prop.name == "skyBox")
                        prop.objectReferenceValue = RenderSettings.skybox;
                    else if (prop.name == "ambientSkyColor")
                        prop.colorValue = RenderSettings.ambientSkyColor;
                    else if (prop.name == "ambientEquatorColor")
                        prop.colorValue = RenderSettings.ambientEquatorColor;
                    else if (prop.name == "ambientGroundColor")
                        prop.colorValue = RenderSettings.ambientGroundColor;
                    else if (prop.name == "ambientMode")
                        prop.intValue = (int)RenderSettings.ambientMode;
                    else if (prop.name == "fog")
                        prop.boolValue = RenderSettings.fog;
                    else if (prop.name == "fogColor")
                        prop.colorValue = RenderSettings.fogColor;
                    else if (prop.name == "fogMode")
                        prop.intValue = (int)RenderSettings.fogMode;
                    else if (prop.name == "fogDensity")
                        prop.floatValue = RenderSettings.fogDensity;
                    else if (prop.name == "fogStart")
                        prop.floatValue = RenderSettings.fogStartDistance;
                    else if (prop.name == "fogEnd")
                        prop.floatValue = RenderSettings.fogEndDistance;
                }

                so.ApplyModifiedProperties();
            }
            
            if (GUILayout.Button("应用"))
            {
                m_Target.Apply();
                EditorUtility.SetDirty(m_Target);
            }
        }
    }
#endif

    #endregion

    [CreateAssetMenu(menuName = "Rendering/Lighting Setting Data", fileName = "LightingSettingData", order = 0)]
    [ExecuteAlways]
    [DisallowMultipleComponent]
    public class LightingSettingData : MonoBehaviour
    {
        #region class

        [Serializable]
        public class EnvRendersSettings
        {
            [Label("天空盒")]
            public Material skyBox;

            [Label("天空颜色")]
            [ColorUsage(true, true)]
            public Color ambientSkyColor = new Color(0.212f, 0.227f, 0.259f);

            [Label("赤道颜色")]
            [ColorUsage(true, true)]
            public Color ambientEquatorColor = new Color(0.172f, 0.188f, 0.227f);

            [Label("地面颜色")]
            [ColorUsage(true, true)]
            public Color ambientGroundColor = new Color(0.067f, 0.067f, 0.078f);

            [Label("环境光模式")]
            public AmbientMode ambientMode = AmbientMode.Flat;

            [Label("雾")]
            public bool fog;

            [Label("雾颜色")]
            public Color fogColor = Color.gray;

            [Label("雾模式")]
            public FogMode fogMode = FogMode.Linear;

            [Label("雾密度")]
            public float fogDensity = 0.05f;

            [Label("雾起始距离")]
            public float fogStart = 150f;

            [Label("雾结束距离")]
            public float fogEnd = 300f;

            public void Fetch()
            {
                skyBox              = RenderSettings.skybox;
                ambientSkyColor     = RenderSettings.ambientSkyColor;
                ambientEquatorColor = RenderSettings.ambientEquatorColor;
                ambientGroundColor  = RenderSettings.ambientGroundColor;
                ambientMode         = RenderSettings.ambientMode;
                fog                 = RenderSettings.fog;
                fogColor            = RenderSettings.fogColor;
                fogMode             = RenderSettings.fogMode;
                fogDensity          = RenderSettings.fogDensity;
                fogStart            = RenderSettings.fogStartDistance;
                fogEnd              = RenderSettings.fogEndDistance;
            }

            public void Apply()
            {
                RenderSettings.skybox              = skyBox;
                RenderSettings.ambientSkyColor     = ambientSkyColor;
                RenderSettings.ambientEquatorColor = ambientEquatorColor;
                RenderSettings.ambientGroundColor  = ambientGroundColor;
                RenderSettings.ambientMode         = ambientMode;
                RenderSettings.fog                 = fog;
                RenderSettings.fogColor            = fogColor;
                RenderSettings.fogMode             = fogMode;
                RenderSettings.fogDensity          = fogDensity;
                RenderSettings.fogStartDistance    = fogStart;
                RenderSettings.fogEndDistance      = fogEnd;
            }
        }

        #endregion

#if UNITY_EDITOR
        /// <summary>
        /// 是否正在烘焙
        /// </summary>
        public static bool IsBaking;
#endif

        private static List<LightingSettingData> m_EnabledDataList = new List<LightingSettingData>();
        private static EnvRendersSettings        m_Backup;

        [Label("天空盒")]
        [SerializeField]
        public Material skyBox;

        [Label("天空颜色")]
        [SerializeField]
        [ColorUsage(true, true)]
        public Color ambientSkyColor;

        [Label("赤道颜色")]
        [SerializeField]
        [ColorUsage(true, true)]
        public Color ambientEquatorColor;

        [Label("地面颜色")]
        [SerializeField]
        [ColorUsage(true, true)]
        public Color ambientGroundColor;

        [Label("环境光模式")]
        [SerializeField]
        public AmbientMode ambientMode;

        [Label("雾")]
        [SerializeField]
        public bool fog;

        [Label("雾颜色")]
        [SerializeField]
        public Color fogColor;

        [Label("雾模式")]
        [SerializeField]
        public FogMode fogMode;

        [Label("雾密度")]
        [SerializeField]
        public float fogDensity;

        [Label("雾起始距离")]
        [SerializeField]
        public float fogStart;

        [Label("雾结束距离")]
        [SerializeField]
        public float fogEnd;

        private void OnEnable()
        {
#if UNITY_EDITOR
            if (IsBaking)
                return;
#endif

            m_EnabledDataList.Add(this);
            if (m_EnabledDataList.Count == 1)
            {
                // 第一个记录当前天空盒子
                if (m_Backup == null)
                {
                    m_Backup = new EnvRendersSettings();
                }

                m_Backup.Fetch();
            }
            else
            {
                m_EnabledDataList[m_EnabledDataList.Count - 2].SetChildActive(false);
            }

            Apply();
        }

        private void OnDisable()
        {
#if UNITY_EDITOR
            if (IsBaking)
                return;
#endif

            if (m_EnabledDataList.Count <= 0) return;
            if (m_EnabledDataList[m_EnabledDataList.Count - 1] == this)
            {
                // 如果当前是正在显示的环境，且只有一个，则还原场景环境
                if (m_EnabledDataList.Count == 1)
                {
                    Revert();
                }
                else // 如果当前是正在显示的环境，且不只一个，则显示上一个场景环境
                {
                    m_EnabledDataList[m_EnabledDataList.Count - 2].Apply();
                }
            }

            m_EnabledDataList.Remove(this);
        }

        /// <summary>
        /// 应用场景环境
        /// </summary>
        public void Apply()
        {
            RenderSettings.skybox              = skyBox;
            RenderSettings.ambientSkyColor     = ambientSkyColor;
            RenderSettings.ambientEquatorColor = ambientEquatorColor;
            RenderSettings.ambientGroundColor  = ambientGroundColor;
            RenderSettings.ambientMode         = ambientMode;
            RenderSettings.fog                 = fog;
            RenderSettings.fogColor            = fogColor;
            RenderSettings.fogMode             = fogMode;
            RenderSettings.fogDensity          = fogDensity;
            RenderSettings.fogStartDistance    = fogStart;
            RenderSettings.fogEndDistance      = fogEnd;
            SetChildActive(true);
        }

        /// <summary>
        /// 设置子节点激活状态
        /// </summary>
        /// <param name="active"> 激活状态 </param>
        private void SetChildActive(bool active)
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(active);
            }
        }

        /// <summary>
        /// 还原场景环境
        /// </summary>
        public void Revert()
        {
            if (m_Backup != null)
            {
                m_Backup.Apply();
            }
        }
    }
}