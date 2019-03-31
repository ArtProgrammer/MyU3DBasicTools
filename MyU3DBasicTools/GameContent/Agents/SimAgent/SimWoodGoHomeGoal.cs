using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI;

namespace GameContent.SimAgent
{
    public class SimWoodGoHomeGoal : GoalComposite<SimWood>
    {
        public SimWoodGoHomeGoal(SimWood p, int type) : 
            base(p, type)
        {
            AddSubGoal(new SimWoodRecoverGoal(p, type));
            AddSubGoal(new SimWoodMoveToGoal(p, type, Owner.Home.position));
        }

        public override void Activate()
        {
            Status = GoalStatus.Active;
        }

        public override GoalStatus Process()
        {
            ActiveIfInactive();

            GoalStatus subStatus = ProcessSubgoals();

            if (subStatus == GoalStatus.Complete ||
                subStatus == GoalStatus.Failed)
            {
                Status = GoalStatus.Complete;
            }

            return Status;
        }
    }
}