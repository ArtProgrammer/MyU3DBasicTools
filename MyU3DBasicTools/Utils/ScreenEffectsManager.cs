using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleAI.Utils
{
    [ExecuteInEditMode]
    public class ScreenEffectsManager : MonoBehaviour
    {
        #region Variables
        public Shader CurShader;

        public float GrayScaleAmount = 1.0f;

        private Material CurMaterial;
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
            GrayScaleAmount = Mathf.Clamp(GrayScaleAmount, 0.0f, 1.0f);
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (!System.Object.ReferenceEquals(null, CurShader))
            {
                TheMaterial.SetFloat("_LuminosityAmount", GrayScaleAmount);
                Graphics.Blit(source, destination, TheMaterial);
            }
            else
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