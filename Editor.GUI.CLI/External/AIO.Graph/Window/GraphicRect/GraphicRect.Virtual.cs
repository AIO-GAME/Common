/*|✩ - - - - - |||
|||✩ Author:   ||| -> XINAN
|||✩ Date:     ||| -> 2023-06-26
|||✩ Document: ||| ->
|||✩ - - - - - |*/

using System;
using UnityEngine;

namespace AIO.UEditor
{
    public partial class GraphicRect
    {
        /// <summary>
        /// 是否显示该矩形
        /// </summary>
        public virtual bool IsShow
        {
            get => isShow;
            set => isShow = value;
        }

        /// <summary>
        /// 是否接收事件
        /// </summary>
        public virtual bool IsEvent
        {
            get => isEvent;
            set => isEvent = value;
        }

        /// <summary>
        /// 矩形范围
        /// </summary>
        public virtual Rect RectData
        {
            get => Rect;
            set => Rect = value;
        }

        /// <summary>
        /// 坐标信息
        /// </summary>
        public virtual Vector2 Position
        {
            get => Rect.position;
            set
            {
                var rectData = Rect;
                rectData.position = value;
                Rect = rectData;
            }
        }

        /// <summary>
        /// 坐标信息 X
        /// </summary>
        public virtual float X
        {
            get => Rect.position.x;
            set
            {
                var rectData = Rect;
                rectData.x = value;
                Rect = rectData;
            }
        }

        /// <summary>
        /// 坐标信息 Y
        /// </summary>
        public virtual float Y
        {
            get => Rect.position.y;
            set
            {
                var rectData = Rect;
                rectData.y = value;
                Rect = rectData;
            }
        }

        /// <summary>
        /// 大小信息
        /// </summary>
        public virtual Vector2 Size
        {
            get => Rect.size;
            set
            {
                var rectData = Rect;
                rectData.size = value;
                Rect = rectData;
            }
        }

        /// <summary>
        /// 大小信息 宽度
        /// </summary>
        public virtual float Width
        {
            get => Rect.width;
            set
            {
                var rectData = Rect;
                rectData.width = value;
                Rect = rectData;
            }
        }

        /// <summary>
        /// 大小信息 高度
        /// </summary>
        public virtual float Height
        {
            get => Rect.height;
            set
            {
                var rectData = Rect;
                rectData.height = value;
                Rect = rectData;
            }
        }

        /// <summary>
        /// 清空数据
        /// </summary>
        public virtual void Clear()
        {
        }

        /// <summary>
        /// 释放
        /// </summary>
        public virtual void Dispose()
        {
            SaveData();
            foreach (var item in Items) item.Dispose();
            Items.Clear();
        }

        /// <summary>
        /// 重置
        /// </summary>
        public virtual void Reset()
        {
        }

        /// <summary>
        /// 进入初始化
        /// </summary>
        public virtual void OnAwake()
        {
        }

        /// <summary>
        /// 进入绘制
        /// </summary>
        protected virtual void OnDraw()
        {
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        public virtual void SaveData()
        {
        }
    }
}