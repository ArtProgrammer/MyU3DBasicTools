using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameContent
{
    public class Trigger<T>
    {
        private ITriggerRegion RegionOfInfluence = null;

        public bool RemoveFromGame
        {
            set;get;
        }

        public bool IsActive
        {
            set;get;
        }

        public Trigger()
        {
            RemoveFromGame = false;
            IsActive = true;
        }

        ~Trigger()
        {
            RegionOfInfluence = null;
        }

        public void AddCircleTriggerRegion(Vector3 pos, float radius)
        {
            RegionOfInfluence = new TriggerRegion_Circle(pos, radius);
        }

        public void AddRectangleTrigger(Vector3 pos, Vector3 extends)
        {
            RegionOfInfluence = new TriggerRegion_Rectangle(pos, extends);
        }

        public virtual bool IsTouchingTrigger(Vector3 pos, float radius)
        {
            if (!System.Object.ReferenceEquals(null, RegionOfInfluence))
            {
                return RegionOfInfluence.IsTouching(pos, radius);
            }

            return false;
        }

        public virtual void Try(T target)
        {

        }

        public virtual void Process(float dt)
        {

        }
    }
}
