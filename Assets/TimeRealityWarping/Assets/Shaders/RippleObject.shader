Shader "RealityWarp/RippleObject"
{
	Properties
	{
		[PerRendererData]_BumpTex("Normal Texture", 2D) = "bump" {}
		[PerRendererData]_BumpStrength("Normal Map Strength", Float) = 1
		[PerRendererData]_Position("Origin Offset", Vector) = (0.0, 0.0, 0.0)
		[PerRendererData]_Distortion ("Distortion", Range(0,1)) = 1
		[PerRendererData]_RingSize("Ring Size", Range(0,2)) = .3
		[PerRendererData]_OuterThick("Ring Thickness", Float) = 4
		[PerRendererData]_FadeAmount("Fade Amount", Range(0,1)) = 0
		[PerRendererData]_FalloffDistance("Falloff Distance", Float) = 1
		[PerRendererData]_Color("Color", Color) = (0,0,0,0)
		[PerRendererData]_BlendType("BlendType", int) = 0
	}
	SubShader
	{
		Tags { "RenderType"="Transparent"
				"Queue"="Transparent"}
		LOD 100
		Cull Off

		GrabPass
		{}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 bumpUV : TEXCOORD0;
				float4 grabPos : TEXCOORD1;
				float2 uv :TEXCOORD2;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _BumpTex;
			float4 _BumpTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.bumpUV = TRANSFORM_TEX(v.uv, _BumpTex);

				o.grabPos = ComputeGrabScreenPos(o.vertex);
				o.uv = v.uv;
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			sampler2D _GrabTexture;
			uniform float _BumpStrength;
			uniform float _Distortion;
			uniform float _RingSize;
			uniform float _OuterThick;
			uniform float _FadeAmount;
			uniform float3 _Position;
			uniform float _FalloffDistance;
			uniform float4 _Color;
			uniform float _BlendType;
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2Dproj(_GrabTexture, i.grabPos);
				float2 bump = tex2D(_BumpTex, i.bumpUV).rg  * 2 - 1;

				if (_RingSize > 0)
				{
					fixed4 col1 = tex2Dproj(_GrabTexture, i.grabPos);
					float thickness = _RingSize - _OuterThick;
					float halfThick = thickness / 2;

					float ringThick = _OuterThick / 2;

					float2 uv = i.uv - _Position - .5;
					float d = clamp(length(uv) - thickness + (bump * _BumpStrength), 0, .5);

					float m = lerp(smoothstep(0, 1, d / ringThick), smoothstep(1, 0, (d - ringThick) / ringThick), step(ringThick, d));
					float distort = clamp(_Distortion * clamp(lerp(1, 0, _RingSize / _FalloffDistance), 0, 1), 0, 1);

					uv.y *= -1;
					i.grabPos.xy += (m * (uv / _FalloffDistance) * distort);

					col = tex2Dproj(_GrabTexture, i.grabPos);

					float4 c = lerp(float4(0,0,0,0), _Color, clamp(m,0,1) * distort * 2);
					col = lerp(col, lerp(col + c, col - c, step(2, _BlendType)), _BlendType);
					col = col * (1 -_FadeAmount) + col1 * _FadeAmount;
				}

				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
