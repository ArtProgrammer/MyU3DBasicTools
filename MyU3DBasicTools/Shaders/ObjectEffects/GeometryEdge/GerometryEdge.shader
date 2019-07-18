Shader "Custom/GerometryEdge"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _EdgeFactor ("EdgeFactor", Range(0,6)) = 3
        _EdgeColor ("Edge Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Pass
        {
            Tags { "LightMode"="ForwardBase" }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "Lighting.cginc"

            // Use shader model 3.0 target, to get nicer looking lighting
            #pragma target 3.0

            fixed4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;
               
            struct a2v
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float3 worldNormal : TEXCOORD1;
                float4 worldPos : TEXCOORD2;
            };
            
            v2f vert(a2v v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed3 worldNormal = normalize(i.worldNormal);
                fixed3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.pos));
                
                fixed3 albedo = tex2D(_MainTex, i.uv).rgb * _Color.rgb;
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albedo;
                fixed3 diffuse = (_LightColor0.rgb) * albedo * 
                    max(0, dot(worldNormal, worldLightDir));

               return fixed4(ambient + diffuse, 1.0);
            }

            ENDCG
        }

        Pass
        {
            Tags {"LightMode" = "ForwardBase"}
            Cull Front

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            fixed4 _EdgeColor;
            float _EdgeFactor;

            float4 vert(appdata_base v) : SV_POSITION
            {
                float4 pos = UnityObjectToClipPos(v.vertex);
                //float3 clipNormal = normalize(mul((float3x3)UNITY_MATRIX_MVP, v.normal));
                float3 clipNormal = mul((float3x3)UNITY_MATRIX_MVP, v.normal);
                //float3 clipNormal = UnityObjectToWorldNormal(v.normal);
                pos.xy += _EdgeFactor * clipNormal.xy;

                return pos;
            }

            fixed4 frag() : SV_Target
            {
                return _EdgeColor;
            }

            ENDCG
        }
    }
    FallBack "Diffuse"
}
