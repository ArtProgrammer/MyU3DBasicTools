Shader "Custom/SimpleNoise"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

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

        fixed random(in fixed2 st)
        {
            return frac(sin(dot(st.xy, fixed2(12.9898, 78.233))) * 43758.5453123);
        }

        fixed noise(in fixed2 st)
        {
            fixed2 i = floor(st);
            fixed2 f = frac(st);

            fixed a = random(i);
            fixed b = random(i + fixed2(1.0, 0.0));
            fixed c = random(i + fixed2(0.0, 1.0));
            fixed d = random(i + fixed2(1.0, 1.0));

            fixed2 u = f * f * (3.0 - 2.0 * f);

            return lerp(a, b, u.x) + (c - a) * u.y * (1.0 - u.x) +
                (d - b) * u.x * u.y;
        }

        fixed simple(in fixed2 st)
        {
            return 1;
        }              

        fixed lines(in fixed2 pos, float b)
        {
            float scale = 10.0;
            pos *= scale;
            return smoothstep(0.0, .5 + b * .5,
                abs((sin(pos.x * 3.1315) + b * 2.0)) * .5);
        }



        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed2 uv = IN.uv_MainTex;
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, uv) * _Color;

            fixed col = noise(fixed2(c.rg));

            //fixed pattern = lines();

            fixed3 color = fixed3(col, col, col);
            color += smoothstep(.15, .2, noise(c.rg * 10 * (_SinTime.x + 0.5)));
            color -= smoothstep(.35, .4, noise(c.rg * 10 * (_SinTime.x + 0.5)));
            o.Albedo = color;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = 0.1;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
