using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameContent
{
    public class ShortcutItem
    {
        public int Index = 0;
        public int Kind = 0;
        public int ItemCfgID = 0;
        public int Count = 0;
        public int MaxCount = 1;
        public int IconID;

        public ShortcutItem()
        {
            MaxCount = 3;
        }
    }
}