Shader "Custom/Line" {
	Properties {
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata_custom {
			    float4 vertex : POSITION;
			    fixed4 color : COLOR;
			};

			struct v2f {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
			};
			
			v2f vert(appdata_custom i) {
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, i.vertex);
				o.color = i.color;
				return o;
			}
			
			fixed4 frag(v2f i) : COLOR {
				return i.color;
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
