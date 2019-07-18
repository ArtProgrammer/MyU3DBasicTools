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
        Follow,
        Attack,
        Flee,
        Hurt,
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
            Evaluators.Add(new SimWoodGoHomEval(.50f));
            Evaluators.Add(new SimWoodGotFoodEval(0.5f));
            Evaluators.Add(new SimWoodAttackEval(1.0f));
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
            if (NotPresent((int)SimWoodGoalType.GotFood))
            {
                RemoveAllSubgoals();
                AddSubGoal(new SimWoodGotFoodGoal(Owner, (int)SimWoodGoalType.GotFood));
            }                
        }

        public void AddGoHomeGoal()
        {
            if (NotPresent((int)SimWoodGoalType.GoHome))
            {
                RemoveAllSubgoals();
                AddSubGoal(new SimWoodGoHomeGoal(Owner, (int)SimWoodGoalType.GoHome));
            }                
        }

        public void AddAttackGoal()
        {
            if (NotPresent((int)SimWoodGoalType.Attack))
            {
                Debug.Log("$Add attack goal");
                RemoveAllSubgoals();
                AddSubGoal(new SimWoodAttackGoal(Owner, (int)SimWoodGoalType.Attack));
            }            
        }
    }
}
