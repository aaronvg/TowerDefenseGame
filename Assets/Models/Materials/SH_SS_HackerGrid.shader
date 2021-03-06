﻿Shader "Custom/SH_SS_HackerGrid" {
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
			};

			v2f vert (appdata v) {
				v2f o;
				o.uv = float4(v.texcoord.xy, 0, 0);
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				return o;
			}

			float4 frag (v2f i) : COLOR {
				float4 subuv = i.uv;
				i.uv.x += _Time;
				subuv.y += _Time * 2;
				float4 ret = tex2D(_MainTex, float2(i.uv.x, i.uv.y));
				float4 sub = tex2D(_MainTex, float2(subuv.x * 4, subuv.y * 4));
				ret = ret + sub;
				return ret;
			}
			ENDCG
		}
	} 
	FallBack "Unlit"
}
