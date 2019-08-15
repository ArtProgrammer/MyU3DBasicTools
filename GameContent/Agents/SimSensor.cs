using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using SimpleAI.Spatial;
using SimpleAI.Timer;
using GameContent.Agents;
using GameContent.SimAgent;
using GameContent.Defence;

namespace GameContent.Agents
{
    public class MemoryRecord
    {
        public bool WithinFOV = false;

        public bool Attackable = false;

        public float TimeLastSensed = -1;

        public float TimeBecameVisible = -1;

        public float TimeLastVisible = 0.0f;

        public Vector3 LastSensedPosition = Vector3.zero;        

        public MemoryRecord()
        {
        }
    }

    public class SimSensor<T> where T : BaseGameEntity
    {
        private Dictionary<T, MemoryRecord> Memories =
            new Dictionary<T, MemoryRecord>();

        public float SearchRange = 16.0f;

        private T Owner = null;

        public float Range
        {
            set
            {
                if (SearchRange.CompareTo(value) != 0)
                {
                    UpdateBoundsSize(value);
                    SearchRange = value;
                }
            }
            get
            {
                return SearchRange;
            }
        }

        public SimSensor(T owner) : base()
        {
            Owner = owner;
        }

        private List<SpatialFruitNode> PotentialTargets = 
            new List<SpatialFruitNode>();

        public Bounds SearchBound;

        private Vector3 BoundSize = Vector3.one;
        
        private Vector3 SelfPos = Vector3.zero;

        private float MemorySpan;

        private void UpdateBoundsSize(float range)
        {
            BoundSize.x = range;
            BoundSize.y = range;
            BoundSize.z = range;

            SearchBound.size = BoundSize;
        }

        private void MakeNewRecordIfNotPresent(T target)
        {
            if (!Memories.ContainsKey(target))
            {
                Memories.Add(target, new MemoryRecord());
            }
        }
        
        public void RemoveFromMemory(T target)
        {
            if (Memories.ContainsKey(target))
            {
                Memories.Remove(target);
            }
        }

        public void AddNewResource(T target)
        {
            if (!System.Object.ReferenceEquals(target, null) &&
                !System.Object.ReferenceEquals(target, Owner))
            {
                MakeNewRecordIfNotPresent(target);

                MemoryRecord mr = Memories[target];

                // if target is attackable.
                if (CombatHolder.Instance.IsLOSOkay(target.Position, 
                    Owner.Position))                
                {
                    mr.Attackable = true;
                    mr.LastSensedPosition = target.Position;
                }
                else
                {
                    mr.Attackable = false;
                }
                
                mr.TimeLastSensed = TimeWrapper.Instance.realtimeSinceStartup;
            }
        }

        public bool IsOpponentAttackable(T op)
        {
            if (Memories.ContainsKey(op))
            {
                return Memories[op].Attackable;
            }
            return false;
        }

        public bool IsOpponentWithinFOV(T op)
        {
            if (Memories.ContainsKey(op))
            {
                return Memories[op].WithinFOV;
            }
            return false;
        }

        public Vector3 GetLastRecordedPositionOfOpponent(T op)
        {
            if (Memories.ContainsKey(op))
            {
                return Memories[op].LastSensedPosition;
            }

            return Vector3.zero;
        }

        public void GetLastRecordedPositionOfOpponent(T op, ref Vector3 pos)
        {
            if (Memories.ContainsKey(op))
            {
                pos = Memories[op].LastSensedPosition;
            }
        }

        public float GetTimeOpponentHasBeenVisible(T op)
        {
            if (Memories.ContainsKey(op))
            {
                if (Memories[op].WithinFOV)
                {
                    return TimeWrapper.Instance.realtimeSinceStartup - 
                        Memories[op].TimeBecameVisible;
                }
            }
            return 0.0f;
        }

        public float GetTimeSinceLastSensed(T op)
        {
            if (Memories.ContainsKey(op))
            {
                if (Memories[op].WithinFOV)
                {
                    return TimeWrapper.Instance.realtimeSinceStartup -
                        Memories[op].TimeLastSensed;
                }
            }
            return 0.0f;
        }

        public float GetTimeOpponentHasBeenOutOfView(T op)
        {
            if (Memories.ContainsKey(op))
            {
                return TimeWrapper.Instance.realtimeSinceStartup -
                    Memories[op].TimeLastVisible;
            }
            return 0.0f;
        }

        List<T> RecentlySensedOpponents = new List<T>();

        public List<T> GetRecentlySensedOpponents()
        {
            RecentlySensedOpponents.Clear();

            float curTime = TimeWrapper.Instance.realtimeSinceStartup;

            foreach (var item in Memories)
            {
                if ((curTime - item.Value.TimeLastSensed) <= MemorySpan)
                {
                    RecentlySensedOpponents.Add(item.Key);
                }
            }

            return RecentlySensedOpponents;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Initialize()
        {
            UpdateBoundsSize(SearchRange);

            SearchBound.size = BoundSize;

            Owner.GetPosition(ref SelfPos);
            SearchBound.center = SelfPos;
        }

        public virtual void Process(float dt)
        {
            UpdateWithinRange();
        }

        public virtual void UpdateWithSrc(BaseGameEntity p)
        { 

        }        

        //public BaseGameEntity CurTarget = null;

        protected virtual void UpdateWithinRange()
        {
            PotentialTargets.Clear();
            Owner.GetPosition(ref SelfPos);
            SearchBound.center = SelfPos;
            
            SpatialManager.Instance.QueryRange(ref SearchBound, PotentialTargets);

            for (int i = 0; i < PotentialTargets.Count; ++i)
            {
                T item = (T)PotentialTargets[i];
                if (!System.Object.ReferenceEquals(item, null) &&
                    !System.Object.ReferenceEquals(item, Owner))
                {
                    //if (DefenceSystem.Instance.IsEnemyRace(Owner.RaceSignal,
                    //    item.RaceSignal))
                    //{
                    //    CurTarget = item;
                    //}

                    MakeNewRecordIfNotPresent(item);

                    MemoryRecord mr = Memories[item];

                    // out method.
                    if (CombatHolder.Instance.IsLOSOkay(item.Position, 
                        Owner.Position))
                    {
                        mr.Attackable = true;

                        if (CombatHolder.Instance.IsSecondInFOVOfFirst(
                            Owner.Position, Owner.Facing, 
                            item.Position, Owner.FOV))
                        {
                            mr.TimeLastSensed = TimeWrapper.Instance.realtimeSinceStartup;
                            mr.LastSensedPosition = item.Position;
                            mr.TimeLastVisible = TimeWrapper.Instance.realtimeSinceStartup;

                            if (!mr.WithinFOV)
                            {
                                mr.WithinFOV = true;
                                mr.TimeBecameVisible = mr.TimeLastSensed;
                            }
                        }
                        else
                        {
                            mr.WithinFOV = false;
                        }
                    }
                    else
                    {
                        mr.Attackable = false;
                        mr.WithinFOV = false;
                    }
                }
            }
        }
    }
}