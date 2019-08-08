using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleAI.ShaderScripts
{
    [ExecuteInEditMode]
    public class OldFilmEffect : MonoBehaviour
    {
        #region Variables
        public Shader CurShader;

        public float OldFilmEffectAmout = 1.0f;

        public Color sepiaColor = Color.white;

        public Texture2D vignetteTexture;

        public float vignetteAmount = 1.0f;

        public Texture2D scratchesTexture;

        public float scratchesYSpeed = 10.0f;

        public float scratchesXSpeed = 10.0f;

        public Texture2D dustTexture;

        public float dustYSpeed = 10.0f;

        public float dustXSpeed = 10.0f;

        private Material CurMaterial = null;

        private float randomValue;

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
                vignetteAmount = Mathf.Clamp01(vignetteAmount);
                OldFilmEffectAmout = Mathf.Clamp(OldFilmEffectAmout, 0f, 1.5f);
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
                    TheMaterial.SetColor("_SepiaColor", sepiaColor);
                    TheMaterial.SetFloat("_VignetteAmount", vignetteAmount);
                    TheMaterial.SetFloat("_EffectAmount", OldFilmEffectAmout);
                    TheMaterial.SetFloat("_distortion", Distortion);
                    TheMaterial.SetFloat("_scale", TheScale);

                    if (vignetteTexture)
                    {
                        TheMaterial.SetTexture("_VignetteTex", vignetteTexture);
                    }

                    if (scratchesTexture)
                    {
                        TheMaterial.SetTexture("_ScratchesTex", scratchesTexture);
                        TheMaterial.SetFloat("_ScratchesYSpeed", scratchesYSpeed);
                        TheMaterial.SetFloat("_ScratchesXSpeed", scratchesXSpeed);
                    }

                    if (dustTexture)
                    {
                        TheMaterial.SetTexture("_DustTex", dustTexture);
                        TheMaterial.SetFloat("_dustYSpeed", dustYSpeed);
                        TheMaterial.SetFloat("_dustXSpeed", dustXSpeed);
                        TheMaterial.SetFloat("_RandomValue", randomValue);
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