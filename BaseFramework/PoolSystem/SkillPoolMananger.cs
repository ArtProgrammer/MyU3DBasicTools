using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Logger;
using GameContent.Skill;

namespace SimpleAI.PoolSystem
{
    public class SkillPoolMananger
    {
        private Dictionary<SkillKindType, SkillPool> SkillPools =
            new Dictionary<SkillKindType, SkillPool>();

        private List<SkillKindType> KindIDList =
            new List<SkillKindType>();

        public BaseSkill Spawn(SkillKindType kindid)
        { 
            if (!SkillPools.ContainsKey(kindid))
            {
                SkillPool sp = new SkillPool();
                sp.SkillKindID = kindid;
                SkillPools.Add(kindid, sp);
            }

            if (!KindIDList.Contains(kindid))
            {
                KindIDList.Add(kindid);
            }

            return SkillPools[kindid].Spawn();
        }

        public void Respawn(BaseSkill skill)
        { 
            if (SkillPools.ContainsKey(skill.KindType))
            {
                SkillPools[skill.KindType].Respawn(skill);
            }
            else
            {
                TinyLogger.Instance.DebugLog("$ can not respawn skill");
            }
        }

        public void SetMaxCount(SkillKindType kindid, int count)
        { 
            if (SkillPools.ContainsKey(kindid))
            {
                SkillPools[kindid].MaxCount = count;
            }
        }

        public void CleanALlUnused()
        { 
            for (int i = 0; i < KindIDList.Count; i++)
            {
                SkillPools[KindIDList[i]].CleanUnused();
            }
        }

        public void Prespawn(SkillKindType kindid, int count)
        {
            if (!SkillPools.ContainsKey(kindid))
            {
                SkillPool sp = new SkillPool();
                sp.SkillKindID = kindid;
                SkillPools.Add(kindid, sp);
            }

            if (!KindIDList.Contains(kindid))
            {
                KindIDList.Add(kindid);
            }

            SkillPools[kindid].Prespawn(count);
        }
    }
}