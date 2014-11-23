Shader "Custom/SH_SS_HackerGrid" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Pass {
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;

			struct appdata {
				float4 vertex : POSITION;
				float4 texcoord  : TEXCOORD0;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float4 uv : TEXCOORD0;
				float4 screenPos : TEXCOORD1;
			};

			v2f vert (appdata v) {
				v2f o;
				o.uv = float4(v.texcoord.xy, 0, 0);
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.screenPos = ComputeScreenPos(o.pos);
				o.screenPos /= o.screenPos.w;
				return o;
			}

			float4 frag (v2f i) : COLOR {
				float4 ret = tex2D(_MainTex, i.uv);
				float4 sub = i.screenPos;
				sub.y += _Time;
				sub.x *= _ScreenParams.x / _ScreenParams.y;
				ret = ret + tex2D(_MainTex, sub * 16) * .35;
				return ret;
			}
			ENDCG
		}
	} 
	FallBack "Unlit"
}
