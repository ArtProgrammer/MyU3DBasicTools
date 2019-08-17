using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameContent.UsableItem
{
    public enum UsableCatalog
    { 
        Skill,
        Item
    }        

    public class BaseUsableData
    {
        public int ID;
        public int Count;
        public int IconID;
        public UsableCatalog Catalog;
    }
}