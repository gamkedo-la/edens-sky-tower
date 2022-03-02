Shader "Custom/CloudSurf"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _CloudColor ("Cloud base color", Color) = (1, 1, 1, 1)
        _CloudTopColor ("Cloud top color", Color) = (1, 1, 1, 1)
        
        _NoiseTex ("Noise Texture", 2D) = "white" {}
        _NoiseSpeedX ("Noise Speed X", Range(-2, 2)) = 1
        _NoiseSpeedY ("Noise Speed Y", Range(-2, 2)) = 1
        _NoiseSpeedZ ("Noise Speed Z", Range(-2, 2)) = 1
        _Strength ("Noise emission strength", Range(0, 2)) = 0.8
        _Height ("Noise Height", Range(0, 0.5)) = 1
        
        _RimColor ("Rim color", Color) = (1, 1, 1, 1)
        _RimPower ("Rim power", Range(0, 40)) = 10
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _NoiseTex;

        struct Input
        {
            float3 worldNormal; INTERNAL_DATA
            float3 worldPos;
            float3 viewDir;
            float4 col;
            float4 noise;
        };

        struct appdata
        {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
        };

        float _NoiseSpeedX, _NoiseSpeedY, _NoiseSpeedZ, _Height, _RimPower, _Strength;
        fixed4 _Color, _RimColor, _CloudColor, _CloudTopColor;
        
        void vert (inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            
            float3 worldSpaceNormal = normalize(mul((float3x3)unity_ObjectToWorld, v.normal.xyz));
            float3 worldNormalS = saturate(pow(worldSpaceNormal * 1.4, 4));
            float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;

            float movementX = _Time.x * _NoiseSpeedX;
            float movementY = _Time.x * _NoiseSpeedY;
            float movementZ = _Time.x * _NoiseSpeedZ;

            float4 xy = float4(worldPos.xy + movementX, 0, 0);
            float4 yz = float4(worldPos.yz + movementY, 0, 0);
            float4 xz = float4(worldPos.xz + movementZ, 0, 0);

            float4 noiseXY = tex2Dlod(_NoiseTex, xy);
            float4 noiseYZ = tex2Dlod(_NoiseTex, yz);
            float4 noiseXZ = tex2Dlod(_NoiseTex, xz);

            float4 noise = noiseXY;
            noise = lerp(noise, noiseXZ, worldNormalS.y);
            noise = lerp(noise, noiseYZ, worldNormalS.x);

            o.noise = noise;
            o.col = lerp(_CloudColor, _CloudTopColor, v.vertex.y);
            v.vertex.xyz += (v.normal * (noise * _Height));
        }

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            half emissionStrength = 2.0 - _Strength;
            half emissionFalloff = dot(normalize(IN.viewDir), o.Normal * emissionStrength);
            half rim = 1.0 - saturate(emissionFalloff);
            // half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal * (IN.noise * (2.0 - _Strength))));
            o.Emission = _RimColor.rgb * pow(rim, _RimPower);
            o.Albedo = _Color.rgb * IN.col;
            o.Alpha = _Color.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
