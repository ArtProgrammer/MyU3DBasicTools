using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameContent
{
    public class HUDUI : MonoBehaviour
    {
        public GameObject BagPanel = null;

        public GameObject RoleInfoPanel = null;

        public void SwitchBag()
        {
            if (BagPanel)
            {
                BagPanel.SetActive(!BagPanel.activeSelf);
            }
        }

        public void SwitchRoleInfo()
        {
            if (RoleInfoPanel)
            {
                RoleInfoPanel.SetActive(!RoleInfoPanel.activeSelf);
            }
        }
    }
}