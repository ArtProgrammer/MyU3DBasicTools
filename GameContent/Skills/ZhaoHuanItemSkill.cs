using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using SimpleAI.Game;
using SimpleAI.Supervisors;

namespace GameContent.Skill
{
    public class ZhaoHuanItemSkill : BaseSkill
    {
        public ZhaoHuanItemSkill()
        {
            KindType = SkillKindType.ZhaoHuanItem;
            Range = 20.0f;
        }

        public override BaseSkill Clone()
        {
            return new ZhaoHuanItemSkill();
        }

        public override void Use(int id, Vector3 pos)
        {
            GlorySupervisor.Instance.SpawnItem(SummonID, pos);
        }
    }
}