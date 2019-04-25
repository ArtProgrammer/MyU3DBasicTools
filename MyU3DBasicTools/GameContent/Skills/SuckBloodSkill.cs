﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Game;

namespace GameContent.Skill
{
    public class SuckBloodSkill : BaseSkill
    {
        public SuckBloodSkill()
        {
            Range = 20.0f;
        }

        public override void Use(BaseGameEntity target)
        {
            SuckBloodBuff sbbuff = new SuckBloodBuff();
            sbbuff.Attach(target);

            sbbuff.SetDst(GetOwner());
        }
    }
}