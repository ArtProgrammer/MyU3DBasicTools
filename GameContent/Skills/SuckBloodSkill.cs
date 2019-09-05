using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Game;

namespace GameContent.Skill
{
    public class SuckBloodSkill : BaseSkill
    {
        public SuckBloodSkill()
        {
            KindType = SkillKindType.SuckXue;
            Range = 20.0f;
        }

        public override BaseSkill Clone()
        {
            return new SuckBloodSkill();
        }

        public override void Use(BaseGameEntity target, BaseGameEntity dst = null)
        {
            //SuckBloodBuff sbbuff = new SuckBloodBuff();
            //sbbuff.Attach(target);

            //sbbuff.SetDst(GetOwner());

            // --------------
            base.Use(target, dst);

            //for (int i = 0; i < BuffList.Count; i++)
            //{
            //    BuffList[i].Dst = GetOwner();
            //}
        }
    }
}