using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using GameContent.SimAgent;
using UnityEngine.UI;

namespace GameContent
{
    public class BagSystemUI : MonoBehaviour
    {
        public RectTransform Root = null;

        public RectTransform ItemsPanel = null;

        public bool IsUIElementsReady = false;

        private List<Image> ItemBtnImages = new List<Image>();

        private List<Text> Txts = new List<Text>();

        public BagSystem Bag = null;

        public void OnItemAdd(int index)
        {
            if (!System.Object.ReferenceEquals(null, Bag))
            {
                BaseBagItem bbi = Bag.GetItemByIndex(index);
                if (!System.Object.ReferenceEquals(null, bbi))
                {
                    AddItem(bbi);
                }
            }
        }

        // Start is called before the first frame update
        void Start()
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
                    var btn = sub.GetComponent<Image>();
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

            SimWood sw = (SimWood)EntityManager.Instance.PlayerEntity;
            if (!System.Object.ReferenceEquals(null, sw))
            {
                Bag = sw.Bag;

                Bag.OnAddItem += OnItemAdd;

                List<BaseBagItem> items = Bag.GetAllItems();

                foreach (var item in items)
                {
                    AddItem(item);
                }
            }
        }

        public void AddItem(BaseBagItem item)
        {
            if (IsUIElementsReady)
            {
                ItemBtnImages[item.Index].sprite =
                    IconsAssetHolder.Instance.GetIconByID(item.IconID);

                Txts[item.Index].text = item.Count.ToString();
            }            
        }

        public void ClickOnItem(int index)
        {
            if (!System.Object.ReferenceEquals(null, Bag))
            {
                Bag.UseItemAtIndex(index);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Close(int index)
        {
            if (Root)
            {
                Root.gameObject.SetActive(false);
            }
        }
    }
}
