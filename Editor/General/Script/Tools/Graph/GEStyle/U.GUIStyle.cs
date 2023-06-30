/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-26
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

using System.Collections.Generic;
using UGUIStyle = UnityEngine.GUIStyle;

namespace UnityEditor
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
        public static UGUIStyle ObjectPickerLargeStatus => Get(nameof(ObjectPickerLargeStatus));

        /// <summary>
        /// 
        /// </summary>
        public static UGUIStyle LargeButton => Get(nameof(LargeButton));

        /// <summary>
        /// 
        /// </summary>
        public static UGUIStyle StatusBarIcon => Get(nameof(StatusBarIcon));

        /// <summary>
        /// 
        /// </summary>
        public static UGUIStyle ChannelStripBg => Get(nameof(ChannelStripBg));
        /// <summary>
        /// 
        /// </summary>
        public static UGUIStyle INThumbnailShadow => Get("IN ThumbnailShadow");
        
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
        public static UGUIStyle DDHeaderStyle => Get("DD HeaderStyle");

        /// <summary>
        /// 
        /// </summary>
        public static UGUIStyle CenteredLabel => Get(nameof(CenteredLabel));

        /// <summary>
        /// 
        /// </summary>
        public static UGUIStyle DropzoneStyle => Get(nameof(DropzoneStyle));
    }
}