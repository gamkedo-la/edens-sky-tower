Shader "Custom/FlowingWaterSurf"
{
    Properties
    {
        _DeepColor ("Deep color", Color) = (0,0.5,1,1)
        _Color ("Shallow color", Color) = (0, 1, 1, 1)
        _DepthOffset ("Depth offset", Range(-10, 10)) = 0
        _Stretch ("Depth stretch", Range(0, 5)) = 2
        _Brightness ("Water brightness", Range(0.5, 2)) = 1.2
        
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags {
            "RenderType"="Transparent"
            "Queue"="Transparent"
        }
        LOD 200
        Blend SrcAlpha OneMinusSrcAlpha

        GrabPass
        {
            "_GrabTex"
        }
        
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows keepalpha vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.5
        #pragma shader_feature VERTEX

        struct Input
        {
            float4 color : COLOR;
            float3 worldNormal;
            float3 worldPos;
            float3 viewDir;
            float4 screenPos;
            float eyeDepth;
        };

        uniform sampler2D _CameraDepthTexture;
        sampler2D _GrabTex;

        half _Glossiness;
        half _Metallic;
        float _DepthOffset, _Stretch, _Brightness;
        fixed4 _Color, _DeepColor;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void vert (inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            COMPUTE_EYEDEPTH(o.eyeDepth);
            
            float3 worldNormal = mul(unity_ObjectToWorld, v.normal);
            float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float4 depthUV = IN.screenPos;
            half depth = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, depthUV));
            float saturation = (depth/_DepthOffset);
            // float saturation = saturate((depth - IN.screenPos.w + _DepthOffset) * _Stretch);
            float4 c = lerp(_Color, _DeepColor, saturation) * _Brightness;
            half4 bgColor = tex2Dproj(_GrabTex, float4(
                IN.screenPos.x,
                IN.screenPos.y,
                IN.screenPos.zw
                ));
            
            // Metallic and smoothness come from slider variables
            o.Albedo = (c * c.a) + bgColor;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
