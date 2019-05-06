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

        public BaseUsableData Data = null;

        public UISkillItem(int index, BaseUsableData data)
        {
            Index = index;
            Data = data;
        }
    }

    public class UISkillData : MonoBehaviour
    {
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

            var data = SKillMananger.Instance.GetSkillData(10001);
            AddItem(0, data, false);
            var data1 = SKillMananger.Instance.GetSkillData(10002);
            AddItem(1, data1, false);
            
            var dataItem1 = ItemManager.Instance.GetItemData(1000001);
            AddItem(2, dataItem1, false);

            var dataItem2 = ItemManager.Instance.GetItemData(1000002);
            AddItem(3, dataItem2, false);
        }

        public void PlaceData()
        { 
            for (int i = 0; i < Items.Count; ++i)
            {
                TheView.OnAddItem(Items[i].Index, Items[i].Data);
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

        public void AddItem(int index, BaseUsableData data, bool alterUI = true)
        {
            if (Items.Count <= index)
            {
                var uiitem = new UISkillItem(index, data);
                Items.Add(index, uiitem);
            }
            else
            {
                Items[index].Data = data;
            }

            if (alterUI)
            {
                TheView.OnAddItem(index, data);
            }
        }
        
        public void RemoveItem(int index)
        {
            TheView.OnRemoveItem(index);
        }

        public void Save()
        {
            
        }
    }
}