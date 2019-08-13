using System;
using System.Collections.Generic;

namespace SimpleAI
{
    class FuzzySetSingleton : FuzzySet
    {
        private float MidPoint;

        private float LeftOffset;

        private float RightOffset;

        public FuzzySetSingleton(float mid,
            float lft,
            float rgt) : base(mid)
        {           
            MidPoint = mid;
            LeftOffset = lft;
            RightOffset = rgt;
        }

        public override float CalculateDOM(float val)
        {
            if (val >= MidPoint - LeftOffset &&
                val <= MidPoint + RightOffset)
            {
                return 1.0f;
            }
            else
            {
                return 0.0f;
            }
        }
    }    
}
