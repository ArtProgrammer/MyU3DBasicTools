using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;

namespace GameContent
{
    public class TargetSystem
    {
        public BaseGameEntity Owner = null;

        protected BaseGameEntity TargetInMind = null;

        public TargetSystem(BaseGameEntity owner)
        {
            Owner = owner;
        }

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

        List<BaseGameEntity> PotentialOps = new List<BaseGameEntity>();
        public virtual void Process(float dt)
        {
            float sqrClosestDist = float.MaxValue;

            PotentialOps.Clear();

            CurTarget = null;

            PotentialOps = Owner.SimSensorMem.GetRecentlySensedOpponents();

            for (int i = 0; i < PotentialOps.Count; i++)
            {
                if (PotentialOps[i].IsAlive && 
                    (!System.Object.ReferenceEquals(PotentialOps[i], Owner)))
                {
                    float sqrDist = Vector3.SqrMagnitude(Owner.Position -
                        PotentialOps[i].Position);

                    if (sqrDist < sqrClosestDist)
                    {
                        sqrClosestDist = sqrDist;
                        CurTarget = PotentialOps[i];
                    }
                }
            }
        }

        public virtual bool IsTargetPresent()
        {
            return !System.Object.ReferenceEquals(CurTarget, null);
        }

        public virtual bool IsTargetWithInFOV()
        {
            return Owner.SimSensorMem.IsOpponentWithinFOV(CurTarget);
        }

        public virtual bool IsTargetAttackable()
        {
            return Owner.SimSensorMem.IsOpponentAttackable(CurTarget);
        }

        public virtual bool CanTargetAttack()
        {
            bool isAttackable = false;
            if (Owner.SimSensorMem.IsOpponentAttackable(CurTarget))
            {
                if (CombatHolder.Instance.IsInAttackRange(Owner, CurTarget))
                {
                    isAttackable = true;
                }
            }
            return isAttackable;
        }

        public Vector3 GetLastRecordedPosition()
        {
            return Owner.SimSensorMem.GetLastRecordedPositionOfOpponent(CurTarget);
        }

        public float GetTimeTargetHasBeenVisible()
        {
            return Owner.SimSensorMem.GetTimeOpponentHasBeenVisible(CurTarget);
        }

        public float GetTimeTargetHasBeenOutOfView()
        {
            return Owner.SimSensorMem.GetTimeOpponentHasBeenOutOfView(CurTarget);
        }

        public virtual void ClearTarget()
        {
            CurTarget = null;
        }
    }
}
