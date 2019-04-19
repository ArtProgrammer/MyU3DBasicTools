using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameContent.Skill;

namespace GameContent.Interaction
{
    public class UISkillInteractor : MonoBehaviour
    {
        UISkillData SkillData = null;

        // Start is called before the first frame update
        void Start()
        {
            LoadContent();
        }

        public void LoadContent()
        {
            SkillData = GetComponent<UISkillData>();
        }

        public void TryItem(int index)
        {
            //SkillData
            BaseSkill skill = SkillData.GetItemByIndex(index);
            if (!System.Object.ReferenceEquals(skill, null))
            {
                SKillMananger.Instance.TryUseSkill(skill.UniqueID);
            }
        }

        public void AddItem(int index)
        {

        }

        public void RemoveItem(int index)
        {
        }

        public void SweepItems(int srcIndex, int dstIndex)
        {
        }
    }
}