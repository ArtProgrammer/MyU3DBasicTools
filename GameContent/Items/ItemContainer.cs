using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameContent.Item
{
    public class ItemInContainer
    {
        public int Index = 0;
        public BaseItem TheItem = null;
    }

    public class ItemContainer : MonoBehaviour
    {
        private Dictionary<int, ItemInContainer> Items =
            new Dictionary<int, ItemInContainer>();

        public bool AddItem(int index, BaseItem item)
        {
            return false;
        }

        public bool RemoveItem(int index)
        {
            return false;
        }

        public bool SweepItems(ItemInContainer src, 
            ItemInContainer dst)
        { 
            return false;
        }


    }
}