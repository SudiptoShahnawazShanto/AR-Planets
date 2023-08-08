// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/PracticeShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}        //Texture as a property as a default value of white
        _SecondTex("Texture", 2D) = "white" {}
        _Range("Range", Range(0,1)) = 0
        _Color("Color", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent" //Makes sprites render after the opaque geometry in the scene
        }

        Pass
        {
            //Alpha Blending = SourceColor * SourceAlpha + DestinationColor * (1-SourceAlpha)
            Blend SrcAlpha OneMinusSrcAlpha

            //Additive Blending
            //Blend One One

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

            sampler2D _MainTex;     //Defining _MainTex
            sampler2D _SecondTex;   //Defining _SecondTex
            float4 _Color;          //Defining _Color
            float _Range;           //Defining _Range

            float4 frag(v2f i) : SV_Target
            {
                float4 gradient = float4(i.uv.r, i.uv.g, 0, 1);

                float4 texColor1 = tex2D(_MainTex, i.uv*2);       //To get the color value from the texture
                float4 texColor2 = tex2D(_SecondTex, i.uv);

                float4 color = lerp(texColor1 * gradient, texColor2 * _Color, _Range);

                return color;  
            }
            ENDCG
        }
    }
}
