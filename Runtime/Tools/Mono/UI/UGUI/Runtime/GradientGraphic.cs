/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2025-10-13
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;
#if UNITY_2017_4 || UNITY_2018_2_OR_NEWER
using UnityEngine.U2D;
#endif

namespace AIO.UI
{
    [HelpURL("https://gist.github.com/yasirkula/7f34c2e190330da41edcca6b383490ff")]
    [RequireComponent(typeof(CanvasRenderer))]
    [AddComponentMenu("UI/Gradient Graphic", 12)]
    public class GradientGraphic : MaskableGraphic, ILayoutElement
    {
        private static readonly Vector2[] s_SlicedVertices = new Vector2[4];
        private static readonly Vector2[] s_SlicedUVs      = new Vector2[4];

        public Sprite sprite;

        public override Texture mainTexture
        {
            get { return sprite ? sprite.texture : s_WhiteTexture; }
        }

        public Color topLeftColor     = Color.white;
        public Color topRightColor    = Color.white;
        public Color bottomLeftColor  = Color.white;
        public Color bottomRightColor = Color.white;

        [Range(1, 5)]
        [Tooltip("Increasing this value will make the gradient smoother by generating more vertices for the UI mesh; increase it only when needed")]
        public int gradientSmoothness = 1;

        public bool useSlicedSprite = true;
        public bool fillCenter      = true;

        public float pixelsPerUnitMultiplier = 1f;

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

                return pixelsPerUnitMultiplier * spritePixelsPerUnit / referencePixelsPerUnit;
            }
        }

        public bool hasBorder
        {
            get { return sprite && sprite.border.sqrMagnitude > 0f; }
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

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            gradientSmoothness      = Mathf.Max(gradientSmoothness, 1);
            pixelsPerUnitMultiplier = Mathf.Max(pixelsPerUnitMultiplier, 0.01f);
        }
#endif

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            Rect  rect   = GetPixelAdjustedRect();
            float width  = rect.width;
            float height = rect.height;

            Vector4 uv          = sprite ? DataUtility.GetOuterUV(sprite) : Vector4.zero;
            Vector2 pivot       = rectTransform.pivot;
            float   bottomLeftX = -width * pivot.x;
            float   bottomLeftY = -height * pivot.y;

            if (useSlicedSprite && hasBorder)
            {
                // Generate sliced Image
                Vector4 padding, inner, border;
                if (sprite)
                {
                    padding = DataUtility.GetPadding(sprite);
                    inner   = DataUtility.GetInnerUV(sprite);
                    border  = sprite.border / pixelsPerUnit;
                }
                else
                {
                    inner   = Vector4.zero;
                    padding = Vector4.zero;
                    border  = Vector4.zero;
                }

                Rect originalRect = rectTransform.rect;

                for (int axis = 0; axis <= 1; axis++)
                {
                    float borderScaleRatio;

                    // The adjusted rect (adjusted for pixel correctness) may be slightly larger than the original rect.
                    // Adjust the border to match the rect to avoid small gaps between borders (case 833201).
                    if (originalRect.size[axis] != 0)
                    {
                        borderScaleRatio =  rect.size[axis] / originalRect.size[axis];
                        border[axis]     *= borderScaleRatio;
                        border[axis + 2] *= borderScaleRatio;
                    }

                    // If the rect is smaller than the combined borders, then there's not room for the borders at their normal size.
                    // In order to avoid artefacts with overlapping borders, we scale the borders down to fit.
                    float combinedBorders = border[axis] + border[axis + 2];
                    if (rect.size[axis] < combinedBorders && combinedBorders != 0)
                    {
                        borderScaleRatio =  rect.size[axis] / combinedBorders;
                        border[axis]     *= borderScaleRatio;
                        border[axis + 2] *= borderScaleRatio;
                    }
                }

                padding /= pixelsPerUnit;

                s_SlicedVertices[0] = new Vector2(padding.x, padding.y);
                s_SlicedVertices[3] = new Vector2(rect.width - padding.z, rect.height - padding.w);

                s_SlicedVertices[1].x = border.x;
                s_SlicedVertices[1].y = border.y;

                s_SlicedVertices[2].x = rect.width - border.z;
                s_SlicedVertices[2].y = rect.height - border.w;

                for (int i = 0; i < 4; i++)
                {
                    s_SlicedVertices[i].x += rect.x;
                    s_SlicedVertices[i].y += rect.y;
                }

                s_SlicedUVs[0] = new Vector2(uv.x, uv.y);
                s_SlicedUVs[1] = new Vector2(inner.x, inner.y);
                s_SlicedUVs[2] = new Vector2(inner.z, inner.w);
                s_SlicedUVs[3] = new Vector2(uv.z, uv.w);

                float invWidth  = 1f / width;
                float invHeight = 1f / height;

                for (int x = 0; x < 3; x++)
                {
                    int x2 = x + 1;

                    for (int y = 0; y < 3; y++)
                    {
                        if (!fillCenter && x == 1 && y == 1)
                            continue;

                        int y2         = y + 1;
                        int startIndex = vh.currentVertCount;

                        if (gradientSmoothness <= 1 || x != 1 || y != 1)
                        {
                            Color c1 = GetColorAt((s_SlicedVertices[x].x - bottomLeftX) * invWidth, (s_SlicedVertices[y].y - bottomLeftY) * invHeight);
                            Color c2 = GetColorAt((s_SlicedVertices[x].x - bottomLeftX) * invWidth, (s_SlicedVertices[y2].y - bottomLeftY) * invHeight);
                            Color c3 = GetColorAt((s_SlicedVertices[x2].x - bottomLeftX) * invWidth, (s_SlicedVertices[y2].y - bottomLeftY) * invHeight);
                            Color c4 = GetColorAt((s_SlicedVertices[x2].x - bottomLeftX) * invWidth, (s_SlicedVertices[y].y - bottomLeftY) * invHeight);

                            vh.AddVert(new Vector3(s_SlicedVertices[x].x, s_SlicedVertices[y].y, 0), c1, new Vector2(s_SlicedUVs[x].x, s_SlicedUVs[y].y));
                            vh.AddVert(new Vector3(s_SlicedVertices[x].x, s_SlicedVertices[y2].y, 0), c2, new Vector2(s_SlicedUVs[x].x, s_SlicedUVs[y2].y));
                            vh.AddVert(new Vector3(s_SlicedVertices[x2].x, s_SlicedVertices[y2].y, 0), c3, new Vector2(s_SlicedUVs[x2].x, s_SlicedUVs[y2].y));
                            vh.AddVert(new Vector3(s_SlicedVertices[x2].x, s_SlicedVertices[y].y, 0), c4, new Vector2(s_SlicedUVs[x2].x, s_SlicedUVs[y].y));

                            vh.AddTriangle(startIndex, startIndex + 1, startIndex + 2);
                            vh.AddTriangle(startIndex + 2, startIndex + 3, startIndex);
                        }
                        else
                        {
                            // Generate multiple small quads in the center
                            float invSmoothness = 1f / gradientSmoothness;

                            float _bottomLeftX = s_SlicedVertices[x].x;
                            float _bottomLeftY = s_SlicedVertices[y].y;
                            float _width       = s_SlicedVertices[x2].x - _bottomLeftX;
                            float _height      = s_SlicedVertices[y2].y - _bottomLeftY;

                            float _bottomLeftUVx = s_SlicedUVs[x].x;
                            float _bottomLeftUVy = s_SlicedUVs[y].y;
                            float _uvWidth       = s_SlicedUVs[x2].x - _bottomLeftUVx;
                            float _uvHeight      = s_SlicedUVs[y2].y - _bottomLeftUVy;

                            for (int i = 0; i <= gradientSmoothness; i++)
                            {
                                for (int j = 0; j <= gradientSmoothness; j++)
                                {
                                    float normalizedX = j * invSmoothness;
                                    float normalizedY = i * invSmoothness;

                                    Vector3 _position = new Vector3(_bottomLeftX + _width * normalizedX, _bottomLeftY + _height * normalizedY, 0f);
                                    Vector2 _uv       = new Vector2(_bottomLeftUVx + _uvWidth * normalizedX, _bottomLeftUVy + _uvHeight * normalizedY);
                                    Color   _color    = GetColorAt((_position.x - bottomLeftX) * invWidth, (_position.y - bottomLeftY) * invHeight);

                                    vh.AddVert(_position, _color, _uv);
                                }
                            }

                            for (int i = 0; i < gradientSmoothness; i++)
                            {
                                int firstTriangle = startIndex + i * (gradientSmoothness + 1);
                                for (int j = 0; j < gradientSmoothness; j++)
                                {
                                    int triangle      = firstTriangle + j;
                                    int aboveTriangle = triangle + gradientSmoothness + 1;

                                    vh.AddTriangle(triangle, aboveTriangle, aboveTriangle + 1);
                                    vh.AddTriangle(aboveTriangle + 1, triangle + 1, triangle);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                // Generate normal Image
                if (gradientSmoothness <= 1)
                {
                    // Generate single quad
                    vh.AddVert(new Vector3(bottomLeftX, bottomLeftY, 0f), bottomLeftColor, new Vector2(uv.x, uv.y));
                    vh.AddVert(new Vector3(bottomLeftX, bottomLeftY + height, 0f), topLeftColor, new Vector2(uv.x, uv.w));
                    vh.AddVert(new Vector3(bottomLeftX + width, bottomLeftY + height, 0f), topRightColor, new Vector2(uv.z, uv.w));
                    vh.AddVert(new Vector3(bottomLeftX + width, bottomLeftY, 0f), bottomRightColor, new Vector2(uv.z, uv.y));

                    vh.AddTriangle(0, 1, 2);
                    vh.AddTriangle(2, 3, 0);
                }
                else
                {
                    // Generate multiple small quads
                    float invSmoothness = 1f / gradientSmoothness;

                    for (int i = 0; i <= gradientSmoothness; i++)
                    {
                        for (int j = 0; j <= gradientSmoothness; j++)
                        {
                            float normalizedX = j * invSmoothness;
                            float normalizedY = i * invSmoothness;

                            Vector3 _position = new Vector3(bottomLeftX + width * normalizedX, bottomLeftY + height * normalizedY, 0f);
                            Vector2 _uv       = new Vector2(uv.z * normalizedX + uv.x * (1f - normalizedX), uv.w * normalizedY + uv.y * (1f - normalizedY));
                            vh.AddVert(_position, GetColorAt(normalizedX, normalizedY), _uv);
                        }
                    }

                    for (int i = 0; i < gradientSmoothness; i++)
                    {
                        int firstTriangle = i * (gradientSmoothness + 1);
                        for (int j = 0; j < gradientSmoothness; j++)
                        {
                            int triangle      = firstTriangle + j;
                            int aboveTriangle = triangle + gradientSmoothness + 1;

                            vh.AddTriangle(triangle, aboveTriangle, aboveTriangle + 1);
                            vh.AddTriangle(aboveTriangle + 1, triangle + 1, triangle);
                        }
                    }
                }
            }
        }

        private Color GetColorAt(float x, float y)
        {
            Color topLerp    = topLeftColor * (1f - x) + topRightColor * x;
            Color bottomLerp = bottomLeftColor * (1f - x) + bottomRightColor * x;
            return bottomLerp * (1f - y) + topLerp * y;
        }

        int ILayoutElement.layoutPriority
        {
            get { return 0; }
        }

        float ILayoutElement.minWidth
        {
            get { return 0; }
        }

        float ILayoutElement.minHeight
        {
            get { return 0; }
        }

        float ILayoutElement.flexibleWidth
        {
            get { return -1; }
        }

        float ILayoutElement.flexibleHeight
        {
            get { return -1; }
        }

        float ILayoutElement.preferredWidth
        {
            get
            {
                if (sprite == null)
                    return 0;

                return DataUtility.GetMinSize(sprite).x / pixelsPerUnit;
            }
        }

        float ILayoutElement.preferredHeight
        {
            get
            {
                if (sprite == null)
                    return 0;

                return DataUtility.GetMinSize(sprite).y / pixelsPerUnit;
            }
        }

        void ILayoutElement.CalculateLayoutInputHorizontal() { }
        void ILayoutElement.CalculateLayoutInputVertical()   { }

        // Whether this is being tracked for Atlas Binding
        private bool m_Tracked = false;

#if UNITY_2017_4 || UNITY_2018_2_OR_NEWER
        private static List<GradientGraphic> m_TrackedTexturelessImages = new List<GradientGraphic>();
        private static bool                  s_Initialized;
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
                GradientGraphic image = m_TrackedTexturelessImages[i];
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