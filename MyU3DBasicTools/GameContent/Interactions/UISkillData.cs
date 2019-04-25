using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using GameContent.Skill;

namespace GameContent.Interaction
{
    public class UISkillItem
    {
        public int Index = 0;

        public BaseSkill Skill = null;

        public UISkillItem(int index, BaseSkill skill)
        {
            Index = index;
            Skill = skill;
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
            LoadContent();
        }

        // Start is called before the first frame update
        void Start()
        {
            TheView = GetComponent<UISkillVIew>();

            PlaceData();
        }

        public void LoadContent()
        {
            var skill = new RiseupSkill();

            TextSB.Append(Application.dataPath);
            TextSB.Append("/Images/PureImages/Board-Games.png");

            skill.Icon = TextSB.ToString();
            AddItem(0, skill, false);

            var skill1 = new SuckBloodSkill();
            skill1.Icon = TextSB.ToString();
            AddItem(1, skill1, false);

            var skill2 = new RiseupSkill();
            skill2.Icon = TextSB.ToString();
            AddItem(2, skill2, false);
        }

        public void PlaceData()
        { 
            for (int i = 0; i < Items.Count; ++i)
            {
                TheView.OnAddItem(Items[i].Index, Items[i].Skill);
            }
        }

        public void SaveContent()
        { 
        }

        public BaseSkill GetItemByIndex(int index)
        {
            if(Items.ContainsKey(index))
            {
                return Items[index].Skill;
            }
            return null;
        }

        public void AddItem(int index, BaseSkill skill, bool alterUI = true)
        {
            if (Items.Count <= index)
            {
                Items.Add(index, new UISkillItem(index, skill));
            }
            else
            {
                Items[index].Skill = skill;
            }

            if (alterUI)
            {
                TheView.OnAddItem(index, skill);
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