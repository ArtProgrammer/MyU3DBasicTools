using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpatialMember
{
    int SpatialNodeID
    {
        set; get;
    }

    void MemberIsDirty();

    void HandleDestory();
}
