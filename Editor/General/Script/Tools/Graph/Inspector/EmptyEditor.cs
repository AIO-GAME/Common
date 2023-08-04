/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-26
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using UnityEngine;

namespace AIO.UEditor
{
    /// <summary>
    /// Editor 基类 无预览窗口 数据类
    /// </summary>
    public abstract partial class EmptyEditor : UnityEditor.Editor
    {
        /// <summary>
        /// 开启预览窗口
        /// </summary>
        protected bool Preview { get; set; } = false;

        /// <summary>
        /// scorll pos
        /// </summary>
        protected Vector2 Vector;

        /// <summary>
        /// 撤销操作名字
        /// </summary>
        protected string UndoNmae;

        /// <summary>
        /// 鼠标点击进入调用
        /// </summary>
        protected abstract void Awake(); //最先调用 修改脚本后 该方法不会重新调用

        /// <summary>
        /// 每次开启调用
        /// </summary>
        protected abstract void OnEnable(); //在awake之后调用 修改脚本后 该方法会自动重新调用

        /// <summary>
        ///
        /// </summary>
        protected abstract void OnDisable(); //脚本或对象禁用时调用

        /// <summary>
        /// 销毁时调用
        /// </summary>
        protected abstract void OnDestroy(); //并且鼠标点击其他实例 且也会调用该方法

        //    /// <summary> 如果要在预览标头中显示自定义控件,请重写此方法 </summary>
        //    /// 标题栏右边可以绘制其他的信息或者按钮等
        //    /// 重载 OnPreviewSettings 接口方便对预览窗口进行控制
        //    /// 预览窗口右上角
        //    public override void OnPreviewSettings()
        //    {
        //        PreviewSettings();
        //    }

        //    /// <summary> 预览标题 预览窗口左上角 </summary>
        //    /// 默认显示的是物体的名称,重载 GetPreviewTitle 接口可以更改标题名称
        //    public override GUIContent GetPreviewTitle()
        //    {
        //        if (PreviewTitle() == null) return PreviewTitle(); return base.GetPreviewTitle();
        //    }

        //    protected override void OnHeaderGUI()
        //    {

        //    }

        //    /// <summary> 如果该组件可以在当前状态下预览 则为True </summary>
        //    public override bool HasPreviewGUI()
        //    {
        //        return preview;
        //    }

        //    /// <summary> 预览内容的绘制 </summary>
        //    /// 最后预览内容的绘制，只需要重载 OnPreviewGUI 接口即可
        //    public override void OnPreviewGUI(Rect r, GUIStyle background)
        //    {
        //        previewGUI(r, background);
        //    }
    }
}
