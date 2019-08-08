using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using GameContent.Skill;
using GameContent.UsableItem;
using GameContent.Item;

namespace GameContent.Interaction
{
    public class UISkillItem
    {
        public int Index = 0;

        public int Count;

        public int MaxCount;

        public BaseUsableData Data = null;

        public UISkillItem()
        { }

        public UISkillItem(int index, BaseUsableData data)
        {
            Index = index;
            Data = data;
        }
    }

    public class UISkillData : MonoBehaviour
    {
        private List<int> ItemIndexes = new List<int>();

        private Dictionary<int, UISkillItem> Items = 
            new Dictionary<int, UISkillItem>();

        UISkillVIew TheView = null;

        StringBuilder TextSB = new StringBuilder();

        private void Awake()
        {
            //LoadContent();
        }

        // Start is called before the first frame update
        void Start()
        {
            LoadContent();

            TheView = GetComponent<UISkillVIew>();

            PlaceData();
        }

        public void LoadContent()
        {
            //var skill = new RiseupSkill();

            //TextSB.Append(Application.dataPath);
            //TextSB.Append("/Images/PureImages/Board-Games.png");

            //skill.Icon = TextSB.ToString();
            //AddItem(0, skill, false);

            //var skill1 = new SuckBloodSkill();
            //skill1.Icon = TextSB.ToString();
            //AddItem(1, skill1, false);

            //var skill2 = new RiseupSkill();
            //skill2.Icon = TextSB.ToString();
            //AddItem(2, skill2, false);

            {
                UISkillItem usi = new UISkillItem();
                usi.Index = 0;
                usi.Count = 1;
                usi.Data = SKillMananger.Instance.GetSkillData(10008);

                AddItem(usi, false);
            }

            //{
            //    UISkillItem usi = new UISkillItem();
            //    usi.Index = 1;
            //    usi.Data = SKillMananger.Instance.GetSkillData(10002);
            //    AddItem(usi, false);
            //}

            {
                UISkillItem usi = new UISkillItem();
                usi.Index = 2;
                usi.Count = 1;
                usi.Data = SKillMananger.Instance.GetSkillData(10003);
                AddItem(usi, false);
            }

            //var dataItem1 = ItemManager.Instance.GetItemData(1000001);
            //AddItem(2, dataItem1, false);

            {
                UISkillItem usi = new UISkillItem();
                usi.Index = 3;
                usi.Count = 5;
                usi.Data = ItemManager.Instance.GetItemData(1000003);
                AddItem(usi, false);
            }
        }

        public void PlaceData()
        {
            int index = 0;
            for (int i = 0; i < ItemIndexes.Count; i++)
            {
                index = ItemIndexes[i];
                TheView.OnAddItem(Items[index]);
            }
        }

        public void SaveContent()
        { 
        }

        public BaseUsableData GetItemByIndex(int index)
        {
            if(Items.ContainsKey(index))
            {
                return Items[index].Data;
            }
            return null;
        }

        public void AddItem(UISkillItem item, bool alterUI = true)
        {
            var index = item.Index;

            ItemIndexes.Add(index);

            if (Items.Count <= index)
            {                
                Items.Add(index, item);
            }
            else
            {
                Items[index] = item;
            }

            if (alterUI)
            {
                TheView.OnAddItem(item);
            }
        }

        //public void AddItem(int index, BaseUsableData data, bool alterUI = true)
        //{
        //    if (Items.Count <= index)
        //    {
        //        var uiitem = new UISkillItem(index, data);
        //        Items.Add(index, uiitem);
        //    }
        //    else
        //    {
        //        Items[index].Data = data;
        //    }

        //    if (alterUI)
        //    {
        //        TheView.OnAddItem(index, data);
        //    }
        //}
        
        public void RemoveItem(int index)
        {
            ItemIndexes.Remove(index);
            TheView.OnRemoveItem(index);
        }

        public void Save()
        {
            
        }
    }
}