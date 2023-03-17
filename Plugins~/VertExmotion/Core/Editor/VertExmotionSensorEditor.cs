using UnityEditor;
using UnityEngine;

namespace Kalagaan.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(VertExmotionSensorBase), true)]
    public class VertExmotionSensorEditor : UnityEditor.Editor
    {
        public enum eSettingsMode
        {
            NONE,
            SENSORS,
            FX,
            COLLIDERS
        }


        //VertExmotionCollider m_vtmcol;
        private static bool m_removeCollider = false;

        //static bool m_showSensorSettings = true;
        //static bool m_showColliderSettings = false;
        private static eSettingsMode m_settingMode = eSettingsMode.SENSORS;


        private static Color m_collisionZone = new Color(.8f, .1f, .2f, .5f);
        private static Color m_collisionZoneAlpha = new Color(.8f, .1f, .2f, .08f);

        private static SkinnedMeshRenderer m_smr;
        //static Transform m_selectedBone = null;

        private static float[] m_fadePanel = { 0, 0, 0 };


#if !UNITY_5_6_OR_NEWER
        protected static Handles.DrawCapFunction RectangleCap = Handles.RectangleCap;
        protected static Handles.DrawCapFunction CircleCap = Handles.CircleCap;
        protected static Handles.DrawCapFunction SphereCap = Handles.SphereCap;
        protected static Handles.DrawCapFunction ArrowCap = Handles.ArrowCap;
        protected static Handles.DrawCapFunction CubeCap = Handles.CubeCap;
#else
        protected static Handles.CapFunction RectangleCap = Handles.RectangleHandleCap;
        protected static Handles.CapFunction CircleCap = Handles.CircleHandleCap;
        protected static Handles.CapFunction SphereCap = Handles.SphereHandleCap;
        protected static Handles.CapFunction ArrowCap = Handles.ArrowHandleCap;
        protected static Handles.CapFunction CubeCap = Handles.CubeHandleCap;
#endif


        public void OnEnable()
        {
            //VertExmotionSensor vms = target as VertExmotionSensor;
            //m_vtmcol = vms.GetComponent<VertExmotionCollider> ();
            m_removeCollider = false;
            m_selectBone = false;
            m_smr = null;
            //m_selectedBone = null;
            _boneNames = null;
        }

        public void OnDisable()
        {
            if (Application.isPlaying)
                return;

            if (target == null)
                return;

            /*
			VertExmotionColliderBase vtmCol = (target as VertExmotionSensorBase).GetComponentInChildren<VertExmotionColliderBase>();
            if (vtmCol != null)
            {
                VertExmotionEditor.ReplaceBaseClass(vtmCol);
                EditorUtility.SetDirty(vtmCol);
            }*/

            //VertExmotionEditor.ReplaceBaseClass ( (target as VertExmotionSensorBase) );
            //EditorUtility.SetDirty(target);
        }


        public override void OnInspectorGUI()
        {
            if (!VertExmotionEditor.m_editorInitialized)
                VertExmotionEditor.InitializeEditorParameters();


            VertExmotionSensorBase vms = target as VertExmotionSensorBase;

            //DrawDefaultInspector ();
            DrawSensorSettings(vms);

            //vms.m_params.damping = Mathf.Clamp( EditorGUILayout.FloatField( "damping", vms.m_params.damping ), 0, 30f );
            //vms.m_params.bouncing = Mathf.Clamp( EditorGUILayout.FloatField( "bouncing", vms.m_params.bouncing ), 0, 30f );
            /*
                    PID pid = new PID ();
                    pid.m_params = new PID.Parameters( vms.m_pid.m_params );
        
        
                    GUILayout.Box( " ", VertExmotionEditor.m_bgStyle, GUILayout.Height(50f), GUILayout.ExpandWidth(true) );
                    pid.GUIDrawResponse ( GUILayoutUtility.GetLastRect(), m_timeUnit );
                    m_timeUnit = EditorGUILayout.FloatField ("time unit (s)",m_timeUnit);
                    m_timeUnit = Mathf.Clamp ( m_timeUnit, 1f, 10f);
            */

            if (VertExmotionEditor.m_lastVTMSelected != null)
                if (GUILayout.Button("Select " + VertExmotionEditor.m_lastVTMSelected.gameObject.name))
                    Selection.activeGameObject = VertExmotionEditor.m_lastVTMSelected.gameObject;


#if KVTM_DEBUG
			GUILayout.Label ( "--------------\nDEBUG\n--------------" );
			DrawDefaultInspector ();
#endif
        }


        Vector2 m_scrollViewPos;

        //Rect m_bgRect = ;
        void OnSceneGUI()
        {
            if (!VertExmotionEditor.m_editorInitialized)
                VertExmotionEditor.InitializeEditorParameters();

            if (!VertExmotionEditor.m_showPanel)
                return;

            VertExmotionEditor.m_showPanelProgress = 1f;
            //VertExmotionEditor.m_showPanel = true;
            VertExmotionEditor.UpdateShowPanel();
            float menuWidth = 215f;

            Handles.BeginGUI();

            VertExmotionEditor.DrawBackground();

            GUILayout.BeginHorizontal();
            GUILayout.Space(5);

            m_scrollViewPos = GUILayout.BeginScrollView(m_scrollViewPos, GUILayout.Width(menuWidth));

            DrawSensorSettings(target as VertExmotionSensorBase);

            GUILayout.Space(10f);
            if (VertExmotionEditor.m_lastVTMSelected != null)
                if (GUILayout.Button("Select " + VertExmotionEditor.m_lastVTMSelected.gameObject.name))
                    Selection.activeGameObject = VertExmotionEditor.m_lastVTMSelected.gameObject;

            GUILayout.EndScrollView();


            GUILayout.EndHorizontal();


            Handles.EndGUI();


            DrawSensorHandle(target as VertExmotionSensorBase, true);
        }


        static string[] _boneNames = null;

        public static Transform SelectBone(VertExmotionSensorBase s)
        {
            //return selected;
            //VertExmotionSensorBase s = target as VertExmotionSensorBase;


            if (m_smr != null)
            {
                int id = 0;


                if (_boneNames == null)
                {
                    _boneNames = new string[m_smr.bones.Length + 1];
                    _boneNames[0] = "- none -";
                    for (int i = 0; i < m_smr.bones.Length; ++i)
                        _boneNames[i + 1] = m_smr.bones[i].name;
                }


                for (int i = 0; i < m_smr.bones.Length; ++i)
                {
                    if (m_smr.bones[i] == s.m_parent)
                    {
                        id = i + 1;
                        break;
                    }
                }

                int newid = EditorGUILayout.Popup("Parent Bone", id, _boneNames);
                /*
                if (newid == id)
                    return null;*/

                if (newid == 0)
                    return s.transform.parent;
                else
                    return m_smr.bones[newid - 1];
            }
            else
                return null;
        }


        static bool m_selectBone = false;

        public static void DrawSensorSettings(VertExmotionSensorBase sensor)
        {
            PID pid = new PID();
            pid.m_params = new PID.Parameters(sensor.m_pid.m_params);
            pid.m_params.limits.x = -float.MaxValue;
            pid.m_params.limits.y = float.MaxValue;


            //float timeUnit = EditorGUILayout.FloatField ("time unit (s)",m_timeUnit);
            //timeUnit = Mathf.Clamp ( timeUnit, 0.1f, 30f);

            GUILayout.Space(10);

            //if( GUILayout.Button( (m_showSensorSettings?"-":"+") + "Sensor Settings", VertExmotionEditor.m_styleTitle ) )
            if (GUILayout.Button((m_settingMode == eSettingsMode.SENSORS ? "-" : "+") + "Sensor Settings", VertExmotionEditor.m_styleTitle))
            {
                //m_showSensorSettings = !m_showSensorSettings;
                m_settingMode = m_settingMode == eSettingsMode.SENSORS ? eSettingsMode.NONE : eSettingsMode.SENSORS;
                //if( m_showSensorSettings ) m_showColliderSettings = false;
            }


            bool tryToGetSkinnedMesh = false;
            //if( m_showSensorSettings )
            //if( m_settingMode == eSettingsMode.SENSORS )
            if (BeginFadeGroup(eSettingsMode.SENSORS))
            {
                GUILayout.Label("Lock the sensor to a gameobject");
                GUILayout.Label("Set a bone for skinnedMesh");
                GUILayout.BeginHorizontal();
                GUILayout.Label("parent", GUILayout.Width(75f));
                sensor.m_parent = EditorGUILayout.ObjectField(sensor.m_parent, typeof(Transform), true) as Transform;
                GUILayout.EndHorizontal();

                if (!tryToGetSkinnedMesh)
                {
                    if (sensor.transform.parent != null)
                        m_smr = sensor.transform.parent.GetComponent<SkinnedMeshRenderer>();
                    tryToGetSkinnedMesh = true;
                }

                if (m_smr != null)
                {
                    /*
                    Transform selectedBone = SelectBone(sensor);
                    if (selectedBone != null)
                        sensor.m_parent = selectedBone;
                    */
                    GUILayout.BeginHorizontal();
                    GUILayout.FlexibleSpace();

                    if (GUILayout.Button("select a bone", EditorStyles.miniButton))
                        m_selectBone = !m_selectBone;

                    GUILayout.EndHorizontal();
                }

                GUILayout.Space(5);
                GUILayout.Label("motion settings : ", VertExmotionEditor.m_styleBold);

                GUILayout.BeginHorizontal();
                GUILayout.Label("layer", GUILayout.Width(145));
                string[] layersName = { "All", "1", "2", "3" };
                int[] layersIds = { 0, 1, 2, 3 };
                sensor.m_layerID = EditorGUILayout.IntPopup(sensor.m_layerID, layersName, layersIds);
                GUILayout.EndHorizontal();

                sensor.m_envelopRadius = EditorGUILayout.FloatField("Radius", sensor.m_envelopRadius);


                sensor.m_params.power = Mathf.Clamp(EditorGUILayout.FloatField("Distance power", sensor.m_params.power), 0, float.MaxValue);
                sensor.m_params.translation.motionFactor = Mathf.Clamp(EditorGUILayout.FloatField("Motion factor", sensor.m_params.translation.motionFactor), 0, float.MaxValue);


                sensor.m_params.translation.axisLimits = EditorGUILayout.Toggle("3 axis limits", sensor.m_params.translation.axisLimits);
                if (!sensor.m_params.translation.axisLimits)
                {
                    sensor.m_params.translation.outerMaxDistance = Mathf.Clamp(EditorGUILayout.FloatField("Outer max distance", sensor.m_params.translation.outerMaxDistance), 0, float.MaxValue);
                    sensor.m_params.translation.innerMaxDistance = Mathf.Clamp(EditorGUILayout.FloatField("Inner max distance", sensor.m_params.translation.innerMaxDistance), 0, float.MaxValue);
                }
                else
                {
                    sensor.m_params.translation.axisOuterMaxDistance = EditorGUILayout.Vector3Field("Outer max distance", sensor.m_params.translation.axisOuterMaxDistance);
                    sensor.m_params.translation.axisOuterMaxDistance.x = Mathf.Clamp(sensor.m_params.translation.axisOuterMaxDistance.x, 0, sensor.m_params.translation.axisOuterMaxDistance.x);
                    sensor.m_params.translation.axisOuterMaxDistance.y = Mathf.Clamp(sensor.m_params.translation.axisOuterMaxDistance.y, 0, sensor.m_params.translation.axisOuterMaxDistance.y);
                    sensor.m_params.translation.axisOuterMaxDistance.z = Mathf.Clamp(sensor.m_params.translation.axisOuterMaxDistance.z, 0, sensor.m_params.translation.axisOuterMaxDistance.z);

                    sensor.m_params.translation.axisInnerMaxDistance = EditorGUILayout.Vector3Field("Inner max distance", sensor.m_params.translation.axisInnerMaxDistance);
                    sensor.m_params.translation.axisInnerMaxDistance.x = Mathf.Clamp(sensor.m_params.translation.axisInnerMaxDistance.x, 0, sensor.m_params.translation.axisInnerMaxDistance.x);
                    sensor.m_params.translation.axisInnerMaxDistance.y = Mathf.Clamp(sensor.m_params.translation.axisInnerMaxDistance.y, 0, sensor.m_params.translation.axisInnerMaxDistance.y);
                    sensor.m_params.translation.axisInnerMaxDistance.z = Mathf.Clamp(sensor.m_params.translation.axisInnerMaxDistance.z, 0, sensor.m_params.translation.axisInnerMaxDistance.z);
                }


                sensor.m_params.inflate = EditorGUILayout.FloatField("Inflate", sensor.m_params.inflate);
                sensor.m_params.damping = Mathf.Clamp(EditorGUILayout.FloatField("Damping", sensor.m_params.damping), 0, 30f);
                sensor.m_pid.m_params.kp = sensor.m_params.damping;
                sensor.m_params.bouncing = Mathf.Clamp(EditorGUILayout.FloatField("Bouncing", sensor.m_params.bouncing), 0, 30f);
                sensor.m_pid.m_params.ki = sensor.m_params.bouncing;
                //GUILayout.Label( "", GUILayout.Height(50f) );
                GUILayout.Box(" ", VertExmotionEditor.m_bgStyle, GUILayout.Height(50f), GUILayout.ExpandWidth(true));
                GUIDrawPidResponse(pid, GUILayoutUtility.GetLastRect(), sensor.m_pidTime);

                sensor.m_pidTime = EditorGUILayout.Slider("t (s)", sensor.m_pidTime, 1f, 10f);

                sensor.unscaledTime = EditorGUILayout.Toggle("Unscaled time", sensor.unscaledTime);
                sensor.timeScale = EditorGUILayout.FloatField("Time scale", sensor.timeScale);

                EditorGUILayout.EndFadeGroup();
            }


            //-----------------------------------------------
            //FX settings
            //-----------------------------------------------
            GUILayout.Space(10);
            if (GUILayout.Button((m_settingMode == eSettingsMode.FX ? "-" : "+") + "FX Settings", VertExmotionEditor.m_styleTitle))
                m_settingMode = m_settingMode == eSettingsMode.FX ? eSettingsMode.NONE : eSettingsMode.FX;

            //if( m_settingMode == eSettingsMode.FX )
            if (BeginFadeGroup(eSettingsMode.FX))
            {
                GUILayout.BeginVertical(EditorStyles.helpBox);
                sensor.m_params.translation.gravityInOut = EditorGUILayout.Vector2Field("Gravity in/out", sensor.m_params.translation.gravityInOut);
                GUILayout.EndVertical();

                GUILayout.BeginVertical(EditorStyles.helpBox);
                sensor.m_params.translation.localOffset = EditorGUILayout.Vector3Field("Local offset", sensor.m_params.translation.localOffset);
                sensor.m_params.translation.worldOffset = EditorGUILayout.Vector3Field("World offset", sensor.m_params.translation.worldOffset);
                GUILayout.EndVertical();

                GUILayout.BeginVertical(EditorStyles.helpBox);
                sensor.m_params.scale = EditorGUILayout.Vector3Field("Scale", sensor.m_params.scale);
                GUILayout.EndVertical();

                GUILayout.BeginVertical(EditorStyles.helpBox);
                sensor.m_params.rotation.axis = EditorGUILayout.Vector3Field("Rotation axis", sensor.m_params.rotation.axis);
                sensor.m_params.rotation.angle = EditorGUILayout.FloatField("Rotation angle", sensor.m_params.rotation.angle);
                GUILayout.EndVertical();


                GUILayout.BeginVertical(EditorStyles.helpBox);
                sensor.m_params.fx.stretch = EditorGUILayout.FloatField("Stretch", sensor.m_params.fx.stretch);
                sensor.m_params.fx.stretchMax = EditorGUILayout.FloatField("Stretch max dist", sensor.m_params.fx.stretchMax);
                sensor.m_params.fx.stretchMinSpeed = EditorGUILayout.FloatField("Stretch min speed", sensor.m_params.fx.stretchMinSpeed);
                GUILayout.EndVertical();
                EditorGUILayout.EndFadeGroup();
            }


            //-----------------------------------------------
            //collider settings
            //-----------------------------------------------

            GUILayout.Space(10);
            if (GUILayout.Button((m_settingMode == eSettingsMode.COLLIDERS ? "-" : "+") + "Collider Settings", VertExmotionEditor.m_styleTitle))
                m_settingMode = m_settingMode == eSettingsMode.COLLIDERS ? eSettingsMode.NONE : eSettingsMode.COLLIDERS;

            //if( m_settingMode == eSettingsMode.COLLIDERS )
            if (BeginFadeGroup(eSettingsMode.COLLIDERS))
            {
                VertExmotionColliderBase vtmCol = sensor.GetComponentInChildren<VertExmotionColliderBase>();
                if (vtmCol == null)
                {
                    if (GUILayout.Button("Add collider"))
                    {
#if VERTEXMOTION_TRIAL
                        //EditorGUILayout.HelpBox("TRIAL VERSION", MessageType.Warning);
                        EditorUtility.DisplayDialog("TRIAL VERSION", "Colliders are not available in the trial version.", "OK");
#else

                        EditorPrefs.SetInt("VertExmotion_LastMode", (int)VertExmotionEditor.eMode.SENSORS);
                        GameObject go = new GameObject("VMCollider");
                        go.transform.parent = sensor.transform;
                        go.transform.localScale = Vector3.one;
                        go.transform.localPosition = Vector3.zero;
                        go.transform.localRotation = Quaternion.identity;
                        VertExmotionColliderBase collider = go.AddComponent<VertExmotionColliderBase>();
                        VertExmotionEditor.ReplaceBaseClass(collider);

#endif
                        m_removeCollider = false;
                    }
                }
                else
                {
                    //draw collider GUI
                    string[] options = new string[32];
                    for (int i = 0; i < 32; ++i)
                        options[i] = LayerMask.LayerToName(i);

                    vtmCol.m_layerMask = EditorGUILayout.MaskField("layer mask", vtmCol.m_layerMask, options);
                    vtmCol.m_smooth = EditorGUILayout.FloatField("Smooth collision", vtmCol.m_smooth);
                    vtmCol.m_disableBackwardCollisions = EditorGUILayout.Toggle("No backward collisions", vtmCol.m_disableBackwardCollisions);
                    vtmCol.m_maximizeSphereCollision = EditorGUILayout.Toggle("Maximize Collision", vtmCol.m_maximizeSphereCollision);


                    vtmCol.m_wobble = EditorGUILayout.Toggle("Wobble", vtmCol.m_wobble);
                    if (vtmCol.m_wobble)
                    {
                        //EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.BeginVertical(EditorStyles.helpBox, GUILayout.Width(100f));
                        vtmCol.m_friction = Mathf.Clamp01(EditorGUILayout.FloatField("Friction", vtmCol.m_friction));
                        vtmCol.m_damping = Mathf.Clamp(EditorGUILayout.FloatField("Damping", vtmCol.m_damping), 0, 30f);
                        vtmCol.m_bouncing = Mathf.Clamp(EditorGUILayout.FloatField("Bouncing", vtmCol.m_bouncing), 0, 30f);
                        vtmCol.m_limit = Mathf.Clamp(EditorGUILayout.FloatField("Limit", vtmCol.m_limit), 0, 30f);
                        GUILayout.Box(" ", VertExmotionEditor.m_bgStyle, GUILayout.Height(50f), GUILayout.ExpandWidth(true));
                        pid.m_params.kp = vtmCol.m_damping;
                        pid.m_params.ki = vtmCol.m_bouncing;
                        GUIDrawPidResponse(pid, GUILayoutUtility.GetLastRect(), 2f);
                        vtmCol.m_pid.unscaledTime = EditorGUILayout.Toggle("Unscaled time", vtmCol.m_pid.unscaledTime);
                        vtmCol.m_pid.timeScale = EditorGUILayout.FloatField("Time scale", vtmCol.m_pid.timeScale);
                        EditorGUILayout.EndVertical();
                        //GUILayout.EndHorizontal();
                    }


                    //show collision zone param
                    int colZoneToDelete = -1;
                    for (int j = 0; j < vtmCol.m_collisionZones.Count; ++j)
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("Zone " + j, VertExmotionEditor.m_styleBold);
                        GUILayout.FlexibleSpace();
                        if (GUILayout.Button("delete"))
                            colZoneToDelete = j;
                        GUILayout.EndHorizontal();
                        vtmCol.m_collisionZones[j].positionOffset = EditorGUILayout.Vector3Field("position", vtmCol.m_collisionZones[j].positionOffset);
                        vtmCol.m_collisionZones[j].radius = EditorGUILayout.FloatField("radius", vtmCol.m_collisionZones[j].radius);


                        if (GUI.changed)
                            //EditorUtility.SetDirty( vtmCol );
                            Undo.RecordObject(vtmCol, "Collider update");
                    }

                    if (colZoneToDelete != -1)
                        vtmCol.m_collisionZones.RemoveAt(colZoneToDelete);


                    if (GUILayout.Button("Add collision zone"))
                    {
                        vtmCol.m_collisionZones.Add(new VertExmotionColliderBase.CollisionZone());
                    }

                    GUILayout.Space(10);

                    //remove collider

                    if (!m_removeCollider)
                    {
                        if (GUILayout.Button("Remove collider"))
                        {
                            m_removeCollider = true;
                        }
                    }
                    else
                    {
                        GUILayout.BeginHorizontal();
                        GUILayout.Label("remove collider?");

                        if (GUILayout.Button("yes"))
                        {
                            if (Application.isPlaying)
                                Destroy(vtmCol.gameObject);
                            else
                                DestroyImmediate(vtmCol.gameObject);
                        }

                        if (GUILayout.Button("no"))
                            m_removeCollider = false;

                        GUILayout.EndHorizontal();
                    }
                }

                EditorGUILayout.EndFadeGroup();
            }

            if (GUI.changed)
            {
                Undo.RegisterFullObjectHierarchyUndo(sensor, "sensor modification");
            }
        }


        public static bool BeginFadeGroup(eSettingsMode mode)
        {
//			m_fadePanel [(int)mode - 1] += ( (m_settingMode == mode) ? VertExmotionEditor.m_dt : -VertExmotionEditor.m_dt ) * 10f;
//			m_fadePanel [(int)mode - 1] = Mathf.Clamp (m_fadePanel [(int)mode - 1], 0.001f, 1f);

            m_fadePanel[(int)mode - 1] = (m_settingMode == mode) ? 1f : 0f;

            if (m_fadePanel[(int)mode - 1] != 0f)
                EditorGUILayout.BeginFadeGroup(m_fadePanel[(int)mode - 1]);

            //return true;
            return m_fadePanel[(int)mode - 1] != 0f;
        }


        public static void GUIDrawPidResponse(PID pid, Rect area, float timeUnit)
        {
            Color c = new Color(1f, 1f, 1f, .1f);

            pid.Init();
            //unit step
            pid.m_target = 1;
            float r = 0;
            Vector2 start = new Vector2(area.x, area.y + area.height);
            Vector2 end = start;

            Handles.color = c;
            for (int i = 0; i < timeUnit; ++i)
            {
                start = new Vector2((float)i * area.width / timeUnit, area.y + area.height);
                end = new Vector2((float)i * area.width / timeUnit, area.y);
                //GLDraw.DrawLine (start, end, c, 1f);
                Handles.DrawLine(start, end);
            }

            start = new Vector2(area.x, area.y + area.height * .5f);
            end = new Vector2(area.x + area.width, area.y + area.height * .5f);
            //GLDraw.DrawLine (start, end, c, 1f);
            Handles.DrawLine(start, end);


            start = new Vector2(area.x, area.y + area.height);
            end = start;

            for (int i = 0; i < area.width; ++i)
            {
                float dt = (float)timeUnit / (float)area.width;
                for (int j = 0; j < 10f; ++j)
                    r = pid.Compute(r, dt * .1f);
                end.x++;
                end.y = area.height - r * area.height * .5f + area.y;
                end.y = Mathf.Clamp(end.y, area.y, area.y + area.height);

                //GLDraw.DrawLine (start, end, Color.green, 1f);
                Handles.color = VertExmotionEditor.orange;
                Handles.DrawLine(start, end);
                start = end;

                //			//draw error
                //			errEnd.x++;
                //			errEnd.y = area.height - (float) lastErr;
                //			errEnd.y = Mathf.Clamp( errEnd.y,  area.y,  area.y+area.height );
                //			GLDraw.DrawLine (errStart, errEnd, Color.red, 1f);
                //			errStart = errEnd;
            }
        }


        public static bool DrawSensorHandle(VertExmotionSensorBase sensor, bool AllInfo)
        {
            bool useHandle = false;
            Color handleColor = VertExmotionEditor.orange;
            if (!AllInfo)
                handleColor.a = .2f;


            Handles.color = handleColor;
            Camera svCam = SceneView.currentDrawingSceneView.camera;
            float constUnit = (svCam.ViewportToWorldPoint(Vector3.zero) - svCam.ViewportToWorldPoint(Vector3.one)).magnitude;
            constUnit = HandleUtility.GetHandleSize(sensor.transform.position) * 10f;

            if (m_settingMode != eSettingsMode.SENSORS)
                AllInfo = false;


            if (m_settingMode == eSettingsMode.SENSORS || m_settingMode == eSettingsMode.FX)
            {
                Handles.DrawSolidDisc(sensor.transform.position, -svCam.transform.forward, (constUnit * .01f));
                Handles.DrawWireDisc(sensor.transform.position, -svCam.transform.forward, sensor.m_envelopRadius * VertExmotionBase.GetScaleFactor(sensor.transform));

                for (int i = 0; i < 10; ++i)
                {
                    handleColor.a = (float)(10 - i) / 10f * .5f;
                    float f = (float)i / 11f * (float)i / 11f;
                    Handles.color = handleColor;
                    Handles.DrawWireDisc(sensor.transform.position, -svCam.transform.forward, sensor.m_envelopRadius * VertExmotionBase.GetScaleFactor(sensor.transform) * f);
                }

                handleColor = VertExmotionEditor.orange;
                if (!AllInfo)
                    handleColor.a = .2f;

                Handles.color = handleColor;

                Vector3 lastPos = sensor.transform.position;
                sensor.transform.position =
                    Handles.FreeMoveHandle(sensor.transform.position, Quaternion.identity, (constUnit * .02f), Vector3.zero, CircleCap);
                if (lastPos != sensor.transform.position)
                    useHandle = true;

                float lastRadius = sensor.m_envelopRadius;

                if (VertExmotionBase.GetScaleFactor(sensor.transform) > 0)
                {
                    float radius = sensor.m_envelopRadius;
                    radius =
                        Vector3.Distance(
                            Handles.FreeMoveHandle(sensor.transform.position + svCam.transform.right * sensor.m_envelopRadius * VertExmotionBase.GetScaleFactor(sensor.transform), Quaternion.identity,
                                (constUnit * .02f), Vector3.zero, CubeCap)
                            , sensor.transform.position) / VertExmotionBase.GetScaleFactor(sensor.transform);

                    if (Mathf.Abs(radius - sensor.m_envelopRadius) > .0001f)
                        sensor.m_envelopRadius = radius;
                }

                if (lastRadius != sensor.m_envelopRadius)
                    useHandle = true;

                if (!AllInfo)
                    return useHandle;


                //draw direction
                Handles.color = Color.cyan;
                lastPos = sensor.transform.position + sensor.transform.forward * constUnit * .1f;
                Handles.DrawLine(sensor.transform.position, lastPos);
                lastPos = Handles.FreeMoveHandle(lastPos, Quaternion.identity, (constUnit * .01f), Vector3.zero, CircleCap);
                if (lastPos != sensor.transform.position + sensor.transform.forward * constUnit * .1f)
                {
                    sensor.transform.LookAt(lastPos, sensor.transform.up);
                    useHandle = true;
                }

                if (sensor.m_params.translation.axisLimits)
                {
                    sensor.transform.rotation = Handles.RotationHandle(sensor.transform.rotation, sensor.transform.position);
                    //sensor.transform.rotation = Handles.FreeRotateHandle(sensor.transform.rotation, sensor.transform.position, constUnit * .1f);
                    /*
                    Handles.color = Color.red;
                    lastPos = sensor.transform.position + sensor.transform.right * constUnit * .1f;
                    Handles.DrawLine(sensor.transform.position, lastPos);
                    lastPos = Handles.FreeMoveHandle(lastPos, Quaternion.identity, (constUnit * .01f), Vector3.zero, CircleCap);
                    if (lastPos != sensor.transform.position + sensor.transform.right * constUnit * .1f)
                    {
                        sensor.transform.LookAt(sensor.transform.position + sensor.transform.forward,  Vector3.Cross(sensor.transform.forward, lastPos)  );
                        useHandle = true;
                    }
                    */
                    /*
                    Handles.color = Color.green;
                    lastPos = sensor.transform.position + sensor.transform.up * constUnit * .1f;
                    Handles.DrawLine(sensor.transform.position, lastPos);
                    lastPos = Handles.FreeMoveHandle(lastPos, Quaternion.identity, (constUnit * .01f), Vector3.zero, CircleCap);
                    if (lastPos != sensor.transform.position + sensor.transform.forward * constUnit * .1f)
                    {
                        sensor.transform.LookAt(lastPos, sensor.transform.up);
                        useHandle = true;
                    }*/
                }


                if (m_selectBone && m_smr != null)
                {
                    //if (g.m_hd.m_smr != null && g.m_hd.m_smr.bones.Length > 0)
                    {
                        for (int b = 0; b < m_smr.bones.Length; ++b)
                        {
                            Handles.BeginGUI();
                            Vector3 UIpos = m_smr.bones[b].position;
                            UIpos = SceneView.currentDrawingSceneView.camera.WorldToScreenPoint(UIpos);
                            GUILayout.BeginArea(new Rect(UIpos.x, SceneView.currentDrawingSceneView.camera.pixelHeight - UIpos.y, 10f, 10f));
                            if (GUILayout.Button(""))
                            {
                                sensor.m_parent = m_smr.bones[b];
                                m_selectBone = false;
                            }

                            GUILayout.EndArea();
                            Handles.EndGUI();
                        }
                    }
                }


                //--------------------------------------------------
                //draw sensors limits
                //--------------------------------------------------

                float sf = VertExmotionBase.GetScaleFactor(sensor.transform);


                if (!sensor.m_params.translation.axisLimits)
                {
                    float max = 20f;
                    Vector3[] points = new Vector3[(int)max + 1];

                    Color col = Color.blue;
                    col.a = .01f;
                    Handles.color = col;

                    Vector3[] limitAxis = new Vector3[4];
                    limitAxis[0] = sensor.transform.right;
                    limitAxis[1] = -sensor.transform.right;
                    limitAxis[2] = sensor.transform.up;
                    limitAxis[3] = -sensor.transform.up;


                    for (int n = 0; n < 4; n++)
                    {
                        col.a = .3f;
                        Handles.color = col;


                        for (float i = 0; i <= max; ++i)
                        {
                            //Vector3 p1 = Quaternion.AngleAxis(i / max * 180f, limitAxis[n]) * sensor.transform.forward * Mathf.Max(sensor.m_params.translation.innerMaxDistance, sensor.m_params.translation.outerMaxDistance) * sf;
                            Vector3 p1 = Quaternion.AngleAxis(i / max * 180f, limitAxis[n]) * sensor.transform.forward;
                            //float lerpFactor = (Vector3.Dot(sensor.transform.forward, p1.normalized) + 1f) * .5f;
                            //float clampMag = (Mathf.Lerp(sensor.m_params.translation.innerMaxDistance, sensor.m_params.translation.outerMaxDistance, lerpFactor) * sf);

                            float dir = Vector3.Dot(sensor.transform.forward, p1.normalized);
                            float clampMag = dir > 0 ? sensor.m_params.translation.outerMaxDistance : sensor.m_params.translation.innerMaxDistance;
                            clampMag = Mathf.Lerp(sensor.m_params.translation.innerMaxDistance, sensor.m_params.translation.outerMaxDistance, (dir + 1) * .5f);
                            //Vector3 proj = Vector3.Dot(sensor.transform.forward, p1) * sensor.transform.forward * clampMag;
                            //p1 = p1 - sensor.transform.forward + proj;
                            //p1 = Vector3.ClampMagnitude(p1, clampMag);
                            p1 *= clampMag;
                            p1 *= sf;
                            p1 += sensor.transform.position;

                            if (i % 4 == 0)
                                Handles.DrawDottedLine(sensor.transform.position, p1, 3f);
                            points[(int)i] = p1;
                        }

                        col.a = .5f;
                        Handles.color = col;
                        Handles.DrawPolyLine(points);
                    }

                    col.a = .03f;
                    Handles.color = col;
                    Handles.DrawSolidDisc(sensor.transform.position, sensor.transform.forward,
                        (sensor.m_params.translation.innerMaxDistance + sensor.m_params.translation.outerMaxDistance) * .5f * sf);
                    col.a = .5f;
                    Handles.color = col;
                    Handles.DrawWireDisc(sensor.transform.position, sensor.transform.forward, (sensor.m_params.translation.innerMaxDistance + sensor.m_params.translation.outerMaxDistance) * .5f * sf);
                }
                else
                {
                    float max = 40f;
                    float alpha = .3f;
                    Vector3[] pointsX = new Vector3[(int)max + 1];
                    Vector3[] pointsY = new Vector3[(int)max + 1];
                    Vector3[] pointsZ = new Vector3[(int)max + 1];
                    Vector3 p;
                    Color col;

                    for (float i = 0; i <= max; ++i)
                    {
                        p = Quaternion.AngleAxis(i / max * 360f, Vector3.right) * Vector3.forward;
                        p.z *= p.z > 0 ? sensor.m_params.translation.axisOuterMaxDistance.z : sensor.m_params.translation.axisInnerMaxDistance.z;
                        p.y *= p.y > 0 ? sensor.m_params.translation.axisOuterMaxDistance.y : sensor.m_params.translation.axisInnerMaxDistance.y;
                        p = sensor.transform.TransformVector(p);
                        p += sensor.transform.position;
                        pointsX[(int)i] = p;
                        col = Color.red;
                        col.a = alpha;
                        Handles.color = col;
                        if (i % 4 == 0)
                            Handles.DrawDottedLine(sensor.transform.position, p, 3f);

                        p = Quaternion.AngleAxis(i / max * 360f, Vector3.up) * Vector3.forward;
                        p.z *= p.z > 0 ? sensor.m_params.translation.axisOuterMaxDistance.z : sensor.m_params.translation.axisInnerMaxDistance.z;
                        p.x *= p.x > 0 ? sensor.m_params.translation.axisOuterMaxDistance.x : sensor.m_params.translation.axisInnerMaxDistance.x;
                        p = sensor.transform.TransformVector(p);
                        p += sensor.transform.position;
                        pointsY[(int)i] = p;
                        col = Color.green;
                        col.a = alpha;
                        Handles.color = col;
                        if (i % 4 == 0)
                            Handles.DrawDottedLine(sensor.transform.position, p, 3f);

                        p = Quaternion.AngleAxis(i / max * 360f, Vector3.forward) * Vector3.up;
                        p.y *= p.y > 0 ? sensor.m_params.translation.axisOuterMaxDistance.y : sensor.m_params.translation.axisInnerMaxDistance.y;
                        p.x *= p.x > 0 ? sensor.m_params.translation.axisOuterMaxDistance.x : sensor.m_params.translation.axisInnerMaxDistance.x;
                        p = sensor.transform.TransformVector(p);
                        p += sensor.transform.position;
                        pointsZ[(int)i] = p;
                        col = Color.blue;
                        col.a = alpha;
                        Handles.color = col;
                        if (i % 4 == 0)
                            Handles.DrawDottedLine(sensor.transform.position, p, 3f);


                        /*
                        Vector3 p1 = Quaternion.AngleAxis(i / max * 180f, sensor.transform.right) * sensor.transform.forward * Mathf.Max(sensor.m_params.translation.axisInnerMaxDistance.z, sensor.m_params.translation.axisOuterMaxDistance.z) * sf;

                        float dir1 = Vector3.Dot(sensor.transform.forward, p1.normalized);
                        float clampMag1 = dir1 > 0 ? sensor.m_params.translation.axisOuterMaxDistance.z : sensor.m_params.translation.axisInnerMaxDistance.z;
                        Vector3 proj1 = Vector3.Dot(sensor.transform.forward, p1) * sensor.transform.forward * clampMag1;

                        float dir2 = Vector3.Dot(sensor.transform.up, p1.normalized);
                        float clampMag2 = dir2 > 0 ? sensor.m_params.translation.axisOuterMaxDistance.y : sensor.m_params.translation.axisInnerMaxDistance.y;
                        Vector3 proj2 = Vector3.Dot(sensor.transform.up, p1) * sensor.transform.up * clampMag2;

                        p1 = proj1 + proj2;

                        p1 += sensor.transform.position;

                        if (i % 4 == 0)
                            Handles.DrawDottedLine(sensor.transform.position, p1, 3f);
                        points[(int)i] = p1;
                        */
                    }

                    Handles.color = Color.red;
                    Handles.DrawPolyLine(pointsX);
                    Handles.color = Color.green;
                    Handles.DrawPolyLine(pointsY);
                    Handles.color = Color.blue;
                    Handles.DrawPolyLine(pointsZ);
                }
            }


            //--------------------------------------------------
            //draw collider handles
            //--------------------------------------------------
            if (m_settingMode == eSettingsMode.COLLIDERS)
            {
                VertExmotionColliderBase vtmCol = sensor.GetComponentInChildren<VertExmotionColliderBase>();

                Handles.DrawWireDisc(sensor.transform.position, -svCam.transform.forward, sensor.m_envelopRadius * VertExmotionBase.GetScaleFactor(sensor.transform));

                Handles.color = Color.cyan;

                if (vtmCol != null)
                {
                    for (int i = 0; i < vtmCol.m_collisionZones.Count; ++i)
                    {
                        Vector3 worldColZonePos = vtmCol.transform.TransformPoint(vtmCol.m_collisionZones[i].positionOffset);

                        float radius = vtmCol.m_collisionZones[i].radius * VertExmotionBase.GetScaleFactor(vtmCol.transform);

                        //Handles.DrawSolidDisc( vtmCol.m_collisionZones[i].positionOffset + sensor.transform.position , -svCam.transform.forward, ( constUnit*.01f ) );
                        Handles.color = m_collisionZoneAlpha;
                        Handles.DrawSolidDisc(worldColZonePos, -svCam.transform.forward, radius);
                        Handles.color = m_collisionZone;
                        Handles.DrawWireDisc(worldColZonePos, sensor.transform.forward, radius);
                        Handles.DrawWireDisc(worldColZonePos, sensor.transform.up, radius);
                        Handles.DrawWireDisc(worldColZonePos, sensor.transform.right, radius);


                        Handles.DrawSolidDisc(worldColZonePos, -svCam.transform.forward, (constUnit * .01f));

                        if (VertExmotionBase.GetScaleFactor(vtmCol.transform) > 0)
                        {
                            float old =
                                Vector3.Distance(
                                    Handles.FreeMoveHandle(worldColZonePos + svCam.transform.right * radius, Quaternion.identity, (constUnit * .02f), Vector3.zero, CubeCap)
                                    , worldColZonePos) / VertExmotionBase.GetScaleFactor(vtmCol.transform);

                            if (Mathf.Abs(old - vtmCol.m_collisionZones[i].radius) > .0001f)
                                vtmCol.m_collisionZones[i].radius = old; //fix some
                        }

                        vtmCol.m_collisionZones[i].positionOffset =
                            vtmCol.transform.InverseTransformPoint(Handles.FreeMoveHandle(worldColZonePos, Quaternion.identity, (constUnit * .02f), Vector3.zero, CircleCap));
                    }
                }
            }


            if (useHandle)
                //EditorUtility.SetDirty( sensor );
                Undo.RecordObject(sensor, "sensor update");


            return useHandle;
        }
    }
}