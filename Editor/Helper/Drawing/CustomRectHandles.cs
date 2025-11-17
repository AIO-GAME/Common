/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2025-10-12
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

namespace AIO.UEditor
{
    /// <summary>
    /// Custom Rect Handles
    /// </summary>
    /// <code>
    /// Draw a single rect handle
    /// CustomRectHandles.Draw(Rect3D, Color, Color);
    /// Draw multiple rect handles with the same colors
    /// CustomRectHandles.Draw(IList&lt;Rect3D>, Color, Color);
    /// Draw multiple rect handles with different colors
    /// CustomRectHandles.Draw(IList&lt;Rect3D>, IList&lt;Color>, IList&lt;Color>);
    /// </code>
    /// <code>
    /// // Center point: Vector3.zero
    /// // Size (width, height): (10.0, 5.0)
    /// // Rotation (by default, rect is aligned with XY plane): (90, 0, 0) -> which means rect is aligned with XZ plane
    /// CustomRectHandles.Rect3D rect = new CustomRectHandles.Rect3D( Vector3.zero, new Vector2( 10f, 5f ), Quaternion.Euler( 90f, 0f, 0f ) );
    ///
    /// // Same position and size, aligned with XY plane (Quaternion.identity)
    /// CustomRectHandles.Rect3D rect2 = new CustomRectHandles.Rect3D( Vector3.zero, new Vector2( 10f, 5f ), Quaternion.identity );
    ///
    /// void OnSceneGUI()
    /// {
    ///     DrawMultipleRectHandles();
    /// }
    ///
    /// void DrawSingleRectHandle()
    /// {
    ///     // Outline color: White
    ///     // Fill color: Translucent green
    ///     CustomRectHandles.Draw( rect, Color.white, new Color( 0f, 1f, 0f, 0.1f ) );
    ///
    ///     Vector3    newCenter   = rect.center;
    ///     Vector2    newSize     = rect.size;
    ///     Quaternion newRotation = rect.rotation;
    /// }
    ///
    /// void DrawMultipleRectHandles()
    /// {
    ///     CustomRectHandles.Rect3D[] rects         = new CustomRectHandles.Rect3D[2] { rect, rect2 };
    ///     Color[]                    outlineColors = new Color[2] { Color.white, Color.white };
    ///     Color[]                    fillColors    = new Color[2] { new Color( 0f, 1f, 0f, 0.1f ), new Color( 1f, 0f, 0f, 0.1f ) };
    ///
    ///     CustomRectHandles.Draw( rects, outlineColors, fillColors );
    ///
    ///     Vector3    newCenter1   = rects[0].center;
    ///     Vector2    newSize1     = rects[0].size;
    ///     Quaternion newRotation1 = rects[0].rotation;
    ///
    ///     Vector3    newCenter2   = rects[1].center;
    ///     Vector2    newSize2     = rects[1].size;
    ///     Quaternion newRotation2 = rects[1].rotation;
    /// }
    /// </code>
    [HelpURL("https://gist.github.com/yasirkula/2b1bd97a917afce7b8b2f5892a139ed3")]
    public class CustomRectHandles : ScriptableObject
    {
        public class Rect3D
        {
            public Vector3    center;
            public Quaternion rotation;
            public Vector2    size;

            public Rect3D(Vector3 center, Vector2 size, Quaternion rotation)
            {
                this.center   = center;
                this.rotation = rotation;
                this.size     = size;
            }

            public bool SurfaceRaycast(Ray ray, out float enter)
            {
                var plane = new Plane(rotation * Vector3.back, center);
                if (!plane.Raycast(ray, out enter)) return false;
                var hitLocalPoint = Matrix4x4.TRS(center, rotation, new Vector3(size.x, size.y, 1f)).inverse.MultiplyPoint3x4(ray.GetPoint(enter));
                return Mathf.Abs(hitLocalPoint.x) <= 0.5f && Mathf.Abs(hitLocalPoint.y) <= 0.5f;
            }

            public void GetSurfaceWorldCorners(Vector3[] fillArray)
            {
                var upDirection    = rotation * new Vector3(0f, size.y * 0.5f, 0f);
                var rightDirection = rotation * new Vector3(size.x * 0.5f, 0f, 0f);
                fillArray[0] = center - upDirection - rightDirection; // Bottom left
                fillArray[1] = center + upDirection - rightDirection; // Top left
                fillArray[2] = center + upDirection + rightDirection; // Top right
                fillArray[3] = center - upDirection + rightDirection; // Bottom right
            }
        }

        private static CustomRectHandles instance = null;

        private readonly List<Rect3D> snapshotRects = new List<Rect3D>(16);

        private bool    isPointerDown;
        private int     activeRect3DIndex = -1;
        private Vector3 previousHandlePosition;

        private Material outlineMaterial, fillMaterial;

        private readonly Rect3D[] singleRect3DArray       = new Rect3D[1];
        private readonly Color[]  singleOutlineColorArray = new Color[1];
        private readonly Color[]  singleFillColorArray    = new Color[1];

        private readonly Vector3[] rectWorldCorners       = new Vector3[4];
        private readonly object[]  resizeHandleParameters = new object[4];
        private readonly object[]  otherHandleParameters  = new object[3];

        private readonly Quaternion[] refAlignments = new Quaternion[]
        {
            Quaternion.LookRotation(Vector3.right, Vector3.up),
            Quaternion.LookRotation(Vector3.right, Vector3.forward),
            Quaternion.LookRotation(Vector3.up, Vector3.forward),
            Quaternion.LookRotation(Vector3.up, Vector3.right),
            Quaternion.LookRotation(Vector3.forward, Vector3.right),
            Quaternion.LookRotation(Vector3.forward, Vector3.up)
        };

        #region Reflection Variables

        private readonly MethodInfo moveHandlesGUI     = typeof(EditorWindow).Assembly.GetType("UnityEditor.RectTool").GetMethod("MoveHandlesGUI", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        private readonly MethodInfo rotationHandlesGUI = typeof(EditorWindow).Assembly.GetType("UnityEditor.RectTool").GetMethod("RotationHandlesGUI", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        private readonly MethodInfo resizeHandlesGUI   = typeof(EditorWindow).Assembly.GetType("UnityEditor.RectTool").GetMethod("ResizeHandlesGUI", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

        private readonly FieldInfo    s_Moving          = typeof(EditorWindow).Assembly.GetType("UnityEditor.RectTool").GetField("s_Moving", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        private readonly PropertyInfo minDragDifference = typeof(EditorWindow).Assembly.GetType("UnityEditor.ManipulationToolUtility").GetProperty("minDragDifference", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
#if UNITY_2020_1_OR_NEWER
        private readonly PropertyInfo incrementalSnapActive = typeof(EditorWindow).Assembly.GetType("UnityEditor.EditorSnapSettings").GetProperty("incrementalSnapActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        private readonly PropertyInfo gridSnapActive        = typeof(EditorWindow).Assembly.GetType("UnityEditor.EditorSnapSettings").GetProperty("gridSnapActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        private readonly PropertyInfo vertexSnapActive      = typeof(EditorWindow).Assembly.GetType("UnityEditor.EditorSnapSettings").GetProperty("vertexSnapActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

#endif
        private static readonly int SrcBlend = Shader.PropertyToID("_SrcBlend");
        private static readonly int DstBlend = Shader.PropertyToID("_DstBlend");
        private static readonly int Cull     = Shader.PropertyToID("_Cull");
        private static readonly int ZBias    = Shader.PropertyToID("_ZBias");

        #endregion

        private void OnEnable()
        {
            fillMaterial = new Material(Shader.Find("Hidden/Internal-Colored")) { hideFlags = HideFlags.HideAndDontSave };
            fillMaterial.SetInt(SrcBlend, (int)BlendMode.SrcAlpha);
            fillMaterial.SetInt(DstBlend, (int)BlendMode.OneMinusSrcAlpha);
            fillMaterial.SetInt(Cull, (int)CullMode.Off);
            fillMaterial.SetFloat(ZBias, -1f);

            outlineMaterial = new Material(Shader.Find("Hidden/Internal-Colored")) { hideFlags = HideFlags.HideAndDontSave };
            outlineMaterial.SetInt(SrcBlend, (int)BlendMode.SrcAlpha);
            outlineMaterial.SetInt(DstBlend, (int)BlendMode.OneMinusSrcAlpha);
            outlineMaterial.SetInt(Cull, (int)CullMode.Off);
            outlineMaterial.SetInt(ZBias, (int)CompareFunction.Always);
        }

        private void OnDisable()
        {
            if (fillMaterial)
            {
                DestroyImmediate(fillMaterial);
                fillMaterial = null;
            }

            if (outlineMaterial)
            {
                DestroyImmediate(outlineMaterial);
                outlineMaterial = null;
            }
        }

        public static void Draw(Rect3D rect, Color outlineColor, Color fillColor)
        {
            if (!instance)
                instance = CreateInstance<CustomRectHandles>();

            instance.singleRect3DArray[0]       = rect;
            instance.singleOutlineColorArray[0] = outlineColor;
            instance.singleFillColorArray[0]    = fillColor;

            instance.DrawInternal(instance.singleRect3DArray, instance.singleOutlineColorArray, instance.singleFillColorArray);
        }

        public static void Draw(IList<Rect3D> rects, Color outlineColor, Color fillColor)
        {
            if (!instance)
                instance = CreateInstance<CustomRectHandles>();

            instance.singleOutlineColorArray[0] = outlineColor;
            instance.singleFillColorArray[0]    = fillColor;

            instance.DrawInternal(rects, instance.singleOutlineColorArray, instance.singleFillColorArray);
        }

        public static void Draw(IList<Rect3D> rects, IList<Color> outlineColors, IList<Color> fillColors)
        {
            if (!instance)
                instance = CreateInstance<CustomRectHandles>();

            instance.DrawInternal(rects, outlineColors, fillColors);
        }

        public void DrawInternal(IList<Rect3D> rects, IList<Color> outlineColors, IList<Color> fillColors)
        {
            while (snapshotRects.Count < rects.Count)
                snapshotRects.Add(new Rect3D(Vector3.zero, Vector2.zero, Quaternion.identity));

            Event ev = Event.current;
            if (ev.type == EventType.MouseDown && ev.button == 0 && !ev.alt)
            {
                isPointerDown     = true;
                activeRect3DIndex = -1;

                for (int i = 0; i < rects.Count; i++)
                {
                    snapshotRects[i].center   = rects[i].center;
                    snapshotRects[i].size     = rects[i].size;
                    snapshotRects[i].rotation = rects[i].rotation;
                }
            }
            else if (ev.type == EventType.MouseUp && ev.button == 0)
            {
                isPointerDown = false;

                if (activeRect3DIndex >= 0)
                {
                    // RectTool's moveHandlesGUI function changes selection on mouse click (i.e. when mouse doesn't move after press: 's_Moving == false')
                    // We don't want that if user clicked on a Rect
                    // Source: https://github.com/Unity-Technologies/UnityCsReference/blob/61f92bd79ae862c4465d35270f9d1d57befd1761/Editor/Mono/GUI/Tools/BuiltinTools.cs#L889-L890
                    s_Moving.SetValue(null, true);
                }
            }

            // Draw outlines and surfaces
            if (ev.type == EventType.Repaint)
            {
                // Draw outlines
                outlineMaterial.SetPass(0);
                for (int i = 0; i < rects.Count; i++)
                {
                    Color outlineColor = outlineColors[Mathf.Min(i, outlineColors.Count - 1)];
                    if (outlineColor.a > 0f)
                    {
                        rects[i].GetSurfaceWorldCorners(rectWorldCorners);

                        GL.Begin(GL.LINES);
                        GL.Color(outlineColor);
                        for (int j = 0; j < 4; j++)
                        {
                            GL.Vertex(rectWorldCorners[j]);
                            GL.Vertex(rectWorldCorners[(j + 1) % 4]);
                        }

                        GL.End();
                    }
                }

                // Draw surfaces
                fillMaterial.SetPass(0);
                for (int i = 0; i < rects.Count; i++)
                {
                    Color fillColor = fillColors[Mathf.Min(i, fillColors.Count - 1)];
                    if (fillColor.a > 0f)
                    {
                        rects[i].GetSurfaceWorldCorners(rectWorldCorners);

                        GL.Begin(GL.TRIANGLES);
                        GL.Color(fillColor);
                        for (int j = 0; j < 2; j++)
                        {
                            GL.Vertex(rectWorldCorners[j * 2 + 0]);
                            GL.Vertex(rectWorldCorners[j * 2 + 1]);
                            GL.Vertex(rectWorldCorners[(j * 2 + 2) % 4]);
                        }

                        GL.End();
                    }
                }
            }

            // Draw rect handles
            // Credit: https://github.com/Unity-Technologies/UnityCsReference/blob/61f92bd79ae862c4465d35270f9d1d57befd1761/Editor/Mono/GUI/Tools/BuiltinTools.cs#L514-L574
            // Credit: https://github.com/Unity-Technologies/UnityCsReference/blob/61f92bd79ae862c4465d35270f9d1d57befd1761/Editor/Mono/GUI/Tools/TransformManipulator.cs
            Color handlesColor = Handles.color;
            int   controlID    = GUIUtility.hotControl;
            bool  isPaintEvent = (!isPointerDown || ev.type == EventType.Repaint);

            // Rect resize handles
            for (int i = 0; i < rects.Count; i++)
            {
                EditorGUI.BeginChangeCheck();
                object[] parameters = GetParametersForRectToolHandles(rects[i], snapshotRects[i], true, isPaintEvent);
                Vector3  newScale   = (Vector3)resizeHandlesGUI.Invoke(null, parameters);
                if (EditorGUI.EndChangeCheck() && isPointerDown && (activeRect3DIndex < 0 || activeRect3DIndex == i))
                {
                    Quaternion rectRotation = (Quaternion)parameters[2];
                    Vector3    scalePivot   = (Vector3)parameters[3];
                    SetScaleDelta(rects[i], snapshotRects[i], newScale, scalePivot, rectRotation);
                }

                if (GUIUtility.hotControl != controlID && activeRect3DIndex < 0)
                    activeRect3DIndex = i;
            }

            // Rect rotate handles
            for (int i = 0; i < rects.Count; i++)
            {
                EditorGUI.BeginChangeCheck();
                object[]   parameters  = GetParametersForRectToolHandles(rects[i], snapshotRects[i], false, isPaintEvent);
                Quaternion newRotation = (Quaternion)rotationHandlesGUI.Invoke(null, parameters);
                if (EditorGUI.EndChangeCheck() && isPointerDown && (activeRect3DIndex < 0 || activeRect3DIndex == i))
                {
                    // I have no idea what's going on here
                    Quaternion rectRotation = (Quaternion)parameters[2];
                    Quaternion delta        = Quaternion.Inverse(rectRotation) * newRotation;
                    delta.ToAngleAxis(out float angle, out Vector3 axis);
                    axis = rectRotation * axis;

                    rects[i].rotation *= Quaternion.Inverse(snapshotRects[i].rotation) * Quaternion.AngleAxis(angle, axis) * snapshotRects[i].rotation;
                    rects[i].rotation =  rects[i].rotation.normalized; // Without this, rotation eventually fails
                }

                if (GUIUtility.hotControl != controlID && activeRect3DIndex < 0)
                    activeRect3DIndex = i;
            }

            if (ev.type == EventType.MouseDown && activeRect3DIndex < 0)
            {
                // Neither resize handles nor rotate handles have captured the input; only move handles are left
                // Move handles should pick the Rect3D that is closest to the camera. So we raycast against
                // all Rect3Ds and consider the closest one the active Rect3D
                Ray   ray         = HandleUtility.GUIPointToWorldRay(ev.mousePosition);
                float minDistance = float.PositiveInfinity;
                for (int i = 0; i < rects.Count; i++)
                {
                    float enter;
                    if (rects[i].SurfaceRaycast(ray, out enter) && enter < minDistance)
                    {
                        activeRect3DIndex = i;
                        minDistance       = enter;
                    }
                }
            }

            // Rect move handles
            for (int i = 0; i < rects.Count; i++)
            {
                if (activeRect3DIndex == i)
                {
                    EditorGUI.BeginChangeCheck();
                    Vector3 newPosition = (Vector3)moveHandlesGUI.Invoke(null, GetParametersForRectToolHandles(rects[i], snapshotRects[i], false, isPaintEvent));
                    if (EditorGUI.EndChangeCheck() && isPointerDown && (activeRect3DIndex < 0 || activeRect3DIndex == i) && newPosition != previousHandlePosition)
                    {
                        previousHandlePosition = newPosition;
                        SetPositionDelta(rects[i], snapshotRects[i], newPosition - snapshotRects[i].center);
                    }
                }
            }

            // moveHandlesGUI modifies Handles.color and doesn't automatically reset it
            Handles.color = handlesColor;
        }

        #region Rect Tool Helper Functions

        private object[] GetParametersForRectToolHandles(Rect3D rect3D, Rect3D snapshotRect3D, bool isResizeHandle, bool isPaintEvent)
        {
            object[] result = isResizeHandle ? resizeHandleParameters : otherHandleParameters;
            result[0] = new Rect(rect3D.size * -0.5f, rect3D.size);
            result[1] = isPaintEvent ? rect3D.center : snapshotRect3D.center;
            result[2] = rect3D.rotation;

            return result;
        }

        // Credit: https://github.com/Unity-Technologies/UnityCsReference/blob/61f92bd79ae862c4465d35270f9d1d57befd1761/Editor/Mono/GUI/Tools/TransformManipulator.cs#L89-L141
        private void SetScaleDelta(Rect3D rect3D, Rect3D snapshotRect3D, Vector3 scaleDelta, Vector3 scalePivot, Quaternion scaleRotation)
        {
            SetPositionDelta(rect3D, snapshotRect3D, scaleRotation * Vector3.Scale(Quaternion.Inverse(scaleRotation) * (snapshotRect3D.center - scalePivot), scaleDelta) + scalePivot - snapshotRect3D.center);

            float      biggestDot   = Mathf.NegativeInfinity;
            Quaternion refAlignment = Quaternion.identity;
            for (int i = 0; i < refAlignments.Length; i++)
            {
                float dot = Mathf.Min(
                                      Mathf.Abs(Vector3.Dot(scaleRotation * Vector3.right, snapshotRect3D.rotation * refAlignments[i] * Vector3.right)),
                                      Mathf.Abs(Vector3.Dot(scaleRotation * Vector3.up, snapshotRect3D.rotation * refAlignments[i] * Vector3.up)),
                                      Mathf.Abs(Vector3.Dot(scaleRotation * Vector3.forward, snapshotRect3D.rotation * refAlignments[i] * Vector3.forward))
                                     );

                if (dot > biggestDot)
                {
                    biggestDot   = dot;
                    refAlignment = refAlignments[i];
                }
            }

            scaleDelta = refAlignment * scaleDelta;
            scaleDelta = Vector3.Scale(scaleDelta, refAlignment * Vector3.one);

            Vector3 scale = Vector3.Scale(snapshotRect3D.size, scaleDelta);
            rect3D.size = scale;
        }

        // Credit: https://github.com/Unity-Technologies/UnityCsReference/blob/61f92bd79ae862c4465d35270f9d1d57befd1761/Editor/Mono/GUI/Tools/TransformManipulator.cs#L148-L219
        private void SetPositionDelta(Rect3D rect3D, Rect3D snapshotRect3D, Vector3 positionDelta)
        {
            Vector3 newPosition = snapshotRect3D.center + positionDelta;
#if UNITY_2020_1_OR_NEWER
            if (!((bool)incrementalSnapActive.GetValue(null, null) || (bool)gridSnapActive.GetValue(null, null) || (bool)vertexSnapActive.GetValue(null, null)))
#endif
            {
                Vector3 minDifference = (Vector3)minDragDifference.GetValue(null, null);

                newPosition.x = Mathf.Approximately(positionDelta.x, 0f) ? snapshotRect3D.center.x : RoundBasedOnMinimumDifference(newPosition.x, minDifference.x);
                newPosition.y = Mathf.Approximately(positionDelta.y, 0f) ? snapshotRect3D.center.y : RoundBasedOnMinimumDifference(newPosition.y, minDifference.y);
                newPosition.z = Mathf.Approximately(positionDelta.z, 0f) ? snapshotRect3D.center.z : RoundBasedOnMinimumDifference(newPosition.z, minDifference.z);
            }

            rect3D.center = newPosition;
        }

        // Credit: https://github.com/Unity-Technologies/UnityCsReference/blob/61f92bd79ae862c4465d35270f9d1d57befd1761/Editor/Mono/Utils/MathUtils.cs#L68-L73
        private float RoundBasedOnMinimumDifference(float valueToRound, float minDifference)
        {
            if (minDifference == 0)
            {
                int decimals = Mathf.Clamp((int)(5 - Mathf.Log10(Mathf.Abs(valueToRound))), 0, 15);
                return (float)System.Math.Round(valueToRound, decimals, System.MidpointRounding.AwayFromZero);
            }

            return (float)System.Math.Round(valueToRound, Mathf.Clamp(-Mathf.FloorToInt(Mathf.Log10(Mathf.Abs(minDifference))), 0, 15), System.MidpointRounding.AwayFromZero);
        }

        #endregion
    }
}