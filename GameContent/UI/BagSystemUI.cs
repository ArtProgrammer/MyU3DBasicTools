using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using SimpleAI.Game;
using GameContent.SimAgent;

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
                InteractItem bbi = Bag.GetItemByIndex(index);
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
                InteractItem bbi = Bag.GetItemByIndex(index);
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

            UILord.Instance.CurBagUI = this;

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

            List<InteractItem> items = Bag.GetAllItems();

            foreach (var item in items)
            {
                AddItem(item);
            }
        }

        /// <summary>
        /// Add ui show on bag ui of items.
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(InteractItem item)
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

        public void ClickOnItem(int index, int count = 0)
        {
            //RemoveItem(index);
            if (!System.Object.ReferenceEquals(null, Bag))
            {
                if (UILord.Instance.HasItem)
                {
                    //if (!System.Object.ReferenceEquals(null, UILord.Instance.CurBagItem))
                    {
                        if (UILord.Instance.CurItemUIType == InteractItemUIType.Bag)
                        {
                            HandleBagItemJoin(UILord.Instance.CurBagItem, index);
                        }
                        else if (UILord.Instance.CurItemUIType == InteractItemUIType.Shortcut)
                        {
                            HandleShortcutItemJoin(UILord.Instance.CurShortcutItem, index);
                        }
                        else if (UILord.Instance.CurItemUIType == InteractItemUIType.RoleInfo)
                        {
                            HandleRoleInfoItemJoin(UILord.Instance.CurRoleInfoItem, index);
                        }

                        UILord.Instance.ClearSelectItem();
                    }
                }
                else
                {
                    InteractItem item = Bag.GetItemByIndex(index);
                    if (!System.Object.ReferenceEquals(null, item))
                    {
                        UILord.Instance.SelectBagItem(item);
                        UILord.Instance.CurItemUIType = InteractItemUIType.Bag;
                    }
                }                
            }
        }

        public void HandleRoleInfoItemJoin(InteractItem srcItem, int dstIndex)
        {
            var itemsrc = srcItem;
            var itemdst = Bag.GetItemByIndex(dstIndex);

            if (!System.Object.ReferenceEquals(null, itemdst))
            {
                bool isSame = itemsrc.CfgID == itemdst.CfgID;

                if (!isSame)
                {
                    //Bag.RemoveBagItem(itemsrc.Index);
                    UILord.Instance.CurRoleInfoUI.RemoveItem(itemsrc.Index);
                    if (!System.Object.ReferenceEquals(null, itemdst))
                    {
                        Bag.RemoveBagItem(itemdst.Index);
                        UILord.Instance.CurRoleInfo.AddItemAtIndex(itemdst.Kind,
                            itemdst.CfgID, itemsrc.Index, itemdst.Count);
                    }

                    Bag.AddItemAtIndex(itemsrc.CfgID, dstIndex, itemsrc.Count);
                }
                else
                {
                    int left = Bag.AddItemAtIndex(itemdst.CfgID, itemdst.Index, itemsrc.Count);
                    if (left <= 0)
                    {
                        UILord.Instance.CurRoleInfoUI.RemoveItem(itemsrc.Index);
                    }
                    else
                    {
                        UILord.Instance.CurRoleInfo.UpdateItemCount(itemsrc.Index, left);
                    }
                }
            }
            else
            {
                //Bag.RemoveBagItem(itemsrc.Index);
                UILord.Instance.CurRoleInfoUI.RemoveItem(itemsrc.Index);
                Bag.AddItemAtIndex(itemsrc.CfgID, dstIndex, itemsrc.Count);
            }
        }

        public void HandleShortcutItemJoin(InteractItem srcItem, int dstIndex)
        {
            var itemsrc = srcItem;
            var itemdst = Bag.GetItemByIndex(dstIndex);

            if (!System.Object.ReferenceEquals(null, itemdst))
            {
                bool isSame = itemsrc.CfgID == itemdst.CfgID;

                if (!isSame)
                {
                    //Bag.RemoveBagItem(itemsrc.Index);
                    UILord.Instance.CurShortcutUI.RemoveItem(itemsrc.Index);
                    if (!System.Object.ReferenceEquals(null, itemdst))
                    {
                        Bag.RemoveBagItem(itemdst.Index);
                        UILord.Instance.CurShortcut.AddItemAtIndex(itemdst.Kind,
                            itemdst.CfgID, itemsrc.Index, itemdst.Count);
                    }

                    Bag.AddItemAtIndex(itemsrc.CfgID, dstIndex, itemsrc.Count);
                }
                else
                {
                    int left = Bag.AddItemAtIndex(itemdst.CfgID, itemdst.Index, itemsrc.Count);
                    if (left <= 0)
                    {
                        UILord.Instance.CurShortcut.RemoveItem(itemsrc.Index);
                    }
                    else
                    {
                        UILord.Instance.CurShortcut.UpdateItemCount(itemsrc.Index, left);
                    }
                }
            }
            else
            {
                //Bag.RemoveBagItem(itemsrc.Index);
                UILord.Instance.CurShortcutUI.RemoveItem(itemsrc.Index);
                Bag.AddItemAtIndex(itemsrc.CfgID, dstIndex, itemsrc.Count);
            }
        }

        public void HandleBagItemJoin(InteractItem srcItem, int dstIndex)
        {
            if (srcItem.Index == dstIndex)
                return;

            var itemsrc = srcItem;
            var itemdst = Bag.GetItemByIndex(dstIndex);

            if (!System.Object.ReferenceEquals(null, itemdst))
            {
                bool isSame = itemsrc.CfgID == itemdst.CfgID;

                if (!isSame)
                {
                    Bag.RemoveBagItem(itemsrc.Index);
                    if (!System.Object.ReferenceEquals(null, itemdst))
                    {
                        Bag.RemoveBagItem(itemdst.Index);
                        Bag.AddItemAtIndex(itemdst.CfgID, itemsrc.Index, itemdst.Count);
                    }

                    Bag.AddItemAtIndex(itemsrc.CfgID, dstIndex, itemsrc.Count);
                }
                else
                {
                    int left = Bag.AddItemAtIndex(itemdst.CfgID, itemdst.Index, itemsrc.Count);
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
                Bag.AddItemAtIndex(itemsrc.CfgID, dstIndex, itemsrc.Count);
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
