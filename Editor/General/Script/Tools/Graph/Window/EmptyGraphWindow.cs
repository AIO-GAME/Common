/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2020-05-20
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using UnityEditor;
using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// 自定义窗口基类
    /// </summary>
    public abstract partial class EmptyGraphWindow : EditorWindow
    {
        /// <summary>
        /// 当前窗口宽度
        /// </summary>
        public float CurrentWidth => position.width;

        /// <summary>
        /// 当前窗口宽度一半
        /// </summary>
        public float CurrentWidthHalf => position.width / 2;

        /// <summary>
        /// 当前窗口高度
        /// </summary>
        public float CurrentHeight => position.height;

        /// <summary>
        /// 当前窗口高度一半
        /// </summary>
        public float CurrentHeightHalf => position.height / 2;

        /// <summary>
        /// 当前窗口中心点
        /// </summary>
        /// <param name="w">宽</param>
        /// <param name="h">高</param>
        protected EmptyGraphWindow(float w = 800, float h = 600)
        {
            minSize = new Vector2(w, h);
        }

        /// <summary>
        /// 当前窗口中心点
        /// </summary>
        public Rect ReliablePosition { get; private set; }

        /// <summary>
        /// 转化为信息字符串
        /// </summary>
        public sealed override string ToString()
        {
            return base.ToString();
        }

        private void ShowButton(Rect rect)
        {
            OnShowButton(rect);
        }

        private void ModifierKeysChanged()
        {
            OnModifierKeysChanged();
        }
    }
}