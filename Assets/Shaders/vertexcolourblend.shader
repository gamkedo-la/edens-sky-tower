Shader "vertexcolourblend"
{
	Properties
	{
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_heightfactor("height factor", Float) = -0.5
		_TextureSample1("Texture Sample 1", 2D) = "white" {}
		_Clampcolourtint("Clamp colour tint", Float) = 0.2
		_Tintcolour("Tint colour", Color) = (0.5176471,0.3490196,0.3632607,0.08627451)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc 
		struct Input
		{
			float4 vertexColor : COLOR;
			float2 uv_texcoord;
		};

		uniform float _heightfactor;
		uniform float _Clampcolourtint;
		uniform sampler2D _TextureSample1;
		uniform float4 _TextureSample1_ST;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform float4 _Tintcolour;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float4 color21 = IsGammaSpace() ? float4(0.6320754,0.1818708,0.1818708,0) : float4(0.3572768,0.02773459,0.02773459,0);
			float4 color22 = IsGammaSpace() ? float4(0.3391406,0.6792453,0.3236028,0) : float4(0.09413625,0.418999,0.08547425,0);
			float4 lerpResult18 = lerp( color21 , color22 , v.color.g);
			v.vertex.xyz += ( lerpResult18 * _heightfactor ).rgb;
			v.vertex.w = 1;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float clampResult36 = clamp( i.vertexColor.b , 0.0 , _Clampcolourtint );
			float2 uv_TextureSample1 = i.uv_texcoord * _TextureSample1_ST.xy + _TextureSample1_ST.zw;
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float4 lerpResult6 = lerp( tex2D( _TextureSample1, uv_TextureSample1 ) , tex2D( _TextureSample0, uv_TextureSample0 ) , i.vertexColor.r);
			float layeredBlendVar34 = clampResult36;
			float4 layeredBlend34 = ( lerp( lerpResult6,_Tintcolour , layeredBlendVar34 ) );
			o.Albedo = layeredBlend34.rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
}