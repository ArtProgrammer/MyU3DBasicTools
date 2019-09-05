using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameContent.SimAgent;

using Config;

namespace GameContent
{
    public class ShortCutSystem : MonoBehaviour
    {
        public List<ShortcutItem> Items = new List<ShortcutItem>();

        private List<int> IndexRecorder = new List<int>();

        public int Volume = 6;

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
            AddItemAtIndex(0, 10002, 0, 1);
            AddItemAtIndex(1, 10001, 1, 1);
            //AddItemAtIndex(0, 10002, 2, 1);
            //AddItemAtIndex(0, 10002, 3, 1);
            //AddItemAtIndex(0, 10002, 4, 1);
            //AddItemAtIndex(0, 10002, 5, 1);
        }

        public List<ShortcutItem> GetAllItems()
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
                if (Items[i].ItemCfgID == cfgID)
                {
                    if (Items[i].Count + count <= Items[i].MaxCount)
                    {
                        return Items[i].Index;
                    }
                }
            }

            for (int i = 0; i < Volume; i++)
            {
                if (IndexRecorder[i] == 0)
                {
                    return i;
                }
            }

            return InvalidIndex;
        }

        /// <summary>
        /// Add item by it's config id.
        /// </summary>
        /// <param name="id">the config id of item.</param>
        /// <returns></returns>
        private bool AddItem(int kind, int id, int count)
        {
            int index = GetAvailableIndex(id, count);

            if (index == InvalidIndex)
            {
                return false;
            }

            AddItemAtIndex(kind, id, index, count);

            return true;
        }

        private int AddItemAtIndex(int kind, int id, int index, int count)
        {
            if (!IsValidAtIndex(id, index))
                return 0;

            ShortcutItem bbi = GetItemByIndex(index);

            if (kind == 0) // item
            {                
                if (!System.Object.ReferenceEquals(null, bbi))
                {
                    bbi.Count += count;
                }
                else
                {
                    bbi = new ShortcutItem();
                    bbi.ItemCfgID = id;
                    bbi.Index = index;
                    bbi.Kind = kind;

                    ItemConfig ic =
                        ConfigDataMgr.Instance.ItemCfgLoader.GetDataByID(id);
                    bbi.IconID = ic.IconID;

                    bbi.Count += count;

                    Items.Add(bbi);
                }
            }
            else if (kind == 1)  // skill
            {
                if (!System.Object.ReferenceEquals(null, bbi))
                {
                    // handle cool down.
                    bbi.Count = 1;
                }
                else
                {
                    bbi = new ShortcutItem();
                    bbi.ItemCfgID = id;
                    bbi.Index = index;
                    bbi.Kind = kind;

                    //ItemConfig ic =
                    //    ConfigDataMgr.Instance.ItemCfgLoader.GetDataByID(id);

                    SkillConfig sc =
                        ConfigDataMgr.Instance.SkillCfgLoader.GetDataByID(id);
                    bbi.IconID = sc.IconID;

                    bbi.Count = 1;

                    Items.Add(bbi);
                }
            }

            IndexRecorder[index] = 1;

            if (!System.Object.ReferenceEquals(null, OnAddItem))
            {
                OnAddItem(index);
            }

            return 0;
        }

        public void RemoveItem(int index)
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
        public void Add(int kind, int id, int count)
        {
            AddItem(kind, id, count);
        }

        public void Add(ShortcutItem item)
        {

        }

        public ShortcutItem GetItemByID(int id)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].ItemCfgID == id)
                {
                    return Items[i];
                }
            }

            return null;
        }

        public ShortcutItem GetItemByIndex(int index)
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

        public bool UseItemAtIndex(int index, int count = 1)
        {
            if (index >= 0)
            {
                ShortcutItem item = GetItemByIndex(index);

                if (!System.Object.ReferenceEquals(null, item))
                {
                    if (!System.Object.ReferenceEquals(null, Owner))
                    {
                        if (item.Kind == 0)
                        {
                            Owner.UseItem(item.ItemCfgID, Owner);

                            item.Count -= count;

                            if (item.Count > 0)
                            {
                                OnItemChange(index);
                            }
                            else
                            {
                                RemoveItem(index);
                            }
                        }
                        else if (item.Kind == 1)
                        {
                            Owner.UseSkill(item.ItemCfgID, Owner);
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
                if (Items[i].ItemCfgID == id)
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
                RemoveItem(index);
            }
        }
    }
}