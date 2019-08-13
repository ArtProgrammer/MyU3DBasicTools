using System;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleAI
{
    /// <summary>
    /// 
    /// </summary>
    class FzVery : FuzzyTerm
    {
        FuzzySet TheSet;

        public FzVery(FzVery fv)
        {
            TheSet = fv.TheSet;
        }

        public override FuzzyTerm Clone()
        {
            return new FzVery(this);
        }

        public override float GetDOM()
        {
            return TheSet.GetDOM() * TheSet.GetDOM();
        }

        public override void ClearDOM()
        {
            TheSet.ClearDOM();
        }

        public override void ORwithDOM(float val)
        {
            TheSet.ORwithDOM(val * val);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    class FzFairly : FuzzyTerm
    {
        FuzzySet TheSet;

        public FzFairly(FzFairly fv)
        {
            TheSet = fv.TheSet;
        }

        public override FuzzyTerm Clone()
        {
            return new FzFairly(this);
        }

        public override float GetDOM()
        {
            return Mathf.Sqrt(TheSet.GetDOM());
        }

        public override void ClearDOM()
        {
            TheSet.ClearDOM();
        }

        public override void ORwithDOM(float val)
        {
            TheSet.ORwithDOM(Mathf.Sqrt(val));
        }
    }
}
