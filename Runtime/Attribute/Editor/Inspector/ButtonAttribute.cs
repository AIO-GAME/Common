// #region
//
// using System;
// using System.Diagnostics;
//
// #endregion
//
// namespace AIO.UEngine
// {
//     /// <summary>
//     /// 按钮检视器
//     /// </summary>
//     [AttributeUsage(AttributeTargets.Method), Conditional("UNITY_EDITOR")]
//     public sealed class ButtonAttribute : InspectorAttribute
//     {
//         #region EnableMode enum
//
//         /// <summary>
//         /// 按钮激活模式
//         /// </summary>
//         public enum EnableMode
//         {
//             /// <summary>
//             /// 总是激活
//             /// </summary>
//             Always,
//
//             /// <summary>
//             /// 只在编辑模式激活
//             /// </summary>
//             Editor,
//
//             /// <summary>
//             /// 只在运行模式激活
//             /// </summary>
//             PlayMode
//         }
//
//         #endregion
//
//         public ButtonAttribute(string     text  = null,
//                                EnableMode mode  = EnableMode.Always,
//                                string     style = "Button",
//                                int        order = 0)
//         {
//             Text  = text;
//             Mode  = mode;
//             Style = style;
//             Order = order;
//         }
//
//         public SpacingMode Spacing { get; set; }
//         
//         public enum SpacingMode
//         {
//             Before = 1,
//             After  = 2
//         }
//         
//         public string     Text  { get; private set; }
//         public EnableMode Mode  { get; private set; }
//         public string     Style { get; private set; }
//         public int        Order { get; private set; }
//     }
// }
