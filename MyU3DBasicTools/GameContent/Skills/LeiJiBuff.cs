using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using SimpleAI.Game;
using SimpleAI.PoolSystem;

using GameContent.UsableItem;

namespace GameContent.Skill
{
    public class LeiJiBuff : BaseBuff
    {
        public LeiJiBuff()
        {
            KindType = BuffKindType.LeiJi;
        }

        public override BaseBuff Clone()
        {
            return new LeiJiBuff();
        }

        public override void Spawned()
        {
            IsActive = true;
        }

        public override void Despawned()
        {
            IsActive = false;
        }

        public override void LoadData()
        {

        }

        public override void Attach(BaseGameEntity target)
        {
            Target = target;
            Target.AddBuff(this);
            OnEnter();
        }

        public override void OnEnter()
        {
            LoadData();
        }

        public override void OnExit()
        {
            //GameLogicSupvisor.Instance.Unregister(this);
            // respawn
        }

        public override void TakeEffect(ref float dt)
        {
            //EffectName;
            // play effect at target. some hit point. 

            //Target.Position;
        }
    }
}