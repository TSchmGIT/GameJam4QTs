Shader "Unlit/RuneShader"
{
	Properties
	{
		_MainTex ("Texture", 2D)		= "white" {}
		_Color("Color", Color)			= (1,0,0,1)
		_GlowColor("Glow Color", Color)	= (1,1,1,1)
		_GlowAmount("GlowAmount", Range(0, 1)) = 0.1

	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		BlendOp Add
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			// indicate that our pass is the "base" pass in forward
			// rendering pipeline. It gets ambient and main directional
			// light data set up; light direction in _WorldSpaceLightPos0
			// and color in _LightColor0
			Tags {"LightMode" = "ForwardBase"}


			CGPROGRAM
			#include "UnityCG.cginc"

			#pragma vertex vert
			#pragma fragment frag

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				return o;
			}
			float _GlowAmount;
			float4 _Color;
			float4 _GlowColor;

			float4 frag(v2f i) : SV_Target
			{
				float4 texCol = tex2D(_MainTex, i.uv);
				float4 color = _GlowAmount * _GlowColor + (1.0f - _GlowAmount) * _Color;
				return color * texCol;
			}

		ENDCG
		}

	}
}
