using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleAI
{
    class FuzzySet_RightShoulder : FuzzySet
    {
        private float PeakPoint;

        private float RightOffset;

        private float LeftOffset;

        public FuzzySet_RightShoulder(
            float peak,
            float leftOffset,
            float rightOffset) :
            base(((peak - leftOffset) + peak) * 0.5f)
        {
            PeakPoint = peak;
            LeftOffset = leftOffset;
            RightOffset = rightOffset;
        }

        public override float CalculateDOM(float val)
        {
            if (((RightOffset.Equals(0.0f) && PeakPoint.Equals(val))) ||
                (LeftOffset.Equals(0.0) && (PeakPoint.Equals(val))))
            {
                return 1.0f;
            }

            //find DOM if left of center
            else if ((val < PeakPoint) && (val >= PeakPoint - LeftOffset))
            {
                float grad = 1.0f / LeftOffset;

                return grad * (val - (PeakPoint - LeftOffset));
            }

            //find DOM if right of center
            else if ((val >= PeakPoint) && (val < (PeakPoint + RightOffset)))
            {
                return 1.0f;
            }

            //out of range of this FLV, return zero
            else
            {
                return 0.0f;
            }
        }
    }
}