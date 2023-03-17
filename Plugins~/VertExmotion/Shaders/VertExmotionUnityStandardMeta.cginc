#include "UnityStandardMeta.cginc"
#include "VertExmotionUnityStandardInput.cginc"
#include "VertExmotion.cginc"


VertexInput ApplyVertExmotion ( VertexInputVM v )
{
	VertexInput v2;
	UNITY_INITIALIZE_OUTPUT(VertexInput, v2);
	//v2.vertex = v.vertex;
	v2.vertex = VertExmotion( v.vertex, v.color );
	v2.uv0 = v.uv0;	
	v2.uv1 = v.uv1;		
	v2.normal = v.normal;
#if defined(DYNAMICLIGHTMAP_ON) || defined(UNITY_PASS_META)
	v2.uv2	= v.uv2;	
#endif
#ifdef _TANGENT_TO_WORLD
	v2.tangent	= v.tangent;
#endif	
	
	
	return v2;
}




v2f_meta vert_metaVM (VertexInputVM v)
{
	return vert_meta( ApplyVertExmotion(v) );
}


