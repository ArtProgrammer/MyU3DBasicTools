﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Game;

namespace GameContent.Skill
{
    public class SuckBloodBuff : BaseBuff
    {
        public int Num = 5;

        public int MaxCount = 3;

        public int CurCount = 0;

        public float Iter = 1.0f;

        public SuckBloodBuff()
        {
            KindType = BuffKindType.SuckXue;
        }

        public override BaseBuff Clone()
        {
            return new SuckBloodBuff();
        }

        public override void Attach(BaseGameEntity target)
        {
            base.Attach(target);
            Target.AddBuff(this);
            OnEnter();
        }

        public void SetDst(BaseGameEntity dst)
        {
            Dst = dst;
        }

        public override void LoadData()
        {
            LifeTime = DelayTime + MaxCount * Iter;
        }

        public override void OnExit()
        {
            base.OnExit();

            Target.RemoveBuff(this);
        }

        public override void TakeEffect(ref float dt)
        {
            if (CurCount >= MaxCount) return;

            if (CurTime > DelayTime + CurCount * Iter)
            {
                Target.Xue -= Num;
                CurCount++;

                if (!System.Object.ReferenceEquals(null, Dst))
                {
                    Dst.Xue += Num;
                }
            }
        }
    }
}