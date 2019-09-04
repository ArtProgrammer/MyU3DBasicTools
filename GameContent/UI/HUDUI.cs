using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameContent
{
    public class HUDUI : MonoBehaviour
    {
        public GameObject BagPanel = null;

        public void SwitchBag()
        {
            if (BagPanel)
            {
                BagPanel.SetActive(!BagPanel.activeSelf);
            }
        }
    }
}