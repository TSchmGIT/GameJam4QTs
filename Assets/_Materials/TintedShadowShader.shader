Shader "Unlit/TintedShadowShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_ShadowColor("ShadowColor", Color) = (0,0,0,1)
		_Step("Step", int) = 8
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			// indicate that our pass is the "base" pass in forward
			// rendering pipeline. It gets ambient and main directional
			// light data set up; light direction in _WorldSpaceLightPos0
			// and color in _LightColor0
			Tags {"LightMode" = "ForwardBase"}

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc" // for UnityObjectToWorldNormal
			#include "UnityLightingCommon.cginc" // for _LightColor0

			struct v2f
			{
				float2 uv : TEXCOORD0;
				fixed4 diff : COLOR0; // diffuse lighting color
				float4 vertex : SV_POSITION;
			};

			int _Step;

			float stepDiff(float diff)
			{
				float newDiff = (int) (diff * _Step) / (float)_Step;
				return newDiff;
			}

			v2f vert(appdata_base v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;
				// get vertex normal in world space
				half3 worldNormal = UnityObjectToWorldNormal(v.normal);
				// dot product between normal and light direction for
				// standard diffuse (Lambert) lighting
				half nl = max(-0.3, dot(worldNormal, _WorldSpaceLightPos0.xyz));
				// factor in the light color
				half diff = nl * _LightColor0;

				o.diff = diff;
				return o;
			}

			sampler2D _MainTex;
			float4 _ShadowColor;

			float4 frag(v2f i) : SV_Target
			{
				// sample texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// multiply by lighting

				float steppedDiff = stepDiff(i.diff);

				fixed4 mixedCol = steppedDiff * col * _LightColor0 + (1.0f - steppedDiff) * _ShadowColor;
				//col *= i.diff;
				return mixedCol;
			}

		ENDCG
	}
	}
}
