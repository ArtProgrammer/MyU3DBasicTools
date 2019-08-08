Shader "TestShaders/ShowDepthCamera"
{
    Properties
    {
        [HideInInspector]_MainTex ("Main Tex", 2D) = "white" {}
        [Header(Wave)]
        _WaveDistance ("Distance from player", float) = 10
        _WaveTrail ("Length of the trail", Range (0, 5)) = 1
        _WaveColor ("Color", Color) = (1,0,0,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 pos : SV_POSITION;
                float4 screenPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            sampler2D _CameraDepthTexture;

            float _WaveDistance;
            float _WaveTrail;
            float4 _WaveColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.pos);
                //o.screenPos.y = 1 - o.screenPos.y;
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : COLOR
            {
            /*
                float depthValue = Linear01Depth(
                    tex2D(_CameraDepthTexture,
                    UNITY_PROJ_COORD(i.screenPos)).r) * 20;
                    */

                float depthValue = Linear01Depth(
                    tex2D(_CameraDepthTexture,
                    i.uv).r) * 20;
                fixed4 source = tex2D(_MainTex, i.uv);                

                float waveFront = step(depthValue, _WaveDistance);
                float waveTrail = smoothstep(_WaveDistance - _WaveTrail, _WaveDistance, depthValue);

                float wave = waveFront * waveTrail;

                fixed4 col = lerp(source, _WaveColor, wave);
                return col; //fixed4(depthValue, depthValue, depthValue, 1);
            }
            ENDCG
        }
    }
}
