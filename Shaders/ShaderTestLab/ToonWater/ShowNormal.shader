Shader "TestShaders/ShowNormal"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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
            // make fog work
            

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                
                float4 pos : SV_POSITION;

                float4 screenPos : TEXCOORD1;

                float3 viewNormal : NORMAL;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            
            sampler2D _CameraDepthTexture;

            sampler2D _CameraNormalsTexture;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv; //TRANSFORM_TEX(v.uv, _MainTex);
                o.screenPos = ComputeScreenPos(o.pos);
                o.viewNormal = COMPUTE_VIEW_NORMAL;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {             
                /*
                float depth = UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture, i.uv));
                depth = Linear01Depth(depth) * 1.0f;
                

                float3 dep = tex2Dproj(_CameraDepthTexture,
                    UNITY_PROJ_COORD(i.screenPos));
                float existingDepthLinear = Linear01Depth(dep);
                
                return float4(depth, depth, depth, 1);
                */

                /*
                float3 existingNormal = tex2Dproj(_CameraNormalsTexture,
                    UNITY_PROJ_COORD(i.screenPos)); //UNITY_PROJ_COORD(i.screenPos)
                return float4(existingNormal, 1);
                */
                
                float depth = UNITY_SAMPLE_DEPTH(tex2D(_CameraDepthTexture, i.uv));
                depth = pow(Linear01Depth(depth), 30);

                return depth;
            }
            ENDCG
        }
    }
}
