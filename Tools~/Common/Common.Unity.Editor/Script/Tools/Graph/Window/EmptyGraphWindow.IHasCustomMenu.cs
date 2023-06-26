/*|✩ - - - - - |||
|||✩ Author:   ||| -> SAM
|||✩ Date:     ||| -> 2023-06-26
|||✩ Document: ||| -> 
|||✩ - - - - - |*/

namespace UnityEditor
{
    public partial class EmptyGraphWindow : IHasCustomMenu
    {
        /// <summary>
        /// 添加自定义菜单项
        /// </summary>
        /// <param name="menu">菜单信息</param>
        public void AddItemsToMenu(GenericMenu menu)
        {
            OnAddItemsToMenu(menu);
        }
    }
}