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

        public void OnItemRemove(int index)
        {
            RemoveItem(index);
        }

        // Start is called before the first frame update
        void Start()
        {            
            InitUIElements();

            LoadUIContent();
        }

        public void SwitchTarget(BagSystem bag)
        {
            if (!System.Object.ReferenceEquals(null, bag) &&
                !System.Object.ReferenceEquals(bag, Bag))
            {
                Bag = bag;

                ClearItems();

                LoadBagContent(Bag);
            }
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

            SimWood sw = (SimWood)EntityManager.Instance.PlayerEntity;
            if (!System.Object.ReferenceEquals(null, sw))
            {
                Bag = sw.Bag;

                LoadBagContent(Bag);
            }
        }

        private void LoadBagContent(BagSystem bag)
        {
            if (System.Object.ReferenceEquals(null, bag))
                return;

            Bag.OnAddItem += OnItemAdd;
            Bag.OnRemoveItem += OnItemRemove;

            List<BaseBagItem> items = Bag.GetAllItems();

            foreach (var item in items)
            {
                AddItem(item);
            }
        }

        /// <summary>
        /// Add ui show on bag ui of items.
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(BaseBagItem item)
        {
            if (IsUIElementsReady)
            {
                ItemBtnImages[item.Index].sprite =
                    IconsAssetHolder.Instance.GetIconByID(item.IconID);

                Txts[item.Index].text = item.Count.ToString();
            }
        }

        /// <summary>
        /// Remove item showed on ui at given index.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveItem(int index)
        {
            if (IsUIElementsReady)
            {
                ItemBtnImages[index].sprite = null;

                Txts[index].text = "";

                if (!System.Object.ReferenceEquals(null, Bag))
                {
                    Bag.RemoveBagItem(index);
                }
            }
        }

        public void ClearItems()
        {
            for (int i = 0; i < ItemBtnImages.Count; i++)
            {
                ItemBtnImages[i].sprite = null;
                Txts[i].text = "";
            }
        }

        public void ClickOnItem(int index)
        {
            RemoveItem(index);
            if (!System.Object.ReferenceEquals(null, Bag))
            {
                Bag.UseItemAtIndex(index);
            }
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
