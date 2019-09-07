using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using SimpleAI.Game;
using GameContent.SimAgent;

namespace GameContent
{
    public class RoleInfoUI : MonoBehaviour
    {
        public RectTransform Root = null;

        public RectTransform ItemsPanel = null;

        public bool IsUIElementsReady = false;

        private List<Image> ItemBtnImages = new List<Image>();

        private List<Text> Txts = new List<Text>();

        public void Close()
        {
            if (Root)
            {
                Root.gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            InitUIElements();

            LoadUIContent();
        }

        private void InitUIElements()
        {
            if (ItemsPanel)
            {
                for (int i = 0; i < ItemsPanel.childCount; i++)
                {
                    Transform sub = ItemsPanel.GetChild(i);
                    var btn = sub.GetComponentInChildren<Image>();
                    if (btn)
                    {
                        ItemBtnImages.Add(btn);
                    }

                    var txt = sub.GetComponentInChildren<Text>();
                    if (txt)
                    {
                        Txts.Add(txt);
                    }
                }

                IsUIElementsReady = true;
            }
        }

        private void LoadUIContent()
        {
            if (!IsUIElementsReady)
                return;

            UILord.Instance.CurRoleInfoUI = this;

            SimWood sw = (SimWood)EntityManager.Instance.PlayerEntity;
            if (!System.Object.ReferenceEquals(null, sw))
            {                
            }
        }        
    }
}