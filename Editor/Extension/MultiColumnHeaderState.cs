using System.Linq;
using UnityEditor.IMGUI.Controls;

namespace AIO.UEditor
{
    public static class MultiColumnHeaderStateExt
    {
        /// <summary>
        /// 自动大小
        /// </summary>
        /// <param name="headerState"></param>
        /// <param name="maxWidth">最大宽</param>
        /// <param name="index">自由大小的序号</param>
        public static void AutoWidth(this MultiColumnHeaderState headerState, float maxWidth, int index = 0)
        {
            var columns = headerState.columns;
            if (columns == null || columns.Length == 0) return;
            var residue = maxWidth - 16;
            residue = columns.
                      Where((t,                    i) => i != index).
                      Aggregate(residue, (current, t) => current - t.width);
            columns[index].width = residue < 100 ? 100 : residue;
        }
    }
}