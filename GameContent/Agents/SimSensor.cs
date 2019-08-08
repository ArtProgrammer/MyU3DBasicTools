using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using SimpleAI.Spatial;
using GameContent.Agents;
using GameContent.SimAgent;
using GameContent.Defence;

namespace GameContent.Agents
{
    public class SimSensor<T> where T : BaseGameEntity
    {
        public float SearchRange = 16.0f;

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

        public SimSensor(T owner)
        {
            Owner = owner;
        }

        private List<SpatialFruitNode> Targets = 
            new List<SpatialFruitNode>();

        public Bounds SearchBound;

        private Vector3 BoundSize = Vector3.one;

        private T Owner = null;

        private Vector3 SelfPos = Vector3.zero;

        private void UpdateBoundsSize(float range)
        {
            BoundSize.x = range;
            BoundSize.y = range;
            BoundSize.z = range;

            SearchBound.size = BoundSize;
        }

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

        private List<MeshRenderer> TargetList = new List<MeshRenderer>();

        //public List<BaseGameEntity> Targets = new List<BaseGameEntity>();

        public BaseGameEntity CurTarget = null;

        public virtual void UpdateWithinRange()
        {
            Targets.Clear();
            Owner.GetPosition(ref SelfPos);
            SearchBound.center = SelfPos;

            for (int i = 0; i < TargetList.Count; ++i)
            {
                TargetList[i].enabled = true;
            }

            TargetList.Clear();

            SpatialManager.Instance.QueryRange(ref SearchBound, Targets);

            for (int i = 0; i < Targets.Count; ++i)
            {
                T item = (T)Targets[i];
                if (!System.Object.ReferenceEquals(item, null) &&
                    !System.Object.ReferenceEquals(item, Owner))
                {
                    if (DefenceSystem.Instance.IsEnemyRace(Owner.RaceSignal,
                        item.RaceSignal))
                    {
                        //item.transform.position =
                        //new Vector3(item.transform.position.x,
                        //10,
                        //item.transform.position.z);

                        //MeshRenderer rdr = item.GetComponent<MeshRenderer>();
                        //if (rdr != null)
                        //{
                        //    TargetList.Add(rdr);
                        //    rdr.enabled = false;
                        //}

                        CurTarget = item;
                    }

                }
            }
        }
    }
}