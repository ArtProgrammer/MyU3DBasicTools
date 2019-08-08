Shader "TestShaders/ShowDepth4"
{
    Properties
    {
        
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

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.pos);
                //o.screenPos.y = 1 - o.screenPos.y;
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float existingDepth01 = tex2Dproj(_CameraDepthTexture,
                    UNITY_PROJ_COORD(i.screenPos)).r;
                float depthValue = (LinearEyeDepth(existingDepth01)) * 0.01; //* 0.0001 // - i.screenPos.w

                return fixed4(depthValue, depthValue, depthValue, 1);
            }
            ENDCG
        }
    }
}
