using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameContent.UI
{
    public class HealthBar : MonoBehaviour
    {
        #region PRIVATE_VARIABLES
        private Camera MainCam = null;

        private Vector3 Pos = Vector3.zero;

        private RectTransform RectTrans = null;
        #endregion

        #region PUBLIC_VARIABLES
        public Slider Bar = null;

        public Transform Target = null;

        public Vector3 Offset = Vector3.zero;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            MainCam = Camera.main;

            Bar = GetComponent<Slider>();
            RectTrans = Bar.GetComponent<RectTransform>();
        }

        public void ChangePercent(float per)
        {
            if (Bar)
            {
                Bar.value = per;
            }
        }

        public void Update()
        {
            Pos = MainCam.WorldToScreenPoint(Target.position);
            RectTrans.position = Pos + Offset;
        }
    }
}
