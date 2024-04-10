#region

using UnityEngine.UI;

#endregion

namespace AIO.UEngine
{
    public static class ImageExtend
    {
        /// <summary>
        /// 设置图片透明度 eg:ColorKit.setAlpha(bar.bar_bg, 0.5f + (i % 2) * 0.5f);
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="alpha">透明度</param>
        public static void SetAlpha(this Image image, float alpha)
        {
            if (image == null) return;
            var color = image.color;
            color.a     = alpha;
            image.color = color;
        }
    }
}