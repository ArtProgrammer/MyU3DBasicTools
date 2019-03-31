using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Spatial;
using SimpleAI.Logger;
using SimpleAI.Utils;

namespace SimpleAI.Spatial
{
    public class SpatialFruitNode : MonoBehaviour, ISpatialMember
    {
        public int SpatialNodeID
        {
            set; get;
        }

        public Vector3 Position = Vector3.zero;

        private bool IsPosChanged = false;

        public bool IsPosDirty
        {
            set
            {
                IsPosChanged = value;

                if (IsPosChanged)
                {
                    MemberIsDirty();
                }
            }
            get
            {
                return IsPosChanged;
            }
        }

        void Start()
        {
            SpatialNodeID = IDAllocator.Instance.GetID();

            TinyLogger.Instance.DebugLog(string.Format("$ spatial node Start {0}", 
                SpatialNodeID));
            SetPosition(transform.position.x, transform.position.y, transform.position.z);

            RegistSpatial();
        }

        void Update()
        {
            if (transform.hasChanged)
            {
                //TinyLogger.Instance.DebugLog("$ fruit node changed pos!");
                SetPosition(transform.position.x, transform.position.y, transform.position.z);
            }
        }

        //public SpatialFruitNode(float x, float y, float z)
        //{
        //    Position.x = x;
        //    Position.y = y;
        //    Position.z = z;

        //    //TinyLogger.Instance.DebugLog("$ SpatialFruitNode ctor 1");
        //}

        //public SpatialFruitNode()
        //{
        //    //TinyLogger.Instance.DebugLog("$ SpatialFruitNode ctor 2");
        //}

        public void RegistSpatial()
        {
            SpatialManager.Instance.AddNode(this);
        }

        public void SetPosition(float x, float y, float z = 0.0f)
        {
            bool isMoved = !x.Equals(Position.x) || !y.Equals(Position.y);

            Position.x = x;
            Position.y = y;
            Position.z = z;

            IsPosDirty = isMoved;
        }

        public void MemberIsDirty()
        {
            SpatialManager.Instance.HandleNodePosChanged(this);
            IsPosDirty = false;
        }

        public void HandleDestory()
        {
            if (SpatialManager.IsAlive)
            {
                SpatialManager.Instance.RemoveNode(this);
            }
        }
    }
}