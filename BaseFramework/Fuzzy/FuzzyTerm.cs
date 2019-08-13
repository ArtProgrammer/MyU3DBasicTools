using System;

namespace SimpleAI
{
    class FuzzyTerm
    {
        public virtual FuzzyTerm Clone()
        {
            return new FuzzyTerm();
        }

        public virtual float GetDOM()
        {
            return 0.0f;
        }

        public virtual void ClearDOM()
        {

        }

        public virtual void ORwithDOM(float val)
        {

        }
    }
}
