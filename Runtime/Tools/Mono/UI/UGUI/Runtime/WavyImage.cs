using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;
#if UNITY_2017_4 || UNITY_2018_2_OR_NEWER
using UnityEngine.U2D;
#endif

namespace AIO.UI
{
    [RequireComponent(typeof(CanvasRenderer))]
    [AddComponentMenu("UI/Wavy Image", 12)]
    public class WavyImage : MaskableGraphic, ILayoutElement
    {
        [SerializeField]
        private Sprite m_Sprite;

        public Sprite sprite
        {
            get { return m_Sprite; }
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

        [Header("Wave Animation")]
        [Tooltip("Number of rows and columns in the generated grid. The larger this value is, the smoother the wave animation will be but the calculations will also take more time.")]
        [Range(0, 10)]
        [SerializeField]
        private int m_WaveSegments = 3;

        public int waveSegments
        {
            get { return m_WaveSegments; }
            set
            {
                if (m_WaveSegments != value)
                {
                    m_WaveSegments = value;
                    SetVerticesDirty();
                }
            }
        }

        [Range(-0.5f, 0.5f)]
        [SerializeField]
        private float m_WaveStrength = 0.15f;

        public float waveStrength
        {
            get => m_WaveStrength;
            set
            {
                if (!Mathf.Approximately(m_WaveStrength, value)) return;
                m_WaveStrength = value;
                SetVerticesDirty();
            }
        }

        [Tooltip("As this value increases, the wave animation will become more 'chaotic' because vertices will start moving in more random directions.")]
        [Range(0f, 1f)]
        [SerializeField]
        private float m_WaveDiversity = 0.5f;

        private float waveDiversitySqrt; // The visual impact of waveDiversity's square root seems to be more uniform than waveDiversity itself

        public float waveDiversity
        {
            get { return m_WaveDiversity; }
            set
            {
                if (m_WaveDiversity != value)
                {
                    m_WaveDiversity   = value;
                    waveDiversitySqrt = Mathf.Sqrt(value);
                    SetVerticesDirty();
                }
            }
        }

        [SerializeField]
        private float m_WaveSpeed = 0.5f;

        public float waveSpeed
        {
            get { return m_WaveSpeed; }
            set
            {
                if (m_WaveSpeed != value)
                {
                    m_WaveSpeed = value;
                    SetVerticesDirty();
                }
            }
        }

        [SerializeField]
        private bool m_WaveSpeedIgnoresTimeScale = true;

        public bool waveSpeedIgnoresTimeScale
        {
            get { return waveSpeedIgnoresTimeScale; }
            set { waveSpeedIgnoresTimeScale = value; }
        }

        private float waveAnimationTime;

        public override Texture mainTexture
        {
            get { return m_Sprite ? m_Sprite.texture : s_WhiteTexture; }
        }

        public float pixelsPerUnit
        {
            get
            {
                float spritePixelsPerUnit = 100;
                if (m_Sprite)
                    spritePixelsPerUnit = m_Sprite.pixelsPerUnit;

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

                if (m_Sprite && m_Sprite.associatedAlphaSplitTexture != null)
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

        protected override void Awake()
        {
            base.Awake();
            waveDiversitySqrt = Mathf.Sqrt(m_WaveDiversity);
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

        private void Update()
        {
            if (m_WaveSegments > 0 && m_WaveSpeed != 0f && m_WaveStrength != 0f)
                SetVerticesDirty();
        }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();

            Color32 color32 = color;
            Rect    rect    = GetPixelAdjustedRect();

            Vector4 uv;
            float   bottomLeftX, bottomLeftY, width, height;
            if (m_Sprite)
            {
                uv = DataUtility.GetOuterUV(m_Sprite);
                Vector4 padding = DataUtility.GetPadding(m_Sprite);
                Vector2 size    = m_Sprite.rect.size;

                int spriteW = Mathf.RoundToInt(size.x);
                int spriteH = Mathf.RoundToInt(size.y);

                if (m_PreserveAspect && size.sqrMagnitude > 0f)
                {
                    float spriteRatio = size.x / size.y;
                    float rectRatio   = rect.width / rect.height;

                    if (spriteRatio > rectRatio)
                    {
                        float oldHeight = rect.height;
                        rect.height =  rect.width * (1f / spriteRatio);
                        rect.y      += (oldHeight - rect.height) * rectTransform.pivot.y;
                    }
                    else
                    {
                        float oldWidth = rect.width;
                        rect.width =  rect.height * spriteRatio;
                        rect.x     += (oldWidth - rect.width) * rectTransform.pivot.x;
                    }
                }

                bottomLeftX = rect.x + rect.width * padding.x / spriteW;
                bottomLeftY = rect.y + rect.height * padding.y / spriteH;
                width       = rect.width * (spriteW - padding.z - padding.x) / spriteW;
                height      = rect.height * (spriteH - padding.w - padding.y) / spriteH;
            }
            else
            {
                uv = Vector4.zero;

                bottomLeftX = rect.x;
                bottomLeftY = rect.y;
                width       = rect.width;
                height      = rect.height;
            }

#if UNITY_EDITOR
            if ((!Application.isPlaying && !WavyImageEditor.previewInEditor) || m_WaveSegments <= 0)
#else
		if( m_WaveSegments <= 0 )
#endif
            {
                // Generate normal Image
                vh.AddVert(new Vector3(bottomLeftX, bottomLeftY, 0f), color32, new Vector2(uv.x, uv.y));
                vh.AddVert(new Vector3(bottomLeftX, bottomLeftY + height, 0f), color32, new Vector2(uv.x, uv.w));
                vh.AddVert(new Vector3(bottomLeftX + width, bottomLeftY + height, 0f), color32, new Vector2(uv.z, uv.w));
                vh.AddVert(new Vector3(bottomLeftX + width, bottomLeftY, 0f), color32, new Vector2(uv.z, uv.y));

                vh.AddTriangle(0, 1, 2);
                vh.AddTriangle(2, 3, 0);
            }
            else
            {
                // Generate multiple small quads and move their vertices
                float invWaveSegments = 1f / m_WaveSegments;
                waveAnimationTime += (m_WaveSpeedIgnoresTimeScale ? Time.unscaledDeltaTime : Time.deltaTime) * m_WaveSpeed;

                float widthOffset  = m_WaveStrength * width;
                float heightOffset = m_WaveStrength * height;

#if UNITY_EDITOR
                waveDiversitySqrt = Mathf.Sqrt(m_WaveDiversity);
#endif

                for (int i = 0; i <= m_WaveSegments; i++)
                {
                    for (int j = 0; j <= m_WaveSegments; j++)
                    {
                        float normalizedX = j * invWaveSegments;
                        float normalizedY = i * invWaveSegments;

                        Vector2 _uv       = new Vector2(uv.z * normalizedX + uv.x * (1f - normalizedX), uv.w * normalizedY + uv.y * (1f - normalizedY));
                        Vector3 _position = new Vector3(bottomLeftX + width * normalizedX, bottomLeftY + height * normalizedY, 0f);
                        _position.x += (Mathf.PerlinNoise(normalizedX * waveDiversitySqrt + waveAnimationTime, normalizedY * waveDiversitySqrt) - 0.5f) * widthOffset;
                        _position.y += (Mathf.PerlinNoise(normalizedX * waveDiversitySqrt, normalizedY * waveDiversitySqrt + waveAnimationTime) - 0.5f) * heightOffset;

                        vh.AddVert(_position, color32, _uv);
                    }
                }

                for (int i = 0; i < m_WaveSegments; i++)
                {
                    int firstTriangle = i * (m_WaveSegments + 1);
                    for (int j = 0; j < m_WaveSegments; j++)
                    {
                        int triangle      = firstTriangle + j;
                        int aboveTriangle = triangle + m_WaveSegments + 1;

                        vh.AddTriangle(triangle, aboveTriangle, aboveTriangle + 1);
                        vh.AddTriangle(aboveTriangle + 1, triangle + 1, triangle);
                    }
                }
            }
        }

        int ILayoutElement.layoutPriority
        {
            get { return 0; }
        }

        float ILayoutElement.minWidth
        {
            get { return 0f; }
        }

        float ILayoutElement.minHeight
        {
            get { return 0f; }
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
            get { return m_Sprite ? (m_Sprite.rect.size.x / pixelsPerUnit) : 0f; }
        }

        float ILayoutElement.preferredHeight
        {
            get { return m_Sprite ? (m_Sprite.rect.size.y / pixelsPerUnit) : 0f; }
        }

        void ILayoutElement.CalculateLayoutInputHorizontal() { }
        void ILayoutElement.CalculateLayoutInputVertical()   { }

        // Whether this is being tracked for Atlas Binding
        private bool m_Tracked = false;

#if UNITY_2017_4 || UNITY_2018_2_OR_NEWER
        private static List<WavyImage> m_TrackedTexturelessImages = new List<WavyImage>();
        private static bool            s_Initialized;
#endif

        private void TrackImage()
        {
            if (m_Sprite != null && m_Sprite.texture == null)
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
                var image = m_TrackedTexturelessImages[i];
                if (!spriteAtlas.CanBindTo(image.sprite)) continue;
                image.SetAllDirty();
                m_TrackedTexturelessImages.RemoveAt(i);
            }
        }
#endif
    }
}