using System;
using System.Collections.Generic;

namespace SimpleAI
{
    public enum DefuzzyMethod
    {
        Max_av,
        Centroid
    }

    class FuzzyModule
    {
        private Dictionary<string, FuzzyVariable> Variables =
            new Dictionary<string, FuzzyVariable>();

        private List<FuzzyRule> Rules = new List<FuzzyRule>();

        public int NumSamples = 15;

        ~FuzzyModule()
        {
            Rules.Clear();
            Variables.Clear();
        }

        private void SetConfidenceOfConsequenceToZero()
        {
            for (int i = 0; i < Rules.Count; i++)
            {
                Rules[i].SetConfidenceOfConsequenceToZero();
            }
        }

        public FuzzyVariable CreateFLV(string name)
        {
            Variables[name] = new FuzzyVariable();

            return Variables[name];
        }

        public void AddRule(FuzzyTerm antecedent, FuzzyTerm consequence)
        {
            Rules.Add(new FuzzyRule(antecedent, consequence));
        }

        public void Fuzzify(string nameOfFLV, float val)
        {
            Variables[nameOfFLV].Fuzzify(val);
        }

        public float DeFuzzify(string name,
            DefuzzyMethod method)
        {
            SetConfidenceOfConsequenceToZero();

            for (int i = 0; i < Rules.Count; i++)
            {
                Rules[i].Calculate();
            }

            switch(method)
            {
                case DefuzzyMethod.Centroid:
                    return Variables[name].DeFuzzifyCentroid(NumSamples);
                case DefuzzyMethod.Max_av:
                    return Variables[name].DeFuzzifyMaxAv();
                default:
                    break;
            }

            return 0;
        }
    }
}
