// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/SimpleGlassShader"
{
    Properties
    {
        _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
        _BumpMap ("Noise text", 2D) = "bump" {}
        _Magnitude ("Magnitude", Range(0, 1)) = 0.05
        _Colour ("Color", Color) = (1, 1, 1, 1)
    }

    SubShader
    {
        Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" 
                "RenderType"="Opaque" }

        GrabPass { }

        pass
        {
            LOD 200

            CGPROGRAM
            // Physically based Standard lighting model, and enable shadows on all light types
            //#pragma surface surf Standard fullforwardshadows
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 3.0

            sampler2D _GrabTexture;

            sampler2D _MainTex;
            sampler2D _BumpMap;
            float _Magnitude;
            float4 _Colour;

            struct vertInput
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct vertexOutput
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
                float4 uvgrab : TEXCOORD1;
            };

            // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
            // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
            // #pragma instancing_options assumeuniformscaling
            UNITY_INSTANCING_BUFFER_START(Props)
                // put more per-instance properties here
            UNITY_INSTANCING_BUFFER_END(Props)

            vertexOutput vert(vertInput v)
            {
                vertexOutput o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uvgrab = ComputeGrabScreenPos(o.vertex);
                o.texcoord = v.texcoord;
                return o;
            }

            half4 frag(vertexOutput i) : COLOR
            {
                half4 mainColor = tex2D(_MainTex, i.texcoord);
                half4 bump = tex2D(_BumpMap, i.texcoord);
                half2 distortion = UnpackNormal(bump).rg;

                i.uvgrab.xy += distortion * _Magnitude;

                fixed4 col = tex2Dproj(_GrabTexture,
                    UNITY_PROJ_COORD(i.uvgrab));
                return col * mainColor  * _Colour;
            }

            ENDCG
        }
    }
    FallBack "Diffuse"
}
