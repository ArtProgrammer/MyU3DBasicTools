using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using SimpleAI.Logger;

namespace GameContent.UI
{
    public class FPSShower : MonoBehaviour
    {
        public float UpdateInterval = 0.5f;

        public Text Txt;

        private float Accum = 0;

        private int Frames = 0;

        private float TimeLeft = 0;

        // Start is called before the first frame update
        void Start()
        {
            Txt = GetComponent<Text>();
            if (!Txt)
            {
                TinyLogger.Instance.DebugLog("$FPS shower needs a Text component!");
                enabled = false;

                return;
            }

            TimeLeft = UpdateInterval;
        }

        // Update is called once per frame
        void Update()
        {
            TimeLeft -= Time.deltaTime;

            Accum += Time.timeScale / Time.deltaTime;

            ++Frames;

            if (TimeLeft <= 0.0f)
            {
                float fps = Accum / Frames;
                string format = string.Format("{0:F2} FPS", fps);
                Txt.text = format;

                if (fps < 30)
                {
                    Txt.color = Color.yellow;
                }
                else if (fps < 10)
                {
                    Txt.color = Color.red;
                }
                else
                {
                    Txt.color = Color.green;
                }

                TimeLeft = UpdateInterval;
                Accum = 0.0f;
                Frames = 0;
            }
        }
    }
}
