using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using GameContent.Skill;

namespace GameContent.Interaction
{
    public class UISkillData : MonoBehaviour
    {
        private Dictionary<int, BaseSkill> Items = 
            new Dictionary<int, BaseSkill>();

        UISkillVIew TheView = null;

        // Start is called before the first frame update
        void Start()
        {
            TheView = GetComponent<UISkillVIew>();

            LoadContent();

            //TheView.
        }

        public void LoadContent()
        {
            var skill = new RiseupSkill();
            skill.Icon = Path.Combine(Application.dataPath, 
                "/Images/PureImages/Board-Games.png");
            AddItem(0, skill);

            //TheView.LoadContent();
        }

        public void SaveContent()
        { 
        }

        public BaseSkill GetItemByIndex(int index)
        {
            if(Items.ContainsKey(index))
            {
                return Items[index];
            }
            return null;
        }

        public void AddItem(int index, BaseSkill skill)
        {
            TheView.OnAddItem(index, skill);
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