using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Kalagaan.Editor
{
    public class VertExmotionUVPaintTool : EditorWindow
    {

        enum eRendermode
        {
            //FACE_WIREFRAME,
            WIREFRAME,
            FACE
        }


        [MenuItem("Window/VertExmotion/Paint tool")]
        static void Init()
        {
            VertExmotionUVPaintTool window = (VertExmotionUVPaintTool)EditorWindow.GetWindow(typeof(VertExmotionUVPaintTool));           
            window.Show();
        }


        VertExmotionEditor m_vtmEditor;
        VertExmotionBase vtm = null;
                
        RenderTexture m_rt;
        string _layer= "TransparentFX";

        Camera m_EditorCam;
        Mesh m_UVMesh;
        GameObject goUVMesh = null;
        int m_brushSize = 100;

        static Texture2D cursorIcon = null;
        public bool m_needRefresh = false;

        //Vector2 m_uvCoord;
        eRendermode m_renderMode = eRendermode.FACE;
        static float textureSize = 512;
        static int m_maxZlayer = 100;
        System.DateTime m_lastUpdate;

        Material m_wireframeMat = null;

        int[,,] m_UV2ID;
        int[,] m_UV2IDZ;

        Color m_smoothColor = Color.black;

        void OnGUI()
        {
            if (VertExmotionEditor.m_currentEditor == null)
            {
                GUILayout.Space(20);
                GUILayout.Label("Select a VertExmotion object", EditorStyles.boldLabel);
                Repaint();
                return;
            }


            if(cursorIcon == null)
                cursorIcon = (Texture2D)Resources.Load("Icons/UVBrush", typeof(Texture2D));


            bool forceInit = false;
            /*
            if (GUILayout.Button("Init"))
                forceInit = true;
            */

            //textureSize = Mathf.FloorToInt( EditorGUILayout.FloatField("Texture size", textureSize) );


            if (m_needRefresh || m_vtmEditor == null || VertExmotionEditor.m_currentEditor != m_vtmEditor || m_UV2ID==null || forceInit || m_UVMesh==null)
            {
                this.titleContent = new GUIContent("UV Paint tool");

                if (VertExmotionEditor.m_currentEditor == null || VertExmotionEditor.m_currentEditor.m_meshCollider == null)
                {                    
                    Repaint();
                    return;
                }

                Clean();

                vtm = VertExmotionEditor.m_currentEditor.target as VertExmotionBase;
                if (vtm != null)
                {
                    m_vtmEditor = VertExmotionEditor.m_currentEditor;
                    m_vtmEditor.m_UVPaintTool = this;                    
                    m_UVMesh = ConvertMeshToUV(m_vtmEditor.m_meshCollider.sharedMesh, 1);
                    m_needRefresh = false;

                    m_lastUpdate = System.DateTime.Now;
                }
            }
            

            if(vtm==null)
            {
                GUILayout.Space(20);
                GUILayout.Label("Select a VertExmotion object", EditorStyles.boldLabel);
                Repaint();
                return;
            }


            if (VertExmotionEditor.m_mode != VertExmotionEditor.eMode.PAINT)
            {
                GUILayout.Space(20);
                GUILayout.Label("Select the Paint tab", EditorStyles.boldLabel);
                Repaint();
                return;
            }


            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical(GUILayout.Width(300));

            m_renderMode = (eRendermode)EditorGUILayout.EnumPopup("Render mode", m_renderMode);

            /*
            m_test = EditorGUILayout.ObjectField(m_test, typeof(MeshToUV), true) as MeshToUV;
            m_material = EditorGUILayout.ObjectField(m_material, typeof(Material), false) as Material;
            m_mesh = EditorGUILayout.ObjectField(m_mesh, typeof(Mesh), false) as Mesh;
            m_camPos = EditorGUILayout.Vector3Field("campos", m_camPos);
            

            if (m_test == null)
                return;

            if(m_preview==null)
                m_preview = new PreviewRenderUtility();


            if (m_test != null)
                m_test.m_UVSpaceThreshold = GUILayout.HorizontalSlider(m_test.m_UVSpaceThreshold, 0f, 1f, GUILayout.Width(200));
           */

            /*
            if (GUILayout.Button("Refresh cam"))
                if (m_EditorCam != null)
                    DestroyImmediate(m_EditorCam.gameObject);
               */

            m_brushSize = EditorGUILayout.IntSlider("Brush size",m_brushSize, 20, 225);
            m_vtmEditor.m_drawIntensity = EditorGUILayout.Slider("Intensity", m_vtmEditor.m_drawIntensity, 0f, 1f);
            m_vtmEditor.m_drawFalloff = EditorGUILayout.Slider("Falloff", m_vtmEditor.m_drawFalloff, 0f, 1f);
            float min = m_vtmEditor.m_brushMinMax.x;
            float max = m_vtmEditor.m_brushMinMax.y;
            GUILayout.BeginHorizontal();
            GUILayout.Label("Min/Max");
            GUILayout.FlexibleSpace();
            min = EditorGUILayout.FloatField(Mathf.Round(min * 100f) / 100f, GUILayout.Width(30));
            GUILayout.Label("/");
            max = EditorGUILayout.FloatField(Mathf.Round(max * 100f) / 100f, GUILayout.Width(30));
            GUILayout.EndHorizontal();
            EditorGUILayout.MinMaxSlider(ref min, ref max, 0f, 1f);
            m_vtmEditor.m_brushMinMax.x = min;
            m_vtmEditor.m_brushMinMax.y = max;

            GUILayout.EndVertical();


            if (m_EditorCam == null)
            {
                GameObject goCam = new GameObject("EditorCam"); //GameObject.CreatePrimitive(PrimitiveType.Sphere);
                m_EditorCam = goCam.AddComponent<Camera>();
                goCam.hideFlags = HideFlags.HideAndDontSave;
                
                m_EditorCam.clearFlags = CameraClearFlags.Color;

                m_EditorCam.transform.position = new Vector3(.5f, .5f, -1f);
                m_EditorCam.transform.rotation = Quaternion.identity;
                m_EditorCam.orthographic = true;
                m_EditorCam.orthographicSize = .5f;
                m_EditorCam.farClipPlane = 10f;
                m_EditorCam.nearClipPlane = .1f;
                m_EditorCam.cameraType = CameraType.Preview;
                m_EditorCam.depth = -100;

                string[] layers = { _layer };
                m_EditorCam.cullingMask = LayerMask.GetMask(layers);

                
                
                  
            }

            m_EditorCam.backgroundColor = m_vtmEditor.ShowGradient() ? Color.gray : Color.blue*.1f;

            Rect rct = GUILayoutUtility.GetLastRect();
            float size = Mathf.Min(position.height - rct.y-50, position.width);

            if (m_rt == null || size != m_rt.width)
            {
                m_EditorCam.targetTexture = null;

                if (m_rt != null)
                    DestroyImmediate(m_rt);

                //size *= 2;

                m_rt = new RenderTexture((int)size, (int)size, 32);
                m_EditorCam.targetTexture = m_rt;

                       
               
            }



            Material[] sharedMaterials = vtm.renderer.sharedMaterials;

            if (goUVMesh == null)
            {
                goUVMesh = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                DestroyImmediate(goUVMesh.GetComponent<SphereCollider>());
                goUVMesh.name = "UV Render";


                if (m_UVMesh != null)
                    goUVMesh.GetComponent<MeshFilter>().sharedMesh = m_UVMesh;
                goUVMesh.layer = LayerMask.NameToLayer(_layer);
                goUVMesh.transform.position = Vector3.zero;
                goUVMesh.transform.rotation = Quaternion.identity;




                Shader shaderEdit = VertExmotionEditor.GetEditorShader();

                //Renderer r = goUVMesh.GetComponent<MeshRenderer>();

                
                if (m_vtmEditor.m_selectedMaterialSlotID != 0)
                {
                    Shader shaderHide = VertExmotionEditor.GetHiddenShader();
                    for (int i = 0; i < sharedMaterials.Length; ++i)
                    {
                        if (sharedMaterials[i] == null)
                            continue;
                        sharedMaterials[i] = new Material(sharedMaterials[i]);
                        sharedMaterials[i].shader = shaderHide;

                    }
                    sharedMaterials[(m_vtmEditor.m_selectedMaterialSlotID - 1)].shader = shaderEdit;

                }                
                
            }
            //Shader shaderEdit = Shader.Find("Hidden/VertExmotion_editor");
            Renderer rdr = goUVMesh.GetComponent<MeshRenderer>();

            MaterialPropertyBlock pb = new MaterialPropertyBlock();
            vtm.renderer.GetPropertyBlock(pb);

            //rdr.sharedMaterials = vtm.renderer.sharedMaterials;
            rdr.sharedMaterials = sharedMaterials;
            rdr.SetPropertyBlock(pb);

            if(m_vtmEditor.m_meshCollider!=null)
                m_UVMesh.triangles = m_vtmEditor.m_meshCollider.sharedMesh.triangles;
            m_UVMesh.colors32 = vtm.m_vertexColors;

            //------------
            //render



            m_EditorCam.clearFlags = CameraClearFlags.SolidColor;

            if (m_renderMode != eRendermode.WIREFRAME)
                m_EditorCam.Render();


            if(m_wireframeMat == null)
            {
                m_wireframeMat = new Material(VertExmotionEditor.GetWireframeShader());
                m_wireframeMat.color = Color.black;
                m_wireframeMat.SetColor("_Color", Color.black);
            }


            if (m_renderMode != eRendermode.FACE)
            {
                if (m_renderMode != eRendermode.WIREFRAME)
                {
                    Material[] mats = new Material[rdr.sharedMaterials.Length];
                    for (int i = 0; i < rdr.sharedMaterials.Length; ++i)
                    {
                        mats[i] = m_wireframeMat;
                    }
                    rdr.sharedMaterials = mats;
                    m_EditorCam.clearFlags = CameraClearFlags.Depth;
                }

                
                GL.wireframe = true;
                m_EditorCam.Render();
                GL.wireframe = false;
            }

            //rdr.sharedMaterials = vtm.renderer.sharedMaterials;

            //------------

            GUILayout.Label("", GUILayout.ExpandHeight(true),GUILayout.ExpandWidth(true));
            rct = GUILayoutUtility.GetLastRect();

            GUILayout.EndHorizontal();
            

            if (rct.width > rct.height)
                rct.width = rct.height;
            if (rct.height > rct.width)
                rct.height = rct.width;


            //rct.width *= 4f;
            //rct.height *= 4f;


            GUI.DrawTexture(rct, m_rt);


            //Debug.Log("" + rct.width + " x " + rct.height);

            Rect cursorRect = new Rect();


            int cursorSize = m_brushSize;
            Vector2 cursorPos = Event.current.mousePosition;
            cursorPos.x -= cursorSize /2;
            cursorPos.y -= cursorSize/2;
            cursorRect.position = cursorPos;
            cursorRect.width = cursorSize;
            cursorRect.height = cursorSize;


            /*
            Rect testRect = new Rect(rct);
            Vector2 p = testRect.position;
            //p.x += 10;
            //p.y += 10;
            testRect.position = p;
            testRect.width = 10;
            testRect.height = 10;
            GUI.Box(testRect, "");


            p = rct.position;
            p.x += rct.width;
            //p.y += 10;
            testRect.position = p;
            testRect.width = 10;
            testRect.height = 10;
            GUI.Box(testRect, "");


            p = rct.position;
            p.y += rct.height;
            //p.y += 10;
            testRect.position = p;
            testRect.width = 10;
            testRect.height = 10;
            GUI.Box(testRect, "");


            p = rct.position;
            p.x += rct.width;
            p.y += rct.height;
            testRect.position = p;
            testRect.width = 10;
            testRect.height = 10;
            GUI.Box(testRect, "");
            */

            Color cursorCol = Color.white;
            if (Event.current.control)
                cursorCol = Color.red;
            if (Event.current.shift)
                cursorCol = Color.yellow;


            cursorCol.a = .3f;
            GUI.color = cursorCol;
            GUI.Label(cursorRect, cursorIcon);
            GUI.color = Color.white;


            float dt = (float)(System.DateTime.Now - m_lastUpdate).TotalMilliseconds;
            //if (dt > 1)
            {

                m_lastUpdate = System.DateTime.Now;

                Paint(rct, dt);
            }


            DestroyImmediate(goUVMesh);
            //goUVMesh.SetActive(false);




            Repaint();
           
           
            SceneView.RepaintAll();

            return;

        }



        void Clean()
        {
            if (m_EditorCam != null)
            {
                m_EditorCam.targetTexture = null;
                DestroyImmediate(m_EditorCam.gameObject);
            }

            if (m_rt != null)
                DestroyImmediate(m_rt);

            m_UV2ID = null;
            m_UV2IDZ = null;
        }

        public void OnDestroy()
        {
            Clean();
        }










        public void Paint(Rect rct, float dt)
        {
            if (Event.current.button == 0 && Event.current.delta.sqrMagnitude > 0)
            {
                Color meanCol = Color.black;
                float cnt = 0;

                for (int i = -m_brushSize / 2; i < m_brushSize / 2; ++i)
                {
                    for (int j = -m_brushSize / 2; j < m_brushSize / 2; ++j)
                    {

                        Vector2 ij = Vector2.zero;
                        ij.x = i;
                        ij.y = j;

                        if (ij.magnitude > m_brushSize * .8f / 2f)
                            continue;


                        float falloff = m_vtmEditor.m_drawFalloff * (ij.magnitude / (m_brushSize*.8f /2f)) ;
                        falloff = (1 - falloff);
                        falloff = 1f - falloff*falloff*falloff;
                        //float intensity = m_vtmEditor.m_drawIntensity - (m_vtmEditor.m_drawIntensity * Mathf.Lerp(0f,1f,falloff));
                                               

                        float intensity = m_vtmEditor.m_drawIntensity * ( 1f- falloff);

                        intensity = Mathf.Clamp01(intensity);

                        //intensity = 1f - ( 1f-intensity)  * (1f-intensity);
                        if(intensity <.98f)
                        	intensity *= dt / 10f;



                        if (rct.Contains(Event.current.mousePosition))
                        {
                            Vector2 UV = (Event.current.mousePosition + ij - rct.position) / rct.width;



                            //m_uvCoord = UV;
                            UV.y = 1f - UV.y;

                            UV *= textureSize;




                            //Debug.Log("" + UV);
                            int scale = Mathf.CeilToInt(textureSize / rct.width);
                            //Debug.Log("" + scale);

                            if (Event.current.shift)
                            {
                                intensity *= .2f;
                                for (int x = (int)UV.x - scale; x <= (int)UV.x + scale; ++x)
                                {
                                    for (int y = (int)UV.y - scale; y <= (int)UV.y + scale; ++y)
                                    {
                                        if (x < textureSize && y < textureSize && x >= 0 && y >= 0)
                                        {

                                            for (int z = 0; z < m_UV2IDZ[(int)x, (int)y]; ++z)
                                            {
                                                int vId = m_UV2ID[(int)x, (int)y, z];
                                                Color c = vtm.m_vertexColors[vId];
                                                meanCol += c * intensity;
                                                cnt += intensity;
                                            }
                                        }
                                    }
                                }
                                if (cnt > 0)
                                {
                                    m_smoothColor = meanCol / cnt;
                                }
                            }


                            for (int x = (int)UV.x - scale; x <= (int)UV.x + scale; ++x)
                            {
                                for (int y = (int)UV.y - scale; y <= (int)UV.y + scale; ++y)
                                {
                                    if (x < textureSize && y < textureSize && x >= 0 && y >= 0)
                                    {

                                        for (int z = 0; z < m_UV2IDZ[(int)x, (int)y]; ++z)
                                        {

                                            int vId = m_UV2ID[(int)x, (int)y, z];


                                            if (vId > 0)
                                            {
                                                vId -= 1;


                                                Color c = vtm.m_vertexColors[vId];
                                                Color targetCol = Event.current.shift ? m_smoothColor : (Event.current.control ? Color.black : Color.white);

                                                switch (m_vtmEditor.m_currentLayer)
                                                {
                                                    case 0:
                                                        //c = Color.Lerp (c, targetCol, intensity * .5f);
                                                        c.r = Mathf.Lerp(c.r, targetCol.r, intensity);
                                                        c.g = Mathf.Lerp(c.g, targetCol.g, intensity);
                                                        c.b = Mathf.Lerp(c.b, targetCol.b, intensity);

                                                        c.r = Mathf.Clamp(c.r, m_vtmEditor.m_brushMinMax.x, m_vtmEditor.m_brushMinMax.y);
                                                        c.g = Mathf.Clamp(c.g, m_vtmEditor.m_brushMinMax.x, m_vtmEditor.m_brushMinMax.y);
                                                        c.b = Mathf.Clamp(c.b, m_vtmEditor.m_brushMinMax.x, m_vtmEditor.m_brushMinMax.y);
                                                        break;
                                                    case 1:
                                                        c.r = Mathf.Lerp(c.r, targetCol.r, intensity);
                                                        c.r = Mathf.Clamp(c.r, m_vtmEditor.m_brushMinMax.x, m_vtmEditor.m_brushMinMax.y);
                                                        break;
                                                    case 2:
                                                        c.g = Mathf.Lerp(c.g, targetCol.g, intensity);
                                                        c.g = Mathf.Clamp(c.g, m_vtmEditor.m_brushMinMax.x, m_vtmEditor.m_brushMinMax.y);
                                                        break;
                                                    case 3:
                                                        c.b = Mathf.Lerp(c.b, targetCol.b, intensity);
                                                        c.b = Mathf.Clamp(c.b, m_vtmEditor.m_brushMinMax.x, m_vtmEditor.m_brushMinMax.y);
                                                        break;
                                                }
                                                vtm.m_vertexColors[vId] = c;// Color.LerpUnclamped(c2*100f, c*100f, intensity)/100f;

                                                

                                                m_vtmEditor.m_needRefresh = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                
            }
        }








        public Mesh ConvertMeshToUV(Mesh reference, float amount)
        {
            //Debug.Log("ConvertMeshToUV " + textureSize);

            m_UV2ID = new int[(int)textureSize+1, (int)textureSize+1, m_maxZlayer];
            m_UV2IDZ = new int[(int)textureSize+1, (int)textureSize+1];

            Mesh UVMesh = Instantiate(reference);
            UVMesh.name = reference.name;
            UVMesh.indexFormat = reference.indexFormat;

            Vector3[] vertices = UVMesh.vertices;
            Vector3[] normals = UVMesh.normals;
            Vector2[] uv = UVMesh.uv;
            int[] tri = UVMesh.triangles;
            bool[] vertexFilter = new bool[vertices.Length];
            



            if (m_vtmEditor.m_selectedMaterialSlotID != 0)
            {
                //need a filter for the material
                for (int i = 0; i < tri.Length; ++i)
                {
                    vertexFilter[tri[i]] = true;
                }
            }
                 

            for (int i = 0; i < vertices.Length; ++i)
            {
                
                if (m_vtmEditor.m_selectedMaterialSlotID != 0)
                {
                    if (!vertexFilter[i])
                        continue;
                }
                

                int vtxId = i;

                Vector3 vertex = Vector3.zero;

                              

                vertex.x = uv[vtxId].x;
                vertex.y = uv[vtxId].y;


                while (vertex.x < 0)
                    vertex.x += 1;
                while (vertex.y < 0)
                    vertex.y += 1;

                while (vertex.x > 1)
                    vertex.x -= 1;
                while (vertex.y > 1)
                    vertex.y -= 1;


                //Debug.Log("uv " + uv[vtxId]);

                /*
                if (vertex.x < 0)
                    vertex.x = 0;
                if (vertex.y < 0)
                    vertex.y = 0;
                */

                //vertex.x = uv[vtxId].x % 1;
                //vertex.y = uv[vtxId].y % 1;

                vertex.x = Mathf.Clamp01(vertex.x);
                vertex.y = Mathf.Clamp01(vertex.y);

                vertex.z = 0;
                vertices[vtxId] = Vector3.Lerp(vertices[vtxId], vertex, amount);
                normals[vtxId] = Vector3.Lerp(normals[vtxId], -Vector3.forward, amount);

                int pixelX = Mathf.FloorToInt(vertex.x * textureSize);
                int pixelY = Mathf.FloorToInt(vertex.y * textureSize);

                int pixelZ = 0;

                /*
                for(int z=0; z< m_maxZlayer; z++)
                {
                    pixelZ = z;
                    if (m_UV2IDZ[pixelX, pixelY, z] == 0)
                        break;                    
                }*/

                
                pixelZ = m_UV2IDZ[pixelX, pixelY]++;

                if (pixelZ >= m_maxZlayer )
                {
                    Debug.Log("please increase texture size ");
                    pixelZ = m_maxZlayer - 1;
                }
                    
                //Debug.Log("store " + pixelX + ","+ pixelY+ "," + pixelZ);
                m_UV2ID[pixelX, pixelY, pixelZ] = vtxId + 1;
            }

            UVMesh.vertices = vertices;
            UVMesh.normals = normals;

            return UVMesh;
        }


    }
}
