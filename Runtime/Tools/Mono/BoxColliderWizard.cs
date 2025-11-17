/*|============|*|
|*|Author:     |*| xinan
|*|Date:       |*| 2025-10-12
|*|E-Mail:     |*| xinansky99@gmail.com
|*|============|*/

using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEditorInternal;

#endif

namespace AIO
{
    [HelpURL("https://gist.github.com/yasirkula/eeceb4626a53c4b892ec73e348642c5c")]
    public class BoxColliderWizard : MonoBehaviour
    {
#if UNITY_EDITOR
        [ContextMenu("Help")]
        private void ShowHelp()
        {
            EditorUtility.DisplayDialog("Help",
                                        "In Edit mode, you can edit the active child colliders using Move, Rotate, Scale and Rect tools. " +
                                        "These colliders need to have 0,0,0 Center and 1,1,1 Size values.\n\n" +
                                        "The following shortcuts work while Scene view has focus:\n" +
                                        "- Tab: toggle active mode\n" +
                                        "- Z: toggle Pivot Position\n" +
                                        "- X: toggle Collider Alignment (in Create mode)", "OK");
        }
#endif
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(BoxColliderWizard))]
    [CanEditMultipleObjects]
    public class BoxColliderWizardEditor : Editor
    {
        private enum Mode
        {
            None,
            Create,
            Edit,
            Delete
        };

        private enum VolumeDrawMode
        {
            None,
            BaseOnly,
            VolumeOutline,
            VolumeFilled
        };

        private enum PivotPosition
        {
            Surface,
            Center
        };

        private enum CreationStage
        {
            PendingClick,
            SetLength,
            SetWidth,
            SetHeight
        };

        private enum ColliderAlignment
        {
            XZ,
            XY,
            YZ,
            RaycastDetermines
        };

        private class ColliderHolder
        {
            public readonly Collider  collider;
            public readonly Transform colliderTransform;

            // Values used in Rect Tool
            public Vector3    snapshotPosition;
            public Quaternion snapshotRotation;
            public Vector3    snapshotScale;

            public bool   isActiveCollider, isBeingCreated;
            public double lastActiveTime;

            public Vector3 position
            {
                get { return colliderTransform.position; }
                set
                {
                    if (!isBeingCreated)
                        Undo.RecordObject(colliderTransform, "Modify Collider");

                    colliderTransform.position = value;
                }
            }

            public Quaternion rotation
            {
                get { return colliderTransform.rotation; }
                set
                {
                    if (!isBeingCreated)
                        Undo.RecordObject(colliderTransform, "Modify Collider");

                    colliderTransform.rotation = value;
                }
            }

            public Vector3 scale
            {
                get { return colliderTransform.lossyScale; }
                set
                {
                    if (!isBeingCreated)
                        Undo.RecordObject(colliderTransform, "Modify Collider");

                    Transform parent       = colliderTransform.parent;
                    int       siblingIndex = colliderTransform.GetSiblingIndex();
                    colliderTransform.SetParent(null, true);
                    try
                    {
                        colliderTransform.localScale = new Vector3(Mathf.Abs(value.x), Mathf.Abs(value.y), Mathf.Abs(value.z));
                    }
                    finally
                    {
                        colliderTransform.SetParent(parent, true);
                        colliderTransform.SetSiblingIndex(siblingIndex);
                    }
                }
            }

            public Vector3 surfacePosition
            {
                get { return (pivotPosition == PivotPosition.Center) ? colliderTransform.position : (colliderTransform.position - colliderTransform.rotation * new Vector3(0f, colliderTransform.lossyScale.y * 0.5f, 0f)); }
                set
                {
                    if (!isBeingCreated)
                        Undo.RecordObject(colliderTransform, "Modify Collider");

                    colliderTransform.position = (pivotPosition == PivotPosition.Center) ? value : (value + colliderTransform.rotation * new Vector3(0f, colliderTransform.lossyScale.y * 0.5f, 0f));
                }
            }

            public ColliderHolder(Collider collider)
            {
                this.collider     = collider;
                colliderTransform = collider.transform;
            }

            public void TakeSnapshot()
            {
                snapshotPosition = colliderTransform.position;
                snapshotRotation = colliderTransform.rotation;
                snapshotScale    = colliderTransform.lossyScale;

                if (pivotPosition == PivotPosition.Surface)
                    snapshotPosition -= snapshotRotation * new Vector3(0f, snapshotScale.y * 0.5f, 0f);

                isActiveCollider = false;
            }

            public bool SurfaceRaycast(Ray ray, out float enter)
            {
                Vector3 surfacePosition = this.surfacePosition;
                Plane   plane           = new Plane(rotation * Vector3.up, surfacePosition);
                if (!plane.Raycast(ray, out enter))
                    return false;

                Vector3 hitLocalPoint = Matrix4x4.TRS(surfacePosition, rotation, new Vector3(scale.x, 1f, scale.z)).inverse.MultiplyPoint3x4(ray.GetPoint(enter));
                return Mathf.Abs(hitLocalPoint.x) <= 0.5f && Mathf.Abs(hitLocalPoint.z) <= 0.5f;
            }

            public void GetSurfaceWorldCorners(Vector3[] fillArray)
            {
                Vector3    position = colliderTransform.position;
                Quaternion rotation = colliderTransform.rotation;
                Vector3    scale    = colliderTransform.lossyScale;

                if (pivotPosition == PivotPosition.Surface)
                    position -= rotation * new Vector3(0f, scale.y * 0.5f, 0f);

                Vector3 tangent   = rotation * new Vector3(0f, 0f, scale.z * 0.5f);
                Vector3 bitangent = rotation * new Vector3(scale.x * 0.5f, 0f, 0f);

                fillArray[0] = position - tangent - bitangent; // Rear left
                fillArray[1] = position + tangent - bitangent; // Forward left
                fillArray[2] = position + tangent + bitangent; // Forward right
                fillArray[3] = position - tangent + bitangent; // Rear right
            }
        }

        private readonly Color COLLIDER_FILL_COLOR                                = new Color(0f, 1f, 0f, 0.3f);
        private readonly Color COLLIDER_FILL_COLOR_DELETE                         = new Color(1f, 0f, 0f, 0.3f);
        private readonly Color COLLIDER_OUTLINE_COLOR                             = new Color(1f, 1f, 1f, 1f);
        private readonly Color COLLIDER_CREATION_GRIDLINES_COLOR                  = new Color(1f, 1f, 1f, 0.85f);
        private readonly Color COLLIDER_CREATION_GRIDLINES_OBSTRUCTED_COLOR       = new Color(0.5f, 0.5f, 0.5f, 0.85f);
        private readonly Color COLLIDER_CREATION_REFERENCE_PLANE_COLOR            = new Color(0f, 0f, 1f, 0.3f);
        private readonly Color COLLIDER_CREATION_REFERENCE_PLANE_OBSTRUCTED_COLOR = new Color(1f, 0f, 0f, 0.3f);

        #region Saved Properties

        private Mode? m_mode;

        private Mode mode
        {
            get
            {
                if (!m_mode.HasValue) m_mode = (Mode)EditorPrefs.GetInt("BCWMode", (int)Mode.None);
                return m_mode.Value;
            }
            set
            {
                if (mode != value)
                {
                    m_mode       = value;
                    Tools.hidden = (value != Mode.None);

                    EditorApplication.update -= SceneView.RepaintAll;
                    if (value == Mode.Delete)
                        EditorApplication.update += SceneView.RepaintAll;

                    if (colliderCreationStage != CreationStage.PendingClick)
                    {
                        colliderCreationStage = CreationStage.PendingClick;
                        DestroyUnfinishedCollider();
                    }

                    EditorPrefs.SetInt("BCWMode", (int)value);
                }
            }
        }

        private VolumeDrawMode? m_volumeDrawMode;

        private VolumeDrawMode volumeDrawMode
        {
            get
            {
                if (!m_volumeDrawMode.HasValue) m_volumeDrawMode = (VolumeDrawMode)EditorPrefs.GetInt("BCWVolume", (int)VolumeDrawMode.VolumeOutline);
                return m_volumeDrawMode.Value;
            }
            set
            {
                if (volumeDrawMode != value)
                {
                    m_volumeDrawMode = value;
                    EditorPrefs.SetInt("BCWVolume", (int)value);
                }
            }
        }

        private static PivotPosition? m_pivotPosition;

        private static PivotPosition pivotPosition
        {
            get
            {
                if (!m_pivotPosition.HasValue) m_pivotPosition = (PivotPosition)EditorPrefs.GetInt("BCWPivot", (int)PivotPosition.Surface);
                return m_pivotPosition.Value;
            }
            set
            {
                if (pivotPosition != value)
                {
                    m_pivotPosition = value;
                    EditorPrefs.SetInt("BCWPivot", (int)value);
                }
            }
        }

        private ColliderAlignment? m_colliderCreationAlignment;

        private ColliderAlignment colliderCreationAlignment
        {
            get
            {
                if (!m_colliderCreationAlignment.HasValue) m_colliderCreationAlignment = (ColliderAlignment)EditorPrefs.GetInt("BCWCAlignment", (int)ColliderAlignment.XZ);
                return m_colliderCreationAlignment.Value;
            }
            set
            {
                if (colliderCreationAlignment != value)
                {
                    m_colliderCreationAlignment = value;
                    EditorPrefs.SetInt("BCWCAlignment", (int)value);
                }
            }
        }

        private bool? m_showColliderProperties;

        private bool showColliderProperties
        {
            get
            {
                if (!m_showColliderProperties.HasValue) m_showColliderProperties = EditorPrefs.GetBool("BCWShowProp", true);
                return m_showColliderProperties.Value;
            }
            set
            {
                if (showColliderProperties != value)
                {
                    m_showColliderProperties = value;
                    EditorPrefs.SetBool("BCWShowProp", value);
                }
            }
        }

        #endregion

        private BoxColliderWizard mainWizard;

        private readonly List<ColliderHolder> colliders        = new List<ColliderHolder>(64);
        private readonly List<ColliderHolder> visibleColliders = new List<ColliderHolder>(64);

        private Material fillMaterial, outlineMaterial;

        private bool    isPointerDown, isRightMouseButtonDown, isRightClick;
        private Vector3 previousHandlePosition;
        private bool?   colliderGizmosWereActive;

        private CreationStage colliderCreationStage = CreationStage.PendingClick;
        private Vector3?      colliderCreationReferencePlaneCenter;
        private Vector3       colliderCreationPreviousPoint, colliderCreationClickedPoint;
        private Quaternion    colliderCreationOrientation;

        private double  rightClickTime;
        private Vector2 rightClickPosition;

#if UNITY_2017_1_OR_NEWER
        private readonly BoxBoundsHandle boundsHandle = new BoxBoundsHandle();
#else
	private readonly BoxBoundsHandle boundsHandle = new BoxBoundsHandle( 1453541 );
#endif

#if UNITY_2017_3_OR_NEWER
        private readonly Plane[] sceneCameraFrustumPlanes = new Plane[6];
#endif

        private readonly string[] modeLabels = new string[4] { "None", "Create", "Edit", "Delete" };

        private readonly Vector3[] colliderSurfaceWorldCorners = new Vector3[4];
        private readonly object[]  resizeHandleParameters      = new object[4];
        private readonly object[]  otherHandleParameters       = new object[3];

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

        private readonly MethodInfo moveHandlesGUI = typeof(EditorWindow).Assembly.GetType("UnityEditor.RectTool").GetMethod("MoveHandlesGUI", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
#pragma warning disable 0414 // Will be needed if Rect tool's rotate handles are uncommented
        private readonly MethodInfo rotationHandlesGUI = typeof(EditorWindow).Assembly.GetType("UnityEditor.RectTool").GetMethod("RotationHandlesGUI", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
#pragma warning restore 0414
        private readonly MethodInfo resizeHandlesGUI = typeof(EditorWindow).Assembly.GetType("UnityEditor.RectTool").GetMethod("ResizeHandlesGUI", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        private readonly MethodInfo raycastWorld     = typeof(EditorWindow).Assembly.GetType("UnityEditor.SceneViewMotion").GetMethod("RaycastWorld", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

        private readonly FieldInfo    s_Moving          = typeof(EditorWindow).Assembly.GetType("UnityEditor.RectTool").GetField("s_Moving", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        private readonly PropertyInfo minDragDifference = typeof(EditorWindow).Assembly.GetType("UnityEditor.ManipulationToolUtility").GetProperty("minDragDifference", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
#if UNITY_2020_1_OR_NEWER
        private readonly PropertyInfo incrementalSnapActive = typeof(EditorWindow).Assembly.GetType("UnityEditor.EditorSnapSettings").GetProperty("incrementalSnapActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        private readonly PropertyInfo gridSnapActive        = typeof(EditorWindow).Assembly.GetType("UnityEditor.EditorSnapSettings").GetProperty("gridSnapActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
        private readonly PropertyInfo vertexSnapActive      = typeof(EditorWindow).Assembly.GetType("UnityEditor.EditorSnapSettings").GetProperty("vertexSnapActive", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
#endif

        #endregion

        private void OnEnable()
        {
            mainWizard = (BoxColliderWizard)target;

            InitializeColliders();

            fillMaterial = new Material(Shader.Find("Hidden/Internal-Colored")) { hideFlags = HideFlags.HideAndDontSave };
            fillMaterial.SetInt("_SrcBlend", (int)BlendMode.SrcAlpha);
            fillMaterial.SetInt("_DstBlend", (int)BlendMode.OneMinusSrcAlpha);
            fillMaterial.SetInt("_Cull", (int)CullMode.Off);
            fillMaterial.SetInt("_ZWrite", (int)CompareFunction.Disabled);
            fillMaterial.SetFloat("_ZBias", -1f);

            outlineMaterial = new Material(Shader.Find("Hidden/Internal-Colored")) { hideFlags = HideFlags.HideAndDontSave };
            outlineMaterial.SetInt("_SrcBlend", (int)BlendMode.SrcAlpha);
            outlineMaterial.SetInt("_DstBlend", (int)BlendMode.OneMinusSrcAlpha);
            outlineMaterial.SetInt("_Cull", (int)CullMode.Off);
            outlineMaterial.SetInt("_ZTest", (int)CompareFunction.Always);

            Tools.hidden = (mode != Mode.None);

            if (mode == Mode.Delete)
            {
                EditorApplication.update -= SceneView.RepaintAll;
                EditorApplication.update += SceneView.RepaintAll;
            }

            // Undo&redo might delete/restore a collider we are editing, refresh the colliders list to be safe
            Undo.undoRedoPerformed -= InitializeColliders;
            Undo.undoRedoPerformed += InitializeColliders;

#if !UNITY_2018_4_OR_NEWER
		// Unity doesn't call OnDisable automatically before code compilation on Unity 5.6 but it does on 2018.4.
		// I don't know which intermediate versions call OnDisable automatically, so me manually call OnDisable on
		// versions up to 2018.4 just before code compilation
		EditorApplication.update -= CallOnDisableBeforeCompilation;
		EditorApplication.update += CallOnDisableBeforeCompilation;
#endif
#if !UNITY_2019_4_OR_NEWER
		// On older Unity versions, editing or creating colliders while the wizard isn't uniformly scaled causes the
		// collider's scale to skyrocket. Thus, we won't activate the wizard on those versions until it is uniformly scaled
		EditorApplication.update -= ShowErrorWhenWizardIsSkewed;
		EditorApplication.update += ShowErrorWhenWizardIsSkewed;
#endif
        }

        private void OnDisable()
        {
            Tools.hidden = false;

            EditorApplication.update -= SceneView.RepaintAll;
            Undo.undoRedoPerformed   -= InitializeColliders;
#if !UNITY_2018_4_OR_NEWER
		EditorApplication.update -= CallOnDisableBeforeCompilation;
#endif
#if !UNITY_2019_4_OR_NEWER
		EditorApplication.update -= ShowErrorWhenWizardIsSkewed;
#endif

            // Restore BoxCollider gizmos state
            if (colliderGizmosWereActive.HasValue)
            {
                ColliderHolder existingBoxCollider = colliders.Find((collider) => collider.collider);
                if (existingBoxCollider != null)
                    InternalEditorUtility.SetIsInspectorExpanded(existingBoxCollider.collider, colliderGizmosWereActive.Value);
                else
                {
                    // Use a dummy BoxCollider to toggle gizmos
                    BoxCollider dummyCollider = new GameObject().AddComponent<BoxCollider>();
                    try
                    {
                        InternalEditorUtility.SetIsInspectorExpanded(dummyCollider, colliderGizmosWereActive.Value);
                    }
                    finally
                    {
                        DestroyImmediate(dummyCollider.gameObject);
                    }
                }

                colliderGizmosWereActive = null;
            }

            if (colliderCreationStage != CreationStage.PendingClick)
            {
                colliderCreationStage = CreationStage.PendingClick;
                DestroyUnfinishedCollider();
            }

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

        private void InitializeColliders()
        {
            if (colliderCreationStage != CreationStage.PendingClick)
            {
                colliderCreationStage = CreationStage.PendingClick;
                DestroyUnfinishedCollider();
            }

            colliders.Clear();

            foreach (Object target in targets)
            {
                BoxColliderWizard wizard = (BoxColliderWizard)target;
                foreach (BoxCollider boxCollider in wizard.GetComponentsInChildren<BoxCollider>(false))
                {
                    if (boxCollider && boxCollider.transform != wizard.transform && boxCollider.center == Vector3.zero && boxCollider.size == Vector3.one)
                    {
                        colliders.Add(new ColliderHolder(boxCollider));

                        if (!colliderGizmosWereActive.HasValue)
                        {
                            // Hide BoxCollider gizmos
                            colliderGizmosWereActive = InternalEditorUtility.GetIsInspectorExpanded(boxCollider);
                            InternalEditorUtility.SetIsInspectorExpanded(boxCollider, false);
                        }
                    }
                }
            }

            if (!colliderGizmosWereActive.HasValue)
            {
                // Use a dummy BoxCollider to toggle gizmos
                BoxCollider dummyCollider = new GameObject().AddComponent<BoxCollider>();
                try
                {
                    colliderGizmosWereActive = InternalEditorUtility.GetIsInspectorExpanded(dummyCollider);
                    InternalEditorUtility.SetIsInspectorExpanded(dummyCollider, false);
                }
                finally
                {
                    DestroyImmediate(dummyCollider.gameObject);
                }
            }
        }

        private void DestroyUnfinishedCollider()
        {
            ColliderHolder colliderToCreate = colliders.Find((collider) => collider.isBeingCreated);
            if (colliderToCreate != null)
            {
                colliders.Remove(colliderToCreate);
                DestroyImmediate(colliderToCreate.collider.gameObject);
            }
        }

#if !UNITY_2018_4_OR_NEWER
	private void CallOnDisableBeforeCompilation()
	{
		if( EditorApplication.isCompiling )
			OnDisable();
	}
#endif

#if !UNITY_2019_4_OR_NEWER
	private void ShowErrorWhenWizardIsSkewed()
	{
		// Check if the wizard is skewed (not uniformly-scaled). When it is skewed, SceneView shows error message at cursor position
		// and we want to constantly repaint Scene view to keep this error message's position correct
		Vector3 wizardScale = mainWizard.transform.lossyScale;
		if( !Mathf.Approximately( wizardScale.x, wizardScale.y ) || !Mathf.Approximately( wizardScale.x, wizardScale.z ) )
			SceneView.RepaintAll();
	}
#endif

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            mode = (Mode)GUILayout.Toolbar((int)mode, modeLabels);

            EditorGUILayout.Space();

            volumeDrawMode = (VolumeDrawMode)EditorGUILayout.EnumPopup("Draw Volume", volumeDrawMode);
            pivotPosition  = (PivotPosition)EditorGUILayout.EnumPopup("Pivot Position (Z)", pivotPosition);

            if (mode == Mode.Create)
            {
                GUI.enabled               = colliderCreationStage == CreationStage.PendingClick || colliderCreationStage == CreationStage.SetLength;
                colliderCreationAlignment = (ColliderAlignment)EditorGUILayout.EnumPopup("Collider Alignment (X)", colliderCreationAlignment);

                if (colliderCreationReferencePlaneCenter.HasValue)
                    EditorGUILayout.HelpBox("Creating colliders on a reference plane (world geometry is ignored as long as reference plane is hit). Right click to disable the reference plane", MessageType.Info);
                else
                    EditorGUILayout.HelpBox("To create colliders on a reference plane that is centered at a specified point, hold CTRL+Shift and left click the target position", MessageType.None);

                if (colliderCreationStage != CreationStage.PendingClick)
                {
                    ColliderHolder colliderToCreate = colliders.Find((collider) => collider.isBeingCreated);
                    if (colliderToCreate != null)
                    {
                        GUI.enabled = false;

                        EditorGUILayout.Space();
                        DrawColliderProperties(colliderToCreate);
                    }
                }

                GUI.enabled = true;
            }
            else if (colliders.Count > 0)
            {
                EditorGUILayout.Space();

#if UNITY_2019_1_OR_NEWER
                showColliderProperties = EditorGUILayout.BeginFoldoutHeaderGroup(showColliderProperties, "Colliders");
#else
			showColliderProperties = EditorGUILayout.Foldout( showColliderProperties, "Colliders", true );
#endif

                if (showColliderProperties)
                {
                    foreach (ColliderHolder collider in colliders)
                    {
                        DrawColliderProperties(collider);
                        EditorGUILayout.Space();
                    }
                }

#if UNITY_2019_1_OR_NEWER
                EditorGUILayout.EndFoldoutHeaderGroup();
#endif
            }

            if (EditorGUI.EndChangeCheck())
                SceneView.RepaintAll();
        }

        private void DrawColliderProperties(ColliderHolder collider)
        {
            GUILayout.Box(collider.collider.name, GUILayout.ExpandWidth(true));

            bool  wideMode   = EditorGUIUtility.wideMode;
            float labelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.wideMode   = true;
            EditorGUIUtility.labelWidth = 60f;

            EditorGUI.BeginChangeCheck();

            EditorGUI.BeginChangeCheck();
            Vector3 position = EditorGUILayout.Vector3Field("Position", collider.colliderTransform.localPosition);
            if (EditorGUI.EndChangeCheck())
            {
                if (!collider.isBeingCreated)
                    Undo.RecordObject(collider.colliderTransform, "Modify Collider");

                collider.colliderTransform.localPosition = position;
            }

            EditorGUI.BeginChangeCheck();
            Vector3 rotation = EditorGUILayout.Vector3Field("Rotation", collider.colliderTransform.localEulerAngles);
            if (EditorGUI.EndChangeCheck())
            {
                if (!collider.isBeingCreated)
                    Undo.RecordObject(collider.colliderTransform, "Modify Collider");

                collider.colliderTransform.localEulerAngles = rotation;
            }

            EditorGUI.BeginChangeCheck();
            Vector3 scale = EditorGUILayout.Vector3Field("Scale", collider.colliderTransform.localScale);
            if (EditorGUI.EndChangeCheck())
            {
                if (!collider.isBeingCreated)
                    Undo.RecordObject(collider.colliderTransform, "Modify Collider");

                collider.colliderTransform.localScale = scale;
            }

            if (EditorGUI.EndChangeCheck())
                SceneView.RepaintAll();

            EditorGUIUtility.labelWidth = labelWidth;
            EditorGUIUtility.wideMode   = wideMode;
        }

        private void OnSceneGUI()
        {
            // Draw scene GUI only once, not per selected BoxColliderWizard
            if (target != mainWizard)
                return;

            if (volumeDrawMode == VolumeDrawMode.None)
                return;

#if !UNITY_2018_4_OR_NEWER
		// OnDisable is called manually just before code recompilation, we can't call OnSceneGUI until compilation is completed
		if( !fillMaterial || !outlineMaterial )
			return;
#endif

            Event ev = Event.current;

            HandleSceneGUIInput(ev);

            ColliderHolder activeCollider   = colliders.Find((collider) => collider.isActiveCollider);
            ColliderHolder colliderToCreate = colliders.Find((collider) => collider.isBeingCreated);
            ColliderHolder colliderToDelete = null;
            if (mode == Mode.Delete)
            {
                Ray   ray                     = HandleUtility.GUIPointToWorldRay(ev.mousePosition);
                float closestColliderDistance = float.PositiveInfinity;
                foreach (ColliderHolder collider in colliders)
                {
                    RaycastHit hit;
                    if (collider.collider.Raycast(ray, out hit, float.PositiveInfinity) && hit.distance < closestColliderDistance)
                    {
                        closestColliderDistance = hit.distance;
                        colliderToDelete        = collider;
                    }
                }
            }

            // Find colliders that are visible to Scene view camera
#if UNITY_2017_3_OR_NEWER
            GeometryUtility.CalculateFrustumPlanes(SceneView.currentDrawingSceneView.camera, sceneCameraFrustumPlanes);
#else
		Plane[] sceneCameraFrustumPlanes = GeometryUtility.CalculateFrustumPlanes( SceneView.currentDrawingSceneView.camera );
#endif
            visibleColliders.Clear();
            foreach (ColliderHolder collider in colliders)
            {
                Vector3 scale             = collider.scale;
                float   maxScaleComponent = scale.x;
                if (scale.y > maxScaleComponent)
                    maxScaleComponent = scale.y;
                if (scale.z > maxScaleComponent)
                    maxScaleComponent = scale.z;

                if (GeometryUtility.TestPlanesAABB(sceneCameraFrustumPlanes, new Bounds(collider.position, new Vector3(maxScaleComponent, maxScaleComponent, maxScaleComponent))))
                    visibleColliders.Add(collider);
            }

            // Frame to the collider when F key is pressed
            if ((mode == Mode.Create || mode == Mode.Edit) && ev.type == EventType.KeyDown && ev.keyCode == KeyCode.F)
            {
                ColliderHolder colliderToFocus = colliderToCreate ?? activeCollider;
                if (colliderToFocus != null)
                {
#if UNITY_2018_2_OR_NEWER
                    SceneView.currentDrawingSceneView.Frame(colliderToFocus.collider.bounds, false);
#else
				float boundsSize = colliderToFocus.collider.bounds.extents.magnitude * 3.3f;
				if( boundsSize != Mathf.Infinity )
				{
					if( boundsSize == 0f )
						boundsSize = 10f;

					SceneView sceneView = SceneView.currentDrawingSceneView;
					EditorApplication.delayCall += () => sceneView.LookAt( colliderToFocus.collider.bounds.center, sceneView.rotation, boundsSize * 2.2f, sceneView.orthographic, true );
				}
#endif
                    ev.Use();
                }
            }

            if (ev.type == EventType.Repaint)
                DrawColliderVisuals(colliderToDelete, ev);

            bool isPanningWindow = (ev.alt && !isPointerDown);
            if (volumeDrawMode != VolumeDrawMode.BaseOnly)
                DrawColliderBounds(visibleColliders, !isPanningWindow && Tools.current == Tool.Rect && mode == Mode.Edit);
            else if (colliderToCreate != null) // Always draw the created collider's Bounds
                DrawColliderBounds(new ColliderHolder[1] { colliderToCreate }, false);

#if !UNITY_2019_4_OR_NEWER
		if( mode != Mode.None )
		{
			// This plugin doesn't work well with skewed colliders on old Unity versions, so don't run the plugin at all if this is the case
			Vector3 wizardScale = mainWizard.transform.lossyScale;
			if( !Mathf.Approximately( wizardScale.x, wizardScale.y ) || !Mathf.Approximately( wizardScale.x, wizardScale.z ) )
			{
				// Show error message at cursor position
				Handles.BeginGUI();
				Vector2 cursorPos = HandleUtility.GUIPointToScreenPixelCoordinate( ev.mousePosition );
				cursorPos.y = Screen.height - cursorPos.y;
				EditorGUI.HelpBox( new Rect( cursorPos - new Vector2( 100f, 90f ), new Vector2( 200f, 45f ) ), "BoxColliderWizard doesn't work when the wizard's Transform isn't uniformly scaled (skewed).", MessageType.Error );
				Handles.EndGUI();

				return;
			}
		}
#endif

            if (mode == Mode.None || isPanningWindow)
                return;

            switch (mode)
            {
                case Mode.Create:
                {
                    EditorGUI.BeginChangeCheck();
                    HandleCreateMode(ref colliderToCreate, ev);
                    if (EditorGUI.EndChangeCheck())
                        Repaint();

                    break;
                }
                case Mode.Edit:
                {
                    EditorGUI.BeginChangeCheck();
                    HandleEditMode(ref activeCollider, ev);
                    if (EditorGUI.EndChangeCheck())
                        Repaint();

                    break;
                }
                case Mode.Delete:
                {
                    HandleDeleteMode(ref colliderToDelete, ev);
                    break;
                }
            }

            if (activeCollider != null)
            {
                activeCollider.lastActiveTime   = EditorApplication.timeSinceStartup;
                activeCollider.isActiveCollider = true;
            }

            HandleUtility.AddDefaultControl(0);
        }

        private void HandleSceneGUIInput(Event ev)
        {
            isRightClick = false;

            if (ev.type == EventType.MouseDown)
            {
                if (ev.button == 0 && !ev.alt) // Left click
                {
                    foreach (ColliderHolder collider in colliders)
                        collider.TakeSnapshot();

                    isPointerDown = true;
                }
                else if (ev.button == 1) // Right click
                {
                    isRightMouseButtonDown = true;

                    if (!ev.alt)
                    {
                        rightClickTime     = EditorApplication.timeSinceStartup;
                        rightClickPosition = ev.mousePosition;
                    }
                }
            }
            else if (ev.type == EventType.MouseUp)
            {
                if (ev.button == 0) // Left release
                {
                    isPointerDown = false;

                    // RectTool's moveHandlesGUI function changes selection on mouse click (i.e. when mouse doesn't move after press: 's_Moving == false'). We don't want that
                    // Source: https://github.com/Unity-Technologies/UnityCsReference/blob/61f92bd79ae862c4465d35270f9d1d57befd1761/Editor/Mono/GUI/Tools/BuiltinTools.cs#L889-L890
                    if (mode == Mode.Edit)
                        s_Moving.SetValue(null, true);
                }
                else if (ev.button == 1) // Right release
                {
                    isRightMouseButtonDown = false;

                    if (EditorApplication.timeSinceStartup - rightClickTime <= 0.3f && Vector2.Distance(ev.mousePosition, rightClickPosition) <= 5f)
                        isRightClick = true;
                }
            }
            else if (ev.type == EventType.KeyDown && !ev.control && !ev.command)
            {
                if (ev.keyCode == KeyCode.Tab && !ev.alt) // Toggle mode
                {
                    mode = (Mode)((int)mode + (ev.shift ? -1 : 1));
                    if ((int)mode >= 4)
                        mode = (Mode)0;
                    else if ((int)mode < 0)
                        mode = (Mode)3;

                    Repaint();
                    ev.Use();
                }
                else if (ev.character == '\t' && !ev.alt)
                {
                    // Unity sends (keyCode == KeyCode.Tab) event and (character == '\t') event separately (but consecutively).
                    // If we don't eat both of them, upon clicking Tab, Scene View's search field gains focus. We need to change
                    // 'mode' in only one of them because we want to switch one tab per Tab click, not two
                    ev.Use();
                }
                else if (ev.keyCode == KeyCode.Z) // Toggle pivotPosition
                {
                    pivotPosition = (PivotPosition)(((int)pivotPosition + 1) % 2);

                    Repaint();
                    ev.Use();
                }
                else if (ev.keyCode == KeyCode.X && mode == Mode.Create) // Toggle colliderCreationAlignment
                {
                    colliderCreationAlignment = (ColliderAlignment)(((int)colliderCreationAlignment + 1) % 4);

                    Repaint();
                    ev.Use();
                }
            }
        }

        private void DrawColliderVisuals(ColliderHolder colliderToDelete, Event ev)
        {
            Matrix4x4 handlesMatrix = Handles.matrix;
            Color     handlesColor  = Handles.color;

            if (volumeDrawMode == VolumeDrawMode.BaseOnly || pivotPosition == PivotPosition.Center)
            {
                // Draw all outlines first so that they can be obstructed by VolumeFilled
                outlineMaterial.SetPass(0);
                foreach (ColliderHolder collider in visibleColliders)
                {
                    collider.GetSurfaceWorldCorners(colliderSurfaceWorldCorners);

                    GL.Begin(GL.LINES);
                    GL.Color(COLLIDER_OUTLINE_COLOR);
                    for (int i = 0; i < 4; i++)
                    {
                        GL.Vertex(colliderSurfaceWorldCorners[i]);
                        GL.Vertex(colliderSurfaceWorldCorners[(i + 1) % 4]);
                    }

                    GL.End();
                }
            }

            if (volumeDrawMode != VolumeDrawMode.VolumeFilled && mode != Mode.Delete)
            {
                // Draw surface only
                fillMaterial.SetPass(0);
                foreach (ColliderHolder collider in visibleColliders)
                {
                    collider.GetSurfaceWorldCorners(colliderSurfaceWorldCorners);

                    GL.Begin(GL.TRIANGLES);
                    GL.Color(COLLIDER_FILL_COLOR);
                    for (int i = 0; i < 2; i++)
                    {
                        GL.Vertex(colliderSurfaceWorldCorners[i * 2 + 0]);
                        GL.Vertex(colliderSurfaceWorldCorners[i * 2 + 1]);
                        GL.Vertex(colliderSurfaceWorldCorners[(i * 2 + 2) % 4]);
                    }

                    GL.End();
                }
            }
            else
            {
                // Draw whole volume
                Color           volumeFillColor = new Color(COLLIDER_FILL_COLOR.r, COLLIDER_FILL_COLOR.g, COLLIDER_FILL_COLOR.b, COLLIDER_FILL_COLOR.a * 0.5f);
                bool            handlesLighting = Handles.lighting;
                CompareFunction zTest           = Handles.zTest;
                Handles.zTest    = CompareFunction.LessEqual;
                Handles.lighting = false;

                foreach (ColliderHolder collider in visibleColliders)
                {
                    Handles.color  = (collider != colliderToDelete) ? volumeFillColor : COLLIDER_FILL_COLOR_DELETE;
                    Handles.matrix = Matrix4x4.TRS(collider.position, collider.rotation, collider.scale);
                    Handles.CubeHandleCap(0, Vector3.zero, Quaternion.identity, 1f, ev.type);
                }

                Handles.lighting = handlesLighting;
                Handles.zTest    = zTest;
                Handles.color    = handlesColor;
                Handles.matrix   = handlesMatrix;
            }
        }

        private void DrawColliderBounds(IList<ColliderHolder> colliders, bool interactable)
        {
            Matrix4x4 handlesMatrix = Handles.matrix;

            // Don't allow interacting with the bounds handles if Rect tool isn't selected
            bool guiEnabled = GUI.enabled;
            GUI.enabled = interactable;
#if UNITY_2018_1_OR_NEWER
            boundsHandle.midpointHandleSizeFunction = interactable ? BoxBoundsHandle.DefaultMidpointHandleSizeFunction : (Handles.SizeFunction)null;
#else
		boundsHandle.midpointHandleSizeFunction = ( position ) => ( interactable ? HandleUtility.GetHandleSize( position ) * 0.03f : 0f );
#endif
            EditorGUI.BeginChangeCheck();

            foreach (ColliderHolder collider in colliders)
            {
                Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, collider.rotation, Vector3.one);
                Handles.matrix = matrix;

                boundsHandle.center = matrix.inverse.MultiplyPoint3x4(collider.position);
                boundsHandle.size   = collider.scale;

                EditorGUI.BeginChangeCheck();
                boundsHandle.DrawHandle();
                if (EditorGUI.EndChangeCheck() && interactable)
                {
                    collider.position = matrix.MultiplyPoint3x4(boundsHandle.center);
                    collider.scale    = boundsHandle.size;
                }
            }

            if (EditorGUI.EndChangeCheck() && interactable)
                Repaint();

            GUI.enabled    = guiEnabled;
            Handles.matrix = handlesMatrix;
        }

        private void HandleCreateMode(ref ColliderHolder colliderToCreate, Event ev)
        {
            // RMB is clicked or ESC is pressed
            if (isRightClick || (ev.isKey && ev.keyCode == KeyCode.Escape))
            {
                if (colliderToCreate != null)
                {
                    // Cancel created collider
                    DestroyUnfinishedCollider();
                    colliderToCreate      = null;
                    colliderCreationStage = CreationStage.PendingClick;

                    if (!isRightClick) // Don't eat right click event because it might be controlling Scene view 'zoom' or 'look around' gizmos
                        ev.Use();

                    return;
                }
                else if (colliderCreationStage == CreationStage.PendingClick && colliderCreationReferencePlaneCenter.HasValue)
                {
                    // Disable reference plane
                    colliderCreationReferencePlaneCenter = null;

                    if (!isRightClick)
                        ev.Use();

                    Repaint();
                }
            }

            if (ev.alt)
                return;

            switch (colliderCreationStage)
            {
                case CreationStage.PendingClick:
                {
                    Quaternion orientation;
                    switch (colliderCreationAlignment)
                    {
                        case ColliderAlignment.XZ:
                            orientation = Quaternion.identity;
                            break;
                        case ColliderAlignment.XY:
                            orientation = Quaternion.Euler(-90f, 0f, 0f);
                            break;
                        case ColliderAlignment.YZ:
                            orientation = Quaternion.Euler(-90f, 90f, 0f);
                            break;
                        default:
                            orientation = colliderCreationOrientation;
                            break;
                    }

                    if (ev.isMouse)
                    {
                        bool referencePlaneHit = false;
                        if (colliderCreationReferencePlaneCenter.HasValue)
                        {
                            // Raycast against it reference plane
                            Ray   ray = HandleUtility.GUIPointToWorldRay(ev.mousePosition);
                            float enter;
                            if (new Plane(orientation * Vector3.up, colliderCreationReferencePlaneCenter.Value).Raycast(ray, out enter))
                            {
                                colliderCreationPreviousPoint = ray.GetPoint(enter);
                                colliderCreationOrientation   = orientation;

                                referencePlaneHit = true;
                            }
                        }

                        // Raycast against world geometry
                        // Normally, we would want to call it inside 'if( !referencePlaneHit )' but somehow, this function causes
                        // the Scene view to constantly repaint which is awesome! Calling SceneView.RepaintAll inside EditorApplication.update
                        // also repaints the Scene view constantly but it uses more CPU than this method. Probably, this method invokes repaint
                        // less often or omits some redundant events that SceneView.RepaintAll doesn't
                        object[] parameters      = new object[2] { ev.mousePosition, null };
                        bool     worldRaycastHit = (bool)raycastWorld.Invoke(null, parameters);

                        if (!referencePlaneHit)
                        {
                            if (worldRaycastHit)
                            {
                                RaycastHit hit = (RaycastHit)parameters[1];

                                colliderCreationPreviousPoint = hit.point;
                                colliderCreationOrientation   = Quaternion.FromToRotation(Vector3.up, hit.normal);
                            }
                            else
                            {
                                // No geometry found, use a dummy point specified distance away from the camera
                                colliderCreationPreviousPoint = HandleUtility.GUIPointToWorldRay(ev.mousePosition).GetPoint(10f);
                                colliderCreationOrientation   = Quaternion.identity;
                            }
                        }
                    }

                    // Draw reference plane at cursor position if RMB isn't held
                    if (!isRightMouseButtonDown && colliderCreationReferencePlaneCenter.HasValue)
                    {
                        const float REFERENCE_PLANE_SIZE = 3f;

                        Vector3 corner1 = colliderCreationPreviousPoint + orientation * new Vector3(-REFERENCE_PLANE_SIZE, 0f, -REFERENCE_PLANE_SIZE);
                        Vector3 corner2 = colliderCreationPreviousPoint + orientation * new Vector3(-REFERENCE_PLANE_SIZE, 0f, REFERENCE_PLANE_SIZE);
                        Vector3 corner3 = colliderCreationPreviousPoint + orientation * new Vector3(REFERENCE_PLANE_SIZE, 0f, REFERENCE_PLANE_SIZE);
                        Vector3 corner4 = colliderCreationPreviousPoint + orientation * new Vector3(REFERENCE_PLANE_SIZE, 0f, -REFERENCE_PLANE_SIZE);

                        fillMaterial.SetInt("_ZTest", (int)CompareFunction.Greater);
                        fillMaterial.SetPass(0);
                        GL.Begin(GL.TRIANGLES);
                        GL.Color(COLLIDER_CREATION_REFERENCE_PLANE_OBSTRUCTED_COLOR);
                        GL.Vertex(corner1);
                        GL.Vertex(corner2);
                        GL.Vertex(corner3);
                        GL.Vertex(corner3);
                        GL.Vertex(corner4);
                        GL.Vertex(corner1);
                        GL.End();

                        fillMaterial.SetInt("_ZTest", (int)CompareFunction.LessEqual);
                        fillMaterial.SetPass(0);
                        GL.Begin(GL.TRIANGLES);
                        GL.Color(COLLIDER_CREATION_REFERENCE_PLANE_COLOR);
                        GL.Vertex(corner1);
                        GL.Vertex(corner2);
                        GL.Vertex(corner3);
                        GL.Vertex(corner3);
                        GL.Vertex(corner4);
                        GL.Vertex(corner1);
                        GL.End();
                    }

                    // Draw the hovered point's XYZ axes
                    float handleSize   = HandleUtility.GetHandleSize(colliderCreationPreviousPoint);
                    Color handlesColor = Handles.color;
                    Handles.color = Color.blue;
                    Handles.ArrowHandleCap(0, colliderCreationPreviousPoint, orientation, handleSize, ev.type);
                    Handles.color = Color.red;
                    Handles.ArrowHandleCap(0, colliderCreationPreviousPoint, orientation * Quaternion.Euler(0f, 90f, 0f), handleSize, ev.type);
                    Handles.color = Color.green;
                    Handles.ArrowHandleCap(0, colliderCreationPreviousPoint, orientation * Quaternion.Euler(-90f, 0f, 0f), handleSize, ev.type);
                    if (ev.control && ev.shift && !colliderCreationReferencePlaneCenter.HasValue)
                    {
                        // Draw a small sphere at cursor indicating that we are in 'enable reference plane' mode
                        CompareFunction zTest = Handles.zTest;
                        Handles.zTest = CompareFunction.LessEqual;
                        Handles.color = new Color(1f, 1f, 0f, 0.5f);
                        Handles.SphereHandleCap(0, colliderCreationPreviousPoint, Quaternion.identity, handleSize * 0.2f, ev.type);
                        Handles.zTest = zTest;
                    }

                    Handles.color = handlesColor;

                    if (ev.type == EventType.MouseDown && ev.button == 0)
                    {
                        if (ev.control && ev.shift && !colliderCreationReferencePlaneCenter.HasValue)
                        {
                            // Enable reference plane when both CTRL and Shift are held
                            colliderCreationReferencePlaneCenter = colliderCreationPreviousPoint;

                            ev.Use();
                            Repaint();
                        }
                        else
                        {
                            BoxCollider boxCollider = new GameObject("BoxCollider", typeof(BoxCollider)).GetComponent<BoxCollider>();
                            boxCollider.center = Vector3.zero;
                            boxCollider.size   = Vector3.one;
                            boxCollider.transform.SetParent(mainWizard.transform, false);
                            boxCollider.transform.SetPositionAndRotation(colliderCreationPreviousPoint, orientation);
                            boxCollider.transform.localScale = Vector3.zero;

                            colliderToCreate = new ColliderHolder(boxCollider) { isBeingCreated = true };
                            colliders.Add(colliderToCreate);

                            colliderCreationClickedPoint = colliderCreationPreviousPoint;
                            colliderCreationStage        = CreationStage.SetLength;

                            ev.Use();
                        }
                    }

                    break;
                }
                case CreationStage.SetLength:
                {
                    const float MIN_COLLIDER_LENGTH = 0.001f;

                    Vector3 upDirection, forwardDirection, rightDirection;
                    switch (colliderCreationAlignment)
                    {
                        case ColliderAlignment.XZ:
                            upDirection      = Vector3.up;
                            forwardDirection = Vector3.forward;
                            rightDirection   = Vector3.right;
                            break;
                        case ColliderAlignment.XY:
                            upDirection      = Vector3.back;
                            forwardDirection = Vector3.up;
                            rightDirection   = Vector3.right;
                            break;
                        case ColliderAlignment.YZ:
                            upDirection      = Vector3.left;
                            forwardDirection = Vector3.up;
                            rightDirection   = Vector3.back;
                            break;
                        default:
                            upDirection      = colliderCreationOrientation * Vector3.up;
                            forwardDirection = colliderCreationOrientation * Vector3.forward;
                            rightDirection   = colliderCreationOrientation * Vector3.right;
                            break;
                    }

                    // Draw gridlines on the alignment plane
                    float gridlinesSpacing = HandleUtility.GetHandleSize(colliderCreationPreviousPoint);
                    DrawGridlines(colliderCreationPreviousPoint, forwardDirection, rightDirection, 4, gridlinesSpacing, COLLIDER_CREATION_GRIDLINES_COLOR);
                    fillMaterial.SetInt("_ZTest", (int)CompareFunction.Greater);
                    DrawGridlines(colliderCreationPreviousPoint, forwardDirection, rightDirection, 4, gridlinesSpacing, COLLIDER_CREATION_GRIDLINES_OBSTRUCTED_COLOR);
                    fillMaterial.SetInt("_ZTest", (int)CompareFunction.LessEqual);

                    Ray   ray   = HandleUtility.GUIPointToWorldRay(ev.mousePosition);
                    Plane plane = new Plane(upDirection, colliderCreationClickedPoint);
                    float enter;
                    if (plane.Raycast(ray, out enter))
                    {
                        Vector3 hitPoint        = ray.GetPoint(enter);
                        Vector3 lengthDirection = hitPoint - colliderCreationPreviousPoint;
                        if (ev.shift)
                        {
                            // Snap direction to an axis
                            Vector3 longestProjection = Vector3.Project(lengthDirection, rightDirection);
                            for (int i = 0; i < 3; i++)
                            {
                                Vector3 projection = Vector3.Project(lengthDirection, forwardDirection + (i - 1) * rightDirection);
                                if (projection.sqrMagnitude > longestProjection.sqrMagnitude)
                                    longestProjection = projection;
                            }

                            lengthDirection = longestProjection;
                        }

                        if (lengthDirection.magnitude >= MIN_COLLIDER_LENGTH)
                        {
                            colliderToCreate.position = ev.control ? colliderCreationPreviousPoint : (colliderCreationPreviousPoint + lengthDirection * 0.5f);
                            colliderToCreate.rotation = Quaternion.LookRotation(lengthDirection, upDirection);
                            colliderToCreate.scale    = new Vector3(0f, 0f, ev.control ? (lengthDirection.magnitude * 2f) : lengthDirection.magnitude);
                        }
                    }

                    if (ev.type == EventType.MouseDown && ev.button == 0)
                    {
                        if (colliderToCreate.scale.magnitude < MIN_COLLIDER_LENGTH)
                        {
                            DestroyUnfinishedCollider();
                            colliderToCreate      = null;
                            colliderCreationStage = CreationStage.PendingClick;
                        }
                        else
                        {
                            colliderCreationPreviousPoint = colliderToCreate.position;
                            colliderCreationClickedPoint  = plane.Raycast(ray, out enter) ? ray.GetPoint(enter) : colliderCreationPreviousPoint;
                            colliderCreationStage         = CreationStage.SetWidth;
                        }

                        ev.Use();
                    }

                    break;
                }
                case CreationStage.SetWidth:
                {
                    Ray   ray   = HandleUtility.GUIPointToWorldRay(ev.mousePosition);
                    Plane plane = new Plane(colliderToCreate.colliderTransform.up, colliderCreationClickedPoint);
                    float enter;
                    if (plane.Raycast(ray, out enter))
                    {
                        Vector3 hitPoint       = ray.GetPoint(enter);
                        Vector3 widthDirection = Vector3.Project(hitPoint - colliderCreationClickedPoint, colliderToCreate.colliderTransform.right);
                        colliderToCreate.position = ev.control ? colliderCreationPreviousPoint : (colliderCreationPreviousPoint + widthDirection * 0.5f);

                        Vector3 scale = colliderToCreate.scale;
                        scale.x                = ev.control ? (widthDirection.magnitude * 2f) : widthDirection.magnitude;
                        colliderToCreate.scale = scale;
                    }

                    if (ev.type == EventType.MouseDown && ev.button == 0 && !ev.alt)
                    {
                        colliderCreationPreviousPoint = colliderToCreate.position;
                        colliderCreationClickedPoint  = plane.Raycast(ray, out enter) ? ray.GetPoint(enter) : colliderCreationPreviousPoint;
                        colliderCreationStage         = CreationStage.SetHeight;

                        ev.Use();
                    }

                    break;
                }
                case CreationStage.SetHeight:
                {
                    // While setting height, plane normal should preferably point towards scene view camera's forward direction
                    Vector3 cameraForward = colliderToCreate.colliderTransform.InverseTransformDirection(SceneView.currentDrawingSceneView.camera.transform.forward);
                    cameraForward.y = 0f;
                    cameraForward   = colliderToCreate.colliderTransform.TransformDirection(cameraForward);

                    Vector3 planeNormal;
                    if (cameraForward.sqrMagnitude > 0.00001f)
                        planeNormal = cameraForward;
                    else if (!SceneView.currentDrawingSceneView.orthographic)
                        planeNormal = colliderToCreate.colliderTransform.forward;
                    else
                    {
                        // In orthographic projection, while looking at the collider directly from above, we can't use a plane normal that is
                        // perpendicular to camera because otherwise collider's Transform values skyrocket in an instant! We should use a
                        // non-perpendicular plane normal. Ideally, collider size should increase when we move the mouse up or right whereas
                        // it should decrease when we move the mouse down or left
                        planeNormal = colliderToCreate.colliderTransform.up + SceneView.currentDrawingSceneView.camera.transform.TransformDirection(new Vector3(-0.707f, -0.707f, 0f)); // 0.707f: 1/sqrt(2)
                    }

                    Ray   ray   = HandleUtility.GUIPointToWorldRay(ev.mousePosition);
                    Plane plane = new Plane(planeNormal, colliderCreationClickedPoint);
                    float enter;
                    if (plane.Raycast(ray, out enter))
                    {
                        Vector3 hitPoint        = ray.GetPoint(enter);
                        Vector3 heightDirection = Vector3.Project(hitPoint - colliderCreationClickedPoint, colliderToCreate.colliderTransform.up);
                        colliderToCreate.position = ev.control ? colliderCreationPreviousPoint : (colliderCreationPreviousPoint + heightDirection * 0.5f);

                        Vector3 scale = colliderToCreate.scale;
                        scale.y                = ev.control ? (heightDirection.magnitude * 2f) : heightDirection.magnitude;
                        colliderToCreate.scale = scale;
                    }

                    if (ev.type == EventType.MouseDown && ev.button == 0)
                    {
                        colliderCreationStage = CreationStage.PendingClick;

                        Undo.RegisterCreatedObjectUndo(colliderToCreate.collider.gameObject, "Create Collider");
                        colliderToCreate.isBeingCreated   = false;
                        colliderToCreate.isActiveCollider = true;
                        colliderToCreate                  = null;

                        ev.Use();
                    }

                    break;
                }
            }
        }

        private void HandleEditMode(ref ColliderHolder activeCollider, Event ev)
        {
            switch (Tools.current)
            {
                case Tool.Move:
                {
                    foreach (ColliderHolder collider in visibleColliders)
                    {
                        EditorGUI.BeginChangeCheck();
                        Vector3 position = Handles.PositionHandle(collider.surfacePosition, (Tools.pivotRotation == PivotRotation.Local) ? collider.rotation : Quaternion.identity);
                        if (EditorGUI.EndChangeCheck())
                        {
                            activeCollider           = collider;
                            collider.surfacePosition = position;
                        }
                    }

                    break;
                }
                case Tool.Rotate:
                {
                    foreach (ColliderHolder collider in visibleColliders)
                    {
                        EditorGUI.BeginChangeCheck();
                        Quaternion rotation = Handles.RotationHandle(collider.rotation, collider.surfacePosition);
                        if (EditorGUI.EndChangeCheck())
                        {
                            activeCollider = collider;

                            // If we unparent the collider first, it almost never skews during rotation
                            Undo.RecordObject(collider.colliderTransform, "Modify Collider");
                            Transform parent       = collider.colliderTransform.parent;
                            int       siblingIndex = collider.colliderTransform.GetSiblingIndex();
                            collider.colliderTransform.SetParent(null, true);
                            try
                            {
                                if (pivotPosition == PivotPosition.Center)
                                    collider.rotation = rotation;
                                else
                                {
                                    // Rotate from the surface
                                    Quaternion delta = rotation * Quaternion.Inverse(collider.rotation);
                                    float      angle;
                                    Vector3    axis;
                                    delta.ToAngleAxis(out angle, out axis);
                                    collider.colliderTransform.RotateAround(collider.surfacePosition, axis, angle);
                                }
                            }
                            finally
                            {
                                collider.colliderTransform.SetParent(parent, true);
                                collider.colliderTransform.SetSiblingIndex(siblingIndex);
                            }
                        }
                    }

                    break;
                }

                case Tool.Scale:
                {
                    foreach (ColliderHolder collider in visibleColliders)
                    {
                        Vector3 surfacePosition = collider.surfacePosition;
                        Vector3 initialScale    = collider.scale;

                        EditorGUI.BeginChangeCheck();
                        Vector3 scale = Handles.ScaleHandle(initialScale, surfacePosition, collider.rotation, HandleUtility.GetHandleSize(surfacePosition));
                        if (EditorGUI.EndChangeCheck())
                        {
                            activeCollider = collider;

#if UNITY_2019_1_OR_NEWER && !UNITY_2020_2_OR_NEWER // Scaling in all axes is broken on some versions: https://issuetracker.unity3d.com/issues/object-size-is-doubled-when-center-scaling-it-with-custom-script-using-handles-dot-scalehandle-method
						int changedScaleComponents = 0;
						if( initialScale.x != scale.x )
							changedScaleComponents++;
						if( initialScale.y != scale.y )
							changedScaleComponents++;
						if( initialScale.z != scale.z )
							changedScaleComponents++;

						if( changedScaleComponents == 1 )
#endif
                            {
                                if (pivotPosition == PivotPosition.Center)
                                    collider.scale = scale;
                                else
                                {
                                    // Scale from the surface. The most reliable way to do it is to use a dummy pivot GameObject
                                    Undo.RecordObject(collider.colliderTransform, "Modify Collider");
                                    Transform pivot = new GameObject().transform;
                                    try
                                    {
                                        pivot.SetPositionAndRotation(surfacePosition, collider.rotation);
                                        pivot.localScale = collider.scale;

                                        Transform parent       = collider.colliderTransform.parent;
                                        int       siblingIndex = collider.colliderTransform.GetSiblingIndex();
                                        collider.colliderTransform.SetParent(pivot, true);
                                        try
                                        {
                                            pivot.localScale = scale;
                                        }
                                        finally
                                        {
                                            collider.colliderTransform.SetParent(parent, true);
                                            collider.colliderTransform.SetSiblingIndex(siblingIndex);
                                        }
                                    }
                                    finally
                                    {
                                        DestroyImmediate(pivot.gameObject);
                                    }
                                }
                            }
                        }
                    }

                    break;
                }
                case Tool.Rect:
                {
                    // Credit: https://github.com/Unity-Technologies/UnityCsReference/blob/61f92bd79ae862c4465d35270f9d1d57befd1761/Editor/Mono/GUI/Tools/BuiltinTools.cs#L514-L574
                    // Credit: https://github.com/Unity-Technologies/UnityCsReference/blob/61f92bd79ae862c4465d35270f9d1d57befd1761/Editor/Mono/GUI/Tools/TransformManipulator.cs
                    Color handlesColor = Handles.color;
                    int   controlID    = GUIUtility.hotControl;
                    bool  isPaintEvent = (!isPointerDown || ev.type == EventType.Repaint);

                    // Rect resize handle
                    foreach (ColliderHolder collider in visibleColliders)
                    {
                        EditorGUI.BeginChangeCheck();
                        object[] parameters = GetParametersForRectToolHandles(collider, true, isPaintEvent);
                        Vector3  scale      = (Vector3)resizeHandlesGUI.Invoke(null, parameters);
                        if (EditorGUI.EndChangeCheck() && isPointerDown && (activeCollider == null || activeCollider == collider))
                        {
                            Quaternion rectRotation = (Quaternion)parameters[2];
                            Vector3    scalePivot   = (Vector3)parameters[3];
                            SetScaleDelta(collider, scale, scalePivot, rectRotation);
                        }

                        if (GUIUtility.hotControl != controlID && activeCollider == null)
                            activeCollider = collider;
                    }

                    // Rect rotate handle
                    //foreach( ColliderHolder collider in visibleColliders )
                    //{
                    //	EditorGUI.BeginChangeCheck();
                    //	object[] parameters = GetParametersForRectToolHandles( collider, false, isPaintEvent );
                    //	Quaternion rotation = (Quaternion) rotationHandlesGUI.Invoke( null, parameters );
                    //	if( EditorGUI.EndChangeCheck() && isPointerDown && ( activeCollider == null || activeCollider == collider ) )
                    //	{
                    //		// I have no idea what's going on here
                    //		Quaternion rectRotation = (Quaternion) parameters[2];
                    //		Quaternion delta = Quaternion.Inverse( rectRotation ) * rotation;
                    //		delta.ToAngleAxis( out float angle, out Vector3 axis );
                    //		axis = rectRotation * axis;

                    //		collider.rotation *= Quaternion.Inverse( collider.snapshotRotation ) * Quaternion.AngleAxis( angle, axis ) * collider.snapshotRotation;
                    //		collider.rotation = collider.rotation.normalized; // Without this, rotation eventually fails
                    //	}

                    //	if( GUIUtility.hotControl != controlID && activeCollider == null )
                    //		activeCollider = collider;
                    //}

                    if (ev.type == EventType.MouseDown && activeCollider == null)
                    {
                        // Neither resize handle nor rotate handle has captured the input; only move handle is left
                        // Move handle should pick the collider that is closest to the camera. So we raycast against
                        // all colliders and consider the closest one the active collider. If two colliders are at the
                        // same distance, set the collider that was most recently interacted active
                        Ray   ray         = HandleUtility.GUIPointToWorldRay(ev.mousePosition);
                        float minDistance = float.PositiveInfinity;
                        foreach (ColliderHolder collider in visibleColliders)
                        {
                            float enter;
                            if (collider.SurfaceRaycast(ray, out enter))
                            {
                                if (Mathf.Approximately(minDistance, enter))
                                {
                                    if (collider.lastActiveTime > activeCollider.lastActiveTime)
                                    {
                                        activeCollider = collider;
                                        minDistance    = enter;
                                    }
                                }
                                else if (enter < minDistance)
                                {
                                    activeCollider = collider;
                                    minDistance    = enter;
                                }
                            }
                        }
                    }

                    // Rect move handle
                    foreach (ColliderHolder collider in visibleColliders)
                    {
                        if (activeCollider == collider)
                        {
                            EditorGUI.BeginChangeCheck();
                            Vector3 newPos = (Vector3)moveHandlesGUI.Invoke(null, GetParametersForRectToolHandles(collider, false, isPaintEvent));
                            if (EditorGUI.EndChangeCheck() && isPointerDown && (activeCollider == null || activeCollider == collider) && newPos != previousHandlePosition)
                            {
                                previousHandlePosition = newPos;
                                SetPositionDelta(collider, newPos - collider.snapshotPosition);
                            }
                        }
                    }

                    // moveHandlesGUI modifies Handles.color and doesn't automatically reset it
                    Handles.color = handlesColor;

                    break;
                }
            }
        }

        private void HandleDeleteMode(ref ColliderHolder colliderToDelete, Event ev)
        {
            if (colliderToDelete != null && ev.type == EventType.MouseDown && ev.button == 0)
            {
                Undo.DestroyObjectImmediate(colliderToDelete.collider.gameObject);
                colliders.Remove(colliderToDelete);
                colliderToDelete = null;

                ev.Use();
                GUIUtility.ExitGUI();
            }
        }

        private void DrawGridlines(Vector3 center, Vector3 verticalDir, Vector3 horizontalDir, int gridlineCount, float spacing, Color color)
        {
            // We have a number of ways to draw gridlines:
            // - Using outlineMaterial with GL.LINES: This is the easiest solution. Gridlines will always be visible on screen.
            //   The downside is, parts of it that are occluded by scene geometry may not be easily noticeable since they will
            //   share the same color with the parts that aren't occluded by scene geometry
            // - Using fillMaterial in one step with GL.LINES: Parts of the gridlines that are occluded by scene geometry won't be
            //   visible in Scene view but seeing these parts (with a different color than the unoccluded parts) would give more
            //   information to the user. Another downside is that, GL.LINES doesn't support _ZBias so it has major z-fighting issues
            // - Using fillMaterial in two steps with GL.LINES: In the second step, we can draw occluded parts with a different color
            //   which is awesome. Still, we have the terrible z-fighting issue
            // - Using fillMaterial in two steps with GL.QUADS: Unlike GL.LINES, GL.QUADS do support _ZBias so it doesn't have z-fighting
            //   issues. However, when looked at a steep angle, they can look very thin. To avoid this, we need to adjust their thickness
            //   vectors while looking from steep angles. But no matter how hard I've tried, I couldn't find a good solution for it, so I
            //   gave up on adjusting the thickness vectors
            //
            // Currently, I'm using the latest method since I do want to draw occluded parts with separate color but I don't want z-fighting.

            float   length   = spacing * gridlineCount;
            Vector3 startPos = center + verticalDir * length * 0.5f - horizontalDir * length * 0.5f;

            // GL.LINES approach
            //fillMaterial.SetPass( 0 );
            //GL.Begin( GL.LINES );
            //GL.Color( color );
            //for( int i = 0; i < gridlineCount; i++ )
            //{
            //	Vector3 gridPosition = startPos - verticalDir * ( i + 0.5f ) * spacing;
            //	GL.Vertex( gridPosition );
            //	GL.Vertex( gridPosition + horizontalDir * length );

            //	gridPosition = startPos + horizontalDir * ( i + 0.5f ) * spacing;
            //	GL.Vertex( gridPosition );
            //	GL.Vertex( gridPosition - verticalDir * length );
            //}
            //GL.End();

            // GL.QUADS approach
            float   steepness              = Mathf.LerpUnclamped(0.025f, 0.01f, Mathf.Pow(Mathf.Abs(Vector3.Dot((SceneView.currentDrawingSceneView.camera.transform.position - center).normalized, Vector3.Cross(horizontalDir, verticalDir))), 0.6f));
            Vector3 thicknessHorizontalDir = horizontalDir * spacing * steepness;
            Vector3 thicknessVerticalDir   = verticalDir * spacing * steepness;

            fillMaterial.SetPass(0);
            GL.Begin(GL.QUADS);
            GL.Color(color);
            for (int i = 0; i < gridlineCount; i++)
            {
                Vector3 gridPosition = startPos - verticalDir * (i + 0.5f) * spacing;
                GL.Vertex(gridPosition - thicknessVerticalDir);
                GL.Vertex(gridPosition + thicknessVerticalDir);
                GL.Vertex(gridPosition + horizontalDir * length + thicknessVerticalDir);
                GL.Vertex(gridPosition + horizontalDir * length - thicknessVerticalDir);

                gridPosition = startPos + horizontalDir * (i + 0.5f) * spacing;
                GL.Vertex(gridPosition - thicknessHorizontalDir);
                GL.Vertex(gridPosition + thicknessHorizontalDir);
                GL.Vertex(gridPosition - verticalDir * length + thicknessHorizontalDir);
                GL.Vertex(gridPosition - verticalDir * length - thicknessHorizontalDir);
            }

            GL.End();
        }

        #region Rect Tool Helper Functions

        private object[] GetParametersForRectToolHandles(ColliderHolder collider, bool isResizeHandle, bool isPaintEvent)
        {
            object[] result = isResizeHandle ? resizeHandleParameters : otherHandleParameters;

            Vector3 scale        = collider.scale;
            Vector2 colliderSize = new Vector2(scale.x, scale.z);
            result[0] = new Rect(colliderSize * -0.5f, colliderSize);                        // Rect
            result[1] = isPaintEvent ? collider.surfacePosition : collider.snapshotPosition; // Handle position
            result[2] = collider.rotation * Quaternion.Euler(90f, 0f, 0f);                   // Rect rotation

            return result;
        }

        // Credit: https://github.com/Unity-Technologies/UnityCsReference/blob/61f92bd79ae862c4465d35270f9d1d57befd1761/Editor/Mono/GUI/Tools/TransformManipulator.cs#L89-L141
        private void SetScaleDelta(ColliderHolder collider, Vector3 scaleDelta, Vector3 scalePivot, Quaternion scaleRotation)
        {
            SetPositionDelta(collider, scaleRotation * Vector3.Scale(Quaternion.Inverse(scaleRotation) * (collider.snapshotPosition - scalePivot), scaleDelta) + scalePivot - collider.snapshotPosition);

            float      biggestDot   = Mathf.NegativeInfinity;
            Quaternion refAlignment = Quaternion.identity;
            for (int i = 0; i < refAlignments.Length; i++)
            {
                float dot = Mathf.Min(
                                      Mathf.Abs(Vector3.Dot(scaleRotation * Vector3.right, collider.snapshotRotation * refAlignments[i] * Vector3.right)),
                                      Mathf.Abs(Vector3.Dot(scaleRotation * Vector3.up, collider.snapshotRotation * refAlignments[i] * Vector3.up)),
                                      Mathf.Abs(Vector3.Dot(scaleRotation * Vector3.forward, collider.snapshotRotation * refAlignments[i] * Vector3.forward))
                                     );

                if (dot > biggestDot)
                {
                    biggestDot   = dot;
                    refAlignment = refAlignments[i];
                }
            }

            scaleDelta = refAlignment * scaleDelta;
            scaleDelta = Vector3.Scale(scaleDelta, refAlignment * Vector3.one);

            collider.scale = Vector3.Scale(collider.snapshotScale, scaleDelta);
        }

        // Credit: https://github.com/Unity-Technologies/UnityCsReference/blob/61f92bd79ae862c4465d35270f9d1d57befd1761/Editor/Mono/GUI/Tools/TransformManipulator.cs#L148-L219
        private void SetPositionDelta(ColliderHolder collider, Vector3 positionDelta)
        {
            Vector3 newPosition = collider.snapshotPosition + positionDelta;
#if UNITY_2020_1_OR_NEWER
            if (!((bool)incrementalSnapActive.GetValue(null, null) || (bool)gridSnapActive.GetValue(null, null) || (bool)vertexSnapActive.GetValue(null, null)))
#endif
            {
                Vector3 minDifference = (Vector3)minDragDifference.GetValue(null, null);

                newPosition.x = Mathf.Approximately(positionDelta.x, 0f) ? collider.snapshotPosition.x : RoundBasedOnMinimumDifference(newPosition.x, minDifference.x);
                newPosition.y = Mathf.Approximately(positionDelta.y, 0f) ? collider.snapshotPosition.y : RoundBasedOnMinimumDifference(newPosition.y, minDifference.y);
                newPosition.z = Mathf.Approximately(positionDelta.z, 0f) ? collider.snapshotPosition.z : RoundBasedOnMinimumDifference(newPosition.z, minDifference.z);
            }

            collider.surfacePosition = newPosition;
        }

        // Credit: https://github.com/Unity-Technologies/UnityCsReference/blob/61f92bd79ae862c4465d35270f9d1d57befd1761/Editor/Mono/Utils/MathUtils.cs#L68-L73
        private float RoundBasedOnMinimumDifference(float valueToRound, float minDifference)
        {
            if (minDifference == 0)
            {
                int decimals = Mathf.Clamp((int)(5 - Mathf.Log10(Mathf.Abs(valueToRound))), 0, 15);
                return (float)Math.Round(valueToRound, decimals, MidpointRounding.AwayFromZero);
            }

            return (float)Math.Round(valueToRound, Mathf.Clamp(-Mathf.FloorToInt(Mathf.Log10(Mathf.Abs(minDifference))), 0, 15), MidpointRounding.AwayFromZero);
        }

        #endregion
    }
#endif
}