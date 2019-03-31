using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI;

namespace GameContent.SimAgent
{
    public class SimWoodGotFoodEval : GoalEvaluator<SimWood>
    {
        public SimWoodGotFoodEval(float bias) : base(bias)
        {

        }

        public override float CalculateDesirability(SimWood p)
        {
            float des = Bias * SimWoodFeature.Instance.Rate2Food(p);

            return des;
        }

        public override void SetGoal(SimWood p)
        {
            if (!System.Object.ReferenceEquals(p, null))
            {
                p.Brain.AddGotFoodGoal();
            }
        }
    }
}