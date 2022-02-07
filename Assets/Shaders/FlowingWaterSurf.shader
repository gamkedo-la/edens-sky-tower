Shader "Custom/FlowingWaterSurf"
{
    Properties
    {
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        
        [Header(Color)]
        _DeepColor ("Deep color", Color) = (0,0.5,1,1)
        _Color ("Shallow color", Color) = (0, 1, 1, 1)
        _Depth ("Water depth", Range(0, 20)) = 10
        _Stretch ("Depth stretch", Range(0, 5)) = 2
        _Brightness ("Water brightness", Range(0.5, 2)) = 1.2
        
        [Header(Surface Noise and Movement)]
        _SideNoiseTex ("Side water texture", 2D) = "white" {}
        _TopNoiseTex ("Top water texture", 2D) = "white" {}
        _HSpeed ("Horizontal flow speed", Range(0, 4)) = 0.14
        _VSpeed ("Vertical flow speed", Range(0, 60)) = 10
        _TopNoiseScale ("Top noise scale", Range(0, 1)) = 0.4
        _SideNoiseStretch ("Side noise stretch", Range(0, 20)) = 10
        _SideNoiseScale ("Side noise scale", Range(0, 1)) = 0.04
        _TopNoiseNormalStrength ("Top noise normal strength", Range(0, 2)) = 0.5
        
        [Header(Vertex Movement)]
        _WaveAmount ("Wave amount", Range(0, 10)) = 0.6
        _WaveSpeed ("Wave speed", Range(0, 10)) = 0.5
        _WaveHeight ("Wave height", Range(0, 1)) = 0.1
        
        [Header(Foam)]
        _FoamColor ("Foam color", Color) = (1, 1, 1, 1)
        _EdgeFoamWidth ("Edge foam width", Range(1, 50)) = 2.35
        _TopFoamSpread ("Foam position", Range(-1, 6)) = 0
        _FoamSoftness ("Foam softness", Range(0, 0.5)) = 0.1
        _FoamWidth ("Foam width", Range(0, 2)) = 0.4
    }
    SubShader
    {
        Tags {
            "RenderType"="Opaque"
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
        #pragma surface surf Standard addshadow fullforwardshadows keepalpha vertex:vert

        // 3.5 = OpenGL ES 3.0
        #pragma target 3.5
        #pragma shader_feature VERTEX

        struct Input
        {
            float3 worldNormal; INTERNAL_DATA
            float3 worldPos;
            float3 viewDir;
            float4 color : COLOR;
            float4 screenPos;
            float eyeDepth;
            float4 viewInterpolator;
        };

        uniform sampler2D _CameraDepthTexture;
        sampler2D _GrabTex;
        
        sampler2D _SideNoiseTex, _TopNoiseTex;
        float _SideNoiseStretch, _SideNoiseScale, _TopNoiseScale, _HSpeed, _VSpeed, _WaveAmount, _WaveSpeed, _WaveHeight;

        float _EdgeFoamWidth, _TopFoamSpread, _FoamSoftness, _FoamWidth;
        fixed4 _FoamColor;

        half _Glossiness;
        half _Metallic;
        float _Depth, _Stretch, _Brightness;
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

            half3 tex = tex2Dlod(_SideNoiseTex, float4(worldPos.xz * _TopNoiseScale * 1, 1, 1));
            float3 movement = sin(
                _Time.z * _WaveSpeed +
                (v.vertex.x * v.vertex.z * _WaveAmount * tex)) * _WaveHeight * (1 - worldNormal.y);

            // unity_Scale
            // v.vertex.xyz += movement;
            // v.vertex.

            half3 worldSpaceVertex = mul(unity_ObjectToWorld, (v.vertex)).xyz;
            o.viewInterpolator.xyz = worldSpaceVertex - _WorldSpaceCameraPos;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float3 worldNormal = WorldNormalVector(IN, o.Normal);
            float3 blendNormal = saturate(pow(worldNormal * 1.4, 4));

            float3 flowDir = (-(worldNormal * 2.0f) - 1.0f) * _HSpeed;
            float vertFlow = _Time.y * _VSpeed;

            float timing =     frac(_Time[1] * 0.5 * 0.5);
            float timing2 =    frac(_Time[1] * 0.5);
            float timingLerp = abs((0.5 - timing) / 0.5);

            // Move 2 textures at slightly diff speeds
            half3 topTex1 = tex2D(_TopNoiseTex, (IN.worldPos.xz * _TopNoiseScale) + flowDir.xz * timing);
            half3 topTex2 = tex2D(_TopNoiseTex, (IN.worldPos.xz * _TopNoiseScale) + flowDir.xz * timing2);

            // Noise textures
            float3 topFoamNoise = lerp(topTex1, topTex2, timingLerp); // @TODO: ripples

            float3 sideFoamNoiseZ = tex2D(_SideNoiseTex, float2(IN.worldPos.z * _SideNoiseStretch, IN.worldPos.y + vertFlow) * _SideNoiseScale);
            float3 sideFoamNoiseX = tex2D(_SideNoiseTex, float2(IN.worldPos.x * _SideNoiseStretch, IN.worldPos.y + vertFlow) * _SideNoiseScale);
            float3 sideFoamNoiseZE = tex2D(_SideNoiseTex, float2(IN.worldPos.z * _SideNoiseStretch, IN.worldPos.y + vertFlow) * _SideNoiseScale * 0.4);
            float3 sideFoamNoiseXE = tex2D(_SideNoiseTex, float2(IN.worldPos.x * _SideNoiseStretch, IN.worldPos.y + vertFlow) * _SideNoiseScale * 0.4);

            float3 noiseTexture = (sideFoamNoiseX + sideFoamNoiseXE) * 0.5;
            noiseTexture = lerp(noiseTexture, (sideFoamNoiseZ + sideFoamNoiseZE) * 0.5, blendNormal.x);
            noiseTexture = lerp(noiseTexture, topFoamNoise, blendNormal.y);

            // Normal-based foam
            float worldNormalDotNoise = dot(o.Normal, worldNormal.y + 0.3) * noiseTexture;

            // Add noise to normal
            // o.Normal = worldNormal + noiseTexture;
            // o.Normal = o.Normal + ripples;

            // Side foam
            float3 sideFoam =
                smoothstep(_TopFoamSpread, _TopFoamSpread + _FoamSoftness, worldNormalDotNoise) *
                smoothstep(worldNormalDotNoise, worldNormalDotNoise + _FoamSoftness, _TopFoamSpread + _FoamWidth);

            // no foam on top of water
            sideFoam *= saturate(1 - worldNormal.y);
            sideFoam *= 4;

            float4 depthUV = IN.screenPos;
            half depth = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, depthUV));

            // Edge foam calculation
            float foamLineS = 1 - saturate(_EdgeFoamWidth * float4(noiseTexture, 1) * (depth - IN.screenPos.w));
            // float foamLineS = 1 - saturate(_Foam * float4(noisetexture,1) * (depth - IN.screenPos.w));// foam line by comparing depth and screenposition
            float foamLine = smoothstep(0.6, 0.8, foamLineS) * 3;

            // float3 combinedFoam = (sideFoam + topFoamNoise + foamLine) * _FoamColor;
            // float3 combinedFoam = (sideFoam + topFoamNoise) * _FoamColor;
            float3 combinedFoam = (sideFoam + foamLine) * _FoamColor;

            // Depth color lerp
            float saturation = saturate((depth - IN.screenPos.w)/_Depth * _Stretch);
            float4 c = lerp(_Color, _DeepColor, saturation) * _Brightness;
            half4 bgColor = tex2Dproj(_GrabTex, float4(
                IN.screenPos.x,
                IN.screenPos.y,
                IN.screenPos.zw
                ));
            o.Albedo = (c * c.a) + bgColor;

            // Add foam color
            o.Albedo += combinedFoam;

            // half4 rtReflections = tex2Dproj(_Refle)
            
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = 1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
