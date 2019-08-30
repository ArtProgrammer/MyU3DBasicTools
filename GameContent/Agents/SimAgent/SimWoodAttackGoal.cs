using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI;
using SimpleAI.Game;

namespace GameContent.SimAgent
{
    public class SimWoodAttackGoal : GoalComposite<SimWood>
    {
        public SimWoodAttackGoal(SimWood p, int type) :
            base(p, type)
        {
            if (!Owner.TargetSys.IsTargetPresent())
            {
                Status = GoalStatus.Complete;
                return;
            }

            //if (Owner.TargetSys.CanTargetAttack())
            //{
            //    AddSubGoal(new SimWoodHurtGoal(p, type, Owner.Target));
            //}
            //else
            //{
            //    AddSubGoal(new SimWoodFollowGoal(p, type, Owner.Target, Owner.AttackRadius));
            //}

            AddSubGoal(new SimWoodHurtGoal(p, type, Owner.Target));
            AddSubGoal(new SimWoodFollowGoal(p, type, Owner.Target, Owner.AttackRadius));
        }
        
        public override void Activate()
        {
            Status = GoalStatus.Active;
        }

        public override GoalStatus Process()
        {
            ActiveIfInactive();

            GoalStatus subStatus = ProcessSubgoals();

            if (subStatus == GoalStatus.Complete)
            {
                Status = GoalStatus.Complete;
            }

            ReactivateIfFailed();

            return Status;
        }

        protected int curRuningGoalIndex = 0;

        public bool NotPresent(int goaltype)
        {
            if (SubGoals.Count > 0)
            {
                return SubGoals[0].GoalType != goaltype;
            }

            return true;
        }
    }
}