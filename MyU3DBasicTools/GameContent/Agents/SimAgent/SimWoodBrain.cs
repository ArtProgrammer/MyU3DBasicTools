using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI;

namespace GameContent.SimAgent
{
    enum SimWoodGoalType
    { 
        GoHome,
        GotFood,
        None
    }

    public class SimWoodBrain : GoalComposite<SimWood>
    {
        private List<GoalEvaluator<SimWood>> Evaluators = 
            new List<GoalEvaluator<SimWood>>();

        private GoalEvaluator<SimWood> MostDesirable = null;

        public SimWoodBrain(SimWood p, int type) :
            base(p, type)
        {
            Evaluators.Add(new SimWoodGoHomEval(1.0f));
            Evaluators.Add(new SimWoodGotFoodEval(1.0f));
        }

        ~SimWoodBrain()
        {
            Evaluators.Clear();
            MostDesirable = null;
        }

        public void AddEvaluator(GoalEvaluator<SimWood> p)
        {
            Evaluators.Add(p);
        }

        public void Arbitrate()
        {
            double best = 0;
            MostDesirable = null;

            for (int i = 0; i < Evaluators.Count; i++)
            {
                double desire = Evaluators[i].CalculateDesirability(Owner);

                if (desire >= best)
                {
                    best = desire;
                    MostDesirable = Evaluators[i];
                }
            }

            if (!System.Object.ReferenceEquals(MostDesirable, null))
                MostDesirable.SetGoal(Owner);
        }

        public override void Activate()
        {
            Arbitrate();

            Status = GoalStatus.Active;
        }

        public override GoalStatus Process()
        {
            ActiveIfInactive();

            GoalStatus substatus = ProcessSubgoals();

            if (substatus == GoalStatus.Complete ||
                substatus == GoalStatus.Failed)
            {
                Status = GoalStatus.Inactive;
            }

            return Status;
        }

        public bool NotPresent(int goaltype)
        {
            if (SubGoals.Count > 0)
            {
                return SubGoals[0].GoalType != goaltype;
            }

            return true;
        }

        public void AddGotFoodGoal()
        {
            //if (NotPresent())
            RemoveAllSubgoals();
            AddSubGoal(new SimWoodGotFoodGoal(Owner, 1));
        }

        public void AddGoHomeGoal()
        {
            RemoveAllSubgoals();
            AddSubGoal(new SimWoodGoHomeGoal(Owner, 2));
        }
    }
}