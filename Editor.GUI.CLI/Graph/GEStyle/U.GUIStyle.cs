/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-26
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System.Collections.Generic;
using UGUIStyle = UnityEngine.GUIStyle;

namespace AIO.UEditor
{
    /// <summary>
    /// 皮肤管理类
    /// </summary>
    public static class GEStyle
    {
        private static readonly Dictionary<string, UGUIStyle> StylesDic = new Dictionary<string, UGUIStyle>();

        /// <summary>
        /// 获取皮肤
        /// </summary>
        public static UGUIStyle Get(in string name)
        {
            if (!StylesDic.ContainsKey(name)) StylesDic.Add(name, new UGUIStyle(name));
            return StylesDic[name];
        }
        
        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle PRInsertion => Get("PR Insertion");
        
        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle ToggleMixed => Get("ToggleMixed");

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle TVInsertion => Get("TV Insertion");

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle TEtoolbarbutton => Get("TE toolbarbutton");

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle toolbarbutton => Get("toolbarbutton");

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle ToolbarDropDownLeft => Get(nameof(ToolbarDropDownLeft));

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle RectangleToolScaleBottom => Get(nameof(RectangleToolScaleBottom));

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle RectangleToolScaleRight => Get(nameof(RectangleToolScaleRight));

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle sv_label_0 => Get(nameof(sv_label_0));

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle sv_label_1 => Get(nameof(sv_label_1));

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle sv_label_2 => Get(nameof(sv_label_2));

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle sv_label_3 => Get(nameof(sv_label_3));

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle sv_label_4 => Get(nameof(sv_label_4));

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle sv_label_5 => Get(nameof(sv_label_5));

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle sv_label_6 => Get(nameof(sv_label_6));

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle sv_label_7 => Get(nameof(sv_label_7));

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle RectangleToolScaleLeft => Get(nameof(RectangleToolScaleLeft));

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle RectangleToolScaleTop => Get(nameof(RectangleToolScaleTop));


        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle ObjectPickerLargeStatus => Get("ObjectPickerLargeStatus");

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle WhiteLargeCenterLabel => Get("WhiteLargeCenterLabel");

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle flowvarPintooltip => Get("flow varPin tooltip");

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle LargeButton => Get(nameof(LargeButton));

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle FoldoutHeader => Get(nameof(FoldoutHeader));

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle StatusBarIcon => Get(nameof(StatusBarIcon));

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle RLFooter => Get("RL Footer");

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle RLHeader => Get("RL Header");

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle RLEmptyFooter => Get("RL Empty Header");

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle ChannelStripBg => Get(nameof(ChannelStripBg));

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle ChannelStripAttenuationBar => Get(nameof(ChannelStripAttenuationBar));

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle ChannelStripAttenuationMarkerSquare => Get(nameof(ChannelStripAttenuationMarkerSquare));

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle CurveEditorBackground => Get("CurveEditorBackground");

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle INThumbnailShadow => Get("IN ThumbnailShadow");

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle GroupBox => Get("GroupBox");

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle TEBoxBackground => Get("TE BoxBackground");

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle FlowNode5 => Get("flow node 5");

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle FlowNode5On => Get("flow node 5 on");

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle FlowNode1 => Get("flow node 1");

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle FlowNode1On => Get("flow node 1 on");

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle FlowNode3 => Get("flow node 3");

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle FlowNode3On => Get("flow node 3 on");

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle FlowNode0 => Get("flow node 0");

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle FlowNode0On => Get("flow node 0 on");

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle FlowNode4 => Get("flow node 4");

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle FlowNode4On => Get("flow node 4 on");

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle ProjectBrowserTextureIconDropShadow => Get(nameof(ProjectBrowserTextureIconDropShadow));

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle DDHeaderStyle => Get("DD HeaderStyle");

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle DDItemCheckmark => Get("DD ItemCheckmark");

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle INEditColliderButton => Get("IN EditColliderButton");

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle dockareaOverlay => Get(nameof(dockareaOverlay));

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle ContentToolbar => Get(nameof(ContentToolbar));

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle HelpBox => Get(nameof(HelpBox));

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle ShurikenModuleTitle => Get(nameof(ShurikenModuleTitle));

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle NotificationBackground => Get(nameof(NotificationBackground));

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle CenteredLabel => Get(nameof(CenteredLabel));

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle PreLabel => Get(nameof(PreLabel));
        
        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle TEDefaultTime => Get("TE DefaultTime");
        
        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle hostview => Get(nameof(hostview));

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle HeaderButton => Get(nameof(HeaderButton));

        /// <summary>
        ///
        /// </summary>
        public static UGUIStyle DropzoneStyle => Get(nameof(DropzoneStyle));
    }
}