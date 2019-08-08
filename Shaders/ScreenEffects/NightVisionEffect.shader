Shader "Hidden/NightVisionEffect"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _VignetteTex ("Vignette Texture", 2D) = "white" {}
        _ScanLineTex ("ScanLine Texture", 2D) = "white" {}
        _NoiseTex ("Noise Texture", 2D) = "white" {}
        _NoiseXSpeed ("Noise X Speed", Float) = 100.0
        _NoiseYSpeed ("Noise Y Speed", Float) = 100.0
        _ScanLineTileAmount ("Scan Line Tile Amount", Float) = 4.0
        _NightVisionColor ("Night vision Color", Color) = (1, 1, 1, 1)
        _Contrast ("Contrast", Range(0, 4)) = 2.0
        _Brightness ("Brightness", Range(0, 2)) = 1
        _RandomValue ("Random Value", Float) = 0
        _distortion ("Distortion", Float) = 1.0
        _scale ("Scale", Float) = 1.0
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
            sampler2D _ScanLineTex;
            sampler2D _NoiseTex;
            fixed4 _NightVisionColor;
            float _Contrast;
            fixed _ScanLineTileAmount;
            fixed _Brightness;
            fixed _RandomValue;

            fixed _NoiseXSpeed;
            fixed _NoiseYSpeed;

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
                //distortedUV = half2(i.uv.x, i.uv.y + (_RandomValue * _SinTime.z *
                    //0.005));
                fixed4 renderTex = tex2D(_MainTex, distortedUV);

                fixed4 vignetteTex = tex2D(_VignetteTex, i.uv);

                // scratches uv and pixels
                half2 scanLinesUV = half2(
                    i.uv.x * _ScanLineTileAmount, 
                    i.uv.y * _ScanLineTileAmount);
                fixed4 scanlineTex = tex2D(_ScanLineTex, scanLinesUV);

                // dust uv and pixels
                half2 noiseUV = half2(
                    i.uv.x + (_RandomValue * (_SinTime.z * _NoiseXSpeed)),
                    i.uv.y + (_RandomValue * (_SinTime.z * _NoiseYSpeed)));
                fixed4 noiseTex = tex2D(_NoiseTex, noiseUV);

                fixed lum = dot(fixed3(0.299, 0.587, 0.114), renderTex.rgb);
                lum += _Brightness;
                fixed4 finalColor = lum * 2 + _NightVisionColor;

                finalColor = pow(finalColor, _Contrast);

                finalColor *= vignetteTex;
                finalColor *= scanlineTex * noiseTex;

                return finalColor;

            }
            ENDCG
        }
    }
}
