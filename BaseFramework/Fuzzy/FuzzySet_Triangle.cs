using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleAI
{
    class FuzzySet_Triangle : FuzzySet
    {
        private float MidPoint;

        private float RightOffset;

        private float LeftOffset;

        public FuzzySet_Triangle(
            float mid,
            float leftOffset,
            float rightOffset) :
            base(((mid - leftOffset) + mid) * 0.5f)
        {
            MidPoint = mid;
            LeftOffset = leftOffset;
            RightOffset = rightOffset;
        }

        public override float CalculateDOM(float val)
        {
            if (((RightOffset.Equals(0.0f) && MidPoint.Equals(val))) ||
                (LeftOffset.Equals(0.0) && (MidPoint.Equals(val))))
            {
                return 1.0f;
            }

            //find DOM if left of center
            if ((val < MidPoint) && (val >= MidPoint - LeftOffset))
            {
                float grad = 1.0f / LeftOffset;

                return grad * (val - (MidPoint - LeftOffset));
            }

            //find DOM if right of center
            else if ((val >= MidPoint) && (val < (MidPoint + RightOffset)))
            {
                float grad = 1.0f / -RightOffset;
                return grad * (val - MidPoint) + 1.0f;
            }

            //out of range of this FLV, return zero
            else
            {
                return 0.0f;
            }
        }

        public override float GetReprensetativeVal()
        {
            return MidPoint;
        }
    }
}