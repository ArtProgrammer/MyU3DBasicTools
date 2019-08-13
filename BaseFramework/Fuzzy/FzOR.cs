using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAI
{
    class FzOR : FuzzyTerm
    {
        private List<FuzzyTerm> Terms = new List<FuzzyTerm>();        

        public FzOR(FzOR fo)
        {
            for (int i = 0; i < fo.Terms.Count; i++)
            {
                Terms.Add(fo.Terms[i].Clone());
            }
        }

        public FzOR(FuzzyTerm op1, FuzzyTerm op2)
        {

        }

        public FzOR(FuzzyTerm op1, FuzzyTerm op2, FuzzyTerm op3)
        {

        }

        public FzOR(FuzzyTerm op1, FuzzyTerm op2, FuzzyTerm op3, FuzzyTerm op4)
        {

        }

        public override FuzzyTerm Clone()
        {
            return new FzOR(this);
        }

        public override float GetDOM()
        {
            float largest = float.MinValue;

            for (int i = 0; i < Terms.Count; i++)
            {
                if (Terms[i].GetDOM() > largest)
                {
                    largest = Terms[i].GetDOM();
                }
            }

            return largest;
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
