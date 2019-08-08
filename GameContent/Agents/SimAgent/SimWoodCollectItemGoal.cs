using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI;
using SimpleAI.Timer;

namespace GameContent.SimAgent
{
    public class SimWoodCollectItemGoal : Goal<SimWood>
    {
        public Regulator CollectReg = new Regulator(3.0f);

        int ItemCount = 0;

        int CurCount = 0;

        public SimWoodCollectItemGoal(SimWood p, int type, int count) :
            base(p, type)
        {
            ItemCount = count;
        }

        public override void Activate()
        {
            Status = GoalStatus.Active;
        }

        public override GoalStatus Process()
        {
            ActiveIfInactive();

            if (Owner.FoodCount == Owner.FoodNeed)
            {
                Status = GoalStatus.Complete;
            }
            else
            {
                if (CollectReg.IsReady())
                {
                    // handle collection action.
                    Owner.FoodCount++;
                }

                Status = GoalStatus.Active;
            }

            return Status;
        }
    }
}