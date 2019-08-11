using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;

namespace GameContent
{
    public class TargetSystem
    {
        public List<BaseGameEntity> TargetList =
            new List<BaseGameEntity>();

        private int TargetCount = 0;

        private BaseGameEntity TargetInMind = null;

        public BaseGameEntity CurTarget
        {
            set
            {
                TargetInMind = value;
            }
            get
            {
                return TargetInMind;
            }
        }

        public virtual void Initialize()
        {
            
        }

        public virtual void Process(float dt)
        {

        }

        public virtual BaseGameEntity GetCurTarget()
        {
            if (TargetCount > 0)
            {
                return TargetList[0];
            }

            return null;
        }

        public virtual bool IsTargetPresent()
        {
            return true;
        }

        public virtual bool IsTargetInFOV()
        {
            return false;
        }

        public virtual bool IsTargetAttackable()
        {
            return !System.Object.ReferenceEquals(TargetInMind, null);
        }

        public virtual void ClearTarget()
        {

        }
    }
}
