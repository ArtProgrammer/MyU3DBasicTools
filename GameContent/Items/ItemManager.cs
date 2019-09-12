using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Game;
using SimpleAI.Utils;
using GameContent.UsableItem;
using GameContent;
using Config;

namespace GameContent.Item
{
    public enum ItemKind
    { 
        Qi,
        Xue,
        LeiFu,
        HuoFu,
        SmallLei,
        Def,
        ATT,

    }

    public enum ItemTargetType
    {
        PlayerSelf,
        TargetBody,
        Place,
        Direction
    }

    public class ItemData : BaseUsableData
    { 
        public ItemData()
        {
            Catalog = UsableCatalog.Item;
        }

        public ItemKind Kind;

        public ItemTargetType TargetType;
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

        private ItemProducer ItemMaker = new ItemProducer();

        public void LoadDatas()
        {
            //{
            //    ItemData idata = new ItemData();
            //    idata.ID = 1000001;
            //    idata.Count = 1;
            //    idata.TargetType = ItemTargetType.PlayerSelf;
            //    idata.Kind = ItemKind.Qi;
            //    //idata.Icon = "red_cross.png";
            //    idata.IconID = 1;

            //    ItemDataPool.Add(idata.ID, idata);
            //}

            //{
            //    ItemData idata = new ItemData();
            //    idata.ID = 1000002;
            //    idata.Count = 1;
            //    idata.TargetType = ItemTargetType.PlayerSelf;
            //    idata.Kind = ItemKind.Xue;
            //    //idata.Icon = "red_cross.png";
            //    idata.IconID = 1;

            //    ItemDataPool.Add(idata.ID, idata);
            //}

            //{
            //    ItemData idata = new ItemData();
            //    idata.ID = 1000003;
            //    idata.Count = 1;
            //    idata.TargetType = ItemTargetType.TargetBody;
            //    idata.Kind = ItemKind.LeiFu;
            //    //idata.Icon = "red_cross.png";
            //    idata.IconID = 1;

            //    ItemDataPool.Add(idata.ID, idata);
            //}

            //{
            //    ItemData idata = new ItemData();
            //    idata.ID = 1000004;
            //    idata.Count = 1;
            //    idata.TargetType = ItemTargetType.TargetBody;
            //    idata.Kind = ItemKind.HuoFu;
            //    //idata.Icon = "Board-Games.png";
            //    idata.IconID = 1;

            //    ItemDataPool.Add(idata.ID, idata);
            //}

            ItemMaker.AddPrototype(new QiItem());
            ItemMaker.AddPrototype(new XueItem());
            ItemMaker.AddPrototype(new LeiFu());
            ItemMaker.AddPrototype(new HuoFu());
            ItemMaker.AddPrototype(new SmallLei());
        }

        public BaseItem SpawnItem(ItemKind kind)
        {
            return ItemMaker.FindAndClone(kind);
        }

        public bool TryUseItem(int id, Vector3 pos)
        {
            //var itemData = GetItemData(id);
            ItemConfig itemData = ConfigDataMgr.Instance.ItemCfgLoader.GetDataByID(id);
            if (!System.Object.ReferenceEquals(null, itemData))
            {
                var item = SpawnItem((ItemKind)itemData.Kind);
                if (!System.Object.ReferenceEquals(null, item))
                {
                    //item.Use(pos);
                    item.Use(itemData.ID, pos);
                    return true;
                }
            }

            return false;
        }

        public bool TryUseItem(int id, BaseGameEntity target)
        { 
            if (!System.Object.ReferenceEquals(null, target))
            {
                //var itemData = GetItemData(id);
                ItemConfig itemData = ConfigDataMgr.Instance.ItemCfgLoader.GetDataByID(id);
                if (!System.Object.ReferenceEquals(null, itemData))
                {
                    var item = SpawnItem((ItemKind)itemData.Kind);
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

        public virtual void OnFixedUpdate(float dt)
        {

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