Shader "Lit/Fog"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _NoiseTex ("Noise Texture", 2D) = "white" {}
        _NormalMap ("Normal Map", 2D) = "bump" {}
        _NoiseScale ("Noise Scale", Float) = 1.0
        _NoiseSpeed ("Noise Speed", Float) = 1.0
        _Glossiness ("Glossiness", Float) = 0.5
        _Metallic ("Metallic", Float) = 0.5
    }
    SubShader
    {
        Tags {
            "Queue"="Transparent"
            "RenderType"="Transparent" }
        LOD 200
        
        Cull Off

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard addshadow fullforwardshadows vertex:disp alpha:fade

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _NoiseTex;
        sampler2D _NormalMap;
        float _NoiseScale;
        float _NoiseSpeed;

        struct MeshData
        {
            float4 vertex : POSITION;
            float4 tangent : TANGENT;
            float3 normal : NORMAL;
            float2 texcoord : TEXCOORD0;
            float2 texcoord1 : TEXCOORD1;
            float2 texcoord2 : TEXCOORD2;
        };

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void disp (inout MeshData m)
        {
            float2 texcoord = m.texcoord + float2(_Time.x * _NoiseSpeed, 0);
            float4 noise = tex2Dlod(_NoiseTex, float4(texcoord, 0, 0));
            m.vertex.xyz += m.normal * _NoiseScale * noise.r;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            float2 texcoord = IN.uv_MainTex;// + float2(_Time.x, 0);
            fixed4 c = tex2D (_MainTex, texcoord) * _Color;
            
            // Metallic and smoothness come from slider variables
            o.Normal = UnpackNormal(tex2D(_NormalMap, texcoord));
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
