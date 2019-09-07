using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GameContent
{
    public class UIInteractMgr : MonoBehaviour,
        IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            //GetComponent<RectTransform>().pivot.Set(0, 0);
            //transform.position = Input.mousePosition;
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            ////eventData.selectedObject;
            Debug.Log("$ bag drag begin ");

            if (eventData.selectedObject)
            {
                var item = eventData.selectedObject.GetComponent<InteractItemUI>();

                if (item)
                {
                    if (item.Belong == InteractItemUIType.Bag)
                    {
                        UILord.Instance.CurBagUI.ClickOnItem(item.Index);
                    }
                    else
                    {
                        UILord.Instance.CurShortcutUI.ClickOnItem(item.Index);
                    }
                    
                }
            }

            //UILord.Instance.CurBagUI.ClickOnItem(Index);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("$ bad drag end");
            //UILord.Instance.CurBagUI.ClickOnItem(Index);

            var pointerObject = eventData.pointerCurrentRaycast.gameObject;

            if (pointerObject)
            {
                var item = pointerObject.GetComponent<InteractItemUI>();
                if (item)
                {
                    if (item.Belong == InteractItemUIType.Bag)
                    {
                        Debug.Log("$ drag end selected object: " + item.Index.ToString());
                        UILord.Instance.CurBagUI.ClickOnItem(item.Index);
                    }
                    else if (item.Belong == InteractItemUIType.Shortcut)
                    {
                        UILord.Instance.CurShortcutUI.ClickOnItem(item.Index);
                    }
                }
            }
        }
    }
}