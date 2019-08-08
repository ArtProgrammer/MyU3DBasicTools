Shader "Hidden/OldFilmEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _VignetteTex ("Vignette Texture", 2D) = "white" {}
        _ScratchesTex ("Scratches Texture", 2D) = "white" {}
        _DustTex ("Dust Texture", 2D) = "white" {}
        _SepiaColor ("Sepia Color", Color) = (1, 1, 1, 1)
        _EffectAmount ("Old film effect amount", Range(0, 1)) = 1.0
        _VignetteAmount ("Vignette Amount", Range(0, 1)) = 1.0
        _ScratchesYSpeed ("Scratches Y Speed", Float) = 10.0
        _ScratchesXSpeed ("Scratches X Speed", Float) = 10.0
        _dustXSpeed ("Dust X Speed", Float) = 10.0
        _dustYSpeed ("Dust Y Speed", Float) = 10.0
        _RandomValue ("Random Value", Float) = 1.0
        _Contrast ("Contrast", Float) = 3.0
        _distortion ("Distortion", Float) = 1.0
        _scale ("Scale", Float) = 1.0

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
            sampler2D _VignetteTex;
            sampler2D _ScratchesTex;
            sampler2D _DustTex;
            fixed4 _SepiaColor;
            float _VignetteAmount;
            fixed _ScratchesAmount;
            fixed _ScratchesYSpeed;
            fixed _ScratchesXSpeed;
            fixed _dustXSpeed;
            fixed _dustYSpeed;
            fixed _EffectAmount;
            fixed _RandomValue;
            fixed _Contrast;
            fixed _distortion;
            fixed _scale;

            fixed OverlayBlendMode(fixed basePixel, fixed blendPixel)
            {
                if (basePixel < 0.5)
                {
                    return (2.0 * basePixel * blendPixel);
                }
                else
                {
                    return (1.0 - 2.0 * (1.0 - basePixel) * (1.0 - blendPixel));
                }

            }

            float2 barrelDistortion(float2 coord)
            {
                float2 h = coord.xy - float2(0.5, 0.5);
                float r2 = h.x * h.x + h.y * h.y;
                float f = 1.0 + r2 * (_distortion * sqrt(r2));

                return f * _scale * h + 0.5;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                half2 distortedUV = barrelDistortion(i.uv);
                distortedUV = half2(i.uv.x, i.uv.y + (_RandomValue * _SinTime.z *
                    0.005));
                fixed4 renderTex = tex2D(_MainTex, i.uv);

                fixed4 vignetteTex = tex2D(_VignetteTex, i.uv);

                // scratches uv and pixels
                half2 scratchesUV = half2(
                    i.uv.x + (_RandomValue * _SinTime.z * _ScratchesXSpeed), 
                    i.uv.y + (_Time.x * _ScratchesYSpeed));
                fixed4 scracthesTex = tex2D(_ScratchesTex, scratchesUV);

                // dust uv and pixels
                half2 dustUV = half2(
                    i.uv.x + (_RandomValue * (_SinTime.z * _dustXSpeed)),
                    i.uv.y + (_RandomValue * (_SinTime.z * _dustYSpeed)));
                fixed4 dustTex = tex2D(_DustTex, dustUV);

                fixed lum = dot(fixed3(0.299, 0.587, 0.114), renderTex.rgb);
                fixed4 finalColor = lum + lerp(_SepiaColor, 
                    _SepiaColor + fixed4(0.1f, 0.1f, 0.1f, 0.1f),
                    _RandomValue);

                finalColor = pow(finalColor, _Contrast);

                fixed3 constanceWhite = fixed3(1, 1, 1);

                finalColor = lerp(finalColor, 
                    finalColor * vignetteTex, _VignetteAmount);

                finalColor.rgb *= lerp(scracthesTex, 
                    constanceWhite, 
                    (_RandomValue));
                finalColor.rgb *= lerp(dustTex.rgb, 
                    constanceWhite, 
                    (_RandomValue * _SinTime.z));

                finalColor = lerp(renderTex, finalColor, _EffectAmount);

                //finalColor = tex2D(_MainTex, i.uv);

                return finalColor;

            }
            ENDCG
        }
    }
}
