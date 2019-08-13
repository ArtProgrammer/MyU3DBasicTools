using System;
using System.Collections.Generic;

namespace SimpleAI
{
    class FuzzySet
    {
        protected float DOM;

        protected float RepresentativeValue;

        public FuzzySet(float repVal)
        {
            DOM = 0.0f;
            RepresentativeValue = repVal;
        }

        public virtual float CalculateDOM(float val)
        {
            return DOM;
        }

        public virtual void ORwithDOM(float val)
        {
            if (val > DOM)
            {
                DOM = val;
            }
        }

        public virtual float GetReprensetativeVal()
        {
            return RepresentativeValue;
        }

        public virtual float GetDOM()
        {
            return DOM;
        }

        public virtual void ClearDOM()
        {
            DOM = 0.0f;
        }

        public virtual void SetDOM(float val)
        {
            DOM = val;
        }
    }
}
