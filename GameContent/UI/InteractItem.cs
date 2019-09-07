using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameContent
{
    public enum InteractItemContainerType
    {
        Bag,
        Shortcut,
        Skill,
        RoleInfo,
    }

    public enum InteractItemType
    {
        Item,
        Skill,
        RoleInfo,
        Shortcut,
        None
    }

    public class InteractItem
    {
        public int GUID;
        public int Index = 0;
        public InteractItemType Kind = InteractItemType.None;
        public int CfgID = 0;
        public int Count = 0;
        public int MaxCount = 1;
        public int IconID;

        public InteractItem()
        {
            MaxCount = 3;
        }
    }
}