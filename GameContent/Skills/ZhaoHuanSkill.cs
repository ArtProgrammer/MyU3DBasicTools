using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using SimpleAI.Supervisors;

namespace GameContent.Skill
{
    public class ZhaoHuanSkill : BaseSkill
    {
        public int NPCID = 10001;

        public ZhaoHuanSkill()
        {
            KindType = SkillKindType.ZhaoHuan;
            Range = 20.0f;
        }

        public override BaseSkill Clone()
        {
            return new ZhaoHuanSkill();
        }

        public override void Use(int id, Vector3 pos)
        {
            GlorySupervisor.Instance.SpawnNpc(NPCID, pos);
        }
    }
}