using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameContent.Item
{
    public class ItemProducer
    {
        private Dictionary<ItemKind, BaseItem> ItemPrototypes =
            new Dictionary<ItemKind, BaseItem>();

        public void AddPrototype(BaseItem item)
        {
            if (!System.Object.ReferenceEquals(null, item))
            {
                if (!ItemPrototypes.ContainsKey(item.Kind))
                {
                    ItemPrototypes.Add(item.Kind, item);
                }
            }
        }

        public BaseItem FindAndClone(ItemKind kind)
        {
            if (ItemPrototypes.ContainsKey(kind))
            {
                return ItemPrototypes[kind].Clone();
            }

            return NullItem.Instance.Clone();
        }

        public void CleanPrototypes()
        {
            ItemPrototypes.Clear();
        }
    }
}