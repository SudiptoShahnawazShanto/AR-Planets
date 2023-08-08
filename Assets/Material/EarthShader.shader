Shader "Custom/EarthShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_CloudTex("Texture", 2D) = "white" {}
		_CloudStrength("Cloud Strength", Range(0, 1)) = 0.5
	}

	SubShader
	{
		Tags
		{
			"Queue" = "Transparent"
			"RenderType" = "Transparent"
		}
		
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
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			sampler2D _CloudTex;
			float _CloudStrength;

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 mainCol = tex2D(_MainTex, i.uv);
				fixed4 cloudCol = tex2D(_CloudTex, i.uv);

				float cloudAlpha = cloudCol.r;

				mainCol.rgb = lerp(mainCol.rgb, cloudCol.rgb, cloudAlpha * _CloudStrength);

				return mainCol;
			}
			ENDCG
		}
	}
}