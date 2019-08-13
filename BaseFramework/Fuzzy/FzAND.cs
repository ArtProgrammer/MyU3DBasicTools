using System;
using System.Collections.Generic;

namespace SimpleAI
{
    class FzAND : FuzzyTerm
    {
        private List<FuzzyTerm> Terms = new List<FuzzyTerm>();

        public FzAND(FzAND fa)
        {
            for (int i = 0; i < fa.Terms.Count; i++)
            {
                Terms.Add(fa.Terms[i].Clone());
            }
        }

        ~FzAND()
        {
            Terms.Clear();
        }

        public FzAND(FuzzyTerm op1, FuzzyTerm op2)
        {
            Terms.Add(op1.Clone());
            Terms.Add(op2.Clone());
        }

        public FzAND(FuzzyTerm op1, FuzzyTerm op2, FuzzyTerm op3)
        {
            Terms.Add(op1.Clone());
            Terms.Add(op2.Clone());
            Terms.Add(op3.Clone());
        }

        public FzAND(FuzzyTerm op1, FuzzyTerm op2, FuzzyTerm op3, FuzzyTerm op4)
        {
            Terms.Add(op1.Clone());
            Terms.Add(op2.Clone());
            Terms.Add(op3.Clone());
            Terms.Add(op4.Clone());
        }

        public override FuzzyTerm Clone()
        {
            return new FzAND(this);
        }

        public override float GetDOM()
        {
            float smallest = float.MaxValue;

            for (int i = 0; i < Terms.Count; i++)
            {
                if (Terms[i].GetDOM() < smallest)
                {
                    smallest = Terms[i].GetDOM();
                }
            }

            return smallest;
        }

        public override void ClearDOM()
        {
            for (int i = 0; i < Terms.Count; i++)
            {
                Terms[i].ClearDOM();
            }
        }

        public override void ORwithDOM(float val)
        {
            for (int i = 0; i < Terms.Count; i++)
            {
                Terms[i].ORwithDOM(val);
            }
        }
    }
}
