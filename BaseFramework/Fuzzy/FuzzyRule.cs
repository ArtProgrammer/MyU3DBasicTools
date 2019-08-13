using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAI
{
    class FuzzyRule
    {
        private FuzzyTerm Antecedent;

        private FuzzyTerm Consequence;

        FuzzyRule(FuzzyRule fr)
        {
            Antecedent = fr.Antecedent.Clone();
            Consequence = fr.Consequence.Clone();
        }

        public FuzzyRule(FuzzyTerm ant,
            FuzzyTerm con)
        {
            Antecedent = ant.Clone();
            Consequence = con.Clone();
        }

        public void SetConfidenceOfConsequenceToZero()
        {
            Consequence.ClearDOM();
        }

        public void Calculate()
        {
            Consequence.ORwithDOM(Antecedent.GetDOM());
        }
    }
}
