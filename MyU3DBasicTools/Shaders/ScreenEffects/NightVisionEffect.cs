using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleAI.ShaderScripts
{
    public class NightVisionEffect : MonoBehaviour
    {
        #region Variables
        public Shader CurShader;

        public float contrast = 2.0f;

        public float brightness = 1.0f;

        public Color nightVisionColor = Color.white;

        public Texture2D vignetteTexture;

        public Texture2D scanlineTexture;

        public float scanlineTileAmount = 4.0f;

        public Texture2D nightVisionNoise;

        public float noiseXSpeed = 100.0f;

        public float noiseYSpeed = 100.0f;

        private Material CurMaterial = null;

        private float randomValue = 0.0f;

        public float Distortion = 1.0f;

        public float TheScale = 1.0f;

        public bool IsEnable = true;

        #endregion

        #region Properties
        Material TheMaterial
        {
            get
            {
                if (System.Object.ReferenceEquals(null, CurMaterial))
                {
                    CurMaterial = new Material(CurShader);
                    CurMaterial.hideFlags = HideFlags.HideAndDontSave;
                }
                return CurMaterial;
            }
        }

        #endregion

        // Start is called before the first frame update
        void Start()
        {
            if (!SystemInfo.supportsImageEffects)
            {
                enabled = false;
                return;
            }

            if (!CurShader && !CurShader.isSupported)
            {
                enabled = false;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (IsEnable)
            {
                contrast = Mathf.Clamp(contrast, 0f, 4f);
                brightness = Mathf.Clamp(brightness, 0f, 2f);
                Distortion = Mathf.Clamp(Distortion, -1f, 1f);
                TheScale = Mathf.Clamp(TheScale, 0f, 3f);
                randomValue = Random.Range(-1f, 1f);
            }
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            bool hasMat = false;

            if (IsEnable)
            {
                if (!System.Object.ReferenceEquals(null, CurShader))
                {
                    TheMaterial.SetFloat("_Contrast", contrast);
                    TheMaterial.SetFloat("_Brightness", brightness);
                    TheMaterial.SetColor("_NightVisionColor", nightVisionColor);
                    TheMaterial.SetFloat("_distortion", Distortion);
                    TheMaterial.SetFloat("_scale", TheScale);

                    if (vignetteTexture)
                    {
                        TheMaterial.SetTexture("_VignetteTex", vignetteTexture);
                    }

                    if (scanlineTexture)
                    {
                        TheMaterial.SetTexture("_ScanLineTex", scanlineTexture);
                        TheMaterial.SetFloat("_ScanLineTileAmount", scanlineTileAmount);
                    }

                    if (nightVisionNoise)
                    {
                        TheMaterial.SetTexture("_NoiseTex", nightVisionNoise);
                        TheMaterial.SetFloat("_NoiseXSpeed", noiseXSpeed);
                        TheMaterial.SetFloat("_NoiseYSpeed", noiseYSpeed);
                    }

                    Graphics.Blit(source, destination, TheMaterial);
                    hasMat = true;
                }
            }

            if (!hasMat)
            {
                Graphics.Blit(source, destination);
            }
        }

        private void OnDisable()
        {
            if (CurMaterial)
            {
                DestroyImmediate(CurMaterial);
            }
        }
    }
}