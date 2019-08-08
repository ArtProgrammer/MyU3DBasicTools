using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameContent.Skill;

namespace SimpleAI.PoolSystem
{
    public class BuffPool
    {
        private Stack<BaseBuff> Buffs =
            new Stack<BaseBuff>();

        public int MaxCount = 18;

        private int CurCount = 0;

        public BuffKindType BuffKind = BuffKindType.None;

        private Queue<BaseBuff> PreSkills = new Queue<BaseBuff>();

        public BaseBuff Spawn()
        {
            if (CurCount > 0)
            {
                CurCount--;
                return Buffs.Pop();
            }
            else
            {
                return SKillMananger.Instance.SkillMaker.FindAndClone(BuffKind);
            }
        }

        public void Respawn(BaseBuff buff)
        {
            if (buff.KindType == BuffKind)
            {
                Buffs.Push(buff);
                CurCount++;
            }
        }

        public void CleanUnused()
        {
            while (CurCount > MaxCount)
            {
                Buffs.Pop();
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