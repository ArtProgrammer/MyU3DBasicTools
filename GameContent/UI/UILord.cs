using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using SimpleAI.Utils;
using SimpleAI.Game;
using SimpleAI.Inputs;
using GameContent.SimAgent;

namespace GameContent
{
    public class UILord : SingletonAsComponent<UILord>
    {
        public static UILord Instance
        {
            get
            {
                return (UILord)InsideInstance;
            }
        }

        public ShortcutItem CurShortcutItem = null;

        public BaseBagItem CurBagItem = null;

        public bool HasItem = false;

        public void Init()
        {
            InputKeeper.Instance.OnLeftClickObject += UseOnTarget;
        }

        public void UseOnTarget(Transform target)
        {
            //target.position = target.position + new Vector3(5, 0, 0);
            if (!System.Object.ReferenceEquals(null, target) &&
                !System.Object.ReferenceEquals(null, target.parent))
            {
                var bge = target.parent.GetComponent<SimWood>();
                if (!System.Object.ReferenceEquals(null, bge))
                {
                    UseItem(bge);
                }
            }
        }

        public void SelectShortcutItem(ShortcutItem item)
        {
            ClearSelectItem();
            CurShortcutItem = item;
            HasItem = true;
        }

        public void SelectBagItem(BaseBagItem item)
        {
            ClearSelectItem();
            CurBagItem = item;
            HasItem = true;
        }

        public void ClearSelectItem()
        {
            CurShortcutItem = null;
            CurBagItem = null;
            HasItem = false;
        }

        public void UseItem(BaseGameEntity target)
        {
            if (HasItem)
            {
                bool used = false;
                var role = (SimWood)EntityManager.Instance.PlayerEntity;

                if (role)
                {
                    if (!System.Object.ReferenceEquals(null, CurShortcutItem))
                    {
                        role.Shortcut.UseItemAtIndex(CurShortcutItem.Index, 1, target);
                        used = true;
                    }

                    else if (!System.Object.ReferenceEquals(null, CurBagItem))
                    {
                        role.Bag.UseItemAtIndex(CurBagItem.Index, 1, target);
                        used = true;
                    }
                }

                if (used)
                {
                    ClearSelectItem();
                }
            }
        }
    }
}
