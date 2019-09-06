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

        public InteractItem CurShortcutItem = null;

        public InteractItem CurBagItem = null;

        public InteractItem CurInteractItem = null;

        public InteractItemUIType CurItemUIType = InteractItemUIType.None;

        public BagSystem CurBag = null;

        public ShortCutSystem CurShortcut = null;

        public BagSystemUI CurBagUI = null;

        public ShortcutUI CurShortcutUI = null;

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

        public void SelectShortcutItem(InteractItem item)
        {
            ClearSelectItem();
            CurShortcutItem = item;
            CurInteractItem = item;
            HasItem = true;
        }

        public void SelectBagItem(InteractItem item)
        {
            ClearSelectItem();
            CurBagItem = item;
            CurInteractItem = item;
            HasItem = true;
        }

        public void ClearSelectItem()
        {
            CurBagItem = null;
            CurShortcutItem = null;
            CurInteractItem = null;
            UILord.Instance.CurItemUIType = InteractItemUIType.None;
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

                    //if (!System.Object.ReferenceEquals(null, CurInteractItem))
                    //{
                    //    if (CurInteractItem.Kind == InteractItemType.Item)
                    //    {

                    //    }
                    //    else if (CurInteractItem.Kind == InteractItemType.Skill)
                    //    {

                    //    }
                    //}
                }

                if (used)
                {
                    ClearSelectItem();
                }
            }
        }
    }
}
