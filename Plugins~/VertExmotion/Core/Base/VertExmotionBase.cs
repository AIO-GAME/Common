#define REPLACE_SHADERS

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Kalagaan
{
	//[AddComponentMenu("VertExmotion/VertExmotion")]
	
	/// VertExmotion is the main class.
	/// require MeshFilter or SkinMeshRenderer
    //[ExecuteInEditMode]
	public class VertExmotionBase : MonoBehaviour {		

		// VertExmotion version
		public static string version = "1.9.9";
        ///version of the instance
        public string m_version = "1.9.9";

        /// number of sensor available by material
        /// limit of 4 for shader model 2 (mobile compatibility)
        public static int MAX_SENSOR = 20;
        static Vector4 ShaderAxisScaleX = new Vector4(1f, 0f, 0f, 1f);
        static Vector4 ShaderAxisScaleY = new Vector4(0f, 1f, 0f, 1f);
        static Vector4 ShaderAxisScaleZ = new Vector4(0f, 0f, 1f, 1f);


        [HideInInspector]
		public string className = "VertExmotion";

        public bool m_executeOnLateUpdate = true;
        public bool m_dontCheckShaderCompatibility = false;
        

        [System.Serializable]
		public class Parameters
		{			
			public bool usePaintDataFromMeshColors = false;
		}


        [System.Serializable]
        public class EditorParameters
        {            
            public int lastSensorSelected = 0;
            public bool m_showMaterialIDWireFrame = false;
            public bool m_hideUnselectedMaterialID = false;
            public float m_gradientFactor = .6f;
        }

        //[HideInInspector]
        public Parameters m_params = new Parameters();

        [HideInInspector]
        public EditorParameters m_editorParams = new EditorParameters();


        /// <summary>
        /// material property block.
        /// </summary>
        [HideInInspector] public MaterialPropertyBlock m_matPropBlk;		

		///mesh reference for editor
		///meshFilter or SkinMeshRenderer
		[System.NonSerialized] public Mesh m_mesh;

        ///mesh reference
		///meshFilter or SkinMeshRenderer
        [System.NonSerialized] Mesh m_runtimeMeshInstance = null;

        ///Sensors list \n
        ///sensor parameters are sent to shader each Update
        [HideInInspector] public List<VertExmotionSensorBase> m_VertExmotionSensors = new List<VertExmotionSensorBase>();

        [HideInInspector] public List<VertExmotionSensorBase> Sensors
        {
            get { return m_VertExmotionSensors; }
        }

        /// 
        ///
        [HideInInspector] public List<float> m_sensorsLinks = new List<float>();

        ///vertices weights\n
        /// 0->static 1->softboby\n
        ///only green parameter is used\n
        /// todo : add 3 other weight layers using RBA channels
        [HideInInspector] public Color32[] m_vertexColors;

        ///editor use only
        ///used to switch current shader by editor shader 



#if (REPLACE_SHADERS)
        [HideInInspector] public Shader[] m_initialShaders;
#else
        [HideInInspector] public Material[] m_initialMaterials;
        [HideInInspector] public Material[] m_editorMaterials;
#endif

        ///shared material
        /// WIP
        //[HideInInspector]
        //public bool m_shareMaterial = false;
        ///shared mesh
        ///WIP

        //public bool m_sharedMesh = true;
        [HideInInspector] public bool m_shareMesh = false;
		///mesh copy
		/// WIP 
		[HideInInspector] public bool m_meshCopy = false;
		///Shader parameters names dictionnary
		///disable string allocation for shader array name
		[HideInInspector] public Dictionary<string,List<string>> m_shaderParamNames = new Dictionary<string, List<string>> ();

        

		static Dictionary<Mesh,Mesh> m_meshPool = new Dictionary<Mesh, Mesh> ();
        [HideInInspector]
        public bool m_showEditorPanel = true;
        public bool m_editMode = true;

        //Unity 5.4 array
        [HideInInspector]
        public Vector4[] m_shaderSensorPos;
        [HideInInspector]
        public Vector4[] m_shaderMotionDirection;
        [HideInInspector]
        public Vector4[] m_shaderRCT;
        [HideInInspector]
        public Vector4[] m_shaderSquashStrech;
        [HideInInspector]
        public Vector4[] m_shaderSpeed;
        [HideInInspector]
        public Vector4[] m_shaderMotionAxis;
        [HideInInspector]
        public Vector4[] m_shaderAxisScaleX;
        [HideInInspector]
        public Vector4[] m_shaderAxisScaleY;
        [HideInInspector]
        public Vector4[] m_shaderAxisScaleZ;
        [HideInInspector]
        public float[] m_shaderLink;


        public float m_normalCorrection = 0f;
        public float m_normalSmooth = .2f;



        public SkinnedMeshRenderer m_smr;
        public MeshFilter m_mf;

        public Renderer m_renderer;
		public new Renderer renderer
		{
			get{
				if( m_renderer==null )
					m_renderer = GetComponent<Renderer> ();
				return m_renderer;
			}
		}		


		void Awake()
		{

            m_smr = GetComponent<SkinnedMeshRenderer>();
            m_mf = GetComponent<MeshFilter>();

            m_matPropBlk = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(m_matPropBlk);
            CleanShaderProperties();
            //renderer.material = new Material(renderer.material.shader);
            //renderer.GetPropertyBlock(m_matPropBlk);
            //m_matPropBlk.Clear();
            

            InitShaderParam ();
            DisableMotion();

            Collider c = GetComponent<Collider>();
            if(c!=null)
                IgnoreCollision(c, true);



            if ( !m_params.usePaintDataFromMeshColors && Application.isPlaying )
			{				
				ApplyMotionData ();
			}
			
		}
		
		
		public static float GetScaleFactor( Transform t )
		{            
            return (t.lossyScale.x + t.lossyScale.y + t.lossyScale.z) / 3f;
            //return t.lossyScale.magnitude;
        }

                             
        public static int[] m_shaderPropertyId;
        public enum eShaderProperty
        {
            KVM_NbSensors,
            KVM_SensorPosition,
            KVM_MotionDirection,
            KVM_RadiusCentripetalTorque,
            KVM_SquashStretch,
            KVM_Speed,
            KVM_MotionAxis,
            KVM_AxisXScale,
            KVM_AxisYScale,
            KVM_AxisZScale,
            KVM_Link,
            KVM_NormalCorrection,
            KVM_NormalSmooth,
            NB_PROPERTIES
        }




        void InitShaderParam()
		{




            //precompute shader param name to avoid string memory allocation by frame
            //"_SensorPosition" + i -> 490B allocated

            //not needed after unity 5.4
            /*
            m_shaderParamNames.Add ("_SensorPosition", new List<string> ());
			m_shaderParamNames.Add ("_MotionDirection", new List<string> ());
			m_shaderParamNames.Add ("_MotionAxis", new List<string> ());
			m_shaderParamNames.Add ("_RadiusCentripetalTorque", new List<string> ());
			m_shaderParamNames.Add ("_SquashStretch", new List<string> ());
			m_shaderParamNames.Add ("_Speed", new List<string> ());
			//m_shaderParamNames.Add ("_MotionZoneID", new List<string> ());
			
			
			foreach( KeyValuePair<string,List<string>> kvp in m_shaderParamNames )
				for (int i=0; i<MAX_SENSOR; ++i)
					kvp.Value.Add( kvp.Key + i );
            */

            if (m_shaderPropertyId == null)
            {
                m_shaderPropertyId = new int[(int)eShaderProperty.NB_PROPERTIES];

                m_shaderPropertyId[(int)eShaderProperty.KVM_NbSensors] = Shader.PropertyToID("KVM_NbSensors");
                m_shaderPropertyId[(int)eShaderProperty.KVM_SensorPosition] = Shader.PropertyToID("KVM_SensorPosition");
                m_shaderPropertyId[(int)eShaderProperty.KVM_MotionDirection] = Shader.PropertyToID("KVM_MotionDirection");
                m_shaderPropertyId[(int)eShaderProperty.KVM_RadiusCentripetalTorque] = Shader.PropertyToID("KVM_RadiusCentripetalTorque");
                m_shaderPropertyId[(int)eShaderProperty.KVM_SquashStretch] = Shader.PropertyToID("KVM_SquashStretch");
                m_shaderPropertyId[(int)eShaderProperty.KVM_Speed] = Shader.PropertyToID("KVM_Speed");
                m_shaderPropertyId[(int)eShaderProperty.KVM_MotionAxis] = Shader.PropertyToID("KVM_MotionAxis");
                m_shaderPropertyId[(int)eShaderProperty.KVM_AxisXScale] = Shader.PropertyToID("KVM_AxisXScale");
                m_shaderPropertyId[(int)eShaderProperty.KVM_AxisYScale] = Shader.PropertyToID("KVM_AxisYScale");
                m_shaderPropertyId[(int)eShaderProperty.KVM_AxisZScale] = Shader.PropertyToID("KVM_AxisZScale");
                m_shaderPropertyId[(int)eShaderProperty.KVM_Link] = Shader.PropertyToID("KVM_Link");
                m_shaderPropertyId[(int)eShaderProperty.KVM_NormalCorrection] = Shader.PropertyToID("KVM_NormalCorrection");
                m_shaderPropertyId[(int)eShaderProperty.KVM_NormalSmooth] = Shader.PropertyToID("KVM_NormalSmooth");
                
            }



            //int size = m_VertExmotionSensors.Count > 0 ? m_VertExmotionSensors.Count : 1;
            int size = MAX_SENSOR;


            //unity 5.4 shader array
            m_shaderSensorPos = new Vector4[size];
            m_shaderMotionDirection = new Vector4[size];
            m_shaderRCT = new Vector4[size];
            m_shaderSquashStrech = new Vector4[size];
            m_shaderSpeed = new Vector4[size];
            m_shaderMotionAxis = new Vector4[size];
            m_shaderAxisScaleX = new Vector4[size];
            m_shaderAxisScaleY = new Vector4[size];
            m_shaderAxisScaleZ = new Vector4[size];
            m_shaderLink = new float[size];

            for (int i = 0; i < m_shaderSensorPos.Length; ++i)
            {
                m_shaderSensorPos[i] = Vector4.zero;
                m_shaderMotionDirection[i] = Vector4.zero;
                m_shaderRCT[i] = Vector4.zero;
                m_shaderSquashStrech[i] = Vector4.zero;
                m_shaderSpeed[i] = Vector4.zero;
                m_shaderMotionAxis[i] = Vector4.zero;
                m_shaderAxisScaleX[i] = new Vector4(1f,0f,0f,1f);
                m_shaderAxisScaleY[i] = new Vector4(0f,1f,0f,1f);
                m_shaderAxisScaleZ[i] = new Vector4(0f,0f,1f,1f);
                m_shaderLink[i] = 0;
            }


            if (m_matPropBlk == null)
            {
                m_matPropBlk = new MaterialPropertyBlock();
                renderer.GetPropertyBlock(m_matPropBlk);
            }            

            m_matPropBlk.SetVectorArray(m_shaderPropertyId[(int)eShaderProperty.KVM_NbSensors], m_shaderSensorPos);
            m_matPropBlk.SetVectorArray(m_shaderPropertyId[(int)eShaderProperty.KVM_MotionDirection], m_shaderMotionDirection);
            m_matPropBlk.SetVectorArray(m_shaderPropertyId[(int)eShaderProperty.KVM_RadiusCentripetalTorque], m_shaderRCT);
            m_matPropBlk.SetVectorArray(m_shaderPropertyId[(int)eShaderProperty.KVM_SquashStretch], m_shaderSquashStrech);
            m_matPropBlk.SetVectorArray(m_shaderPropertyId[(int)eShaderProperty.KVM_Speed], m_shaderSpeed);
            m_matPropBlk.SetVectorArray(m_shaderPropertyId[(int)eShaderProperty.KVM_MotionAxis], m_shaderMotionAxis);
            m_matPropBlk.SetVectorArray(m_shaderPropertyId[(int)eShaderProperty.KVM_AxisXScale], m_shaderAxisScaleX);
            m_matPropBlk.SetVectorArray(m_shaderPropertyId[(int)eShaderProperty.KVM_AxisYScale], m_shaderAxisScaleY);
            m_matPropBlk.SetVectorArray(m_shaderPropertyId[(int)eShaderProperty.KVM_AxisZScale], m_shaderAxisScaleZ);
            m_matPropBlk.SetFloatArray(m_shaderPropertyId[(int)eShaderProperty.KVM_Link], m_shaderLink);
            //m_matPropBlk.Clear();
            renderer.SetPropertyBlock(m_matPropBlk);

        }
		
		


		public Mesh GetMesh()
		{
			MeshFilter mf = GetComponent<MeshFilter> ();
			if (mf) {
				if( Application.isPlaying )
					return mf.mesh;
				else
					return mf.sharedMesh;
			}
			SkinnedMeshRenderer smr = GetComponent<SkinnedMeshRenderer>();
			if (smr)
			{
				if( Application.isPlaying )
					return smr.sharedMesh;
				else				
					return smr.sharedMesh;
			}

			return null;
		}



		public void SetMesh( Mesh m )
		{
			MeshFilter mf = GetComponent<MeshFilter> ();
			if (mf) {
				if( Application.isPlaying )
					mf.mesh = m;
				else
					mf.sharedMesh = m;
			}
			SkinnedMeshRenderer smr = GetComponent<SkinnedMeshRenderer>();
			if (smr)
			{
				smr.sharedMesh = m;
			}
		}


		
		/// Check render on current gameObject. \n
		/// Initialise m_mesh reference.
		public void InitMesh()
		{
            if (renderer == null)
                return;

			Material[] materials;
			if( Application.isPlaying )
				materials = renderer.materials;
			else
				materials = renderer.sharedMaterials;



#if (REPLACE_SHADERS)
            if ( m_initialShaders == null || ( m_initialShaders.Length != materials.Length ) )
			{
				m_initialShaders = new Shader[materials.Length];
				for( int i=0; i<m_initialShaders.Length; ++i )
					m_initialShaders[i] = materials[i].shader;
			}
#else

            if (m_initialMaterials == null || (m_initialMaterials.Length != materials.Length))
            {
                m_initialMaterials = materials;
                /*
                m_initialMaterials = new Material[materials.Length];
                for (int i = 0; i < m_initialMaterials.Length; ++i)
                    m_initialMaterials[i] = materials[i];
                  */
            }
#endif

            if(m_mesh!=null)
            {
                if (Application.isPlaying)
                    Destroy(m_mesh);
                else
                    DestroyImmediate(m_mesh);
            }


            m_meshCopy = false;
			MeshFilter mf = GetComponent<MeshFilter> ();
			if (mf) {
				if( Application.isPlaying )
					m_mesh = mf.mesh;
				else
					m_mesh = mf.sharedMesh;
			}
			SkinnedMeshRenderer smr = GetComponent<SkinnedMeshRenderer>();
			if (smr)
			{
                if (Application.isPlaying)
                {
                    m_mesh = smr.sharedMesh;
                }
                else if(smr.sharedMesh != null)
                {
                    m_mesh = Instantiate(smr.sharedMesh) as Mesh;
                    smr.BakeMesh(m_mesh);
                    m_meshCopy = true;
                }
			}



            /*
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr)
            {
                if (Application.isPlaying)
                    m_mesh = sr.sprite.vertices;
                else
                {
                    m_mesh = Instantiate(smr.sharedMesh) as Mesh;
                    smr.BakeMesh(m_mesh);
                    m_meshCopy = true;
                }
            }*/

            DisableMotion ();
		}
		
		
		
		public void ApplyMotionData()
		{
			MeshFilter mf = GetComponent<MeshFilter> ();
			if (mf) {
				if( Application.isPlaying )
				{
					if( m_shareMesh )
					{
						Mesh refMesh = mf.sharedMesh;
						if( !m_meshPool.ContainsKey(refMesh) )
						{
							//store mesh in Mesh pool
							mf.mesh.colors32 = m_vertexColors;
							m_meshPool.Add( refMesh, mf.mesh );
							//Debug.Log("Add mesh to Mesh pool");
						}
						else
						{
							//get mesh from Mesh pool
							mf.mesh = m_meshPool[refMesh];
							//Debug.Log("get mesh from Mesh pool");
						}
					}
					else
					{
						//create a copy in memory
						mf.mesh.colors32 = m_vertexColors;
                        m_runtimeMeshInstance = mf.mesh;

                    }
				}				
			}
			SkinnedMeshRenderer smr = GetComponent<SkinnedMeshRenderer>();
			if (smr)
			{
                if (m_vertexColors != null && m_vertexColors.Length == smr.sharedMesh.vertexCount)
                {
                    if( Application.isPlaying )
                        smr.sharedMesh = Instantiate(smr.sharedMesh) as Mesh;                    
                    smr.sharedMesh.colors32 = m_vertexColors;
                    m_runtimeMeshInstance = smr.sharedMesh;
                }
			}
		}
		
		
		
		/// Check vertex color array. \n
		/// Initialise vertices weights.
		public void InitVertices()
		{                

            InitMesh();
            
            if (m_mesh == null)
                return;
  
			if ( m_vertexColors == null || m_vertexColors.Length  == 0 || m_vertexColors.Length != m_mesh.colors.Length)
			{
				if( m_mesh.colors.Length == 0 )
				{
                    if(m_vertexColors ==null || m_vertexColors.Length != m_mesh.vertexCount)
					    m_vertexColors = new Color32[m_mesh.vertices.Length];//create new colors informations
					m_mesh.colors32 = m_vertexColors;
				}
				else
					m_vertexColors = m_mesh.colors32;//copy colors from mesh


			}


		}



        public void Update()
        {
            if (!m_executeOnLateUpdate)
                UpdateShaders();
        }

        public void LateUpdate()
        {
            if (m_executeOnLateUpdate)
                UpdateShaders();
        }

       
        public void UpdateShaders ()
		{
            //m_matPropBlk.Clear();

            /*
            if (m_shaderSensorPos.Length != m_VertExmotionSensors.Count)
                InitShaderParam();
            */
            

            if (!Application.isPlaying)
			{
                //editor update
                return;
			}


#if VERTEXMOTION_TRIAL
            if (!Application.isEditor)
                return;
#endif


            if (m_matPropBlk == null)
            {
                m_matPropBlk = new MaterialPropertyBlock();
            }
            renderer.GetPropertyBlock(m_matPropBlk);


            /*
             //-------------------------------------------------------------------
            //UNITY < 5.4

                        //set shader parameters array
                        for (int i=0; i<m_VertExmotionSensors.Count; ++i)
                        {
                            if( i==MAX_SENSOR )
                                break;

                            m_matPropBlk.SetVector ( m_shaderParamNames["_SensorPosition"][i], m_VertExmotionSensors[i].transform.position );
                            m_matPropBlk.SetVector( m_shaderParamNames["_MotionDirection"][i], m_VertExmotionSensors[i].m_motionDirection );

                            Vector4 radiusCentripetalTorque = Vector4.zero;
                            radiusCentripetalTorque.x = m_VertExmotionSensors[i].m_envelopRadius*GetScaleFactor( m_VertExmotionSensors[i].transform );
                            radiusCentripetalTorque.y = m_VertExmotionSensors[i].m_centripetalForce*GetScaleFactor( m_VertExmotionSensors[i].transform );
                            radiusCentripetalTorque.z = m_VertExmotionSensors[i].m_motionTorqueForce;

                            Vector4 squashStrech = Vector4.zero;
                            squashStrech.x = m_VertExmotionSensors[i].m_params.fx.squash;
                            squashStrech.y = m_VertExmotionSensors[i].m_stretch;


                            m_matPropBlk.SetVector( m_shaderParamNames["_RadiusCentripetalTorque"][i], radiusCentripetalTorque );
                            m_matPropBlk.SetVector( m_shaderParamNames["_SquashStretch"][i], squashStrech );
                            m_matPropBlk.SetVector( m_shaderParamNames["_Speed"][i], m_VertExmotionSensors[i].m_speedStrech );

                            m_matPropBlk.SetVector( m_shaderParamNames["_MotionAxis"][i], m_VertExmotionSensors[i].m_torqueAxis );
                        }
            */


            //-------------------------------------------------------------------
            //UNITY 5.4

            

            //for (int i = 0; i < m_VertExmotionSensors.Count; ++i)            
            for (int i = 0; i < MAX_SENSOR; ++i)
            {   

                if (i < m_VertExmotionSensors.Count)
                {

                    if (m_VertExmotionSensors[i] == null)
                    {
                        m_VertExmotionSensors.RemoveAt(i--);
                        continue;
                    }

                    m_shaderSensorPos[i] = m_VertExmotionSensors[i].transform.position;
                    m_shaderSensorPos[i].w = m_VertExmotionSensors[i].m_layerID;
                    m_shaderMotionDirection[i] = m_VertExmotionSensors[i].m_motionDirection;
                    m_shaderMotionDirection[i].w = m_VertExmotionSensors[i].m_params.power;

                    Vector4 radiusCentripetalTorque = Vector4.zero;
                    radiusCentripetalTorque.x = m_VertExmotionSensors[i].m_envelopRadius * GetScaleFactor(m_VertExmotionSensors[i].transform);
                    radiusCentripetalTorque.y = m_VertExmotionSensors[i].m_centripetalForce * GetScaleFactor(m_VertExmotionSensors[i].transform);
                    radiusCentripetalTorque.z = m_VertExmotionSensors[i].m_motionTorqueForce;
                    m_shaderRCT[i] = radiusCentripetalTorque;

                    m_shaderSquashStrech[i].x = m_VertExmotionSensors[i].m_params.fx.squash;
                    m_shaderSquashStrech[i].y = m_VertExmotionSensors[i].m_stretch;

                    m_shaderSpeed[i] = m_VertExmotionSensors[i].m_speedStrech;
                    m_shaderMotionAxis[i] = m_VertExmotionSensors[i].m_torqueAxis;

                    Vector3 axis = m_VertExmotionSensors[i].transform.right;
                    m_shaderAxisScaleX[i].x = axis.x;
                    m_shaderAxisScaleX[i].y = axis.y;
                    m_shaderAxisScaleX[i].z = axis.z;
                    m_shaderAxisScaleX[i].w = m_VertExmotionSensors[i].m_params.scale.x;

                    axis = m_VertExmotionSensors[i].transform.up;
                    m_shaderAxisScaleY[i].x = axis.x;
                    m_shaderAxisScaleY[i].y = axis.y;
                    m_shaderAxisScaleY[i].z = axis.z;
                    m_shaderAxisScaleY[i].w = m_VertExmotionSensors[i].m_params.scale.y;

                    axis = m_VertExmotionSensors[i].transform.forward;
                    m_shaderAxisScaleZ[i].x = axis.x;
                    m_shaderAxisScaleZ[i].y = axis.y;
                    m_shaderAxisScaleZ[i].z = axis.z;
                    m_shaderAxisScaleZ[i].w = m_VertExmotionSensors[i].m_params.scale.z;

                    if (i < m_sensorsLinks.Count)
                        m_shaderLink[i] = m_sensorsLinks[i];
                    else
                        m_shaderLink[i] = 0;
                }
                else
                {
                    m_shaderSensorPos[i] = Vector4.zero;
                    m_shaderMotionDirection[i] = Vector4.zero;
                    m_shaderRCT[i] = Vector4.zero;
                    m_shaderSquashStrech[i] = Vector4.zero;
                    m_shaderSpeed[i] = Vector4.zero;
                    m_shaderMotionAxis[i] = Vector4.zero;
                    m_shaderAxisScaleX[i] = ShaderAxisScaleX;
                    m_shaderAxisScaleY[i] = ShaderAxisScaleY;
                    m_shaderAxisScaleZ[i] = ShaderAxisScaleZ;
                }

            }
            
            m_matPropBlk.SetFloat(m_shaderPropertyId[(int)eShaderProperty.KVM_NbSensors], m_VertExmotionSensors.Count);
            m_matPropBlk.SetVectorArray(m_shaderPropertyId[(int)eShaderProperty.KVM_SensorPosition], m_shaderSensorPos);
            m_matPropBlk.SetVectorArray(m_shaderPropertyId[(int)eShaderProperty.KVM_MotionDirection], m_shaderMotionDirection);
            m_matPropBlk.SetVectorArray(m_shaderPropertyId[(int)eShaderProperty.KVM_RadiusCentripetalTorque], m_shaderRCT);
            m_matPropBlk.SetVectorArray(m_shaderPropertyId[(int)eShaderProperty.KVM_SquashStretch], m_shaderSquashStrech);
            m_matPropBlk.SetVectorArray(m_shaderPropertyId[(int)eShaderProperty.KVM_Speed], m_shaderSpeed);
            m_matPropBlk.SetVectorArray(m_shaderPropertyId[(int)eShaderProperty.KVM_MotionAxis], m_shaderMotionAxis);
            m_matPropBlk.SetVectorArray(m_shaderPropertyId[(int)eShaderProperty.KVM_AxisXScale], m_shaderAxisScaleX);
            m_matPropBlk.SetVectorArray(m_shaderPropertyId[(int)eShaderProperty.KVM_AxisYScale], m_shaderAxisScaleY);
            m_matPropBlk.SetVectorArray(m_shaderPropertyId[(int)eShaderProperty.KVM_AxisZScale], m_shaderAxisScaleZ);
            m_matPropBlk.SetFloatArray(m_shaderPropertyId[(int)eShaderProperty.KVM_Link], m_shaderLink);

            m_matPropBlk.SetFloat(m_shaderPropertyId[(int)eShaderProperty.KVM_NormalCorrection], m_normalCorrection);
            m_matPropBlk.SetFloat(m_shaderPropertyId[(int)eShaderProperty.KVM_NormalSmooth], m_normalSmooth);

            //-------------------------------------------------------------------

            renderer.SetPropertyBlock ( m_matPropBlk);


			
		}
		
		/// Clear all motion data. \n
		/// Send all parameters to shader.	
		public void DisableMotion()
		{
            CleanShaderProperties();
            return;

   //         if ( !Application.isPlaying )
   //         {
   //             //renderer.SetPropertyBlock(new MaterialPropertyBlock());
   //             return;
   //         }
			
			//if (m_shaderSensorPos.Length != m_VertExmotionSensors.Count)
			//	InitShaderParam ();

   //         //m_matPropBlk.Clear();
   //         if (m_matPropBlk == null)
   //             m_matPropBlk = new MaterialPropertyBlock();
   //         //renderer.GetPropertyBlock(m_matPropBlk);

   //         /*
   //         //  < unity 5.4
   //         for (int i=0; i<m_VertExmotionSensors.Count; ++i)
			//{

			//	m_matPropBlk.SetVector( m_shaderParamNames["_MotionDirection"][i], Vector4.zero );
			//	m_matPropBlk.SetVector( m_shaderParamNames["_MotionAxis"][i], Vector4.zero );
			//	m_matPropBlk.SetVector( m_shaderParamNames["_RadiusCentripetalTorque"][i], Vector4.zero );				
			//}
   //         */

   //         //unity 5.4
   //         /*
   //         m_shaderSensorPos = new Vector4[MAX_SENSOR];
   //         m_shaderMotionDirection = new Vector4[MAX_SENSOR];
   //         m_shaderRCT = new Vector4[MAX_SENSOR];
   //         m_shaderSquashStrech = new Vector4[MAX_SENSOR];
   //         m_shaderSpeed = new Vector4[MAX_SENSOR];
   //         m_shaderMotionAxis = new Vector4[MAX_SENSOR];
   //         */

   //         //Debug.Log("disable sensors " + m_VertExmotionSensors.Count);

   //         for (int i = 0; i < m_shaderSensorPos.Length; ++i)
   //         //for (int i = 0; i < MAX_SENSOR; ++i)
   //         {
   //             m_shaderSensorPos[i] = Vector4.zero;
   //             m_shaderMotionDirection[i] = Vector4.zero;
   //             m_shaderRCT[i] = Vector4.zero;
   //             m_shaderSquashStrech[i] = Vector4.zero;
   //             m_shaderSpeed[i] = Vector4.zero;
   //             m_shaderMotionAxis[i] = Vector4.zero;

   //         }
            
   //         m_matPropBlk.SetVectorArray("_SensorPosition", m_shaderSensorPos);
   //         m_matPropBlk.SetVectorArray("_MotionDirection", m_shaderMotionDirection);
   //         m_matPropBlk.SetVectorArray("_RadiusCentripetalTorque", m_shaderRCT);
   //         m_matPropBlk.SetVectorArray("_SquashStretch", m_shaderSquashStrech);
   //         m_matPropBlk.SetVectorArray("_Speed", m_shaderSpeed);
   //         m_matPropBlk.SetVectorArray("_MotionAxis", m_shaderMotionAxis);
            

   //         //m_matPropBlk.Clear();
   //         renderer.SetPropertyBlock (m_matPropBlk);
            



        }



        public void CleanShaderProperties()
        {

            //Debug.Log("CleanShaderProperties");
            if (m_shaderPropertyId == null)
                InitShaderParam();


            m_matPropBlk = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(m_matPropBlk);
            m_matPropBlk.SetFloat(m_shaderPropertyId[(int)eShaderProperty.KVM_NbSensors], 0);
            m_matPropBlk.SetVectorArray(m_shaderPropertyId[(int)eShaderProperty.KVM_SensorPosition], new Vector4[MAX_SENSOR]);
            m_matPropBlk.SetVectorArray(m_shaderPropertyId[(int)eShaderProperty.KVM_MotionDirection], new Vector4[MAX_SENSOR]);
            m_matPropBlk.SetVectorArray(m_shaderPropertyId[(int)eShaderProperty.KVM_RadiusCentripetalTorque], new Vector4[MAX_SENSOR]);
            m_matPropBlk.SetVectorArray(m_shaderPropertyId[(int)eShaderProperty.KVM_SquashStretch], new Vector4[MAX_SENSOR]);
            m_matPropBlk.SetVectorArray(m_shaderPropertyId[(int)eShaderProperty.KVM_Speed], new Vector4[MAX_SENSOR]);
            m_matPropBlk.SetVectorArray(m_shaderPropertyId[(int)eShaderProperty.KVM_MotionAxis], new Vector4[MAX_SENSOR]);
            
            m_matPropBlk.SetVectorArray(m_shaderPropertyId[(int)eShaderProperty.KVM_AxisXScale], new Vector4[MAX_SENSOR]);
            m_matPropBlk.SetVectorArray(m_shaderPropertyId[(int)eShaderProperty.KVM_AxisYScale], new Vector4[MAX_SENSOR]);
            m_matPropBlk.SetVectorArray(m_shaderPropertyId[(int)eShaderProperty.KVM_AxisZScale], new Vector4[MAX_SENSOR]);
            m_matPropBlk.SetFloatArray(m_shaderPropertyId[(int)eShaderProperty.KVM_Link], new float[MAX_SENSOR]);
            
            renderer.SetPropertyBlock(m_matPropBlk);
        }




        /// <summary>
        /// Sets the time scale on each sensor.
        /// </summary>
        /// <param name="timeScale">Time scale.</param>
        public void SetTimeScale( float timeScale )
		{
			for (int i=0; i<m_VertExmotionSensors.Count; ++i)
				m_VertExmotionSensors[i].timeScale = timeScale;
		}



		/// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
		public VertExmotionSensorBase CreateSensor()
		{
            return CreateSensor("VertExmotion Sensor");
		}



        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public VertExmotionSensorBase CreateSensor(string name)
        {
            GameObject go = new GameObject(name);
            VertExmotionSensorBase vms = go.AddComponent<VertExmotionSensorBase>();
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            go.transform.localRotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;
            vms.m_parent = transform;
            //m_VertExmotionSensors.Add( vms );

            return vms;
        }




        /// <summary>
        /// Add a sensor to the list
        /// </summary>
        /// <param name="sensor"></param>
        public void AddSensor( VertExmotionSensorBase sensor )
        {
            if (sensor == null)
                return;

            while (m_sensorsLinks.Count < Sensors.Count)
                m_sensorsLinks.Add(0);

            Sensors.Add(sensor);
            m_sensorsLinks.Add(0);
        }


        /// <summary>
        /// Add a linked sensor to the list
        /// </summary>
        /// <param name="sensor"></param>
        /// <param name="linked"></param>
        public void AddSensor(VertExmotionSensorBase sensor, bool linked)
        {
            if (sensor == null)
                return;

            //link
            // 0 -> unlinked
            // 1 -> master link
            // -1 -> slave link
            // 2-> master & slave

            if(m_sensorsLinks.Count > Sensors.Count)
                m_sensorsLinks.RemoveRange(Sensors.Count, m_sensorsLinks.Count - Sensors.Count);

            while (m_sensorsLinks.Count < Sensors.Count)
                m_sensorsLinks.Add(0);

            if (linked && Sensors.Count>0)
            {
                int previousID = m_sensorsLinks.Count-1;

                if (m_sensorsLinks[previousID] == 0)
                    m_sensorsLinks[previousID] = 1;
                if (m_sensorsLinks[previousID] == -1)
                    m_sensorsLinks[previousID] = 2;

                m_sensorsLinks.Add(-1);
            }
            else
            {
                m_sensorsLinks.Add(0);
            }

            Sensors.Add(sensor);

        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="sensor"></param>
        public void RemoveSensor(VertExmotionSensorBase sensor)
        {         

            int idx = Sensors.FindIndex(s => s == sensor);

            if (idx == -1)
                return;

            while (m_sensorsLinks.Count < Sensors.Count)
                m_sensorsLinks.Add(0);

            Sensors.RemoveAt(idx);

            if (m_sensorsLinks.Count > 0)
            {
                //link
                // 0 -> unlinked
                // 1 -> master link
                // -1 -> slave link
                // 2-> master & slave

                int link = (int)m_sensorsLinks[idx];
                switch (link)
                {
                    case 1:                    
                        if (m_sensorsLinks.Count > idx + 1)
                            m_sensorsLinks[idx + 1] = 1;
                        break;

                    case -1:
                        if (idx > 0)
                        {
                            if (m_sensorsLinks[idx - 1] == 1)                            
                                m_sensorsLinks[idx - 1] = 0;                            
                            else if (m_sensorsLinks[idx - 1] == 2)                                                            
                                m_sensorsLinks[idx - 1] = -1;
                            
                        }
                        break;
                }
            }

            m_sensorsLinks.RemoveAt(idx);

        }


        /// <summary>
        /// Link a sensor to the previous in the list
        /// </summary>
        /// <param name="sensor"></param>
        public void Link(VertExmotionSensorBase sensor)
        {
            Link(Sensors.IndexOf(sensor));
        }


        /// <summary>
        /// Link a sensor to the previous in the list
        /// </summary>
        /// <param name="sensorId"></param>
        public void Link(int sensorId)
        {
            if (sensorId > Sensors.Count  || sensorId<0)
                return;

            //link
            // 0 -> unlinked
            // 1 -> master link
            // -1 -> slave link
            // 2-> master & slave

            while (m_sensorsLinks.Count < Sensors.Count)
                m_sensorsLinks.Add(0);

            if (sensorId > 0)
            {
                int previous = sensorId - 1;
                switch ((int)m_sensorsLinks[sensorId])
                {
                    case 0:
                        if ((int)m_sensorsLinks[previous] == -1)
                            m_sensorsLinks[previous] = 2;
                        else
                            m_sensorsLinks[previous] = 1;

                        m_sensorsLinks[sensorId] = -1;
                        break;

                    case 1:
                        if ((int)m_sensorsLinks[previous] == -1)
                            m_sensorsLinks[previous] = 2;
                        else
                            m_sensorsLinks[previous] = 2;

                        m_sensorsLinks[sensorId] = 2;
                        break;

                }

                
            }
            
        }




        /// <summary>
        /// Unlink a sensor
        /// </summary>
        /// <param name="sensor"></param>
        public void UnLink(VertExmotionSensorBase sensor)
        {
            UnLink(Sensors.IndexOf(sensor));
        }


        /// <summary>
        /// Unlink a sensor
        /// </summary>
        /// <param name="sensorId"></param>
        public void UnLink(int sensorId)
        {
            if (sensorId > Sensors.Count || sensorId < 0)
                return;

            //link
            // 0 -> unlinked
            // 1 -> master link
            // -1 -> slave link
            // 2-> master & slave

            while (m_sensorsLinks.Count < Sensors.Count)
                m_sensorsLinks.Add(0);

            if (sensorId > 0)
            {
                int previous = sensorId - 1;
                switch ((int)m_sensorsLinks[sensorId])
                {                    
                    case -1:
                        if ((int)m_sensorsLinks[previous] == 1)
                            m_sensorsLinks[previous] = 0;
                        else
                            m_sensorsLinks[previous] = -1;
                    break;

                    case 2:
                        if ((int)m_sensorsLinks[previous] == 1)
                            m_sensorsLinks[previous] = 0;
                        else
                            m_sensorsLinks[previous] = -1;

                        int next = sensorId + 1;
                        if(next< m_sensorsLinks.Count)
                        {
                            m_sensorsLinks[next] = 1;
                        }

                        break;
                }

                m_sensorsLinks[sensorId] = 0;
            }
        
        }





        /// <summary>
        /// ClassExists.
        /// </summary>
        /// <returns><c>true</c>, if class Exist, <c>false</c> otherwise.</returns>
        /// <param name="className">Class name.</param>
        public static bool ClassExists( string className )
		{
			System.Reflection.Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
			
			foreach (var A in assemblies)
			{
				System.Type[] types = A.GetTypes();
				foreach (var T in types)				
					if (T.Name == className )					
						return true;
				
			}
			return false;
		}
		
		
		public static System.Type ClassType( string className )
		{
			System.Reflection.Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
			
			foreach (var A in assemblies)
			{
				System.Type[] types = A.GetTypes();
				foreach (var T in types)				
					if (T.Name == className )					
						return T;
				
			}
			return null;
		}




        private void OnDisable()
        {
            CleanShaderProperties();
        }




        void OnDestroy()
        {

            //Debug.Log("OnDestroy");    
            DisableMotion();

            if (m_mesh != null)
            {
                if (Application.isPlaying)
                    Destroy(m_mesh);
                else
                    DestroyImmediate(m_mesh);
            }

            if (m_runtimeMeshInstance != null)
            {
                if (Application.isPlaying)
                    Destroy(m_runtimeMeshInstance);
                else
                    DestroyImmediate(m_runtimeMeshInstance);
            }

            //computeShader buffer
            if (m_vertexBuff != null)
                m_vertexBuff.Release();
            if (m_normalBuff != null)
                m_normalBuff.Release();
            if (m_tangentBuff != null)
                m_tangentBuff.Release();
            if (m_colorBuff != null)
                m_colorBuff.Release();

        }


        ///Ignore current frame motion.
        public void IgnoreFrame()
        {
            for (int i = 0; i < m_VertExmotionSensors.Count; ++i)
                m_VertExmotionSensors[i].IgnoreFrame();
        }


        ///Reset motion.
        public void ResetMotion()
        {
            for (int i = 0; i < m_VertExmotionSensors.Count; ++i)
                m_VertExmotionSensors[i].ResetMotion();
        }


        /// <summary>
        /// Ignore collider from colliding with sensors' collision zones
        /// </summary>
        /// <param name="collider"></param>
        /// <param name="ignore"></param>
        public void IgnoreCollision(Collider collider, bool ignore)
        {
            for (int i = 0; i < m_VertExmotionSensors.Count; ++i)
                m_VertExmotionSensors[i].IgnoreCollision(collider,ignore);
        }







        #region BakeMash compute shader

        ComputeShader m_cs;
        ComputeBuffer m_vertexBuff = null;
        ComputeBuffer m_normalBuff = null;
        ComputeBuffer m_tangentBuff = null;
        ComputeBuffer m_colorBuff = null;
        Vector3[] m_originalVertice = null;
        Vector3[] m_originalNormals = null;       

        public Mesh BakeMesh()
        {
            if (!SystemInfo.supportsComputeShaders)
            {
                Debug.LogError("System doesn't support Compute shaders");
                return null;
            }

            if (m_smr != null)
            {
                if (m_mesh == null)
                {
                    m_mesh = new Mesh();
                    m_mesh.name = "VertExmotion_BakeMesh";
                }
                m_smr.BakeMesh(m_mesh);
            }
            else if (m_mf != null)
            {
                if (m_mesh == null)
                {
                    m_mesh = Instantiate(m_mf.sharedMesh);
                    m_originalVertice = m_mesh.vertices;
                    m_originalNormals = m_mesh.normals;
                    m_mesh.name = "VertExmotion_BakeMesh";
                }
                else
                {
                    m_mesh.vertices = m_originalVertice;
                    m_mesh.normals = m_originalNormals;
                }
            }
            else
            {
                return null;//no renderer detected
            }
            m_mesh.MarkDynamic();



            if (m_cs == null)
                m_cs = Instantiate(Resources.Load<ComputeShader>("VertExmotionBakeMesh"));
            //m_cs = Resources.Load<ComputeShader>("VertExmotionBakeMesh");


            UpdateShaders();

            int[] floatPID ={
                VertExmotionBase.m_shaderPropertyId[(int)VertExmotionBase.eShaderProperty.KVM_NbSensors],
                VertExmotionBase.m_shaderPropertyId[(int)VertExmotionBase.eShaderProperty.KVM_NormalCorrection],
                VertExmotionBase.m_shaderPropertyId[(int)VertExmotionBase.eShaderProperty.KVM_NormalSmooth]
            };

            for (int i = 0; i < floatPID.Length; ++i)
                m_cs.SetFloat(floatPID[i], m_matPropBlk.GetFloat(floatPID[i]));

            int[] vectorArrayPID ={
                VertExmotionBase.m_shaderPropertyId[(int)VertExmotionBase.eShaderProperty.KVM_SensorPosition],
                VertExmotionBase.m_shaderPropertyId[(int)VertExmotionBase.eShaderProperty.KVM_MotionDirection],
                VertExmotionBase.m_shaderPropertyId[(int)VertExmotionBase.eShaderProperty.KVM_RadiusCentripetalTorque],
                VertExmotionBase.m_shaderPropertyId[(int)VertExmotionBase.eShaderProperty.KVM_SquashStretch],
                VertExmotionBase.m_shaderPropertyId[(int)VertExmotionBase.eShaderProperty.KVM_Speed],
                VertExmotionBase.m_shaderPropertyId[(int)VertExmotionBase.eShaderProperty.KVM_MotionAxis],
                VertExmotionBase.m_shaderPropertyId[(int)VertExmotionBase.eShaderProperty.KVM_AxisXScale],
                VertExmotionBase.m_shaderPropertyId[(int)VertExmotionBase.eShaderProperty.KVM_AxisYScale],
                VertExmotionBase.m_shaderPropertyId[(int)VertExmotionBase.eShaderProperty.KVM_AxisZScale]
            };


            for (int i = 0; i < vectorArrayPID.Length; ++i)
                m_cs.SetVectorArray(vectorArrayPID[i], m_matPropBlk.GetVectorArray(vectorArrayPID[i]));


            int floatArrayPID = VertExmotionBase.m_shaderPropertyId[(int)VertExmotionBase.eShaderProperty.KVM_Link];
            m_cs.SetFloats(floatArrayPID, m_matPropBlk.GetFloatArray(floatArrayPID));


            int csKID = m_cs.FindKernel("CSMain");


            if (m_vertexBuff == null)
            {
                m_vertexBuff = new ComputeBuffer(m_mesh.vertexCount, System.Runtime.InteropServices.Marshal.SizeOf(typeof(Vector3)));
                m_normalBuff = new ComputeBuffer(m_mesh.vertexCount, System.Runtime.InteropServices.Marshal.SizeOf(typeof(Vector3)));
                m_tangentBuff = new ComputeBuffer(m_mesh.vertexCount, System.Runtime.InteropServices.Marshal.SizeOf(typeof(Vector4))); ;
                m_colorBuff = new ComputeBuffer(m_mesh.vertexCount, System.Runtime.InteropServices.Marshal.SizeOf(typeof(Color)));
                m_cs.SetBuffer(csKID, "_vertexBuf", m_vertexBuff);
                m_cs.SetBuffer(csKID, "_normalBuf", m_normalBuff);
                m_cs.SetBuffer(csKID, "_tangentBuf", m_tangentBuff);
                m_cs.SetBuffer(csKID, "_colorBuf", m_colorBuff);
            }

            Vector3[] vertice = m_mesh.vertices;
            Vector3[] normals = m_mesh.normals;
            Vector4[] tangents = m_mesh.tangents;
            Color[] colors = m_mesh.colors;

            m_vertexBuff.SetData(vertice);
            m_normalBuff.SetData(normals);
            m_tangentBuff.SetData(tangents);
            m_colorBuff.SetData(colors);

            m_cs.SetMatrix("_ObjectToWorld", transform.localToWorldMatrix);
            m_cs.SetMatrix("_WorldToObject", transform.worldToLocalMatrix);

            m_cs.Dispatch(csKID, Mathf.CeilToInt((float)m_mesh.vertexCount / (float)512), 1, 1);
            m_vertexBuff.GetData(vertice);
            m_normalBuff.GetData(normals);

            m_mesh.vertices = vertice;
            m_mesh.normals = normals;

            return m_mesh;
        }

        #endregion


    }





}

