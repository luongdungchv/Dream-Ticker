Shader "Unlit/MirrorShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Power ("Power", Float) = 1
        _EdgeOpacity ("EdgeOpacity", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "Queue"="Transparent"}
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha

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
                float3 normal: NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal: TEXCOORD1;
            };

            float4 _Color;
            float _Power;
            float _EdgeOpacity;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.normal = v.normal;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 newUV = i.uv * 2 - float2(1, 1);
                float dist = length(newUV) / 1.5;
                float4 startCol = float4(_Color.xyz, 0);
                float alpha = lerp(0, 1, dist);
                float4 col = float4(_Color.xyz, alpha);

                col = pow(col, _Power);
                float3 absNormal = abs(i.normal);
                col *= absNormal.x;

                float4 edgeColor = float4(_Color.xyz, _EdgeOpacity);

                col += absNormal.y * edgeColor;
                col += absNormal.z * edgeColor;
                col = saturate(col);
                //return float4(abs(i.normal), 1);
                return col;
            }
            ENDCG
        }
    }
}
