using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI;

namespace GameContent.SimAgent
{
    public class SimWoodMoveToGoal : Goal<SimWood>
    {
        Vector3 TargetPos = Vector3.zero;

        public SimWoodMoveToGoal(SimWood p, int type, Vector3 pos) :
            base(p, type)
        {
            TargetPos = pos;
        }

        public override void Activate()
        {
            Status = GoalStatus.Active;

            Owner.SetDestination(TargetPos);
            Owner.StartMove();
        }

        public override GoalStatus Process()
        {
            ActiveIfInactive();

            if (Owner.IsCloseEnough(ref TargetPos))
            {
                Status = GoalStatus.Complete;
            }
            else
            {
                Status = GoalStatus.Active;
            }

            return Status;
        }
    }
}