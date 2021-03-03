// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

Shader "Custom/LavaShader"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
		_Bump("Bump", 2D) = "bump" {}
        _MainColor ("Main Colour", Color) = (1,0,0,1)
        _Strength("Strength", Range(0,2)) = 1.0
        _Speed("Speed", Range(-200,200)) = 100
		_Sink("Sink Depth", Range(-5,5)) = 2
    }
    SubShader
    {
        Tags { "RenderType" = "Transparent"}
        CGPROGRAM
		#pragma surface surf Lambert vertex:vert
            
        half4 _MainColor;
        float _Strength;
        float _Speed;
		float _Sink;

        uniform int _Length = 0;
        uniform half2 _Points[100];
        uniform fixed3 _Colors[100];

        struct Input {
            float2 uv_MainTex;
			float2 uv_Bump;
        };

        void vert (inout appdata_full v) {

			v.vertex.y += ((cos(v.vertex.y) + cos(v.vertex.x + (_Speed * _Time))) * _Strength) + _Sink;
        }
            
		sampler2D _MainTex;
		sampler2D _Bump;

		void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
			o.Normal = UnpackNormal(tex2D(_Bump, IN.uv_Bump));
		}
        ENDCG
        
    }
    Fallback "Diffuse"
}
