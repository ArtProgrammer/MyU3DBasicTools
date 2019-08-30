using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameContent;

namespace GameContent.SimAgent
{
    public sealed class SimWoodFeature
    {
        public static readonly SimWoodFeature TheInstance = new SimWoodFeature();

        private SimWoodFeature() { }

        static SimWoodFeature() { }

        public static SimWoodFeature Instance
        { 
            get
            {
                return TheInstance;
            }
        }

        public float Rate2Food(SimWood p)
        {
            //float rate = 0.0f;
            float rate = 1.0f - p.FoodCount / p.FoodNeed;

            if (p.FoodCount <= 0)
                rate = 1.0f;

            return rate;
        }

        public float Rate2Home(SimWood p)
        {
            float rate = 0.0f;

            if (p.FoodCount >= p.FoodNeed)
                rate = 1.0f;

            return rate;
        }

        private float RateToAttack = 0.0f;

        public float Rate2Attack(SimWood p)
        {
            if (p.FoodCount >= p.FoodNeed)
            {
                RateToAttack = 1.0f;
            }

            if (p.FoodCount <= 1)
            {
                RateToAttack = 0.0f;
            }

            return RateToAttack;
        }

        public float Rate2Hurt(SimWood p)
        {
            float rate = 0.0f;
            if (CombatHolder.Instance.IsInAttackRange(p, p.Target))
            {
                rate = 1.0f;
            }

            return rate;
        }

        public float Rate2Follow(SimWood p)
        {
            float rate = 0.0f;
            if (CombatHolder.Instance.IsCloseEnough(p, p.Target))
            {
                rate = 1.0f;
            }

            return rate;
        }
    }
}