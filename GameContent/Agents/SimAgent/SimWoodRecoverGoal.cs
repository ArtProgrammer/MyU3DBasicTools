using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI;
using SimpleAI.Timer;

namespace GameContent.SimAgent
{
    public class SimWoodRecoverGoal : Goal<SimWood>
    {
        int Count = 0;

        int RecoverType = 0;

        float Rate = 1;

        Regulator Reg = new Regulator(2.0f);

        public SimWoodRecoverGoal(SimWood p, int type) :
            base(p, type)
        { }

        public override void Activate()
        {
            Status = GoalStatus.Active;
        }

        public override GoalStatus Process()
        {
            ActiveIfInactive();

            if (Owner.FoodCount <= 0)
            {
                Status = GoalStatus.Complete;
            } else { 
                if (Reg.IsReady())
                {
                    Owner.FoodCount--;
                }
            }

            return Status;
        }
    }
}