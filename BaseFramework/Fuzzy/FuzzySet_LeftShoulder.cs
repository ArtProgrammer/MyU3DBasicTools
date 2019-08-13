using System;
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
            if (RightOffset.Equals(0.0f) &&
                PeakPoint.Equals(val))) ||
       (isEqual(m_dLeftOffset, 0.0) && (isEqual(m_dPeakPoint, val))))
            {
                return 1.0;
            }

            //find DOM if right of center
            else if ((val >= m_dPeakPoint) && (val < (m_dPeakPoint + m_dRightOffset)))
            {
                double grad = 1.0 / -m_dRightOffset;

                return grad * (val - m_dPeakPoint) + 1.0;
            }

            //find DOM if left of center
            else if ((val < m_dPeakPoint) && (val >= m_dPeakPoint - m_dLeftOffset))
            {
                return 1.0;
            }

            //out of range of this FLV, return zero
            else
            {
                return 0.0;
            }
        }
    }
}
