Shader "Unlit/Sky"
{
    Properties
    {
        [Header(Day Sky Settings)]
        _DayTopColor ("Top Color", Color) = (0, .5, 1, 1)
        _DayBottomColor ("Bottom Color", Color) = (0, 1, .5, 1)
        
        [Header(Night Sky Settings)]
        _NightTopColor ("Top Color", Color) = (.3, 0, .2, 1)
        _NightBottomColor ("Bottom Color", Color) = (.5, 0, .5, 1)
        
        [Header(Horizon Settings)]
        _HorizonOffset ("Horizon Offset", Range(-1, 1)) = 0
        _HorizonIntensity ("Horizon Intensity", Range(0, 10)) = 3
        _DayHorizonColor ("Day Horizon Color", Color) = (1, 1, 1, 1)
        _NightHorizonColor ("Night Horizon Color", Color) = (0, 0, 0, 1)
        _SunSetColor ("Sunset/Rise color", Color) = (0, 0, 0, 0)
        [Toggle(REFLECTED_SKY)] _ReflectedSky("Reflected sky (clouds above and below)", Float) = 1
        
        [Header(Cloud Settings)]
        _BaseNoise ("Base Noise", 2D) = "black" {}
        _BaseNoiseScale ("Base Noise Scale", Range(0, 1)) = 0.2
        _NoiseSpeed ("Noise Speed", Range(-10, 10)) = 0.5
        _Distort ("Distort", 2D) = "block" {}
        _DistortScale ("Distort Scale", Range(0, 1)) = 0.2
        _DistortSpeed ("Distortion Speed", Range(-10, 10)) = 0.1
        _SecondaryNoise ("Secondary Noise", 2D) = "black" {}
        _SecondaryNoiseScale ("Secondary Noise Scale", Range(0, 1)) = 0.2
        _ExtraDistortion ("Additional Distortion", Range(0, 1)) = 0.1
        _CloudSpeed ("Cloud speed", Range(-10, 10)) = 1
        _CloudCutoff ("Cloud cutoff", Range(0, 1)) = 0.3
        _Fuzziness ("Fuzziness", Range(0, 1)) = 0.05
        _LayeredFuzziness("Layered Fuzziness",  Range(0, 1)) = 0.01
        
        [Header(Day Clouds)]
        _DayCloudColorEdge("Edge cloud color", Color) = (1, 1, 1, .8)
        _DayCloudColorMain("Main cloud color", Color) = (.8, .9, .8, .8)
        _DayCloudColorLayered("Layered clouds color", Color) = (.5, .5, .5, 1)
        _DayCloudBrightness("Brightness", Range(0, 10)) = 2.5
        
        [Header(Sun and Moon)]
        _SunRadius ("Sun Radius", Range(0, 1)) = 0.1
        _SunAttenuation ("Sun Attenuation", Range(0, 100)) = 2
        _SunColor ("Sun Color", Color) = (1, 1, 1, 1)
        
        [Header(Stars)]
        _Stars ("Stars texture", 2D) = "black" {}
        _StarsCutoff ("Star alpha cutoff", Range(0, 1)) = 0.1
        _StarsSpeed ("Stars move speed", Range(0, 1)) = 0.1
        _StarsSkyColor("Stars Sky Color", Color) = (0, 0.2, 0.1, 1)
    }
    
    SubShader
    {
        Tags {
            "RenderType"="Background"
            "PreviewType"="Skybox"
        }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog
            #pragma shader_feature REFLECTED_SKY

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 uv : TEXCOORD0;
            };

            struct v2f
            {
                float3 uv : TEXCOORD0;
                float3 worldPos : TEXCOORD1;
                UNITY_FOG_COORDS(2)
                float4 vertex : SV_POSITION;
            };

            sampler2D _BaseNoise, _SecondaryNoise, _Distort;
            half4 _DayCloudColorEdge, _DayCloudColorMain, _DayCloudColorLayered;
            float _BaseNoiseScale, _NoiseSpeed, _DistortScale, _DistortSpeed, _CloudSpeed, _SecondaryNoiseScale, _ExtraDistortion, _CloudCutoff, _Fuzziness, _LayeredFuzziness, _DayCloudBrightness;
            float _HorizonIntensity, _HorizonOffset;
            half4 _DayTopColor, _DayHorizonColor, _DayBottomColor, _NightTopColor, _NightHorizonColor, _SunSetColor, _NightBottomColor;

            float _SunRadius, _SunAttenuation;
            half4 _SunColor;
            
            sampler2D _Stars;
            float _StarsCutoff, _StarsSpeed;
            half4 _StarsSkyColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
#if REFLECTED_SKY
                float horizon = abs(i.uv.y * _HorizonIntensity - _HorizonOffset); // clouds above and below
#else
                float horizon = saturate((i.uv.y * _HorizonIntensity) - _HorizonOffset);
#endif
                
                // Sky color
                float horizonGlow = saturate((1-horizon) * saturate(_WorldSpaceLightPos0.y));
                float4 dayGlow = horizonGlow * _DayHorizonColor;
                float4 dayGradient = lerp(_DayBottomColor, _DayTopColor, saturate(i.uv.y));
                float4 nightGradient = lerp(_NightBottomColor, _NightTopColor, saturate(i.uv.y));

                // Clouds
                float2 skyUV = i.worldPos.xz / abs(i.worldPos.y);
                float baseNoise = tex2D(_BaseNoise, (skyUV - (_Time.x * _NoiseSpeed)) * _BaseNoiseScale);
                float distort = tex2D(_Distort, (skyUV + baseNoise - (_Time.x * _DistortSpeed)) * _DistortScale);
                float secondaryNoise = tex2D(_SecondaryNoise, (skyUV + (distort * _ExtraDistortion) - (_Time.x * _CloudSpeed)) * _SecondaryNoiseScale);
                float finalNoise = saturate(distort * secondaryNoise) * 3 * saturate(horizon);
                
                float clouds = saturate(smoothstep(_CloudCutoff, _CloudCutoff + _Fuzziness, finalNoise));
                float cloudsLayered = saturate(smoothstep(_CloudCutoff, _CloudCutoff + _Fuzziness + _LayeredFuzziness, secondaryNoise) * clouds);

                // Cloud color
                float4 cloudColorMain = lerp(_DayCloudColorLayered, _DayCloudColorMain, cloudsLayered);
                float4 cloudColor = lerp(_DayCloudColorEdge, cloudColorMain, clouds) * clouds;

                // Sun
                float cloudsNegative = (1 - clouds) * horizon;
                float sun = distance(i.uv.xyz, _WorldSpaceLightPos0);
                float sunDisc = 1 - (sun / _SunRadius);
                sunDisc = saturate(sunDisc);
                sunDisc = saturate(sunDisc * _SunAttenuation);

                // Moon
                float moon = 0;
                float3 sunAndMoon = (sunDisc * _SunColor) + moon;
                sunAndMoon *= cloudsNegative;

                // Stars
                float3 stars = tex2D(_Stars, skyUV + (_StarsSpeed * _Time.x));
                stars *= saturate(-_WorldSpaceLightPos0.y);
                stars = step(_StarsCutoff, stars);
                // stars += (baseNoise * _StarsSkyColor);
                stars *= cloudsNegative;

                // Day/Night depending on sun position
                float4 skyGradients = lerp(nightGradient, dayGradient, saturate(_WorldSpaceLightPos0.y));

                float sunset = saturate((1 - horizon) * saturate(_WorldSpaceLightPos0.y * 1));
                float3 sunsetColor = horizon * _SunSetColor * saturate(_WorldSpaceLightPos0.y * 1);
                
                // Combine color and apply fog
                fixed3 col = skyGradients + dayGlow + cloudColor + sunAndMoon + stars + sunsetColor;
                
                UNITY_APPLY_FOG(i.fogCoord, col);

                return fixed4(col, 1);
            }
            ENDCG
        }
    }
}
