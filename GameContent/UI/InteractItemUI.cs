using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GameContent
{
    public enum InteractItemUIType
    {
        Bag,
        Shortcut,
        Skill,
        RoleInfo,
        None
    }

    public class InteractItemUI : MonoBehaviour, IPointerDownHandler,
        IPointerClickHandler
    {
        public int Index = 0;

        public InteractItemUIType Belong = InteractItemUIType.None;

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("$ InteractItemUI pointer down: " + Index.ToString());            
            //if (Belong == InteractItemUIType.Shortcut)
            //{
            //    //UILord.Instance.CurShortcutItem
            //    UILord.Instance.CurShortcutUI.ClickOnItem(Belong, Index);
            //}
            //else if (Belong == InteractItemUIType.Bag)
            //{
            //    UILord.Instance.CurBagUI.ClickOnItem(Belong, Index);
            //}
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log("$ InteractItemUI pointer click: " + Index.ToString());

            if (Belong == InteractItemUIType.Bag)
            {
                UILord.Instance.CurBagUI.ClickOnItem(Index);
            }
            else if (Belong == InteractItemUIType.Shortcut)
            {
                UILord.Instance.CurShortcutUI.ClickOnItem(Index);
            }
        }
    }
}
