Shader "Unlit/testShader"
{
    Properties
    {
        _Value ("Value", Float) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct MeshData // Get Mesh Data
            {
                float4 vertex : POSITION;
                // float3 normals: NORMAL;
                float2 uv0 : TEXCOORD0; // diffuse/normal map tetures
                //float2 uv1 : TEXCOORD1; // uv1 coordinates, lightmap coordinates
            };

            struct v2f // Vertex to frag shader
            {
                //float2 uv : TEXCOORD0; // data doesn't need to be uv
                // UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION; // Clip Space position
            };

            float _Value;

            v2f vert (MeshData v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex); // Local to clip
                // o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                // UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                return float4(0.5, 1, 1, 1);
            }
            ENDCG
        }
    }
}
