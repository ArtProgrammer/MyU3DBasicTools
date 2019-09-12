using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using SimpleAI.Supervisors;

namespace GameContent.Skill
{
    public class ChangeSpeedSkill : BaseSkill
    {
        public ChangeSpeedSkill()
        {
            KindType = SkillKindType.ChangeSpeed;
            Range = 20.0f;
        }

        public override BaseSkill Clone()
        {
            return new ChangeSpeedSkill();
        }

        public override void Use(BaseGameEntity target, BaseGameEntity dst = null)
        {
            if (target)
            {
                base.Use(target, dst);
            }
        }
    }
}