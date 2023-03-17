// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Hidden/VertExmotion_editor" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		 //_Weights ("Weights", Range (0, 1)) = 0
		 _SensorId( "SensorId", int ) = 1
		 //_LayerId("LayerId", int) = 1
	}
	SubShader {
		
			 Cull Off
		Pass{
	CGPROGRAM
		#pragma target 3.0
		
		#pragma vertex vert
		#pragma fragment frag
		//#pragma multi_compile VERTEXMOTION_GRADIENT_OFF VERTEXMOTION_GRADIENT_ON
		#include "UnityCG.cginc"
		#include "./VertExmotion.cginc"

		uniform float4 KVMEditor_SensorPosition[NB_SENSORS];
		uniform float4 KVMEditor_RadiusCentripetalTorque[NB_SENSORS];
		uniform float KVMEditor_Power[NB_SENSORS];
		uniform float KVMEditor_Links[NB_SENSORS];

		struct vertexToFragment {
		float4 vertex : POSITION;
		float2 uv_MainTex : TEXCOORD0;
		float4 color : COLOR;		
		};

				
		sampler2D _MainTex;
		int _SensorId;
		int _LayerId;
		float _gradientFactor = .5;
		//float _Power;

		void vert (appdata_full v, out vertexToFragment o) {

			

		
		/*
		if( _SensorId == -1 )
		{			
				
		}
		else
		*/
		if (_SensorId > -1)//id==-1 -> show paint info
		{

			
//#ifdef VERTEXMOTION_GRADIENT_ON	
			o.color.rgb = float3(0, 0, 0);
//#endif


			float4 wrldPos = mul( unity_ObjectToWorld, v.vertex  );	
			
			//paint info
			
			
			for (int i = 0; i < NB_SENSORS; ++i)			
			{
				if (i < KVM_NbSensors)
				{
					_LayerId = KVMEditor_SensorPosition[i].w;
					float3 layerColor = float3(0, 0, 0);
					if (_LayerId == 0)
						layerColor = float3(1, 1, 1) * max(max(v.color.r, v.color.g), v.color.b);
					if (_LayerId == 1)
						layerColor = v.color.rrr;
					if (_LayerId == 2)
						layerColor = v.color.ggg;
					if (_LayerId == 3)
						layerColor = v.color.bbb;
					

					int idNext = clamp(i + 1, 0, NB_SENSORS - 1);

					float lrp = 0;									

					//KVMEditor_Links[i] = 1;
					
					if (KVMEditor_Links[i] == 1)
					{
						float3 v = KVMEditor_SensorPosition[i].xyz - KVMEditor_SensorPosition[idNext].xyz;
						float3 nv = normalize(v);
						float p0 = dot(nv, normalize(wrldPos.xyz - KVMEditor_SensorPosition[idNext].xyz));
						float p1 = dot(nv, wrldPos.xyz - KVMEditor_SensorPosition[idNext].xyz);
						lrp = p0 <= 0.0 ? 1.0 : (p1 >= length(v) ? 0.0 : 1.0 - (p1 / length(v)));						
					}
					

					//---------------------------		
					float4 SensorPosition = lerp(KVMEditor_SensorPosition[i], KVMEditor_SensorPosition[idNext], lrp);
					float4 Power = lerp(KVMEditor_Power[i], KVMEditor_Power[idNext], lrp);
					float4 RadiusCentripetalTorque = lerp(KVMEditor_RadiusCentripetalTorque[i], KVMEditor_RadiusCentripetalTorque[idNext], lrp);

					float dist = distance(wrldPos.xyz, SensorPosition.xyz);

					if (dist < RadiusCentripetalTorque.x && KVMEditor_Links[i] >= 0)
					{
						float f = 1 - dist / (RadiusCentripetalTorque.x + .0000001f);
						f = pow(abs(f), Power);						




//#ifdef VERTEXMOTION_GRADIENT_ON	
						o.color.rgb = max( float3(f,f,f)*layerColor, o.color.rgb);
/*
#else						
						//o.color.rgb = lerp(float3(0, 1, 0), o.color.rgb,1-f);
						o.color.rgb = lerp(float3(0, 1, 0)*layerColor.x, o.color.rgb, 1 - f);
#endif							
	*/					



					}
				}
		
			}
			
		}
		else
		{
			//paint info
			if (_LayerId == 0)
				o.color.rgb = float3(1, 1, 1) * max(max(v.color.r, v.color.g), v.color.b);
			if (_LayerId == 1)
				o.color.rgb = v.color.rrr;
			if (_LayerId == 2)
				o.color.rgb = v.color.ggg;
			if (_LayerId == 3)
				o.color.rgb = v.color.bbb;

			o.color.a = 1;
		}
		//o.color.rgb = float3(1, 0, 0);

		o.vertex = UnityObjectToClipPos (v.vertex);	
		o.uv_MainTex = float4( v.texcoord.xy, 0, 0 );
			
		}


		fixed4 frag(vertexToFragment IN) : COLOR {
			float4 col;  	
			
/*
#ifdef VERTEXMOTION_WIREFRAME_ON
		return float4(.5, .5, .5, 1.);
#endif
*/

//#ifdef VERTEXMOTION_GRADIENT_ON
		//gradient
		float4 red = float4(1, 0, 0, 1);
		float4 yellow = float4(1, 1, 0, 1);
		float4 green = float4(0, 1, 0, 1);
		float4 blue = float4(0, 0, 4, 1);

		float f = IN.color.r;	
		float4 c1;
		c1 = lerp(yellow, red, (f - .6) * 3.3333);
		c1 = f < .6 ? lerp(green, yellow, (f - .3) * 3.3333) : c1;
		c1 = f < .3 ? lerp(blue, green, f * 3.3333) : c1;

		//return col;
//#else
		float4 c2;
		c2 = tex2D (_MainTex, IN.uv_MainTex);
		c2.x = c2.y = c2.z = (c2.x + c2.y + c2.z) / 3;
		c2 = lerp(IN.color, c2, .4);
		//return lerp( IN.color, col, .4 );
//#endif			

		return lerp(c2, c1 , _gradientFactor);
		}
		ENDCG

	 }
	} 
	//FallBack "Diffuse"
}
