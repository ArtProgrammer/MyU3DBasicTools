using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameContent.Skill;

namespace SimpleAI.PoolSystem
{
    public class SkillPool
    {
        private Stack<BaseSkill> Skills =
            new Stack<BaseSkill>();

        public int MaxCount = 18;

        private int CurCount = 0;

        public SkillKindType SkillKindID = SkillKindType.None;

        private Queue<BaseSkill> PreSkills = new Queue<BaseSkill>();

        public BaseSkill Spawn()
        { 
            if (CurCount > 0)
            {
                CurCount--;
                return Skills.Pop();
            }
            else
            {
                return SKillMananger.Instance.SkillMaker.FindAndClone(SkillKindID);
            }
        }

        public void Respawn(BaseSkill skill)
        {
            if (skill.KindType == SkillKindID)
            {
                Skills.Push(skill);
                CurCount++;
            }
        }

        public void CleanUnused()
        { 
            while (CurCount > MaxCount)
            {
                Skills.Pop();
                CurCount--;
            }
        }

        public void Prespawn(int count)
        { 
            for (int i = 0; i < count; i++)
            {
                PreSkills.Enqueue(Spawn());
            }

            for (int i = 0; i < count; i++)
            {
                Respawn(PreSkills.Dequeue());
            }
        }
    }
}