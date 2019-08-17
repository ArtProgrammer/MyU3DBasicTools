using System;
using System.Collections.Generic;

using GameContent.Item;

namespace GameContent
{
    public class BaseBagItem
    {
        public int GUID;

        public int Index;

        public int ItemID; //BaseItem

        public int ItemCfgID; //ItemData

        public BaseBagItem() { }
    }

    public class BagSystem
    {
        private Dictionary<int, BaseBagItem> BagItems =
            new Dictionary<int, BaseBagItem>();

        private List<BaseBagItem> Items = new List<BaseBagItem>();

        public int BagVolume = 16;

        public void Load()
        {

        }

        /// <summary>
        /// Add item to bag by it's instance id.
        /// </summary>
        /// <param name="id"></param>
        private void AddBagItem(int id)
        {

        }

        public void Add(int id)
        {

        }

        public void Add(BaseBagItem item)
        {

        }

        public BaseBagItem GetByID(int id)
        {
            //if (BagItems.ContainsKey(id))
            //{
            //    return BagItems[id];
            //}

            return null;
        }

        public void Save()
        {

        }
    }
}
