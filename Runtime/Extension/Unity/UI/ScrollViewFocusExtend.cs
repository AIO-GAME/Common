/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2025-10-12
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System.Collections;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;
#if SUPPORT_UNITASK
using Cysharp.Threading.Tasks;
#endif

namespace AIO.UEngine
{
    /// <summary>
    /// ScrollView聚焦扩展类
    /// </summary>
    public static class ScrollViewFocusExtend
    {
        /// <summary>
        /// 计算聚焦位置的滚动位置
        /// </summary>
        /// <param name="scrollView"> 滚动视图 </param>
        /// <param name="focusPoint"> 聚焦点（内容坐标系） </param>
        /// <returns> 滚动位置 </returns>
        [Preserve]
        public static Vector2 CalculateFocusedScrollPosition(this ScrollRect scrollView, Vector2 focusPoint)
        {
            var contentSize  = scrollView.content.rect.size;
            var viewportSize = ((RectTransform)scrollView.content.parent).rect.size;
            var contentScale = scrollView.content.localScale;

            contentSize.Scale(contentScale);
            focusPoint.Scale(contentScale);

            var scrollPosition = scrollView.normalizedPosition;
            if (scrollView.horizontal && contentSize.x > viewportSize.x)
                scrollPosition.x = Mathf.Clamp01((focusPoint.x - viewportSize.x * 0.5f) / (contentSize.x - viewportSize.x));
            if (scrollView.vertical && contentSize.y > viewportSize.y)
                scrollPosition.y = Mathf.Clamp01((focusPoint.y - viewportSize.y * 0.5f) / (contentSize.y - viewportSize.y));

            return scrollPosition;
        }

        /// <summary>
        /// 计算聚焦项的滚动位置
        /// </summary>
        /// <param name="scrollView"> 滚动视图 </param>
        /// <param name="item"> 聚焦项（内容坐标系） </param>
        /// <returns> 滚动位置 </returns>
        [Preserve]
        public static Vector2 CalculateFocusedScrollPosition(this ScrollRect scrollView, RectTransform item)
        {
            Vector2 itemCenterPoint   = scrollView.content.InverseTransformPoint(item.transform.TransformPoint(item.rect.center));
            var     contentSizeOffset = scrollView.content.rect.size;
            contentSizeOffset.Scale(scrollView.content.pivot);
            return scrollView.CalculateFocusedScrollPosition(itemCenterPoint + contentSizeOffset);
        }

        /// <summary>
        /// 聚焦到指定位置
        /// </summary>
        /// <param name="scrollView"> 滚动视图 </param>
        /// <param name="focusPoint"> 聚焦点（内容坐标系） </param>
        [Preserve]
        public static void FocusAtPoint(this ScrollRect scrollView, Vector2 focusPoint) { scrollView.normalizedPosition = scrollView.CalculateFocusedScrollPosition(focusPoint); }

        /// <summary>
        /// 聚焦到指定位置
        /// </summary>
        /// <param name="scrollView"> 滚动视图 </param>
        /// <param name="item"> 聚焦点（内容坐标系） </param>
        [Preserve]
        public static void FocusOnItem(this ScrollRect scrollView, RectTransform item) { scrollView.normalizedPosition = scrollView.CalculateFocusedScrollPosition(item); }

        /// <summary>
        /// 缓动到指定滚动位置
        /// </summary>
        /// <param name="scrollView"> 滚动视图 </param>
        /// <param name="targetNormalizedPos"> 目标滚动位置 </param>
        /// <param name="speed"> 缓动速度 </param>
        [Preserve]
        private static IEnumerator LerpToScrollPositionCoroutine(this ScrollRect scrollView, Vector2 targetNormalizedPos, float speed)
        {
            var   initialNormalizedPos = scrollView.normalizedPosition;
            float t                    = 0f;
            while (t < 1f)
            {
                scrollView.normalizedPosition = Vector2.LerpUnclamped(initialNormalizedPos, targetNormalizedPos, 1f - (1f - t) * (1f - t));
                yield return null;
                t += speed * Time.unscaledDeltaTime;
            }

            scrollView.normalizedPosition = targetNormalizedPos;
        }

        /// <summary>
        /// 缓动聚焦到指定位置
        /// </summary>
        /// <param name="scrollView"> 滚动视图 </param>
        /// <param name="focusPoint"> 聚焦点（内容坐标系） </param>
        /// <param name="speed"> 缓动速度 </param>
        [Preserve]
        public static IEnumerator FocusAtPointCoroutine(this ScrollRect scrollView, Vector2 focusPoint, float speed) { yield return scrollView.LerpToScrollPositionCoroutine(scrollView.CalculateFocusedScrollPosition(focusPoint), speed); }

        /// <summary>
        /// 缓动聚焦到指定项
        /// </summary>
        /// <param name="scrollView"> 滚动视图 </param>
        /// <param name="item"> 聚焦项（内容坐标系） </param>
        /// <param name="speed"> 缓动速度 </param>
        [Preserve]
        public static IEnumerator FocusOnItemCoroutine(this ScrollRect scrollView, RectTransform item, float speed) { yield return scrollView.LerpToScrollPositionCoroutine(scrollView.CalculateFocusedScrollPosition(item), speed); }

#if SUPPORT_UNITASK

        /// <summary>
        /// 取消掉之前的任务
        /// </summary>
        public static bool cancelOutPreviousTask;

        /// <summary>
        /// 正在运行的缓动任务数量
        /// </summary>
        public static int runningLerpTask;

        private static async UniTask LerpToScrollPositionAsync(this ScrollRect scrollView, Vector2 targetNormalizedPos, float speed)
        {
            var   initialNormalizedPos = scrollView.normalizedPosition;
            float t                    = 0f;

            while (t < 1f)
            {
                scrollView.normalizedPosition = Vector2.LerpUnclamped(initialNormalizedPos, targetNormalizedPos, 1f - (1f - t) * (1f - t));
                await UniTask.Yield(PlayerLoopTiming.Update);
                t += speed * Time.unscaledDeltaTime;
                if (!cancelOutPreviousTask) continue;
                t                     = 1;
                cancelOutPreviousTask = false;
            }

            scrollView.normalizedPosition = targetNormalizedPos;
            runningLerpTask--;
        }

        /// <summary>
        /// 缓动聚焦到指定项
        /// </summary>
        /// <param name="scrollView"> 滚动视图 </param>
        /// <param name="focusPoint"> 聚焦项（内容坐标系） </param>
        /// <param name="speed"> 缓动速度 </param>
        [Preserve]
        public static async UniTask FocusAtPointAsync(this ScrollRect scrollView, Vector2 focusPoint, float speed)
        {
            if (runningLerpTask > 0) cancelOutPreviousTask = true;
            runningLerpTask++;
            await scrollView.LerpToScrollPositionAsync(scrollView.CalculateFocusedScrollPosition(focusPoint), speed);
        }

        /// <summary>
        /// 缓动聚焦到指定项
        /// </summary>
        /// <param name="scrollView"> 滚动视图 </param>
        /// <param name="item"> 聚焦项（内容坐标系） </param>
        /// <param name="speed"> 缓动速度 </param>
        [Preserve]
        public static async UniTask FocusOnItemAsync(this ScrollRect scrollView, RectTransform item, float speed)
        {
            if (runningLerpTask > 0) cancelOutPreviousTask = true;
            runningLerpTask++;
            await scrollView.LerpToScrollPositionAsync(scrollView.CalculateFocusedScrollPosition(item), speed);
        }
#endif
    }
}