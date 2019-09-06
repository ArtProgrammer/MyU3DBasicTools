using System;
using System.Collections.Generic;
using UnityEngine;

using Config;
using SimpleAI.Game;
using SimpleAI.Supervisors;
using GameContent.Item;
using GameContent.SimAgent;

namespace GameContent
{
    public class BagSystem : MonoBehaviour
    {
        private List<InteractItem> Items = new List<InteractItem>();

		private List<int> IndexRecorder = new List<int>();

        public int Volume = 20;

        public int BagVolume = 20;

        private int InvalidIndex = -1;

        public SimWood Owner = null;

        public Action<int> OnAddItem;

        public Action<int> OnItemChange;

        public Action<int> OnRemoveItem;

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
            Items.Capacity = Volume;

            for (int i = 0; i < Volume; i++)
			{
				IndexRecorder.Add(0);
			}
        }

        public void Load()
        {
            AddItemAtIndex(10002, 1, 2);
            AddItemAtIndex(10002, 2, 2);
            AddItemAtIndex(10002, 3, 1);
            AddItemAtIndex(10004, 4, 1);
            AddItemAtIndex(10005, 5, 1);
        }

        public List<InteractItem> GetAllItems()
		{
			return Items;
		}

		private bool IsValidAtIndex(int cfgID, int index)
        {
            return GetAvailableIndex(cfgID, index) != InvalidIndex;
        }

        private int GetAvailableIndex(int cfgID, int count)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].CfgID == cfgID)
                {
                    if (Items[i].Count + count <= Items[i].MaxCount)
                    {
                        return Items[i].Index;
                    }
                }
            }

			for (int i = 0; i < BagVolume; i++)
			{
				if (IndexRecorder[i] == 0)
				{
					return i;
				}
			}

            return InvalidIndex;
        }

        /// <summary>
        /// Add item to bag by it's config id.
        /// </summary>
        /// <param name="id">the config id of item.</param>
        /// <returns></returns>
        private bool AddBagItem(int id, int count)
        {
            int index = GetAvailableIndex(id, count);

            if (index == InvalidIndex)
            {
                return false;
            }

			AddItemAtIndex(id, index, count);

			return true;
        }

        /// <summary>
        /// Add item to index, check the item must be the same type
        /// if there are already an item exist there.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public int AddItemAtIndex(int id, int index, int count)
        {
            if (!IsValidAtIndex(id, index))
                return 0;

            int countLeft = 0;

            InteractItem bbi = GetItemByIndex(index);
            if (!System.Object.ReferenceEquals(null, bbi))
            {
                bbi.Count += count;

                if (bbi.Count > bbi.MaxCount)
                {
                    countLeft = bbi.Count - bbi.MaxCount;
                    bbi.Count = bbi.MaxCount;
                }
            }
            else
            {
                bbi = new InteractItem();
                bbi.CfgID = id;
                bbi.Index = index;

                ItemConfig ic = ConfigDataMgr.Instance.ItemCfgLoader.GetDataByID(id);
                bbi.IconID = ic.IconID;

                bbi.Count += count;
                bbi.Kind = InteractItemType.Item;

                Items.Add(bbi);
            }            

			IndexRecorder[index] = 1;

            if (!System.Object.ReferenceEquals(null, OnAddItem))
            {
                OnAddItem(index);
            }

			return countLeft;
        }

        public void ChangeBagItem(int index, int count)
        {
            if (index < 0 || index >= IndexRecorder.Count)
                return;

            if (IndexRecorder[index] == 1)
            {
                bool hasLeft = false;
                for (int i = 0; i < Items.Count; i++)
                {
                    if (Items[i].Index == index)
                    {
                        Items[i].Count = count;
                        hasLeft = Items[i].Count > 0;
                        break;
                    }
                }

                if (hasLeft &&
                    !System.Object.ReferenceEquals(null, OnItemChange))
                {
                    OnItemChange(index);
                }else if (!System.Object.ReferenceEquals(null, OnRemoveItem))
                {
                    OnRemoveItem(index);
                }
            }
        }

        public void RemoveBagItem(int index)
        {
            if (index < 0 || index >= IndexRecorder.Count)
                return;

            if (IndexRecorder[index] == 1)
			{
                for (int i = 0; i < Items.Count; i++)
                {
                    if (Items[i].Index == index)
                    {
                        Items.RemoveAt(i);
                        break;
                    }
                }
                //if (!System.Object.ReferenceEquals(null, Items[index]))
                {
                    IndexRecorder[index] = 0;
                }

                if (!System.Object.ReferenceEquals(null, OnItemChange))
                {
                    OnItemChange(index);
                }

                if (!System.Object.ReferenceEquals(null, OnRemoveItem))
                {
                    OnRemoveItem(index);
                }
			}
        }

        /// <summary>
		/// 
		/// </summary>
		/// <param name="id">now it's the config id of the item.</param>
        public void Add(int id, int count)
        {
			AddBagItem(id, count);
		}

        public void Add(InteractItem item)
        {

        }

        public InteractItem GetItemByID(int id)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].CfgID == id)
                {
                    return Items[i];
                }
            }

            return null;
        }

        public InteractItem GetItemByIndex(int index)
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

        public bool UseItemAtIndex(int index, int count, BaseGameEntity target = null)
        {
            if (index >= 0)
            {
                InteractItem item = GetItemByIndex(index);

                if (!System.Object.ReferenceEquals(null, item))
                {
                    if (!System.Object.ReferenceEquals(null, Owner))
                    {
                        if (System.Object.ReferenceEquals(null, target))
                        {
                            target = Owner;
                        }
                        Owner.UseItem(item.CfgID, target);

                        item.Count -= count;

                        if (item.Count > 0)
                        {
                            OnItemChange(index);
                        }
                        else
                        {
                            RemoveBagItem(index);
                        }                        
                    }

                    return true;
                }                
            }
            return false;
        }

        public bool UseItemByID(int id)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].CfgID == id)
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

        public void DropItem(int index, Vector3 pos)
        {
            if (index < Items.Count)
            {
                //var item = Items[index];
                //for (int i = 0; i < Items.Count; i++)
                //{
                //    var item = Items[i];
                //    if (item.Index == index)
                //    {
                //        RemoveBagItem(index);
                //    }
                //}

                RemoveBagItem(index);
            }
        }
    }
}
