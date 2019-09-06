using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using SimpleAI.Game;
using GameContent.SimAgent;

namespace GameContent
{
    public class ShortcutUI : MonoBehaviour
    {
        public RectTransform Root = null;

        public RectTransform ItemsPanel = null;

        public bool IsUIElementsReady = false;

        private List<Image> ItemBtnImages = new List<Image>();

        private List<Text> Txts = new List<Text>();

        public ShortCutSystem Shortcut = null;

        // Start is called before the first frame update
        void Start()
        {
            InitUIElements();

            LoadUIContent();
        }

        public void OnItemAdd(int index)
        {
            if (!System.Object.ReferenceEquals(null, Shortcut))
            {
                InteractItem bbi = Shortcut.GetItemByIndex(index);
                if (!System.Object.ReferenceEquals(null, bbi))
                {
                    AddItem(bbi);
                }
            }
        }

        public void OnItemChange(int index)
        {
            if (!System.Object.ReferenceEquals(null, Shortcut))
            {
                InteractItem bbi = Shortcut.GetItemByIndex(index);
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
                Shortcut = sw.Shortcut;

                UILord.Instance.CurShortcut = Shortcut;
                UILord.Instance.CurShortcutUI = this;

                LoadContent(Shortcut);
            }
        }

        private void LoadContent(ShortCutSystem src)
        {
            if (System.Object.ReferenceEquals(null, src))
                return;

            Shortcut.OnAddItem += OnItemAdd;
            Shortcut.OnRemoveItem += OnItemRemove;
            Shortcut.OnItemChange += OnItemChange;

            List<InteractItem> items = Shortcut.GetAllItems();

            foreach (var item in items)
            {
                AddItem(item);
            }
        }

        public void AddItem(InteractItem item)
        {
            if (IsUIElementsReady)
            {
                ItemBtnImages[item.Index].sprite =
                    IconsAssetHolder.Instance.GetIconByID(item.IconID);

                Txts[item.Index].text = item.Count.ToString();
            }
        }

        public void RemoveItem(int index)
        {
            if (IsUIElementsReady)
            {
                ItemBtnImages[index].sprite = null;

                Txts[index].text = "";

                if (!System.Object.ReferenceEquals(null, Shortcut))
                {
                    Shortcut.RemoveItem(index);
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

        public void ClickOnItem(InteractItemUIType itemUIType, int index)
        {
            if (!System.Object.ReferenceEquals(null, Shortcut))
            {
                if (UILord.Instance.HasItem)
                {
                    if (!System.Object.ReferenceEquals(null,
                        UILord.Instance.CurBagItem))
                    {
                        HandleBagItemJoin(UILord.Instance.CurBagItem,
                            index);
                    }
                    else if (!System.Object.ReferenceEquals(null,
                           UILord.Instance.CurShortcutItem))
                    {
                        HandleShortcutItemJoin(UILord.Instance.CurShortcutItem,
                            index);
                    }
                }
                else
                {
                    //Shortcut.UseItemAtIndex(index, 1);
                    InteractItem item = Shortcut.GetItemByIndex(index);
                    if (!System.Object.ReferenceEquals(null, item))
                    {
                        UILord.Instance.SelectShortcutItem(item);
                        UILord.Instance.CurItemUIType = itemUIType;
                    }
                }                
            }
        }

        public void HandleShortcutItemJoin(InteractItem srcItem, int dstIndex)
        {
            if (srcItem.Index == dstIndex)
                return;

            if (Shortcut)
            {
                var InteractItem = Shortcut.GetItemByIndex(dstIndex);

                if (!System.Object.ReferenceEquals(null, InteractItem))
                {
                    if (InteractItem.CfgID == srcItem.CfgID)
                    {
                        int left = Shortcut.AddItemAtIndex(srcItem.Kind,
                            srcItem.CfgID, dstIndex, srcItem.Count);

                        if (left <= 0)
                        {
                            Shortcut.RemoveItem(srcItem.Index);
                        }
                        else
                        {
                            Shortcut.UpdateItemCount(srcItem.Index, left);
                        }
                        //UILord.Instance.CurBag.ChangeBagItem(srcItem.Index,
                        //    left);
                    }
                    else
                    {
                        Shortcut.RemoveItem(InteractItem.Index);
                        Shortcut.AddItemAtIndex(srcItem.Kind,
                            srcItem.CfgID, dstIndex, srcItem.Count);
                        Shortcut.RemoveItem(srcItem.Index);
                        Shortcut.AddItemAtIndex(srcItem.Kind,
                            InteractItem.CfgID, srcItem.Index,
                            InteractItem.Count);
                    }
                }
                else
                {
                    Shortcut.AddItemAtIndex(0, srcItem.CfgID, dstIndex, srcItem.Count);
                    // remove the item in bag.
                    //UILord.Instance.CurBag.RemoveBagItem(srcItem.Index);
                    Shortcut.RemoveItem(srcItem.Index);
                }

                UILord.Instance.ClearSelectItem();
            }
        }

        public void HandleBagItemJoin(InteractItem srcItem, int dstIndex)
        {
            if (Shortcut)
            {
                var InteractItem = Shortcut.GetItemByIndex(dstIndex);

                if (!System.Object.ReferenceEquals(null, InteractItem))
                {
                    if (InteractItem.Kind == 0)
                    {
                        if (InteractItem.CfgID == srcItem.CfgID)
                        {
                            int left = Shortcut.AddItemAtIndex(0,
                                srcItem.CfgID, dstIndex, srcItem.Count);

                            UILord.Instance.CurBag.ChangeBagItem(srcItem.Index,
                                left);
                        }
                        else
                        {                           
                            Shortcut.RemoveItem(InteractItem.Index);
                            Shortcut.AddItemAtIndex(0, srcItem.CfgID,
                                dstIndex, srcItem.Count);
                            UILord.Instance.CurBag.RemoveBagItem(srcItem.Index);
                            UILord.Instance.CurBag.AddItemAtIndex(
                                InteractItem.CfgID, srcItem.Index,
                                InteractItem.Count);
                        }
                    }
                    else
                    {
                        Shortcut.RemoveItem(InteractItem.Index);
                        Shortcut.AddItemAtIndex(0, srcItem.CfgID,
                            dstIndex, srcItem.Count);
                        UILord.Instance.CurBag.RemoveBagItem(srcItem.Index);
                    }
                }
                else
                {
                    Shortcut.AddItemAtIndex(0, srcItem.CfgID, dstIndex, srcItem.Count);
                    // remove the item in bag.
                    UILord.Instance.CurBag.RemoveBagItem(srcItem.Index);
                }

                UILord.Instance.ClearSelectItem();
            }
        }

        //public void HandleItemIndexChange(InteractItem srcItem, int dstIndex)
        //{
        //    UILord.Instance.ClearSelectItem();
        //}

        public void Close(int index)
        {
            if (Root)
            {
                Root.gameObject.SetActive(false);
            }
        }
    }
}