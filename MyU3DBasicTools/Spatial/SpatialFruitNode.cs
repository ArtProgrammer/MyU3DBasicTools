using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Spatial;
using SimpleAI.Logger;

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
        //TinyLogger.Instance.DebugLog("$ spatial node Start");
        SetPosition(transform.position.x, transform.position.z);

        RegistSpatial();
    }

    void Update()
    {
        if (transform.hasChanged)
        {
            SetPosition(transform.position.x, transform.position.z);
        }
    }

    public SpatialFruitNode(float x, float y, float z)
    {
        Position.x = x;
        Position.y = y;
        Position.z = z;

        //TinyLogger.Instance.DebugLog("$ SpatialFruitNode ctor 1");
    }

    public SpatialFruitNode()
    {
        //TinyLogger.Instance.DebugLog("$ SpatialFruitNode ctor 2");
    }

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

    }
}
