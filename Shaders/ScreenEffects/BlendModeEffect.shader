Shader "Hidden/BlendModeEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlendTex ("Blend Texture", 2D) = "white" {}
        _Opacity ("Blend Opacity", Range(0, 1)) = 1
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _BlendTex;
            fixed _Opacity;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                
                fixed4 blendTex = tex2D(_BlendTex, i.uv);

                //fixed4 blendedMultiply = col * blendTex;

                //fixed4 blendedMultiply = col + blendTex;

                fixed4 blendedMultiply = (1.0 - (1.0 - col) * (1.0 - blendTex));

                col = lerp(col, blendedMultiply, _Opacity);

                return col;
            }
            ENDCG
        }
    }
}
