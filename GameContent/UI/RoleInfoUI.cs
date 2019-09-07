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

        public RoleInfoSystem RoleInfo = null;

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
                RoleInfo = sw.RoleInfo;
                UILord.Instance.CurRoleInfo = RoleInfo;

                LoadContent(RoleInfo);
            }
        }

        private void LoadContent(RoleInfoSystem src)
        {
            if (System.Object.ReferenceEquals(null, src))
                return;

            RoleInfo.OnAddItem += OnItemAdd;
            RoleInfo.OnRemoveItem += OnItemRemove;
            RoleInfo.OnItemChange += OnItemChange;

            List<InteractItem> items = RoleInfo.GetAllItems();

            foreach (var item in items)
            {
                AddItem(item);
            }
        }

        public void OnItemAdd(int index)
        {
            if (!System.Object.ReferenceEquals(null, RoleInfo))
            {
                InteractItem bbi = RoleInfo.GetItemByIndex(index);
                if (!System.Object.ReferenceEquals(null, bbi))
                {
                    AddItem(bbi);
                }
            }
        }

        public void OnItemChange(int index)
        {
            if (!System.Object.ReferenceEquals(null, RoleInfo))
            {
                InteractItem bbi = RoleInfo.GetItemByIndex(index);
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

                if (!System.Object.ReferenceEquals(null, RoleInfo))
                {
                    RoleInfo.RemoveItem(index);
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
            if (!System.Object.ReferenceEquals(null, RoleInfo))
            {
                if (UILord.Instance.HasItem)
                {
                    if (!System.Object.ReferenceEquals(null,
                        UILord.Instance.CurBagItem))
                    {
                        HandleBagItemJoin(UILord.Instance.CurBagItem,
                            index);
                    }                    
                }
                else
                {
                    //RoleInfo.UseItemAtIndex(index, 1);
                    InteractItem item = RoleInfo.GetItemByIndex(index);
                    if (!System.Object.ReferenceEquals(null, item))
                    {
                        UILord.Instance.SelectRoleItem(item);
                        UILord.Instance.CurItemUIType = InteractItemUIType.RoleInfo;
                    }
                }
            }
        }

        public void HandleBagItemJoin(InteractItem srcItem, int dstIndex)
        {
            if (RoleInfo)
            {
                var InteractItem = RoleInfo.GetItemByIndex(dstIndex);

                if (!System.Object.ReferenceEquals(null, InteractItem))
                {
                    if (InteractItem.Kind == InteractItemType.Item)
                    {
                        if (InteractItem.CfgID == srcItem.CfgID)
                        {
                            int left = RoleInfo.AddItemAtIndex(0,
                                srcItem.CfgID, dstIndex, srcItem.Count);

                            UILord.Instance.CurBag.ChangeBagItem(srcItem.Index,
                                left);
                        }
                        else
                        {
                            RoleInfo.RemoveItem(InteractItem.Index);
                            RoleInfo.AddItemAtIndex(0, srcItem.CfgID,
                                dstIndex, srcItem.Count);
                            UILord.Instance.CurBag.RemoveBagItem(srcItem.Index);
                            UILord.Instance.CurBag.AddItemAtIndex(
                                InteractItem.CfgID, srcItem.Index,
                                InteractItem.Count);
                        }
                    }
                    else
                    {
                        RoleInfo.RemoveItem(InteractItem.Index);
                        RoleInfo.AddItemAtIndex(0, srcItem.CfgID,
                            dstIndex, srcItem.Count);
                        UILord.Instance.CurBag.RemoveBagItem(srcItem.Index);
                    }
                }
                else
                {
                    RoleInfo.AddItemAtIndex(0, srcItem.CfgID, dstIndex, srcItem.Count);
                    // remove the item in bag.
                    UILord.Instance.CurBag.RemoveBagItem(srcItem.Index);
                }

                UILord.Instance.ClearSelectItem();
            }
        }
    }
}