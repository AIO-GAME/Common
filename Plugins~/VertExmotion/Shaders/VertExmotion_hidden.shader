Shader "Hidden/VertExmotion_hidden" {
	Properties {
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Standard nolightmap
		#pragma multi_compile_instancing

		sampler2D _MainTex;
		struct Input {
			float2 uv_MainTex;
		};
		void surf (Input IN, inout SurfaceOutputStandard o) {
			clip(-1);
		}
		ENDCG
	}
	
}
