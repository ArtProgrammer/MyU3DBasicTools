using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleAI
{
    class FuzzyVariable
    {
        private Dictionary<string, FuzzySet> MemberSets = 
            new Dictionary<string, FuzzySet>();

        private float MinRange;

        private float MaxRange;

        private void AdjustRangeToFit(float min, float max)
        {
            if(min < MinRange) MinRange = min;
            if (max > MaxRange) MaxRange = max;
        }

        public FuzzyVariable()
        {
            MinRange = MaxRange = 0.0f;
        }

        ~FuzzyVariable()
        {
            foreach (var item in MemberSets)
            {
                item.Value.ClearDOM();
            }

            MemberSets.Clear();
        }

        FzSet AddLeftShoulderSet(string name, float min,
            float peak, float max)
        {
            //MemberSets.Add(name, new );
            AdjustRangeToFit(min, max);

            return new FzSet(MemberSets[name]);
        }

        FzSet AddRightShoulderSet(string name, float min,
            float peak, float max)
        {
            //MemberSets.Add(name, new );
            AdjustRangeToFit(min, max);

            return new FzSet(MemberSets[name]);
        }

        FzSet AddTriangleSet(string name, float min,
            float peak, float max)
        {
            //MemberSets.Add(name, new );
            AdjustRangeToFit(min, max);

            return new FzSet(MemberSets[name]);
        }

        FzSet AddSingletonSet(string name, float min,
            float peak, float max)
        {
            //MemberSets.Add(name, new );
            AdjustRangeToFit(min, max);

            return new FzSet(MemberSets[name]);
        }

        public void Fuzzify(float val)
        {
            if (val >= MinRange && val <= MaxRange)
            {
                foreach (var item in MemberSets)
                {
                    item.Value.SetDOM(item.Value.CalculateDOM(val));
                }
            }
        }

        public float DeFuzzifyMaxAv()
        {
            float bottom = 0.0f;
            float top = 0.0f;

            foreach (var item in MemberSets)
            {
                bottom += item.Value.GetDOM();
                top += item.Value.GetReprensetativeVal() * item.Value.GetDOM();
            }

            if (bottom.Equals(0.0f))
            {
                return 0.0f;
            }

            return top / bottom;
        }

        public float DeFuzzifyCentroid(int numSamples)
        {
            float stepSize = (MaxRange - MinRange) / numSamples;

            float totalArea = 0.0f;
            float sumOfMoments = 0.0f;

            for (int i = 0; i <= numSamples; ++i)
            {
                foreach (var item in MemberSets)
                {
                    float contribution = Mathf.Min(
                        item.Value.CalculateDOM(MinRange + i * stepSize),
                        item.Value.GetDOM());

                    totalArea += contribution;

                    sumOfMoments += (MinRange + i * stepSize) * contribution;
                }
            }

            if (totalArea.Equals(0.0f))
            {
                return 0.0f;
            }

            return sumOfMoments / totalArea;
        }
    }
}
