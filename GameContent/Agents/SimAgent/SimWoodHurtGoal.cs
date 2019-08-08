using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI;
using SimpleAI.Game;
using SimpleAI.Messaging;

namespace GameContent.SimAgent
{
    public class SimWoodHurtGoal : Goal<SimWood>
    {
        public SimWoodHurtGoal(SimWood p, int type, BaseGameEntity target) :
            base(p, type)
        {
            Owner.SetTarget(target);
        }

        public override void Activate()
        {
            Status = GoalStatus.Active;
        }

        public override GoalStatus Process()
        {
            //if (Status == GoalStatus.Complete)
            //    return Status;

            ActiveIfInactive();

            if (Owner.IsTargetLost)
            {
                Status = GoalStatus.Complete;
            }
            else if (Owner.IsInAttackRange(Owner.GetTarget()))
            {
                Debug.Log("$ try hurt target");
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
