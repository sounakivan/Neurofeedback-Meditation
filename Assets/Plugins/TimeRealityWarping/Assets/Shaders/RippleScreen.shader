Shader "Hidden/RippleScreen"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_BumpTex("Normal Texture", 2D) = "bump" {}
		_BumpStrength("Normal Map Strength", Range(0, 2)) = 1
		_Position("Position", Vector) = (0.0, 0.0, 0.0)
		_Distortion ("Distortion", Range(-1,1)) = 1
		_RingSize("Ring Size", Range(0,2)) = 0
		_OuterThick("Outer Thickness", Range(0,1)) = 0
		_FadeAmount("Fade Amount", Range(0,1)) = 0
		_FalloffDistance("Falloff Distance", Range(0, 2)) = 1
		_WorldSpace("World Space", Float) = 0
		_Color("Color", Color) = (0,0,0,0)
		_BlendType("BlendType", int) = 0
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float2 bumpUV : TEXCOORD1;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _BumpTex;
			float4 _BumpTex_ST;

			v2f vert (appdata v)
			{
				v2f o;
				
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.bumpUV = TRANSFORM_TEX(v.uv, _BumpTex);
				o.uv = v.uv;
				return o;
			}
			
			uniform float _BumpStrength;
			uniform float _Distortion;
			uniform float _RingSize;
			uniform float _OuterThick;
			uniform float _FadeAmount;
			uniform float3 _Position;
			uniform float _FalloffDistance;
			uniform float _WorldSpace;
			uniform float4 _Color;
			uniform float _BlendType;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				float2 bump = tex2D(_BumpTex, i.bumpUV).rg  * 2 - 1;
				if (_RingSize > 0)
				{
					fixed4 col1 = tex2D(_MainTex, i.uv);
					float thickness = _RingSize - _OuterThick;
					float halfThick = thickness / 2;

					float ringThick = _OuterThick / 2;

					float2 uv = i.uv - lerp(.5, 0, _WorldSpace) - _Position;
					float d = clamp(length(uv) - thickness + (bump * _BumpStrength), 0, .5);

					float m = lerp(smoothstep(0, 1, d / ringThick), smoothstep(1, 0, (d - ringThick) / ringThick), step(ringThick, d));
					float distort = clamp(_Distortion * clamp(lerp(1, 0, _RingSize / _FalloffDistance), 0, 1), -1, 1);

					i.uv += (m * clamp(-uv, -.1, .1) * distort);

					col = tex2D(_MainTex, i.uv);

					float4 c = lerp(float4(0,0,0,0), _Color, m * abs(distort));
					col = (col * (1 -_FadeAmount) + col1 * _FadeAmount);
					col = lerp(col, lerp(col + c, col - c, step(2, _BlendType)), _BlendType);
				}
				
				
				return col;
			}
			ENDCG
		}
	}
}
