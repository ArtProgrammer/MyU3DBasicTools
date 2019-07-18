using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI;
using SimpleAI.Game;
using SimpleAI.Messaging;

namespace GameContent.SimAgent
{
    public class SimWoodFollowGoal : Goal<SimWood>
    {
        private float Distance = 2.0f;

        private Vector3 TargetPos = Vector3.zero;

        public SimWoodFollowGoal(SimWood p, int type, BaseGameEntity target, float distance) :
            base(p, type)
        {
            Owner.SetTarget(target);
            Distance = distance;
        }

        public override void Activate()
        {
            Status = GoalStatus.Active;
            Owner.GetTarget().GetPosition(ref TargetPos);
            Owner.SetDestination(TargetPos);
            Owner.StartMove();
        }

        public override GoalStatus Process()
        {
            ActiveIfInactive();

            if (Owner.IsTargetLost)
            {
                Status = GoalStatus.Complete;
            }
            else if ((Owner.IsCloseEnough(Owner.GetTarget())))
            {
                Owner.StopMove();
                Status = GoalStatus.Complete;
            }
            else
            {
                Owner.GetTarget().GetPosition(ref TargetPos);
                Owner.SetDestination(TargetPos);
                Status = GoalStatus.Active;
            }

            return Status;
        }

        public override void Terminate()
        {
            Status = GoalStatus.Inactive;
            Owner.StopMove();
        }
    }
}
