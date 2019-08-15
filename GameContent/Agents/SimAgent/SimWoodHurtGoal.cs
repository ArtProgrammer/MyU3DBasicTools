using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI;
using SimpleAI.Game;
using SimpleAI.Messaging;

using GameContent;

namespace GameContent.SimAgent
{
    public class SimWoodHurtGoal : Goal<SimWood>
    {
        public SimWoodHurtGoal(SimWood p, int type, BaseGameEntity target) :
            base(p, type)
        {
            Owner.Target = target;
        }

        public override void Activate()
        {
            Status = GoalStatus.Active;
        }

        public override GoalStatus Process()
        {
            ActiveIfInactive();

            if (Owner.IsTargetLost)
            {
                Status = GoalStatus.Complete;
            }
            else if (CombatHolder.Instance.IsInAttackRange(Owner, Owner.Target))
            {
                Debug.Log("$ try hurt target");
                Owner.UseWeapon();
                Status = GoalStatus.Complete;
            }
            else
            {
                Status = GoalStatus.Failed;
            }

            return Status;
        }

        public override void Terminate()
        {
            Status = GoalStatus.Inactive;
        }

        public override bool HandleMessage(Telegram msg)
        {
            // if target dead.
            // 
            return true;
        }
    }
}
