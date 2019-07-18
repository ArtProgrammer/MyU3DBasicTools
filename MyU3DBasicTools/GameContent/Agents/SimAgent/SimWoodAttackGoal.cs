using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI;
using SimpleAI.Game;

namespace GameContent.SimAgent
{
    public class SimWoodAttackGoal : GoalComposite<SimWood>
    {
        //private List<GoalEvaluator<SimWood>> Evaluators =
        //    new List<GoalEvaluator<SimWood>>();

        //private GoalEvaluator<SimWood> MostDesirable = null;

        public SimWoodAttackGoal(SimWood p, int type) :
            base(p, type)
        {
            AddSubGoal(new SimWoodHurtGoal(p, type, Owner.GetTarget()));
            AddSubGoal(new SimWoodFollowGoal(p, type, Owner.GetTarget(), Owner.AttackRadius));

            //Evaluators.Add(new SimWoodFollowEval(0.5f));
            //Evaluators.Add(new SimWoodHurtEval(1.0f));
            curRuningGoalIndex = 0;
        }

        ~SimWoodAttackGoal()
        {
            //Evaluators.Clear();
            //MostDesirable = null;
        }

        //public void Arbitrate()
        //{
        //    double best = 0;
        //    MostDesirable = null;

        //    for (int i = 0; i < Evaluators.Count; i++)
        //    {
        //        double desire = Evaluators[i].CalculateDesirability(Owner);

        //        if (desire >= best)
        //        {
        //            best = desire;
        //            MostDesirable = Evaluators[i];
        //        }
        //    }

        //    if (!System.Object.ReferenceEquals(MostDesirable, null))
        //        MostDesirable.SetGoal(Owner);
        //}

        public override void Activate()
        {
            //Arbitrate();
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

        protected int curRuningGoalIndex = 0;
        //protected override GoalStatus ProcessSubgoals()
        //{
        //    while (SubGoals.Count > curRuningGoalIndex)
        //    {
        //        Goal<SimWood> firstgoal = SubGoals[curRuningGoalIndex];
        //        if (firstgoal.IsComplete || firstgoal.HasFailed)
        //        {
        //            firstgoal.Terminate();
        //            //curRuningGoalIndex += 1;
        //            //SubGoals.Remove(firstgoal);
        //        }
        //        else
        //        {
        //            break;
        //        }
        //    }

        //    if (SubGoals.Count > curRuningGoalIndex)
        //    {
        //        GoalStatus status = SubGoals[curRuningGoalIndex].Process();

        //        if (status == GoalStatus.Complete && SubGoals.Count > 1)
        //        {
        //            return GoalStatus.Active;
        //        }

        //        return status;
        //    }
        //    else
        //    {
        //        return GoalStatus.Complete;
        //    }
        //}

        public bool NotPresent(int goaltype)
        {
            if (SubGoals.Count > 0)
            {
                return SubGoals[0].GoalType != goaltype;
            }

            return true;
        }

        //public void AddFollowGoal()
        //{
        //    if (NotPresent((int)SimWoodGoalType.Follow))
        //    {
        //        RemoveAllSubgoals();
        //        AddSubGoal(new SimWoodFollowGoal(Owner, (int)SimWoodGoalType.GotFood, Owner.GetTarget()));
        //    }
        //}

        //public void AddHurtGoal()
        //{
        //    if (NotPresent((int)SimWoodGoalType.Hurt))
        //    {
        //        RemoveAllSubgoals();
        //        AddSubGoal(new SimWoodHurtGoal(Owner, (int)SimWoodGoalType.GotFood, Owner.GetTarget()));
        //    }
        //}
    }
}