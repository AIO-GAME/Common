using UnityEngine;

namespace AIO
{
    public static partial class RHelper
    {
        /// <summary>
        /// Unity 计算
        /// </summary>
        public static partial class Math
        {
            /// <summary>
            /// 两点距离
            /// </summary>
            public static float Distance(Vector2 one, Vector2 two)
            {
                return System.Math.Abs(one.x - two.x) + System.Math.Abs(one.y - two.y);
            }

            /// <summary>
            /// 矩形相交
            /// </summary>
            public static bool IsRect(in Rect one, in Rect two)
            {
                var point = new Vector2(two.x, two.y); //左上角
                for (var i = 0; i < 4; i++)
                {
                    if (point.x >= one.xMin &&
                        point.x < one.xMax &&
                        point.y >= one.yMin &&
                        point.y < one.yMax) return true;
                    switch (i)
                    {
                        case 0:
                            point.x = two.x;
                            point.y = two.y + two.height;
                            break;
                        case 1:
                            point.x = two.x + +two.width;
                            point.y = two.y;
                            break;
                        case 2:
                            point.x = two.x + +two.width;
                            point.y = two.y + two.height;
                            break;
                    }
                }

                point = new Vector2(one.x, one.y);
                for (var i = 0; i < 4; i++)
                {
                    if (point.x >= two.xMin &&
                        point.x < two.xMax &&
                        point.y >= two.yMin &&
                        point.y < two.yMax) return true;
                    switch (i)
                    {
                        case 0:
                            point.x = one.x;
                            point.y = one.y + one.height;
                            break;
                        case 1:
                            point.x = one.x + +one.width;
                            point.y = one.y;
                            break;
                        case 2:
                            point.x = one.x + +one.width;
                            point.y = one.y + one.height;
                            break;
                    }
                }

                return false;
            }
        }
    }
}
