using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameContent.UsableItem;
using GameContent.Item;

namespace GameContent.Interaction
{
    public class BagItem
    {
        public int Index;

        public int Count;

        public int MaxCount;

        public BaseUsableData Data = null;

        public BagItem()
        {

        }

        public BagItem(int index, BaseUsableData data)
        {
            Index = index;
            Data = data;
        }
    }

    public class UIBagData : MonoBehaviour
    {
        private List<int> ItemIndexes = new List<int>();

        private Dictionary<int, BagItem> Items = new Dictionary<int, BagItem>();

        //private List<BagItem> Items = new List<BagItem>();

        private UIBagView TheView = null;

        public void LoadContent()
        {
            {
                BagItem bi = new BagItem();
                bi.Index = 0;
                bi.Data = ItemManager.Instance.GetItemData(1000001);
                bi.Count = 1;
                bi.MaxCount = 9;

                //Items.Add(bi.Index, bi);
                //AddItem(bi.Index, bi.Data, false);
                AddItem(bi, false);
            }

            {
                BagItem bi = new BagItem();
                bi.Index = 1;
                bi.Data = ItemManager.Instance.GetItemData(1000002);
                bi.Count = 2;
                bi.MaxCount = 9;

                //Items.Add(bi.Index, bi);
                //AddItem(bi.Index, bi.Data, false);
                AddItem(bi, false);
            }

            {
                BagItem bi = new BagItem();
                bi.Index = 3;
                bi.Data = ItemManager.Instance.GetItemData(1000003);
                bi.Count = 3;
                bi.MaxCount = 9;

                //Items.Add(bi.Index, bi);
                //AddItem(bi.Index, bi.Data, false);
                AddItem(bi, false);
            }
        }

        public void PlaceData()
        {
            int index = 0;
            for (int i = 0; i < ItemIndexes.Count; i++)
            {
                index = ItemIndexes[i];
                TheView.OnAddItem(Items[index]);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            LoadContent();

            TheView = GetComponent<UIBagView>();

            PlaceData();
        }

        public BaseUsableData GetItemByIndex(int index)
        {
            if (Items.ContainsKey(index))
            {
                return Items[index].Data;
            }
            return null;
        }

        public void AddItem(BagItem item, bool alterUI = true)
        {
            var index = item.Index;
            ItemIndexes.Add(index);

            if (Items.Count <= index)
            {
                Items.Add(index, item);
            }
            else
            {
                Items[index] = item;
            }

            if (alterUI)
            {
                TheView.OnAddItem(item);
            }

        }

        //public void AddItem(int index, BaseUsableData data, bool alterUI = true)
        //{
        //    ItemIndexes.Add(index);

        //    if (Items.Count <= index)
        //    {
        //        var uiitem = new BagItem(index, data);
        //        Items.Add(index, uiitem);
        //    }
        //    else
        //    {
        //        Items[index].Data = data;
        //    }

        //    if (alterUI)
        //    {
        //        TheView.OnAddItem(index, data);
        //    }
        //}

        public void RemoveItem(int index)
        {
            ItemIndexes.Remove(index);

            TheView.OnRemoveItem(index);
        }

        public void Save()
        {

        }
    }
}