using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            float rate = 0.0f;
            //float rate = p.FoodCount / p.FoodNeed;
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
    }
}