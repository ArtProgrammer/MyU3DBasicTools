using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using SimpleAI.Game;
using SimpleAI.PoolSystem;

using GameContent.UsableItem;

namespace GameContent.Skill
{
    public class TianLeiBuff : BaseBuff
    {
        public TianLeiBuff()
        {
            KindType = BuffKindType.TianLei;
        }

        public override BaseBuff Clone()
        {
            return new TianLeiBuff();
        }

        public override void LoadData()
        {

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

        public override void OnEnter()
        {
            LoadData();
            Spawned();
        }

        public override void OnExit()
        {
            //GameLogicSupvisor.Instance.Unregister(this);
            // respawn
            Despawned();
        }

        public override void TakeEffect(ref float dt)
        {
            //Target.Xue -= 1000;
            var pos = Target.transform.position;

            Target.transform.position = new Vector3(pos.x, pos.y + 5.0f, pos.z);

            if (!System.Object.ReferenceEquals(null, EffectName))
            {
                // play the effect at position.
                //Target.Position;
            }
        }
    }
}