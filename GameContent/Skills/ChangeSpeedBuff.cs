using System.Collections;
using System.Collections.Generic;
using UnityEngine;



using SimpleAI.Game;
using SimpleAI.PoolSystem;

using GameContent.SimAgent;
using GameContent.UsableItem;

namespace GameContent.Skill
{
    public class ChangeSpeedBuff : BaseBuff
    {        
        public ChangeSpeedBuff()
        {
            KindType = BuffKindType.ChangeSpeed;
        }

        public override BaseBuff Clone()
        {
            return new ChangeSpeedBuff();
        }

        public override void Spawned()
        {
            base.Spawned();
        }

        public override void Despawned()
        {
            base.Despawned();
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

            if (Target)
            {
                Target.MoveSpeed += TheValue;

                var rolemove = Target.GetComponent<RoleMovement>();
                if (!System.Object.ReferenceEquals(null, rolemove))
                {
                    rolemove.MoveSpeed += TheValue;
                }
            }
        }

        public override void OnExit()
        {
            //GameLogicSupvisor.Instance.Unregister(this);
            // respawn

            if (Target)
            {
                Target.MoveSpeed -= TheValue;

                var rolemove = Target.GetComponent<RoleMovement>();
                if (!System.Object.ReferenceEquals(null, rolemove))
                {
                    rolemove.MoveSpeed -= TheValue;
                }
            }

            base.OnExit();
        }

        public override void TakeEffect(ref float dt)
        {
            //EffectName;
            // play effect at target. some hit point. 

            //Target.Position;
        }
    }
}