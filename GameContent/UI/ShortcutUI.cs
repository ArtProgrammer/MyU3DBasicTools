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
                ShortcutItem bbi = Shortcut.GetItemByIndex(index);
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
                ShortcutItem bbi = Shortcut.GetItemByIndex(index);
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

            List<ShortcutItem> items = Shortcut.GetAllItems();

            foreach (var item in items)
            {
                AddItem(item);
            }
        }

        public void AddItem(ShortcutItem item)
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

        public void ClickOnItem(int index)
        {
            if (!System.Object.ReferenceEquals(null, Shortcut))
            {
                //Shortcut.UseItemAtIndex(index, 1);
                ShortcutItem item = Shortcut.GetItemByIndex(index);
                if (!System.Object.ReferenceEquals(null, item))
                {
                    UILord.Instance.SelectShortcutItem(item);
                }
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