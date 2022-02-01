Shader "Custom/NewSurfaceShader"
{
    Properties {
	    texture_1 ("Texture 1", 2D) = "white" {}
	    _MainTex ("Foo", 2D) = "white" {}
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200
        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0
		
        sampler2D _MainTex;
        struct Input {
            float2 uv_MainTex;
        };
        UNITY_INSTANCING_BUFFER_START(Props)
        UNITY_INSTANCING_BUFFER_END(Props)
		
#define hlsl_atan(x,y) atan2(x, y)
#define mod(x,y) ((x)-(y)*floor((x)/(y)))
inline float4 textureLod(sampler2D tex, float2 uv, float lod) {
    return tex2D(tex, uv);
}
inline float2 tofloat2(float x) {
    return float2(x, x);
}
inline float2 tofloat2(float x, float y) {
    return float2(x, y);
}
inline float3 tofloat3(float x) {
    return float3(x, x, x);
}
inline float3 tofloat3(float x, float y, float z) {
    return float3(x, y, z);
}
inline float3 tofloat3(float2 xy, float z) {
    return float3(xy.x, xy.y, z);
}
inline float3 tofloat3(float x, float2 yz) {
    return float3(x, yz.x, yz.y);
}
inline float4 tofloat4(float x, float y, float z, float w) {
    return float4(x, y, z, w);
}
inline float4 tofloat4(float x) {
    return float4(x, x, x, x);
}
inline float4 tofloat4(float x, float3 yzw) {
    return float4(x, yzw.x, yzw.y, yzw.z);
}
inline float4 tofloat4(float2 xy, float2 zw) {
    return float4(xy.x, xy.y, zw.x, zw.y);
}
inline float4 tofloat4(float3 xyz, float w) {
    return float4(xyz.x, xyz.y, xyz.z, w);
}
inline float2x2 tofloat2x2(float2 v1, float2 v2) {
    return float2x2(v1.x, v1.y, v2.x, v2.y);
}

// EngineSpecificDefinitions


float rand(float2 x) {
    return frac(cos(mod(dot(x, tofloat2(13.9898, 8.141)), 3.14)) * 43758.5453);
}

float2 rand2(float2 x) {
    return frac(cos(mod(tofloat2(dot(x, tofloat2(13.9898, 8.141)),
						      dot(x, tofloat2(3.4562, 17.398))), tofloat2(3.14))) * 43758.5453);
}

float3 rand3(float2 x) {
    return frac(cos(mod(tofloat3(dot(x, tofloat2(13.9898, 8.141)),
							  dot(x, tofloat2(3.4562, 17.398)),
                              dot(x, tofloat2(13.254, 5.867))), tofloat3(3.14))) * 43758.5453);
}

float param_rnd(float minimum, float maximum, float seed) {
	return minimum+(maximum-minimum)*rand(tofloat2(seed));
}

float3 rgb2hsv(float3 c) {
	float4 K = tofloat4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
	float4 p = c.g < c.b ? tofloat4(c.bg, K.wz) : tofloat4(c.gb, K.xy);
	float4 q = c.r < p.x ? tofloat4(p.xyw, c.r) : tofloat4(c.r, p.yzx);

	float d = q.x - min(q.w, q.y);
	float e = 1.0e-10;
	return tofloat3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
}

float3 hsv2rgb(float3 c) {
	float4 K = tofloat4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
	float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
	return c.z * lerp(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
}


float fbm_value(float2 coord, float2 size, float offset, float seed) {
	float2 o = floor(coord)+rand2(tofloat2(seed, 1.0-seed))+size;
	float2 f = frac(coord);
	float p00 = rand(mod(o, size));
	float p01 = rand(mod(o + tofloat2(0.0, 1.0), size));
	float p10 = rand(mod(o + tofloat2(1.0, 0.0), size));
	float p11 = rand(mod(o + tofloat2(1.0, 1.0), size));
	p00 = sin(p00 * 6.28318530718 + offset) / 2.0 + 0.5;
	p01 = sin(p01 * 6.28318530718 + offset) / 2.0 + 0.5;
	p10 = sin(p10 * 6.28318530718 + offset) / 2.0 + 0.5;
	p11 = sin(p11 * 6.28318530718 + offset) / 2.0 + 0.5;
	float2 t =  f * f * f * (f * (f * 6.0 - 15.0) + 10.0);
	return lerp(lerp(p00, p10, t.x), lerp(p01, p11, t.x), t.y);
}

float fbm_perlin(float2 coord, float2 size, float offset, float seed) {
	float2 o = floor(coord)+rand2(tofloat2(seed, 1.0-seed))+size;
	float2 f = frac(coord);
	float a00 = rand(mod(o, size)) * 6.28318530718 + offset;
	float a01 = rand(mod(o + tofloat2(0.0, 1.0), size)) * 6.28318530718 + offset;
	float a10 = rand(mod(o + tofloat2(1.0, 0.0), size)) * 6.28318530718 + offset;
	float a11 = rand(mod(o + tofloat2(1.0, 1.0), size)) * 6.28318530718 + offset;
	float2 v00 = tofloat2(cos(a00), sin(a00));
	float2 v01 = tofloat2(cos(a01), sin(a01));
	float2 v10 = tofloat2(cos(a10), sin(a10));
	float2 v11 = tofloat2(cos(a11), sin(a11));
	float p00 = dot(v00, f);
	float p01 = dot(v01, f - tofloat2(0.0, 1.0));
	float p10 = dot(v10, f - tofloat2(1.0, 0.0));
	float p11 = dot(v11, f - tofloat2(1.0, 1.0));
	float2 t =  f * f * f * (f * (f * 6.0 - 15.0) + 10.0);
	return 0.5 + lerp(lerp(p00, p10, t.x), lerp(p01, p11, t.x), t.y);
}

float fbm_perlinabs(float2 coord, float2 size, float offset, float seed) {
	return abs(2.0*fbm_perlin(coord, size, offset, seed)-1.0);
}

float mod289(float x) {
    return x - floor(x * (1.0 / 289.0)) * 289.0;
}

float permute(float x) {
    return mod289(((x * 34.0) + 1.0) * x);
}

float2 rgrad2(float2 p, float rot, float seed) {
	float u = permute(permute(p.x) + p.y) * 0.0243902439 + rot; // Rotate by shift
	u = frac(u) * 6.28318530718; // 2*pi
	return tofloat2(cos(u), sin(u));
}

float fbm_simplex(float2 coord, float2 size, float offset, float seed) {
	coord *= 2.0; // needed for it to tile
	coord += rand2(tofloat2(seed, 1.0-seed)) + size;
	size *= 2.0; // needed for it to tile
	coord.y += 0.001;
    float2 uv = tofloat2(coord.x + coord.y*0.5, coord.y);
    float2 i0 = floor(uv);
    float2 f0 = frac(uv);
    float2 i1 = (f0.x > f0.y) ? tofloat2(1.0, 0.0) : tofloat2(0.0, 1.0);
    float2 p0 = tofloat2(i0.x - i0.y * 0.5, i0.y);
    float2 p1 = tofloat2(p0.x + i1.x - i1.y * 0.5, p0.y + i1.y);
    float2 p2 = tofloat2(p0.x + 0.5, p0.y + 1.0);
    i1 = i0 + i1;
    float2 i2 = i0 + tofloat2(1.0, 1.0);
    float2 d0 = coord - p0;
    float2 d1 = coord - p1;
    float2 d2 = coord - p2;
    float3 xw = mod(tofloat3(p0.x, p1.x, p2.x), size.x);
    float3 yw = mod(tofloat3(p0.y, p1.y, p2.y), size.y);
    float3 iuw = xw + 0.5 * yw;
    float3 ivw = yw;
    float2 g0 = rgrad2(tofloat2(iuw.x, ivw.x), offset, seed);
    float2 g1 = rgrad2(tofloat2(iuw.y, ivw.y), offset, seed);
    float2 g2 = rgrad2(tofloat2(iuw.z, ivw.z), offset, seed);
    float3 w = tofloat3(dot(g0, d0), dot(g1, d1), dot(g2, d2));
    float3 t = 0.8 - tofloat3(dot(d0, d0), dot(d1, d1), dot(d2, d2));
    t = max(t, tofloat3(0.0));
    float3 t2 = t * t;
    float3 t4 = t2 * t2;
    float n = dot(t4, w);
    return 0.5 + 5.5 * n;
}

float fbm_cellular(float2 coord, float2 size, float offset, float seed) {
	float2 o = floor(coord)+rand2(tofloat2(seed, 1.0-seed))+size;
	float2 f = frac(coord);
	float min_dist = 2.0;
	for(float x = -1.0; x <= 1.0; x++) {
		for(float y = -1.0; y <= 1.0; y++) {
			float2 neighbor = tofloat2(float(x),float(y));
			float2 node = rand2(mod(o + tofloat2(x, y), size)) + tofloat2(x, y);
			node =  0.5 + 0.25 * sin(offset + 6.2831 * node);
			float2 diff = neighbor + node - f;
			float dist = length(diff);
			min_dist = min(min_dist, dist);
		}
	}
	return min_dist;
}

float fbm_cellular2(float2 coord, float2 size, float offset, float seed) {
	float2 o = floor(coord)+rand2(tofloat2(seed, 1.0-seed))+size;
	float2 f = frac(coord);
	float min_dist1 = 2.0;
	float min_dist2 = 2.0;
	for(float x = -1.0; x <= 1.0; x++) {
		for(float y = -1.0; y <= 1.0; y++) {
			float2 neighbor = tofloat2(float(x),float(y));
			float2 node = rand2(mod(o + tofloat2(x, y), size)) + tofloat2(x, y);
			node = 0.5 + 0.25 * sin(offset + 6.2831*node);
			float2 diff = neighbor + node - f;
			float dist = length(diff);
			if (min_dist1 > dist) {
				min_dist2 = min_dist1;
				min_dist1 = dist;
			} else if (min_dist2 > dist) {
				min_dist2 = dist;
			}
		}
	}
	return min_dist2-min_dist1;
}

float fbm_cellular3(float2 coord, float2 size, float offset, float seed) {
	float2 o = floor(coord)+rand2(tofloat2(seed, 1.0-seed))+size;
	float2 f = frac(coord);
	float min_dist = 2.0;
	for(float x = -1.0; x <= 1.0; x++) {
		for(float y = -1.0; y <= 1.0; y++) {
			float2 neighbor = tofloat2(float(x),float(y));
			float2 node = rand2(mod(o + tofloat2(x, y), size)) + tofloat2(x, y);
			node = 0.5 + 0.25 * sin(offset + 6.2831*node);
			float2 diff = neighbor + node - f;
			float dist = abs((diff).x) + abs((diff).y);
			min_dist = min(min_dist, dist);
		}
	}
	return min_dist;
}

float fbm_cellular4(float2 coord, float2 size, float offset, float seed) {
	float2 o = floor(coord)+rand2(tofloat2(seed, 1.0-seed))+size;
	float2 f = frac(coord);
	float min_dist1 = 2.0;
	float min_dist2 = 2.0;
	for(float x = -1.0; x <= 1.0; x++) {
		for(float y = -1.0; y <= 1.0; y++) {
			float2 neighbor = tofloat2(float(x),float(y));
			float2 node = rand2(mod(o + tofloat2(x, y), size)) + tofloat2(x, y);
			node = 0.5 + 0.25 * sin(offset + 6.2831*node);
			float2 diff = neighbor + node - f;
			float dist = abs((diff).x) + abs((diff).y);
			if (min_dist1 > dist) {
				min_dist2 = min_dist1;
				min_dist1 = dist;
			} else if (min_dist2 > dist) {
				min_dist2 = dist;
			}
		}
	}
	return min_dist2-min_dist1;
}

float fbm_cellular5(float2 coord, float2 size, float offset, float seed) {
	float2 o = floor(coord)+rand2(tofloat2(seed, 1.0-seed))+size;
	float2 f = frac(coord);
	float min_dist = 2.0;
	for(float x = -1.0; x <= 1.0; x++) {
		for(float y = -1.0; y <= 1.0; y++) {
			float2 neighbor = tofloat2(float(x),float(y));
			float2 node = rand2(mod(o + tofloat2(x, y), size)) + tofloat2(x, y);
			node = 0.5 + 0.5 * sin(offset + 6.2831*node);
			float2 diff = neighbor + node - f;
			float dist = max(abs((diff).x), abs((diff).y));
			min_dist = min(min_dist, dist);
		}
	}
	return min_dist;
}

float fbm_cellular6(float2 coord, float2 size, float offset, float seed) {
	float2 o = floor(coord)+rand2(tofloat2(seed, 1.0-seed))+size;
	float2 f = frac(coord);
	float min_dist1 = 2.0;
	float min_dist2 = 2.0;
	for(float x = -1.0; x <= 1.0; x++) {
		for(float y = -1.0; y <= 1.0; y++) {
			float2 neighbor = tofloat2(float(x),float(y));
			float2 node = rand2(mod(o + tofloat2(x, y), size)) + tofloat2(x, y);
			node = 0.5 + 0.25 * sin(offset + 6.2831*node);
			float2 diff = neighbor + node - f;
			float dist = max(abs((diff).x), abs((diff).y));
			if (min_dist1 > dist) {
				min_dist2 = min_dist1;
				min_dist1 = dist;
			} else if (min_dist2 > dist) {
				min_dist2 = dist;
			}
		}
	}
	return min_dist2-min_dist1;
}

// MIT License Inigo Quilez - https://www.shadertoy.com/view/Xd23Dh
float fbm_voronoise( float2 coord, float2 size, float offset, float seed) {
    float2 i = floor(coord) + rand2(tofloat2(seed, 1.0-seed)) + size;
    float2 f = frac(coord);
    
	float2 a = tofloat2(0.0);
	
    for( int y=-2; y<=2; y++ ) {
    	for( int x=-2; x<=2; x++ ) {
        	float2  g = tofloat2( float(x), float(y) );
			float3  o = rand3( mod(i + g, size) + tofloat2(seed) );
			o.xy += 0.25 * sin(offset + 6.2831*o.xy);
			float2  d = g - f + o.xy;
			float w = pow( 1.0-smoothstep(0.0, 1.414, length(d)), 1.0 );
			a += tofloat2(o.z*w,w);
		}
    }
	
    return a.x/a.y;
}
uniform sampler2D texture_1;
static const float texture_1_size = 1024.0;

static const float p_o4778_albedo_color_r = 1.000000000;
static const float p_o4778_albedo_color_g = 1.000000000;
static const float p_o4778_albedo_color_b = 1.000000000;
static const float p_o4778_albedo_color_a = 1.000000000;
static const float p_o4778_metallic = 0.000000000;
static const float p_o4778_roughness = 1.000000000;
static const float p_o4778_emission_energy = 1.000000000;
static const float p_o4778_normal = 1.000000000;
static const float p_o4778_ao = 1.000000000;
static const float p_o4778_depth_scale = -0.500000000;
static const float seed_o119291 = 0.000000000;
static const float p_o119291_scale_x = 4.000000000;
static const float p_o119291_scale_y = 4.000000000;
static const float p_o119291_folds = 0.000000000;
static const float p_o119291_iterations = 3.000000000;
static const float p_o119291_persistence = 0.000000000;
float o119291_fbm(float2 coord, float2 size, int folds, int octaves, float persistence, float offset, float seed, float _seed_variation_) {
	float normalize_factor = 0.0;
	float value = 0.0;
	float scale = 1.0;
	for (int i = 0; i < octaves; i++) {
		float noise = fbm_simplex(coord*size, size, offset, seed);
		for (int f = 0; f < folds; ++f) {
			noise = abs(2.0*noise-1.0);
		}
		value += noise * scale;
		normalize_factor += scale;
		size *= 2.0;
		scale *= persistence;
	}
	return value / normalize_factor;
}
float o4778_input_depth_tex(float2 uv, float _seed_variation_) {
float o119291_0_1_f = o119291_fbm((uv), tofloat2(p_o119291_scale_x, p_o119291_scale_y), int(p_o119291_folds), int(p_o119291_iterations), p_o119291_persistence, (_Time.y/10.0), (seed_o119291+_seed_variation_), _seed_variation_);

return o119291_0_1_f;
}
static const float p_o310349_gradient_0_pos = 0.000000000;
static const float p_o310349_gradient_0_r = 0.341176480;
static const float p_o310349_gradient_0_g = 0.482352942;
static const float p_o310349_gradient_0_b = 0.556862772;
static const float p_o310349_gradient_0_a = 0.419607848;
static const float p_o310349_gradient_1_pos = 1.000000000;
static const float p_o310349_gradient_1_r = 1.000000000;
static const float p_o310349_gradient_1_g = 1.000000000;
static const float p_o310349_gradient_1_b = 1.000000000;
static const float p_o310349_gradient_1_a = 0.368627459;
float4 o310349_gradient_gradient_fct(float x) {
  if (x < p_o310349_gradient_0_pos) {
    return tofloat4(p_o310349_gradient_0_r,p_o310349_gradient_0_g,p_o310349_gradient_0_b,p_o310349_gradient_0_a);
  } else if (x < p_o310349_gradient_1_pos) {
    return lerp(tofloat4(p_o310349_gradient_0_r,p_o310349_gradient_0_g,p_o310349_gradient_0_b,p_o310349_gradient_0_a), tofloat4(p_o310349_gradient_1_r,p_o310349_gradient_1_g,p_o310349_gradient_1_b,p_o310349_gradient_1_a), ((x-p_o310349_gradient_0_pos)/(p_o310349_gradient_1_pos-p_o310349_gradient_0_pos)));
  }
  return tofloat4(p_o310349_gradient_1_r,p_o310349_gradient_1_g,p_o310349_gradient_1_b,p_o310349_gradient_1_a);
}
static const float p_o190994_amount = 1.000000000;
float o190994_input_in(float2 uv, float _seed_variation_) {
float4 o190988_0 = textureLod(texture_1, uv, 0.0);

return (dot((o190988_0).rgb, tofloat3(1.0))/3.0);
}
float3 nm_o190994(float2 uv, float amount, float size, float _seed_variation_) {
	float3 e = tofloat3(1.0/size, -1.0/size, 0);
	float2 rv;
	if (0 == 0) {
		rv = tofloat2(1.0, -1.0)*o190994_input_in(uv+e.xy, _seed_variation_);
		rv += tofloat2(-1.0, 1.0)*o190994_input_in(uv-e.xy, _seed_variation_);
		rv += tofloat2(1.0, 1.0)*o190994_input_in(uv+e.xx, _seed_variation_);
		rv += tofloat2(-1.0, -1.0)*o190994_input_in(uv-e.xx, _seed_variation_);
		rv += tofloat2(2.0, 0.0)*o190994_input_in(uv+e.xz, _seed_variation_);
		rv += tofloat2(-2.0, 0.0)*o190994_input_in(uv-e.xz, _seed_variation_);
		rv += tofloat2(0.0, 2.0)*o190994_input_in(uv+e.zx, _seed_variation_);
		rv += tofloat2(0.0, -2.0)*o190994_input_in(uv-e.zx, _seed_variation_);
		rv *= size*amount/128.0;
	} else if (0 == 1) {
		rv = tofloat2(3.0, -3.0)*o190994_input_in(uv+e.xy, _seed_variation_);
		rv += tofloat2(-3.0, 3.0)*o190994_input_in(uv-e.xy, _seed_variation_);
		rv += tofloat2(3.0, 3.0)*o190994_input_in(uv+e.xx, _seed_variation_);
		rv += tofloat2(-3.0, -3.0)*o190994_input_in(uv-e.xx, _seed_variation_);
		rv += tofloat2(10.0, 0.0)*o190994_input_in(uv+e.xz, _seed_variation_);
		rv += tofloat2(-10.0, 0.0)*o190994_input_in(uv-e.xz, _seed_variation_);
		rv += tofloat2(0.0, 10.0)*o190994_input_in(uv+e.zx, _seed_variation_);
		rv += tofloat2(0.0, -10.0)*o190994_input_in(uv-e.zx, _seed_variation_);
		rv *= size*amount/512.0;
	} else if (0 == 2) {
		rv += tofloat2(2.0, 0.0)*o190994_input_in(uv+e.xz, _seed_variation_);
		rv += tofloat2(-2.0, 0.0)*o190994_input_in(uv-e.xz, _seed_variation_);
		rv += tofloat2(0.0, 2.0)*o190994_input_in(uv+e.zx, _seed_variation_);
		rv += tofloat2(0.0, -2.0)*o190994_input_in(uv-e.zx, _seed_variation_);
		rv *= size*amount/64.0;
	} else {
		rv += tofloat2(1.0, 0.0)*o190994_input_in(uv+e.xz, _seed_variation_);
		rv += tofloat2(0.0, 1.0)*o190994_input_in(uv+e.zx, _seed_variation_);
		rv += tofloat2(-1.0, -1.0)*o190994_input_in(uv, _seed_variation_);
		rv *= size*amount/20.0;
	}
	return tofloat3(0.5)+0.5*normalize(tofloat3(rv, -1.0));
}


		
        void surf (Input IN, inout SurfaceOutputStandard o) {
      		float _seed_variation_ = 0.0;
			float2 uv = IN.uv_MainTex;
float o119291_0_1_f = o119291_fbm((uv), tofloat2(p_o119291_scale_x, p_o119291_scale_y), int(p_o119291_folds), int(p_o119291_iterations), p_o119291_persistence, (_Time.y/10.0), (seed_o119291+_seed_variation_), _seed_variation_);
float4 o310349_0_1_rgba = o310349_gradient_gradient_fct(o119291_0_1_f);
float3 o190994_0_1_rgb = nm_o190994((uv), p_o190994_amount, 1024.000000000, _seed_variation_);

			o.Albedo = ((o310349_0_1_rgba).rgb).rgb*tofloat4(p_o4778_albedo_color_r, p_o4778_albedo_color_g, p_o4778_albedo_color_b, p_o4778_albedo_color_a).rgb;
            o.Metallic = 1.0*p_o4778_metallic;
            o.Smoothness = 1.0-1.0*p_o4778_roughness;
            o.Alpha = 1.0;
			o.Normal = o190994_0_1_rgb*tofloat3(-1.0, 1.0, -1.0)+tofloat3(1.0, 0.0, 1.0);

        }
        ENDCG
    }
    FallBack "Diffuse"
}



