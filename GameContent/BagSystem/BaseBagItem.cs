using System;
using GameContent.Item;

namespace GameContent
{
    /// <summary>
    /// Item in bag, record it's information in the bag.
    /// </summary>
    public class BaseBagItem
    {
        public int GUID;

        public int Index;

        public int Count;

        public int MaxCount;

        public int ItemID; //BaseItem

        public int ItemCfgID; //ItemData

        public ItemData CfgData;  // runtime property.

        public int IconID;   // runtime property.

        public BaseBagItem() {
            MaxCount = 3;
        }
    }
}
