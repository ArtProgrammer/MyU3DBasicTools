using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using SimpleAI.Utils;
using SimpleAI.Game;
using SimpleAI.Inputs;
using GameContent.SimAgent;
using GameContent.Item;

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

        public InteractItem CurRoleInfoItem = null;

        public InteractItem CurInteractItem = null;

        public InteractItemUIType CurItemUIType = InteractItemUIType.None;

        public BagSystem CurBag = null;

        public ShortCutSystem CurShortcut = null;

        public RoleInfoSystem CurRoleInfo = null;

        public BagSystemUI CurBagUI = null;

        public ShortcutUI CurShortcutUI = null;

        public RoleInfoUI CurRoleInfoUI = null;

        public bool HasItem = false;

        public void Init()
        {
            InputKeeper.Instance.OnLeftClickObject += UseOnTarget;
            InputKeeper.Instance.OnLeftClickPos += UseAtPosition;
        }

        protected bool IsCurSkillUsable()
        {
            return false;
        }

        protected bool IsCurItemUsable()
        {
            return false;
        }

        public bool IsCurItemUsableOnTarget()
        {
            if (CurInteractItem.Kind == InteractItemType.Item)
            {
                var data =
                    ConfigDataMgr.Instance.ItemCfgLoader.GetDataByID(CurInteractItem.CfgID);
                return data.TargetType == (int)ItemTargetType.TargetBody;
            }
            else if (CurInteractItem.Kind == InteractItemType.Skill)
            {
                var data =
                    ConfigDataMgr.Instance.SkillCfgLoader.GetDataByID(CurInteractItem.CfgID);
                return data.TargetType == (int)ItemTargetType.TargetBody;
            }

            return false;
        }

        public bool IsCurItemUsableOnSelf()
        {
            if (CurInteractItem.Kind == InteractItemType.Item)
            {
                var data =
                    ConfigDataMgr.Instance.ItemCfgLoader.GetDataByID(CurInteractItem.CfgID);
                return data.TargetType == (int)ItemTargetType.PlayerSelf;
            }
            else if (CurInteractItem.Kind == InteractItemType.Skill)
            {
                var data =
                    ConfigDataMgr.Instance.SkillCfgLoader.GetDataByID(CurInteractItem.CfgID);
                return data.TargetType == (int)ItemTargetType.PlayerSelf;
            }

            return false;
        }

        public bool IsCurItemUsableOnPos()
        {
            if (CurInteractItem.Kind == InteractItemType.Item)
            {
                var data =
                    ConfigDataMgr.Instance.ItemCfgLoader.GetDataByID(CurInteractItem.CfgID);
                return data.TargetType == (int)ItemTargetType.Place;
            }
            else if (CurInteractItem.Kind == InteractItemType.Skill)
            {
                var data =
                    ConfigDataMgr.Instance.SkillCfgLoader.GetDataByID(CurInteractItem.CfgID);
                return data.TargetType == (int)ItemTargetType.Place;
            }

            return false;
        }

        public void UseOnTarget(Transform target)
        {
            if (HasItem)
            {
                if (IsCurItemUsableOnSelf() || IsCurItemUsableOnTarget())
                {
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
            }
        }

        public void UseAtPosition(Vector3 pos)
        {
            if (HasItem)
            {
                if (IsCurItemUsableOnPos())
                {
                    UseItem(pos);
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

        public void SelectRoleItem(InteractItem item)
        {
            ClearSelectItem();
            CurRoleInfoItem = item;
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
            CurRoleInfoItem = null;
            CurInteractItem = null;
            UILord.Instance.CurItemUIType = InteractItemUIType.None;
            HasItem = false;
        }

        public void UseItem(Vector3 pos)
        {
            if (HasItem)
            {
                bool used = false;
                var role = (SimWood)EntityManager.Instance.PlayerEntity;

                if (role)
                {
                    if (!System.Object.ReferenceEquals(null, CurShortcutItem))
                    {
                        role.Shortcut.UseItemAtIndex(CurShortcutItem.Index, 1, pos);
                        used = true;
                    }

                    else if (!System.Object.ReferenceEquals(null, CurBagItem))
                    {
                        role.Bag.UseItemAtIndex(CurBagItem.Index, 1, pos);
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
