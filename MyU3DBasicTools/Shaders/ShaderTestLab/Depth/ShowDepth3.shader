Shader "TestShaders/ShowDepth3"
{
    Properties
    {
        [Header(Wave)]
        _WaveDistance ("Distance from player", float) = 10
        _WaveTrail ("Length of the trail", Range(0, 5)) = 1
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

            sampler2D _CameraDepthTexture;

            float _WaveDistance;
            float _WaveTrail;
            float4 _WaveColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.pos);
                o.screenPos.y = 1 - o.screenPos.y;
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {                
                float depth = UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture, i.uv));                
                
                depth = Linear01Depth(depth);

                float waveFront = step(depth, _WaveDistance * 0.5);
                float waveTrail = smoothstep(_WaveDistance - _WaveTrail, _WaveDistance, depth);
                float wave = waveFront * waveTrail;

                //fixed4 col = lerp(fixed4(1,1,1,1), _WaveColor, wave);

                return wave; //fixed4(depthValue, depthValue, depthValue, 1);
            }
            ENDCG
        }
    }
}
