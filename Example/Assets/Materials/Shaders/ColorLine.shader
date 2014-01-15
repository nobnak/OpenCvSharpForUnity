Shader "Custom/ColorLine" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Color ("Color", Color) = (1, 1, 1, 1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			fixed4 _Color;
			
			struct appdata_custom {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
			};

			struct vsout {
				float4 vertex : POSITION;
				fixed4 color : TEXCOORD0;
			};
			
			vsout vert(appdata_custom i) {
				vsout o;
				o.vertex = mul(UNITY_MATRIX_MVP, i.vertex);				
				o.color = i.color;
				return o;
			}
			
			fixed4 frag(vsout i) : COLOR {
				return _Color * i.color;
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
