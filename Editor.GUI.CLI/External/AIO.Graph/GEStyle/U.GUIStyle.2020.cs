/*|✩ - - - - - |||
|||✩ Date:     ||| -> 2023-10-31
|||✩ Document: ||| -> Automatic Generation Unity 2020
|||✩ - - - - - |*/

#if UNITY_2020
using UGUIStyle = UnityEngine.GUIStyle;

namespace AIO.UEditor
{
    public partial class GEStyle
    {
        public static UGUIStyle FloatFieldLinkButton => Get("FloatFieldLinkButton");
        public static UGUIStyle PreviewPackageInUse => Get("PreviewPackageInUse");
        public static UGUIStyle StatusBarIcon => Get("StatusBarIcon");
        public static UGUIStyle ToolbarSeachCancelButton => Get("ToolbarSeachCancelButton");
        public static UGUIStyle ToolbarSeachCancelButtonEmpty => Get("ToolbarSeachCancelButtonEmpty");
        public static UGUIStyle ToolbarSeachTextField => Get("ToolbarSeachTextField");
        public static UGUIStyle ToolbarSeachTextFieldPopup => Get("ToolbarSeachTextFieldPopup");
        public static UGUIStyle WinBtnClose => Get("WinBtnClose");
        public static UGUIStyle WinBtnCloseMac => Get("WinBtnCloseMac");
        public static UGUIStyle WinBtnMax => Get("WinBtnMax");
        public static UGUIStyle WinBtnMaxMac => Get("WinBtnMaxMac");
        public static UGUIStyle WinBtnMinMac => Get("WinBtnMinMac");
        public static UGUIStyle WinBtnRestore => Get("WinBtnRestore");
        public static UGUIStyle WinBtnRestoreMac => Get("WinBtnRestoreMac");
    }
}
#endif
