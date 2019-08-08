using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI;

namespace GameContent.SimAgent
{
    public class SimWoodGoHomEval : GoalEvaluator<SimWood>
    {
        public SimWoodGoHomEval(float bias) : base (bias)
        { 

        }

        public override float CalculateDesirability(SimWood p)
        {
            float des = Bias * SimWoodFeature.TheInstance.Rate2Home(p);

            return des;
        }

        public override void SetGoal(SimWood p)
        {
            if (!System.Object.ReferenceEquals(p, null))
            {
                p.Brain.AddGoHomeGoal();
            }
        }
    }
}