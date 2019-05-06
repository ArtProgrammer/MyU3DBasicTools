using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Game;
using SimpleAI.Utils;
using GameContent.UsableItem;

namespace GameContent.Item
{
    public enum ItemKind
    { 
        Qi,
        Xue
    }

    public class ItemData : BaseUsableData
    { 
        public ItemData()
        {
            Catalog = UsableCatalog.Item;
        }

        public ItemKind Kind;
    }

    public class ItemManager : SingletonAsComponent<ItemManager>,
        IUpdateable
    {
        public static ItemManager Instance
        { 
            get
            {
                return (ItemManager)InsideInstance;
            }
        }

        private Dictionary<int, BaseItem> ItemList =
            new Dictionary<int, BaseItem>();

        private Dictionary<int, ItemData> ItemDataPool =
            new Dictionary<int, ItemData>();

        public void LoadDatas()
        {
            {
                ItemData idata = new ItemData();
                idata.ID = 1000001;
                idata.Count = 1;
                idata.Kind = ItemKind.Qi;
                idata.Icon = Application.dataPath + "/Images/PureImages/red_cross.png";

                ItemDataPool.Add(idata.ID, idata);
            }

            {
                ItemData idata = new ItemData();
                idata.ID = 1000002;
                idata.Count = 1;
                idata.Kind = ItemKind.Xue;
                idata.Icon = Application.dataPath + "/Images/PureImages/red_cross.png";

                ItemDataPool.Add(idata.ID, idata);
            }
        }

        public BaseItem SpawnItem(ItemKind kind)
        { 
            switch (kind)
            {
                case ItemKind.Qi:
                    var qiitem = new QiItem();
                    return qiitem;
                case ItemKind.Xue:
                    var xueitem = new XueItem();
                    return xueitem;
                default:
                    return null;
            }
        }

        public bool TryUseItem(int id, BaseGameEntity target)
        { 
            if (!System.Object.ReferenceEquals(null, target))
            {
                var itemData = GetItemData(id);
                if (!System.Object.ReferenceEquals(null, itemData))
                {
                    var item = SpawnItem(itemData.Kind);
                    if (!System.Object.ReferenceEquals(null, item))
                    {
                        item.Use(target);
                        return true;
                    }
                }
            }

            return false;
        }

        public void Start()
        {
            GameLogicSupvisor.Instance.Register(this);
        }

        public void OnUpdate(float dt)
        {
            UpdateItems(dt);
        }

        public ItemData GetItemData(int id)
        { 
            if (ItemDataPool.ContainsKey(id))
            {
                return ItemDataPool[id];
            }

            return null;
        }

        public void UpdateItems(float dt)
        { 
            for (int i = 0; i < ItemList.Count; i++)
            {
                ItemList[i].Process(dt);
            }
        }

        public void AddItem(BaseItem item)
        { 
            if (!System.Object.ReferenceEquals(null, item))
            {
                ItemList.Add(item.ID, item);
            }
        }
    }
}