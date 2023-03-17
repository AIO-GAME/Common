//#pragma shader_feature VERTEXMOTION_OFF //copy this line in shader

#if SHADER_TARGET < 30
#define VERTEXMOTION_LOW_MOBILE
#endif

#ifdef VERTEXMOTION_ASE_HD_LW_RP
#undef VERTEXMOTION_LOW_MOBILE
#define VERTEXMOTION_HIDE_STANDARD_FUNCTIONS
#endif

#ifdef VERTEXMOTION_URP
#undef VERTEXMOTION_LOW_MOBILE
#define VERTEXMOTION_HIDE_STANDARD_FUNCTIONS
#endif

#ifdef VERTEXMOTION_HDRP
#undef VERTEXMOTION_LOW_MOBILE
#define VERTEXMOTION_HIDE_STANDARD_FUNCTIONS
#endif


//#define VERTEXMOTION_EXTERNAL_FORCES

/*
//multiple include files doesn't work for relative path before unity 2018 :(
#include  "VertExmotion_core.cginc"

#ifndef VERTEXMOTION_ASE_HD_LW_RP
#include "VertExmotion_functions.cginc"
#endif
#include "VertExmotion_ase.cginc"
*/


//-----------------------------------------------------------------
//CORE
//-----------------------------------------------------------------
#ifdef VERTEXMOTION_LOW_MOBILE
static const int NB_SENSORS = 4;
#else
static const int NB_SENSORS = 20;
#endif


uniform float4 KVM_SensorPosition[NB_SENSORS];
uniform float4 KVM_MotionDirection[NB_SENSORS];
uniform float4 KVM_MotionAxis[NB_SENSORS];
uniform float4 KVM_RadiusCentripetalTorque[NB_SENSORS];
uniform float4 KVM_SquashStretch[NB_SENSORS];
uniform float4 KVM_Speed[NB_SENSORS];
uniform float4 KVM_AxisXScale[NB_SENSORS];
uniform float4 KVM_AxisYScale[NB_SENSORS];
uniform float4 KVM_AxisZScale[NB_SENSORS];
uniform float KVM_Link[NB_SENSORS];
float KVM_NormalCorrection;
float KVM_NormalSmooth;
float KVM_NbSensors;

#define PI_180            3.14159265359f/180.0f


#ifdef VERTEXMOTION_EXTERNAL_FORCES

sampler2D _VertexColorTex;
int KVM_ID_MODE;
int _IDTextureSize;
float2 GetUVFromIdx(int vID)
{
	float textureSize = float(_IDTextureSize);
	float halfPixel = (1.0 / textureSize)*.5;
	float idx = float(vID) / textureSize;
	float idy = floor(idx) / textureSize;
	idx -= floor(idx);

	return float2(idx + halfPixel, 1 - idy - halfPixel);
}

#endif //VERTEXMOTION_EXTERNAL_FORCES


float3x3 RotAxis(float3 axis, float angle)
{
    axis = normalize(axis);
    float s, c;
    sincos(angle, s, c);
    float oc = 1.0 - c;
    float3 as = axis * s;
    float3x3 p = float3x3(axis.x * axis, axis.y * axis, axis.z * axis);
    float3x3 q = float3x3(c, -as.z, as.y, as.z, c, -as.x, -as.y, as.x, c);
    return p * oc + q;
}




float4 VertExmotionBase(float4 wrldPos, float4 col, inout float weight)
{

    #if VERTEXMOTION_OFF
    return wrldPos;
    #endif

	#ifdef VERTEXMOTION_EXTERNAL_FORCES
    if( KVM_ID_MODE )
    {
		float2 vUV = GetUVFromIdx(col);
		col = tex2Dlod(_VertexColorTex, float4(vUV, 0, 0));
	}
	#endif

    float3 torqueDir = float3(0, 0, 0);
    float4 motionDir = float4(0, 0, 0, 0);
    float3 centripetalDir = float3(0, 0, 0);
    float dist;
    float3 squash = float3(0, 0, 0);

	//KVM_NbSensors = clamp(KVM_NbSensors, 0, NB_SENSORS);

    for (int i = 0; i < NB_SENSORS; ++i)
    {
        if (i < KVM_NbSensors)
        {
#ifndef VERTEXMOTION_LOW_MOBILE	
			int idNext = clamp(i + 1, 0, NB_SENSORS - 1);

			float lrp = 0;

			if (KVM_Link[i] == 1)
			{
				float3 v = KVM_SensorPosition[i].xyz - KVM_SensorPosition[idNext].xyz;
				float3 nv = normalize(v);
				float p0 = dot(nv, normalize(wrldPos.xyz - KVM_SensorPosition[idNext].xyz));
				float p1 = dot(nv, wrldPos.xyz - KVM_SensorPosition[idNext].xyz);
				lrp = p0 <= 0.0 ? 1.0 : (p1 >= length(v) ? 0.0 : 1.0 - (p1 / length(v)));
				//lrp = 0;
				//lrp = 1;
				//lrp = 1.0 - (p1 / length(v));
			}


			//---------------------------		
			float4 SensorPosition = lerp(KVM_SensorPosition[i], KVM_SensorPosition[idNext], lrp);
			float4 MotionDirection = lerp(KVM_MotionDirection[i], KVM_MotionDirection[idNext], lrp);
			float4 MotionAxis = lerp(KVM_MotionAxis[i], KVM_MotionAxis[idNext], lrp);
			float4 RadiusCentripetalTorque = lerp(KVM_RadiusCentripetalTorque[i], KVM_RadiusCentripetalTorque[idNext], lrp);
			float4 SquashStretch = lerp(KVM_SquashStretch[i], KVM_SquashStretch[idNext], lrp);
			float4 Speed = lerp(KVM_Speed[i], KVM_Speed[idNext], lrp);
			float4 AxisXScale = lerp(KVM_AxisXScale[i], KVM_AxisXScale[idNext], lrp);
			float4 AxisYScale = lerp(KVM_AxisYScale[i], KVM_AxisYScale[idNext], lrp);
			float4 AxisZScale = lerp(KVM_AxisZScale[i], KVM_AxisZScale[idNext], lrp);
			/*
			float3 sensorDir0 = wrldPos.xyz - SensorPosition.xyz;
			dist = length(sensorDir0);
			if (i == 0 && dist < RadiusCentripetalTorque.x)
				return float4(SensorPosition.xyz, 1);
			return wrldPos;
			*/
			///--------------------------
#else
            float4 SensorPosition = KVM_SensorPosition[i];
            float4 MotionDirection = KVM_MotionDirection[i];
            float4 MotionAxis = KVM_MotionAxis[i];
            float4 RadiusCentripetalTorque = KVM_RadiusCentripetalTorque[i];
            float4 SquashStretch = KVM_SquashStretch[i];
            float4 Speed = KVM_Speed[i];
            float4 AxisXScale = KVM_AxisXScale[i];
            float4 AxisYScale = KVM_AxisYScale[i];
            float4 AxisZScale = KVM_AxisZScale[i];
#endif

            float3 sensorDir = wrldPos.xyz - SensorPosition.xyz;
            dist = length(sensorDir);
            if (dist < RadiusCentripetalTorque.x && KVM_Link[i] >= 0)
            {
                float f = 1.0f - dist / (RadiusCentripetalTorque.x + .0000001f);
                float p = pow(abs(f), MotionDirection.w);
                weight = i == 0 ? f : (weight + f) * .5;
                f = p;
                if (length(MotionAxis.xyz) > 0)
                    torqueDir = mul(RotAxis(MotionAxis.xyz, RadiusCentripetalTorque.z * PI_180 * f), (wrldPos - SensorPosition).xyz) - (wrldPos - SensorPosition).xyz;

                centripetalDir = dist > 0 ? normalize((wrldPos.xyz - SensorPosition.xyz).xyz) : float3(0, 0, 0);
                float3 motion = (MotionDirection.xyz + torqueDir + centripetalDir * RadiusCentripetalTorque.y) * f;

                if (dist > 0)
                {
					//scale
                    float3 axisScale;
                    axisScale = dot(normalize(sensorDir), normalize(AxisXScale.xyz)) * dist * AxisXScale.xyz;
                    motion += (axisScale * AxisXScale.w - axisScale) * f;
                    axisScale = dot(normalize(sensorDir), normalize(AxisYScale.xyz)) * dist * AxisYScale.xyz;
                    motion += (axisScale * AxisYScale.w - axisScale) * f;
                    axisScale = dot(normalize(sensorDir), normalize(AxisZScale.xyz)) * dist * AxisZScale.xyz;
                    motion += (axisScale * AxisZScale.w - axisScale) * f;
                }

#ifndef VERTEXMOTION_LOW_MOBILE
				if (length(Speed.xyz) > 0)
				{
					//stretch
					float d = dot(Speed.xyz, centripetalDir);
					if (d >= 0)
						motion += d * d * d * SquashStretch.y * Speed.xyz;

					//stretch reduce volume
					float3 c1 = cross(normalize(Speed.xyz), centripetalDir);
					float3 c2 = cross(normalize(Speed.xyz), c1);
					float d2 = dot((wrldPos - SensorPosition).xyz, c2);

					if (length(c2) > 0)
					{
						motion -= normalize(c2)* length(Speed.xyz) * d2 * SquashStretch.y * .8f;
						motion += normalize(c2)* length(Speed.xyz)* d2 * SquashStretch.x;
					}
				}
#endif				
                float layerWeight = ((KVM_SensorPosition[i].w == 2) ? col.g : ((KVM_SensorPosition[i].w == 1) ? col.r : ((KVM_SensorPosition[i].w == 3) ? col.b : max(max(col.r, col.g), col.b))));
                motionDir.xyz += motion * layerWeight;
                weight *= layerWeight;
            }
        }
    }


    
    return (wrldPos + motionDir);    
    
}





#ifndef VERTEXMOTION_HIDE_STANDARD_FUNCTIONS

//-----------------------------------------------------------------
//FUNCTIONS
//-----------------------------------------------------------------

#include "UnityCG.cginc"



float4 VertExmotion(float4 vpos, float4 col)
{
    float4 wrldPos = mul(unity_ObjectToWorld, vpos);
    float w = 0;
    wrldPos = VertExmotionBase(wrldPos, col, w);
    vpos.xyz = mul(unity_WorldToObject, wrldPos);
    return vpos;
}


float4 VertExmotion(float4 vpos, float4 col, inout float weight)
{
    float4 wrldPos = mul(unity_ObjectToWorld, vpos);
    wrldPos = VertExmotionBase(wrldPos, col, weight);
    vpos.xyz = mul(unity_WorldToObject, wrldPos);
    return vpos;
}


float4 VertExmotion(float4 vpos, float4 col, inout float3 n, float4 t)
{
	//_NormalCorrection = .8;
    float w = 0;
    float4 newpos = VertExmotion(vpos, col, w);
#ifndef VERTEXMOTION_LOW_MOBILE
#if !VERTEXMOTION_NORMAL_CORRECTION_OFF
	if (length(t.xyz) > 0 && KVM_NormalCorrection>0)
	{
		float4 biTan = float4(cross(n, t.xyz), 0);
		float4 Tan = float4(cross( biTan.xyz, n.xyz), 0);
		float4 posTan = VertExmotion(vpos + normalize(Tan) * .001, col);
		float4 posBiTan = VertExmotion(vpos + normalize(biTan) * .001, col);
		float3 newNormal = normalize(cross(normalize(posTan - newpos), normalize(posBiTan - newpos)));
		if(length(newNormal)>0)
			n = lerp(n, newNormal, KVM_NormalCorrection*(w < KVM_NormalSmooth ? w * (1.0 / KVM_NormalSmooth) : 1));
	}
#endif
#endif
    return newpos;
}



float4 VertExmotionUV(float4 vpos, float4 uv)
{
    half4 wrldPos = mul(unity_ObjectToWorld, vpos);
    int sensorId = 0;

	//compute torque
    half3 torqueDir = half3(0, 0, 0);
    half4 motionDir = half4(0, 0, 0, 0);
    half3 centripetalDir = half3(0, 0, 0);
    half dist;

    for (int i = 0; i < NB_SENSORS; ++i)
    {
        sensorId = i;

        torqueDir.xyz = cross(normalize(wrldPos.xyz - KVM_SensorPosition[sensorId].xyz).xyz, KVM_MotionAxis[sensorId].xyz);
        torqueDir *= KVM_RadiusCentripetalTorque[sensorId].z;

        centripetalDir = normalize((wrldPos - KVM_SensorPosition[sensorId]).xyz) * KVM_RadiusCentripetalTorque[sensorId].y;
        motionDir.xyz += KVM_MotionDirection[sensorId].xyz + torqueDir + centripetalDir;
    }

    vpos.xyz = mul(unity_WorldToObject, wrldPos + motionDir * uv.y * uv.y * uv.y).xyz;
    vpos.w = vpos.w;
    return vpos;
}


void VertExmotion(inout appdata_full v)
{
	//float4 position = VertExmotion(v.vertex, v.color);//without normal fix
    float4 position = VertExmotion(v.vertex, v.color, v.normal, v.tangent);
    v.vertex = position;
}


void VertExmotionUV(inout appdata_full v)
{
    v.vertex = VertExmotionUV(v.vertex, v.texcoord);
}


//------------------------------------------
//Shader Forge function

float3 VertExmotionSF(float3 wrldXYZ, float wrldW, float3 col)
{
    float w = 0;
    return mul(unity_WorldToObject, VertExmotionBase(float4(wrldXYZ, wrldW), float4(col, 0), w) - float4(wrldXYZ, 1)).xyz;
}


float3 VertExmotionSF(float3 wrldXYZ, float wrldW, float3 col, inout float3 n, float3 t)
{
    float w = 0;
    float4 newpos = VertExmotionBase(float4(wrldXYZ, wrldW), float4(col, 0), w);
#ifndef VERTEXMOTION_LOW_MOBILE
#if !VERTEXMOTION_NORMAL_CORRECTION_OFF
	if (length(t.xyz) > 0 && KVM_NormalCorrection>0)
	{
		float w2 = 0;
		float4 biTan = float4(cross(n.xyz, t.xyz), 1);
		float4 Tan = float4(cross( biTan.xyz, n.xyz), 0);
		float4 posTan = VertExmotionBase(float4(wrldXYZ + normalize(Tan) * .001, wrldW), float4(col, 0), w2);
		float4 posBiTan = VertExmotionBase(float4(wrldXYZ + normalize(biTan) * .001, wrldW), float4(col, 0), w2);
		float3 newNormal = normalize(cross(normalize(posTan - newpos), normalize(posBiTan - newpos)));
		if(length(newNormal)>0)
			n = lerp(n, newNormal, KVM_NormalCorrection*(w < KVM_NormalSmooth ? w * (1.0 / KVM_NormalSmooth) : 1));
	}
#endif
#endif
    return mul(unity_WorldToObject, newpos - float4(wrldXYZ, 1)).xyz;
}




//------------------------------------------
// simple vertex pass for surface shader
void VertExmotion_appdata_full_vert(inout appdata_full v)
{
    VertExmotion(v);
}


//------------------------------------------
// simple vertex/fragment pass
struct VertExmotion_v2f
{
    float4 vertex : SV_POSITION;
};

VertExmotion_v2f VertExmotion_vert(appdata_full v)
{
    VertExmotion_v2f o;
    VertExmotion(v);
    o.vertex = UnityObjectToClipPos(v.vertex);
    return o;
}

fixed4 VertExmotion_frag(VertExmotion_v2f i) : SV_Target
{
    return float4(0, 0, 0, 0);
}


//-----------------------------------------------------------------

#endif //VERTEXMOTION_HIDE_STANDARD_FUNCTIONS not defined




//-----------------------------------------------------------------
//URP functions
//-----------------------------------------------------------------


#ifdef VERTEXMOTION_URP


float3 VertExmotionURP(float4 posOS, float4 col)
{
    float3 worldXYZ = TransformObjectToWorld(posOS.xyz).xyz;

	float w = 0;
	float w2 = 0;
	float4 newpos = VertExmotionBase(float4(worldXYZ, 0.0), col, w);
	return TransformWorldToObject(newpos.xyz).xyz;
}

float3 VertExmotionURP(float4 posOS, float4 col, inout float3 n, float4 t)
{
    float3 worldXYZ = TransformObjectToWorld(posOS.xyz).xyz;

	float w = 0;
	float w2 = 0;
	float4 newpos = VertExmotionBase(float4(worldXYZ, 0.0), col, w);

#if !VERTEXMOTION_NORMAL_CORRECTION_OFF
	if (length(t.xyz) > 0 && KVM_NormalCorrection > 0)
	{
		float4 biTan = float4(cross(n, t.xyz), 1);
		float4 Tan = float4(cross( biTan.xyz, n.xyz), 0);
		float4 posTan = VertExmotionBase(float4(worldXYZ + normalize(Tan.xyz) * .001, 0.0), col, w2);
		float4 posBiTan = VertExmotionBase(float4(worldXYZ + normalize(biTan.xyz) * .001, 0.0), col, w2);
		float3 newNormal = normalize(cross(normalize(posTan.xyz - newpos.xyz), normalize(posBiTan.xyz - newpos.xyz)));
		if(length(newNormal)>0)
			n = lerp(n, newNormal, KVM_NormalCorrection*(w < KVM_NormalSmooth ? w * (1.0 / KVM_NormalSmooth) : 1));
	}
#endif

	return TransformWorldToObject(newpos.xyz).xyz;
}

#endif





//-----------------------------------------------------------------
//HDRP functions
//-----------------------------------------------------------------


#ifdef VERTEXMOTION_HDRP


float3 VertExmotionHDRP(float4 posWrld, float4 col)
{
	float3 worldXYZ = posWrld.xyz;

	float w = 0;
	float w2 = 0;
	float4 newpos = VertExmotionBase(float4(worldXYZ, 0.0), col, w);
	return newpos.xyz;
}


float3 VertExmotionHDRP(float4 posWrld, float4 col, inout float3 n, float4 t)
{
	float3 worldXYZ = posWrld.xyz;

	float w = 0;
	float w2 = 0;
	float4 newpos = VertExmotionBase(float4(worldXYZ, 0.0), col, w);

#if !VERTEXMOTION_NORMAL_CORRECTION_OFF
	if (length(t.xyz) > 0 && KVM_NormalCorrection > 0)
	{
		float4 biTan = float4(cross(n, t.xyz), 1);
		float4 Tan = float4(cross(biTan.xyz, n.xyz), 0);
		float4 posTan = VertExmotionBase(float4(worldXYZ + normalize(Tan.xyz) * .001, 0.0), col, w2);
		float4 posBiTan = VertExmotionBase(float4(worldXYZ + normalize(biTan.xyz) * .001, 0.0), col, w2);
		float3 newNormal = normalize(cross(normalize(posTan.xyz - newpos.xyz), normalize(posBiTan.xyz - newpos.xyz)));
		if (length(newNormal) > 0)
			n = lerp(n, newNormal, KVM_NormalCorrection*(w < KVM_NormalSmooth ? w * (1.0 / KVM_NormalSmooth) : 1));
	}
#endif

	return newpos.xyz;
}

#endif



//-----------------------------------------------------------------
//Amplify Shader Editor
//-----------------------------------------------------------------


#ifdef VERTEXMOTION_ASE_HD_LW_RP


float3 VertExmotionWorldPosASE(float3 worldXYZ, float4 col)
{
	float w = 0;
	return VertExmotionBase(float4(worldXYZ, 0.0), col, w).xyz - worldXYZ;
}


float3 VertExmotionWorldPosASE(float3 worldXYZ, float4 col, inout float3 n, float3 t)
{
	float w = 0;
	float w2 = 0;
	float4 newpos = VertExmotionBase(float4(worldXYZ, 0.0), col, w);

#if !VERTEXMOTION_NORMAL_CORRECTION_OFF
	if (length(t.xyz) > 0 && KVM_NormalCorrection > 0)
	{
		float4 biTan = float4(cross(n, t.xyz), 1);
		float4 Tan = float4(cross( biTan.xyz, n.xyz), 0);
		float4 posTan = VertExmotionBase(float4(worldXYZ + normalize(Tan) * .001, 0.0), col, w2);
		float4 posBiTan = VertExmotionBase(float4(worldXYZ + normalize(biTan) * .001, 0.0), col, w2);
		float3 newNormal = normalize(cross(normalize(posTan - newpos), normalize(posBiTan - newpos)));
		if(length(newNormal)>0)
			n = lerp(n, newNormal, KVM_NormalCorrection*(w < KVM_NormalSmooth ? w * (1.0 / KVM_NormalSmooth) : 1));
	}
#endif

	return newpos - worldXYZ;
}

#else


#ifndef VERTEXMOTION_HIDE_STANDARD_FUNCTIONS

float3 VertExmotionAdvancedASE(float3 localXYZ, float4 col)
{
    return VertExmotion(float4(localXYZ, 1), col) - localXYZ;
}

float3 VertExmotionAdvancedASE(float3 localXYZ, float4 col, inout float3 n, float3 t)
{
    return VertExmotion(float4(localXYZ, 1), col, n, float4(t, 1)) - localXYZ;
}


float4 VertExmotionASE(inout appdata_full v)
{
    float4 oldpos = v.vertex;
    VertExmotion(v);
    float4 newpos = v.vertex;
    v.vertex = oldpos;
    return newpos - oldpos;
}

#endif //VERTEXMOTION_HIDE_STANDARD_FUNCTIONS

#endif



//-----------------------------------------------------------------