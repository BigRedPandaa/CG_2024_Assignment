Shader "Custom/SpecularShader" {
    Properties {
        _Color ("Color", Color) = (0,0,0,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _SpecularColor("Specular", Color) = (0.2,0.2,0.2)
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200
    
        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf StandardSpecular fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

sampler2D _MainTex;

struct Input
{
    float2 uv_MainTex;
};

half _Glossiness;
fixed3 _SpecularColor;
fixed4 _Color;

void surf(Input IN, inout SurfaceOutputStandardSpecular o)
{
            // Albedo comes from a texture tinted by color
    fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
    o.Albedo = c.rgb;
            // Specular from specular color
    o.Specular = _SpecularColor;
            // Smoothness come from slider variable
    o.Smoothness = _Glossiness;
    o.Alpha = c.a;
    o.Emission = half3(0, 0, 0);
}
        ENDCG
    }
FallBack"Diffuse"
}