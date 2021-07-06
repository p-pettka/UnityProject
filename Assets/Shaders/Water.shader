Shader "Unlit/Water"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DisplacementTexture("Displacement", 2D) = "bump"{}
        _DisplacementX("Displacement X", Range(0, 1)) = 0
        _DisplacementY("Displacement Y", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

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
                float2 uv2 : TEXCOORD1;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _DisplacementTexture;
            float4 _DisplacementTexture_ST;
            float4 _MainTex_ST;
            float _DisplacementX;
            float _DisplacementY;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv2 = TRANSFORM_TEX(v.uv2, _DisplacementTexture);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                i.uv.y = 1.0 - i.uv.y;
                
                fixed4 col = tex2D(_MainTex, i.uv);
                half disVal = tex2D(_DisplacementTexture, i.uv2).g;
                i.uv.x += disVal * _DisplacementX - _DisplacementX * 0.5;
                i.uv.y += disVal * _DisplacementY - _DisplacementY * 0.5;
                return col;
            }
            ENDCG
        }
    }
}
