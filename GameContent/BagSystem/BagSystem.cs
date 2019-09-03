using System;
using System.Collections.Generic;
using UnityEngine;

using Config;
using GameContent.Item;

namespace GameContent
{
    public class BagSystem : MonoBehaviour
    {
        private List<BaseBagItem> Items = new List<BaseBagItem>();

		private List<int> IndexRecorder = new List<int>();

        public int Volume = 16;

        public int BagVolume = 16;

        private int InvalidIndex = -1;

        public Action<int> OnAddItem;

        private void Awake()
        {
            Initialize();
            
        }

        void Start()
		{
			Load();
		}

        private void Initialize()
        {
            Items.Capacity = 16;

            for (int i = 0; i < Volume; i++)
			{
				IndexRecorder.Add(0);
			}
        }

        public void Load()
        {
            AddItemAtIndex(10001, 1);
            AddItemAtIndex(10002, 2);
            AddItemAtIndex(10003, 3);
            AddItemAtIndex(10004, 4);
            AddItemAtIndex(10005, 5);
        }

        public List<BaseBagItem> GetAllItems()
		{
			return Items;
		}

		private bool IsValidAtIndex(int cfgID, int index)
        {
            if (IndexRecorder[index] == 0)
            //if (Items[index] == null)
            {
                return true;
            }

            return false;
        }

        private int GetAvailableIndex(int cfgID)
        {
			for (int i = 0; i < BagVolume; i++)
			{
				if (IndexRecorder[i] == 0)
				{
					return i;
				}
			}

            return InvalidIndex;
        }

        private bool AddBagItem(int id)
        {
            int index = GetAvailableIndex(id);

            if (index == InvalidIndex)
            {
                return false;
            }

			AddItemAtIndex(id, index);

			return true;
        }

        private int AddItemAtIndex(int id, int index)
        {
            if (!IsValidAtIndex(id, index))
                return 0;

            BaseBagItem bbi = new BaseBagItem();
            bbi.ItemCfgID = id;
            bbi.Index = index;

            ItemConfig ic = ConfigDataMgr.Instance.ItemCfgLoader.GetDataByID(id);
            bbi.IconID = ic.IconID;

            bbi.Count = 1;
            bbi.ItemID = id;

            Items.Add(bbi);

			IndexRecorder[index] = 1;

            if (!System.Object.ReferenceEquals(null, OnAddItem))
            {
                OnAddItem(index);
            }

			return 0;
        }

        private void RemoveBagItem(int index)
        {
            if (IndexRecorder[index] == 1)
			{
                //if (!System.Object.ReferenceEquals(null, Items[index]))
				{
					IndexRecorder[index] = 0;
				}				
			}
        }

        /// <summary>
		/// 
		/// </summary>
		/// <param name="id">now it's the config id of the item.</param>
        public void Add(int id)
        {
			AddBagItem(id);
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
            if (index > 0 && index < Items.Count)
            {
                BaseBagItem item = Items[index];
                //

                return true;
            }
            return false;
        }

        public bool UseItemByID(int id)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].ItemID == id)
                {
                    // 
                    return true;
                }
            }

            return false;
        }

        public void Save()
        {

        }
    }
}
