using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using SimpleAI.Logger;
using GameContent.Skill;

namespace SimpleAI.PoolSystem
{
    public class BuffPoolManager
    {
        private Dictionary<BuffKindType, BuffPool> BuffPools =
            new Dictionary<BuffKindType, BuffPool>();

        private List<BuffKindType> KindIDList =
            new List<BuffKindType>();

        public BaseBuff Spawn(BuffKindType kindid)
        {
            if (!BuffPools.ContainsKey(kindid))
            {
                BuffPool sp = new BuffPool();
                sp.BuffKind = kindid;
                BuffPools.Add(kindid, sp);
            }

            if (!KindIDList.Contains(kindid))
            {
                KindIDList.Add(kindid);
            }

            return BuffPools[kindid].Spawn();
        }

        public void Respawn(BaseBuff buff)
        {
            if (BuffPools.ContainsKey(buff.KindType))
            {
                BuffPools[buff.KindType].Respawn(buff);
            }
            else
            {
                TinyLogger.Instance.DebugLog("$ can not respawn skill");
            }
        }

        public void SetMaxCount(BuffKindType kindid, int count)
        {
            if (BuffPools.ContainsKey(kindid))
            {
                BuffPools[kindid].MaxCount = count;
            }
        }

        public void CleanALlUnused()
        {
            for (int i = 0; i < KindIDList.Count; i++)
            {
                BuffPools[KindIDList[i]].CleanUnused();
            }
        }

        public void Prespawn(BuffKindType kindid, int count)
        {
            if (!BuffPools.ContainsKey(kindid))
            {
                BuffPool sp = new BuffPool();
                sp.BuffKind = kindid;
                BuffPools.Add(kindid, sp);
            }

            if (!KindIDList.Contains(kindid))
            {
                KindIDList.Add(kindid);
            }

            BuffPools[kindid].Prespawn(count);
        }

        public void FinalClean()
        {
            CleanALlUnused();

            // clean every pool.

            KindIDList.Clear();
        }
    }
}