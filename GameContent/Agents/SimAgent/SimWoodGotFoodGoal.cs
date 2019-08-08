using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI;

namespace GameContent.SimAgent
{
    public class SimWoodGotFoodGoal : GoalComposite<SimWood>
    {
        public SimWoodGotFoodGoal(SimWood p, int type) :
            base(p, type)
        {
            AddSubGoal(new SimWoodCollectItemGoal(p, type, p.FoodNeed - p.FoodCount));
            AddSubGoal(new SimWoodMoveToGoal(p, type, Owner.Food.position));
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