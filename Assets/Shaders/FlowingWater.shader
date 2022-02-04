Shader "Custom/FlowingWater"
{
    Properties
    {
        _Smoothness("Smoothness", Range(0, 1)) = 0.5
        _Metallic("Metallic", Range(0, 1)) = 0.5
        [Space]
        [Header(Color)]
        _DeepColor("Deep Color", Color) = (0, 0.5, 1, 0.8)
        _ShallowColor("Shallow color", Color) = (0, 1, 1, 0.5)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 200
        
        CGPROGRAM

        #pragma surf Standard fullforwardshadows keepalpha

        #pragma target 3.5
        #pragma shader_feature VERTEX

        fixed4 _DeepColor;
        fixed4 _ShallowColor;
        
        struct surfaceInput
        {
            float3 worldNormal; INTERNAL_DATA
            float3 worldPos;
            float3 viewDir;
            float4 color : COLOR;
            float4 screenPos;
            float4 eyeDepth;
            float4 viewInterpolator;
        };

        void surf(surfaceInput IN, inout SurfaceOutputStandard o)
        {
            
        }

        ENDCG
    }
}
