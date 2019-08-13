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
            //if (p.FoodCount <= 0)
            //rate = 1.0f;

            return rate;
        }

        public float Rate2Home(SimWood p)
        {
            float rate = 0.0f;

            if (p.FoodCount >= p.FoodNeed)
                rate = 1.0f;

            return rate;
        }

        public float Rate2Attack(SimWood p)
        {
            float rate = 0.0f;

            //if (p.FoodCount <= p.FoodNeed * 0.5)
            //{
            //    rate = 0.5f;
            //}

            if (p.FoodCount >= p.FoodNeed)
            {
                rate = (float)p.FoodCount / p.FoodNeed;
            }            

            return rate;
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