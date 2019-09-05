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

        public void OnItemChange(int index)
        {
            if (!System.Object.ReferenceEquals(null, Bag))
            {
                BaseBagItem bbi = Bag.GetItemByIndex(index);
                if (!System.Object.ReferenceEquals(null, bbi))
                {
                    Txts[index].text = bbi.Count.ToString();
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
                UILord.Instance.CurBag = Bag;

                LoadBagContent(Bag);
            }
        }

        private void LoadBagContent(BagSystem bag)
        {
            if (System.Object.ReferenceEquals(null, bag))
                return;

            Bag.OnAddItem += OnItemAdd;
            Bag.OnRemoveItem += OnItemRemove;
            Bag.OnItemChange += OnItemChange;

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
            //RemoveItem(index);
            if (!System.Object.ReferenceEquals(null, Bag))
            {
                if (UILord.Instance.HasItem)
                {
                    if (!System.Object.ReferenceEquals(null, UILord.Instance.CurBagItem))
                    {
                        HandleItemIndexChange(UILord.Instance.CurBagItem, index);

                        UILord.Instance.ClearSelectItem();
                    }
                }
                else
                {
                    BaseBagItem item = Bag.GetItemByIndex(index);
                    if (!System.Object.ReferenceEquals(null, item))
                    {
                        UILord.Instance.SelectBagItem(item);
                    }
                }                
            }
        }

        public void HandleItemIndexChange(BaseBagItem srcItem, int dstIndex)
        {
            if (srcItem.Index == dstIndex)
                return;

            var itemsrc = srcItem;
            var itemdst = Bag.GetItemByIndex(dstIndex);

            if (!System.Object.ReferenceEquals(null, itemdst))
            {
                bool isSame = itemsrc.ItemCfgID == itemdst.ItemCfgID;

                if (!isSame)
                {
                    Bag.RemoveBagItem(itemsrc.Index);
                    if (!System.Object.ReferenceEquals(null, itemdst))
                    {
                        Bag.RemoveBagItem(itemdst.Index);
                        Bag.AddItemAtIndex(itemdst.ItemCfgID, itemsrc.Index, itemdst.Count);
                    }

                    Bag.AddItemAtIndex(itemsrc.ItemCfgID, dstIndex, itemsrc.Count);
                }
                else
                {
                    int left = Bag.AddItemAtIndex(itemdst.ItemCfgID, itemdst.Index, itemsrc.Count);
                    if (left <= 0)
                    {
                        Bag.RemoveBagItem(itemsrc.Index);
                    }
                    else
                    {
                        Bag.ChangeBagItem(itemsrc.Index, left);
                    }
                }
            }
            else
            {
                Bag.RemoveBagItem(itemsrc.Index);
                Bag.AddItemAtIndex(itemsrc.ItemCfgID, dstIndex, itemsrc.Count);
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
