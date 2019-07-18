using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using SimpleAI.Utils;
using SimpleAI.Logger;

namespace GameContent.Loading
{
    public class LoadingTest : MonoBehaviour
    {
        public Image TestImage = null;

        public AudioSource AudioSrc = null;

        // Start is called before the first frame update
        void Start()
        {
            HttpModular.Instance.TryGetTexture("http://192.168.124.112/Mongol.png", setImage);
            HttpModular.Instance.TryGetAudio("http://192.168.124.112/Nohchicho.wav", AudioType.WAV, SetAudio);
        }

        public void setImage(Texture2D tex)
        {
            TinyLogger.Instance.DebugLog("%Texure loaded! Width = " + tex.width + ", height = " + tex.height);
            Sprite newsp = Sprite.Create(tex,
                    new Rect(0, 0,
                    tex.width, tex.height),
                    new Vector2(0, 0),
                    100);

            TestImage.sprite = newsp;

            TinyLogger.Instance.DebugLog("$ temp path: " + Application.temporaryCachePath);
        }

        public void SetAudio(AudioClip audioClip)
        {
            AudioSrc.clip = audioClip;
            AudioSrc.Play();


            Debug.Log(System.Environment.Version.ToString());
        }

        public void Transition()
        {
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
        }
    }
}