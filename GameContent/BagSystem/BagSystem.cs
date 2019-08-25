using System;
using System.Collections.Generic;

using GameContent.Item;

namespace GameContent
{
    public class BagSystem
    {
        private Dictionary<int, BaseBagItem> BagItems =
            new Dictionary<int, BaseBagItem>();

        private List<BaseBagItem> Items = new List<BaseBagItem>();

        public int BagVolume = 16;

        private int InvalidIndex = -1;

        public void Load()
        {

        }

        private bool IsValidAtIndex(int cfgID, int index)
        {
            return false;
        }

        private int GetAvailableIndex(int cfgID)
        {
            return InvalidIndex;
        }

        private bool AddBagItem(int id)
        {
            int index = GetAvailableIndex(id);

            if (index == InvalidIndex)
            {
                return false;
            }

            return true;
        }

        private int AddItemAtIndex(int id, int index)
        {
            if (!IsValidAtIndex(id, index))
                return 0;

            return 0;
        }

        private void RemoveBagItem()
        {

        }

        public void Add(int id)
        {

        }

        public void Add(BaseBagItem item)
        {

        }

        public BaseBagItem GetItemByID(int id)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].ItemID == id)
                {
                    return Items[i];
                }
            }

            return null;
        }

        public BaseBagItem GetItemByIndex(int index)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Index == index)
                {
                    return Items[i];
                }
            }

            return null;
        }

        public bool UseItemAtIndex(int index)
        {
            return false;
        }

        public bool UseItemByID(int id)
        {
            return false;
        }

        public void Save()
        {

        }
    }
}
