using System;
using GameContent.Item;

namespace GameContent
{
    /// <summary>
    /// Runtime instances.
    /// </summary>
    public class BaseBagItem
    {
        public int GUID;

        public int Index;

        public int Count;

        public int MacCount;

        public int ItemID; //BaseItem

        public int ItemCfgID; //ItemData

        public ItemData CfgData;  // runtime property.

        public int IconID;   // runtime property.

        public BaseBagItem() {
            MacCount = 49;
        }
    }
}
