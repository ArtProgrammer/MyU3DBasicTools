﻿using System;
using System.Collections.Generic;

namespace SimpleAI
{
    class FuzzySet_LeftShoulder : FuzzySet
    {
        private float PeakPoint;

        private float RightOffset;

        private float LeftOffset;

        public FuzzySet_LeftShoulder(
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

            //find DOM if right of center
            else if ((val >= PeakPoint) && (val < (PeakPoint + RightOffset)))
            {
                float grad = 1.0f / -RightOffset;

                return grad * (val - PeakPoint) + 1.0f;
            }

            //find DOM if left of center
            else if ((val < PeakPoint) && (val >= PeakPoint - LeftOffset))
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
