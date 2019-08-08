Shader "Custom/SimpleToon"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _RampTex ("Ramp", 2D) = "white" {}
        _CelShadingLevels ("Cel shading level", Range(0, 1)) = 0.6
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        //#pragma surface surf SimpleLambert
        #pragma surface surf Toon

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _RampTex;
        float _CelShadingLevels;

        struct Input
        {
            float2 uv_MainTex;
        };

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        half4 LightSimpleLamber(SurfaceOutput s, half3 lightDir, half atten)
        {
            half NdotL = dot (s.Normal, lightDir);
            half4 c;
            c.rgb = s.Albedo * _LightColor0.rgb * (NdotL * atten * 1);
            c.a = s.Alpha;

            return c;
        }

        fixed4 LightingToon(SurfaceOutput s, fixed3 lightDir, fixed atten)
        {
            half NdotL = dot(s.Normal, lightDir);
            NdotL = tex2D(_RampTex, fixed2(NdotL, 0.5));

            //half cel = floor(NdotL * _CelShadingLevels) / (_CelShadingLevels - 0.5);

            fixed4 c;
            c.rgb = s.Albedo * _LightColor0.rgb * NdotL * atten;
            c.a = s.Alpha;

            return c;
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
