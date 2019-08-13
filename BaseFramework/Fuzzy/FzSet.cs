using System;
using System.Collections.Generic;

namespace SimpleAI
{
    class FzSet : FuzzyTerm
    {
        private FuzzySet TheSet;

        public FzSet(FuzzySet fs)
        {
            TheSet = fs;
        }

        public FzSet(FzSet fs)
        {
            TheSet = new FuzzySet(fs.TheSet.GetReprensetativeVal());
        }

        public override FuzzyTerm Clone()
        {
            return new FzSet(this);
        }

        public override float GetDOM()
        {
            return TheSet.GetDOM();
        }

        public override void ClearDOM()
        {
            TheSet.ClearDOM();
        }

        public override void ORwithDOM(float val)
        {
            TheSet.ORwithDOM(val);
        }
    }
}
