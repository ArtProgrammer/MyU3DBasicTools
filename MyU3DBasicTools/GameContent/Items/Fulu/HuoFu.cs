using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using GameContent.Skill;
using GameContent.UsableItem;

namespace GameContent.Item
{
    public class HuoFu : BaseFulu
    {
        public int SkillID = 10003;

        // 
        public int Cost = 0;

        //
        public int Energy = 100;

        public HuoFu()
        {
            Kind = ItemKind.HuoFu;
            TargetType = ItemTargetType.TargetBody;
        }

        public override BaseItem Clone()
        {
            return new HuoFu();
        }

        public override void Use(BaseGameEntity target)
        {
            if (SkillID != 0)
            {
                SKillMananger.Instance.TryUseSkill(SkillID, target, null);
            }
        }
    }
}