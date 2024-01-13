/*|✩ - - - - - |||
|||✩ Author:   ||| -> xi nan
|||✩ Date:     ||| -> 2023-06-26

|||✩ - - - - - |*/

namespace AIO.UEditor
{
    public partial class EmptyGraphWindow : UnityEditor.IHasCustomMenu
    {
        /// <summary>
        /// 添加自定义菜单项
        /// </summary>
        /// <param name="menu">菜单信息</param>
        public void AddItemsToMenu(UnityEditor.GenericMenu menu)
        {
            OnAddItemsToMenu(menu);
        }
    }
}
