Shader"Custom/SpecularShader" {
    Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
        _SpecColor ("Specular Color", Color) = (1,1,1,1)
        _Shininess ("Shininess", Range(0.03, 1)) = 0.078125
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
LOD 300

        CGPROGRAM
        #pragma surface surf BlinnPhong

sampler2D _MainTex;
float4 _Color;
float4 _SpecColor1;
half _Shininess;

struct Input
{
    float2 uv_MainTex;
};

void surf(Input IN, inout SurfaceOutput o)
{
    float4 tex = tex2D(_MainTex, IN.uv_MainTex) * _Color;
    o.Albedo = tex.rgb;
    o.Specular = _SpecColor1.a;
    o.Gloss = _Shininess;
}
        ENDCG
    }
FallBack"Specular"
}
