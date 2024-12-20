Shader "Custom/StrangeShader"
{
   Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		//Cull Off ZWrite Off ZTest Always
		Tags{ "RenderType" = "Opaque" }
		LOD 200

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			//mat2 in glsl is equal to float2x2 in hlsl supposedly...
			//mat2 mm2(in float a) { float c = cos(a), s = sin(a); return mat2(c, s, -s, c); }
			float2x2 mm2(in float a) { float c = cos(a), s = sin(a); return float2x2(c, -s, s, c); }
			
			float2x2 m2 = float2x2(0.95534, -0.29552, 0.29552, 0.95534);

			float tri(in float x) {
				return clamp(abs(frac(x) - .5), 0.01, 0.49);
			}
			
			fixed2 tri2(in fixed2 p) { return fixed2(tri(p.x) + tri(p.y), tri(p.y + tri(p.x))); }

			float triNoise2d(in fixed2 p, float spd)
			{
				float z = 1.8;
				float z2 = 2.5;
				float rz = 0.;
				// replace *= with mul()
				//p *= mm2(p.x*0.06);
				p = mul(p, mm2(p.x*0.06));
				fixed2 bp = p;
				for (float i = 0.; i<5.; i++)
				{
					fixed2 dg = tri2(bp*1.85)*.75;
					//dg *= mm2(_Time*spd);
					dg = mul(dg, mm2(_Time.y*spd));
					p -= dg / z2;

					bp *= 1.3;
					z2 *= .45;
					z *= .42;
					p *= 1.21 + (rz - 1.0)*.02;

					rz += tri(p.x + tri(p.y))*z;
					//p *= -m2;
					p = mul(p, -m2);
				}
				return clamp(1. / pow(rz*29., 1.3), 0., .55);
			}
			
			float hash21(in fixed2 n) { return frac(sin(dot(n, fixed2(12.9898, 4.1414))) * 43758.5453); }

			fixed4 aurora(fixed3 ro, fixed3 rd)
			{
				fixed4 col = fixed4(0, 0, 0, 0);
				fixed4 avgCol = fixed4(0, 0, 0, 0);

				for (float i = 0.; i<50.; i++)
				{
					//float of = 0.006*hash21(gl_FragCoord.xy)*smoothstep(0., 15., i);
					float of = 0.006 * hash21(_ScreenParams.xy) * smoothstep(0., 15., i);
					float pt = ((.8 + pow(i, 1.4)*.002) - ro.y) / (rd.y*2. + 0.4);
					pt -= of;
					//vec3 bpos = ro + pt*rd;
					fixed3 bpos = ro + pt * rd;
					//vec2 p = bpos.zx;
					fixed2 p = bpos.zx;
					float rzt = triNoise2d(p, 0.06);
					//vec4 col2 = vec4(0, 0, 0, rzt);
					fixed4 col2 = fixed4(0, 0, 0, rzt);
					//col2.rgb = (sin(1. - vec3(2.15, -.5, 1.2) + i*0.043)*0.5 + 0.5)*rzt;
					col2.rgb = (sin(1. - fixed3(2.15, -.5, 1.2) + i * 0.043) * 0.5 + 0.5) * rzt;
					//avgCol = mix(avgCol, col2, .5);
					avgCol = lerp(avgCol, col2, .5);

					col += avgCol*exp2(-i*0.065 - 2.5)*smoothstep(0., 5., i);

				}

				col *= (clamp(rd.y*15. + .4, 0., 1.));


				//return clamp(pow(col,vec4(1.3))*1.5,0.,1.);
				//return clamp(pow(col,vec4(1.7))*2.,0.,1.);
				//return clamp(pow(col,vec4(1.5))*2.5,0.,1.);
				//return clamp(pow(col,vec4(1.8))*1.5,0.,1.);

				//return smoothstep(0.,1.1,pow(col,vec4(1.))*1.5);
				return col*1.8;
				//return pow(col,vec4(1.))*2.
			}

			fixed3 hash33(fixed3 p)
			{
				p = frac(p * fixed3(443.8975, 397.2973, 491.1871));
				p += dot(p.zxy, p.yxz + 19.27);
				return frac(fixed3(p.x * p.y, p.z*p.x, p.y*p.z));
			}

			fixed3 stars(in fixed3 p)
			{
				fixed3 c = fixed3(0., 0., 0.);
				float res = _ScreenParams.x*1.;

				for (float i = 0.; i<4.; i++)
				{
					fixed3 q = frac(p*(.15*res)) - 0.5;
					fixed3 id = floor(p*(.15*res));
					fixed2 rn = hash33(id).xy;
					float c2 = 1. - smoothstep(0., .6, length(q));
					c2 *= step(rn.x, .0005 + i*i*0.001);
					c += c2*(lerp(fixed3(1.0, 0.49, 0.1), fixed3(0.75, 0.9, 1.), rn.y)*0.1 + 0.9);
					p *= 1.3;
				}
				return c*c*.8;
			}

			fixed3 bg(in fixed3 rd)
			{
				float sd = dot(normalize(fixed3(-0.5, -0.6, 0.9)), rd)*0.5 + 0.5;
				sd = pow(sd, 5.);
				fixed3 col = lerp(fixed3(0.05, 0.1, 0.2), fixed3(0.1, 0.05, 0.2), sd);
				return col*.63;
			}

			fixed4 frag(v2f_img i) : SV_Target
			{

				//vec2 q = fragCoord.xy / iResolution.xy;
				fixed2 q = (i.uv*_ScreenParams.xy) / _ScreenParams.xy;;
				fixed2 p = q - 0.5;
				//p.x *= iResolution.x / iResolution.y;
				p.x *= _ScreenParams.x / _ScreenParams.y;

				fixed3 ro = fixed3(0, 0, -6.7);
				fixed3 rd = normalize(fixed3(p, 1.3));
				
				// no mouse interaction for now...
				//vec2 mo = iMouse.xy / iResolution.xy - .5;
				//mo = (mo == vec2(-.5)) ? mo = vec2(-0.1, 0.1) : mo;
				fixed2 mo = fixed2(0, 0);
				//mo.x *= iResolution.x / iResolution.y;
				mo.x *= _ScreenParams.x / _ScreenParams.y;
				//rd.yz *= mm2(mo.y);
				rd.yz = mul(rd.yz, mm2(mo.y));
				//rd.xz *= mm2(mo.x + sin(time*0.05)*0.2);
				rd.xz = mul(rd.xz, mm2(mo.x + sin(_Time.y * 0.05) * 0.2));

				fixed3 col = fixed3(0., 0., 0.);
				fixed3 brd = rd;
				float fade = smoothstep(0., 0.01, abs(brd.y))*0.1 + 0.9;

				col = bg(rd)*fade;

				if (rd.y > 0.) {
					fixed4 aur = smoothstep(0., 1.5, aurora(ro, rd))*fade;
					col += stars(rd);
					col = col*(1. - aur.a) + aur.rgb;
				}
				else //Reflections
				{
					rd.y = abs(rd.y);
					col = bg(rd)*fade*0.6;
					fixed4 aur = smoothstep(0.0, 2.5, aurora(ro, rd));
					col += stars(rd)*0.1;
					col = col*(1. - aur.a) + aur.rgb;
					fixed3 pos = ro + ((0.5 - ro.y) / rd.y)*rd;
					float nz2 = triNoise2d(pos.xz*fixed2(.5, .7), 0.);
					col += lerp(fixed3(0.2, 0.25, 0.5)*0.08, fixed3(0.3, 0.3, 0.5)*0.7, nz2*0.4);
				}

				fixed4 color = fixed4(col, 1);

				return color;
			}
			ENDCG
		}
	}
}