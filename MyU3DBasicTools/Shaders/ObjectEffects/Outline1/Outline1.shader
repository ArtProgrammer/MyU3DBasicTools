Shader "Custom/Outline1"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _OutlineCol ("Outline color", Color) = (1,0,0,1)
        _OutlineFactor ("Outline factor", Float) = 0.1
    }
    SubShader
    {
        Pass
        {
            Cull Front

            CGPROGRAM
            #include "UnityCG.cginc"
            #pragma vertex vert
            #pragma fragment frag

            #pragma target 3.0

            fixed4 _OutlineCol;
            float _OutlineFactor;
            
            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                //v.vertex.xyz += normalize(v.normal) * _OutlineFactor;

                o.pos = UnityObjectToClipPos(v.vertex);
                //float3 normal = UnityObjectToWorldNormal(v.normal);

                float3 normal = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);

                float2 offset = TransformViewToProjection(normal.xy);
                o.pos.xy += offset * _OutlineFactor * 0.001;
                //o.pos.xy += 1.0;

                return o;
            };

            fixed4 frag(v2f i) : SV_Target
            {
                return _OutlineCol;
            }
            
            ENDCG
        }

        Pass
        {
            CGPROGRAM

            #include "Lighting.cginc"
            #pragma vertex vert
            #pragma fragment frag

            fixed4 _Color;
            sampler2D _MainTex;
            float4 _MainTex_ST;

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
                float2 uv : TEXCOORD1;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
                fixed3 worldNormal = normalize(i.worldNormal);
                fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);
                fixed3 lambert = 0.5 * dot(worldNormal, worldLightDir) + 0.5;
                fixed3 diffuse = lambert * _Color.xyz * _LightColor0.xyz + ambient;
                fixed4 color = tex2D(_MainTex, i.uv);
                color.rgb = color.rgb * _Color;

                return fixed4(color);
            }

            ENDCG
        }
    }
    FallBack "Diffuse"
}
