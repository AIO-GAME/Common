#region using

using System;
using AIO.UEngine;
using UnityEngine;
using UnityEngine.Rendering;

#endregion

namespace AIO
{
    public static class ScreenClick
    {
        private static ScreenClickEffect _instance;
        private static Transform         Root;

        public static void Init(Transform root, Camera uiCamera, GameObject prefab = null)
        {
            if (root == null) throw new Exception("ScreenClick.Init root is null !");
            if (uiCamera == null) throw new Exception("ScreenClick.Init uiCamera is null !");

            if (Root != null)
            {
                Root.gameObject.TryRemoveComponent<ScreenClickEffect>();
                _instance = null;
            }

            Root = root;
            if (_instance == null)
            {
                _instance = Root.gameObject.AddComponent<ScreenClickEffect>();
            }

            if (_instance == null) throw new Exception("传入的Root节点无法添加ScreenClickEffect脚本 !");

            _instance.RootTransform = Root;
            _instance.UICamera      = uiCamera;
            _instance.Template      = prefab;
            _instance.Reset();
        }

        public static float Scale
        {
            get => _instance != null ? _instance.Scale : 0f;
            set
            {
                if (_instance != null) _instance.Scale = value;
            }
        }
    }

    /// <summary>
    /// 屏幕点击特效
    /// </summary>
    [DisallowMultipleComponent]
    internal class ScreenClickEffect : MonoBehaviour
    {
        private GameObject effObj;
        private Transform  effTrans;
        private Vector2    effPos;

        /// <summary>
        /// 最后点击时间
        /// </summary>
        [ReadOnly]
        public float LastClickTime;

        /// <summary>
        /// 特效预设
        /// </summary>
        public GameObject Template;

        /// <summary>
        /// UI根节点
        /// </summary>
        public Transform RootTransform;

        /// <summary>
        /// 实体对象
        /// </summary>
        public GameObject Entity;

        /// <summary>
        /// 特效缩放比例
        /// </summary>
        public float Scale
        {
            get => Template != null ? Template.transform.localScale.x : 0f;
            set
            {
                if (Template != null) Template.transform.localScale = Vector3.one * value;
                if (Entity != null) Entity.transform.localScale     = Vector3.one * value;
            }
        }

        /// <summary>
        /// UI摄像机
        /// </summary>
        public Camera UICamera;

        internal void Reset() { CreateEffect(); }

        void CreateEffect()
        {
            if (effObj != null)
            {
                Destroy(effObj);
                effObj = null;
            }

            effObj = new GameObject("Effect/ScreenClick");
            effObj.transform.SetParent(RootTransform);
            effObj.AddComponent<SortingGroup>().sortingOrder = 30000;

            effTrans = effObj.transform;
            effTrans.SetParent(gameObject.transform);
            if (Template != null) Entity = Instantiate(Template, effTrans, false);
            effObj.SetActive(false);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                LastClickTime = Time.time;
                if (effObj != null)
                {
                    effObj.SetActive(false);
                    // 设置特效位置到点击位置
                    var screenPos = Input.mousePosition;
                    screenPos.z = Mathf.Abs(UICamera.transform.position.z - RootTransform.position.z); // 计算深度
                    var worldPos = UICamera.ScreenToWorldPoint(screenPos);

                    effTrans.position = worldPos;
                    effObj.SetActive(true);
                }
            }
        }

        private void OnDestroy()
        {
            if (effObj != null)
            {
                Destroy(effObj);
                effObj = null;
            }
        }

        public void Destroy() { Destroy(this); }
    }
}