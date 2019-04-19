using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    private int TheID = 0;

    public int ID
    { 
        set
        {
            TheID = value;
        }
        get
        {
            return TheID;
        }
    }

    private int TheCount = 1;

    public int Count
    { 
        set
        {
            TheCount = value;
        }
        get
        {
            return TheCount;
        }
    }

    private int TheKind = 0;

    public int Kind
    { 
        set
        {
            TheKind = value;
        }
        get
        {
            return TheKind;
        }
    }

    public virtual void Use()
    { 

    }
}

