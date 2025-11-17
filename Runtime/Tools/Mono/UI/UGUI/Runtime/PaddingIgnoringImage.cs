using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;
#if UNITY_2017_4 || UNITY_2018_2_OR_NEWER
using UnityEngine.U2D;
#endif

namespace AIO.UI
{
    // Most of this code is a copy&paste of Unity's UnityEngine.UI.Image.cs
    [RequireComponent(typeof(CanvasRenderer))]
    public class PaddingIgnoringImage : MaskableGraphic, ILayoutElement
    {
        [SerializeField]
        private Sprite m_Sprite;

        public Sprite sprite
        {
            get => m_Sprite;
            set
            {
                if (m_Sprite)
                {
                    if (m_Sprite != value)
                    {
                        m_Sprite = value;
                        SetAllDirty();
                        TrackImage();
                    }
                }
                else if (value)
                {
                    m_Sprite = value;
                    SetAllDirty();
                    TrackImage();
                }
            }
        }

        [SerializeField]
        private bool m_PreserveAspect;

        public bool preserveAspect
        {
            get { return m_PreserveAspect; }
            set
            {
                if (m_PreserveAspect != value)
                {
                    m_PreserveAspect = value;
                    SetVerticesDirty();
                }
            }
        }

        [SerializeField]
        private bool m_UseSpriteMesh;

        public bool useSpriteMesh
        {
            get => m_UseSpriteMesh;
            set
            {
                if (m_UseSpriteMesh != value)
                {
                    m_UseSpriteMesh = value;
                    SetVerticesDirty();
                }
            }
        }

        public override Texture mainTexture
        {
            get { return sprite ? sprite.texture : s_WhiteTexture; }
        }

        public float pixelsPerUnit
        {
            get
            {
                float spritePixelsPerUnit = 100;
                if (sprite)
                    spritePixelsPerUnit = sprite.pixelsPerUnit;

                float referencePixelsPerUnit = 100;
                if (canvas)
                    referencePixelsPerUnit = canvas.referencePixelsPerUnit;

                return spritePixelsPerUnit / referencePixelsPerUnit;
            }
        }

        public override Material material
        {
            get
            {
                if (m_Material != null)
                    return m_Material;

                if (sprite && sprite.associatedAlphaSplitTexture != null)
                {
#if UNITY_EDITOR
                    if (Application.isPlaying)
#endif
                        return Image.defaultETC1GraphicMaterial;
                }

                return defaultMaterial;
            }
            set { base.material = value; }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            TrackImage();
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            if (m_Tracked)
                UnTrackImage();
        }

        private Vector4 GetDrawingDimensions()
        {
            Vector4 padding = sprite ? DataUtility.GetPadding(sprite) : Vector4.zero;
            Vector2 size    = sprite ? sprite.rect.size : Vector2.zero;
            size -= new Vector2(padding.x + padding.z, padding.y + padding.w);

            Rect r = GetPixelAdjustedRect();

            if (preserveAspect && size.sqrMagnitude > 0f)
            {
                float spriteRatio = size.x / size.y;
                float rectRatio   = r.width / r.height;

                if (spriteRatio > rectRatio)
                {
                    float oldHeight = r.height;
                    r.height =  r.width * (1f / spriteRatio);
                    r.y      += (oldHeight - r.height) * rectTransform.pivot.y;
                }
                else
                {
                    float oldWidth = r.width;
                    r.width =  r.height * spriteRatio;
                    r.x     += (oldWidth - r.width) * rectTransform.pivot.x;
                }
            }

            return new Vector4(r.x, r.y, r.x + r.width, r.y + r.height);
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            Vector4 v       = GetDrawingDimensions();
            Vector4 uv      = sprite ? DataUtility.GetOuterUV(sprite) : Vector4.zero;
            Color32 color32 = color;

            if (!sprite || !useSpriteMesh)
            {
                vh.AddVert(new Vector3(v.x, v.y), color32, new Vector2(uv.x, uv.y));
                vh.AddVert(new Vector3(v.x, v.w), color32, new Vector2(uv.x, uv.w));
                vh.AddVert(new Vector3(v.z, v.w), color32, new Vector2(uv.z, uv.w));
                vh.AddVert(new Vector3(v.z, v.y), color32, new Vector2(uv.z, uv.y));

                vh.AddTriangle(0, 1, 2);
                vh.AddTriangle(2, 3, 0);
            }
            else
            {
                Vector4 spritePadding         = DataUtility.GetPadding(sprite);
                Vector2 spriteRectSize        = sprite.rect.size;
                Vector2 spriteNonPaddingSize  = spriteRectSize - new Vector2(spritePadding.x + spritePadding.z, spritePadding.y + spritePadding.w);
                Vector2 spritePivot           = sprite.pivot;
                Vector2 spriteNormalizedPivot = new Vector2((spritePivot.x - spritePadding.x) / spriteNonPaddingSize.x, (spritePivot.y - spritePadding.y) / spriteNonPaddingSize.y);
                Vector2 spriteBoundSize       = Vector2.Scale(sprite.bounds.size, new Vector2(spriteNonPaddingSize.x / spriteRectSize.x, spriteNonPaddingSize.y / spriteRectSize.y));

                Vector2 drawingSize = new Vector2(v.z - v.x, v.w - v.y);
                Vector2 drawOffset  = Vector2.Scale(rectTransform.pivot - spriteNormalizedPivot, drawingSize);

                Vector2[] vertices  = sprite.vertices;
                Vector2[] uvs       = sprite.uv;
                ushort[]  triangles = sprite.triangles;

                for (int i = 0; i < vertices.Length; ++i)
                    vh.AddVert(new Vector3((vertices[i].x / spriteBoundSize.x) * drawingSize.x - drawOffset.x, (vertices[i].y / spriteBoundSize.y) * drawingSize.y - drawOffset.y), color32, new Vector2(uvs[i].x, uvs[i].y));
                for (int i = 0; i < triangles.Length; i += 3)
                    vh.AddTriangle(triangles[i + 0], triangles[i + 1], triangles[i + 2]);
            }
        }

        public override void SetNativeSize()
        {
            if (sprite != null)
            {
                Vector4 padding = DataUtility.GetPadding(sprite);
                Vector2 size    = sprite.rect.size;
                size -= new Vector2(padding.x + padding.z, padding.y + padding.w);

                float w = size.x / pixelsPerUnit;
                float h = size.y / pixelsPerUnit;

                rectTransform.anchorMax = rectTransform.anchorMin;
                rectTransform.sizeDelta = new Vector2(w, h);

                SetAllDirty();
            }
        }

        int ILayoutElement.layoutPriority => 0;

        float ILayoutElement.minWidth => 0f;

        float ILayoutElement.minHeight => 0f;

        float ILayoutElement.flexibleWidth => -1;

        float ILayoutElement.flexibleHeight => -1;

        float ILayoutElement.preferredWidth => sprite ? sprite.rect.size.x / pixelsPerUnit : 0f;

        float ILayoutElement.preferredHeight => sprite ? sprite.rect.size.y / pixelsPerUnit : 0f;

        void ILayoutElement.CalculateLayoutInputHorizontal() { }
        void ILayoutElement.CalculateLayoutInputVertical()   { }

        // Whether this is being tracked for Atlas Binding
        private bool m_Tracked = false;

#if UNITY_2017_4 || UNITY_2018_2_OR_NEWER
        private static List<PaddingIgnoringImage> m_TrackedTexturelessImages = new List<PaddingIgnoringImage>();
        private static bool                       s_Initialized;
#endif

        private void TrackImage()
        {
            if (sprite != null && sprite.texture == null)
            {
#if UNITY_2017_4 || UNITY_2018_2_OR_NEWER
                if (!s_Initialized)
                {
                    SpriteAtlasManager.atlasRegistered += RebuildImage;
                    s_Initialized                      =  true;
                }

                m_TrackedTexturelessImages.Add(this);
#endif
                m_Tracked = true;
            }
        }

        private void UnTrackImage()
        {
#if UNITY_2017_4 || UNITY_2018_2_OR_NEWER
            m_TrackedTexturelessImages.Remove(this);
#endif
            m_Tracked = false;
        }

#if UNITY_2017_4 || UNITY_2018_2_OR_NEWER
        private static void RebuildImage(SpriteAtlas spriteAtlas)
        {
            for (int i = m_TrackedTexturelessImages.Count - 1; i >= 0; i--)
            {
                PaddingIgnoringImage image = m_TrackedTexturelessImages[i];
                if (spriteAtlas.CanBindTo(image.sprite))
                {
                    image.SetAllDirty();
                    m_TrackedTexturelessImages.RemoveAt(i);
                }
            }
        }
#endif
    }
}