using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using GameContent;

namespace GameContent.Interaction
{
    public class UIShortcut : MonoBehaviour
    {
        public List<ShortcutItem> ItemOnShortcut = new List<ShortcutItem>();

        RectTransform Root = null;

        List<Image> BtnList = new List<Image>();

        List<Text> Texts = new List<Text>();

        private void Initialize()
        {
            InitDatas();
            InitView();
        }

        private void InitDatas()
        {
            {
                ShortcutItem scItem = new ShortcutItem();
                scItem.Index = 0;
                scItem.ItemCfgID = 1;
                scItem.Count = 1;

                ItemOnShortcut.Add(scItem);
            }

            {
                ShortcutItem scItem = new ShortcutItem();
                scItem.Index = 1;
                scItem.ItemCfgID = 2;
                scItem.Count = 1;

                ItemOnShortcut.Add(scItem);
            }

            {
                ShortcutItem scItem = new ShortcutItem();
                scItem.Index = 4;
                scItem.ItemCfgID = 2;
                scItem.Count = 1;

                ItemOnShortcut.Add(scItem);
            }
        }

        private void InitView()
        {
            for (int i = 0; i < ItemOnShortcut.Count; i++)
            {
                OnAddItem(ItemOnShortcut[i]);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            Root = GetComponent<RectTransform>();

            Transform sub = null;

            if (!System.Object.ReferenceEquals(null, Root))
            {
                for (int i = 0; i < Root.childCount; i++)
                {
                    sub = Root.GetChild(i);
                    Image btnimg = sub.GetComponent<Image>();
                    Button btn = sub.GetComponent<Button>();
                    //btn.onClick.AddListener( () => OnSkilLClick(i));
                    BtnList.Add(btnimg);

                    var txt = sub.GetComponentInChildren<Text>();
                    Texts.Add(txt);
                }

                sub = null;
            }

            Initialize();
        }

        private void OnDestroy()
        {
            BtnList.Clear();
            Texts.Clear();
        }

        public void AddItem(int cfgID, int count, int index)
        {

        }

        public void OnAddItem(UISkillItem item)
        {
            Sprite sp = IconsAssetHolder.Instance.GetIconByID(1);

            if (!System.Object.ReferenceEquals(null, sp))
            {
                BtnList[0].sprite = sp;
            }
        }

        public void OnAddItem(ShortcutItem item)
        {
            Sprite sp = IconsAssetHolder.Instance.GetIconByID(item.ItemCfgID);

            if (!System.Object.ReferenceEquals(null, sp))
            {
                BtnList[item.Index].sprite = sp;

                Config.IconsConfig icon = ConfigDataMgr.Instance.IconCfgLoader.GetDataByID(item.ItemCfgID);

                Texts[item.Index].text = icon.Name;
            }
        }

        public void OnRemoveItem(int index)
        {
            //
        }

        public void OnItemChange(int index)
        {

        }

        public void TryUseItem(int index)
        {

        }
    }
}