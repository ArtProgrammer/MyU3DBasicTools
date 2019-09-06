using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GameContent
{
    public class InteractItemUI : MonoBehaviour/*,
        IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler*/
    {
        public int Index = 0;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        //void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        //{
        //    Debug.Log("$ point down " + Index.ToString());
        //}

        //void IDragHandler.OnDrag(PointerEventData eventData)
        //{
        //    //GetComponent<RectTransform>().pivot.Set(0, 0);
        //    //transform.position = Input.mousePosition;
        //}

        //void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        //{
        //    //eventData.selectedObject;
        //    //Debug.Log("$ drag begin " + Index.ToString());

        //    //UILord.Instance.CurBagUI.ClickOnItem(Index);
        //}

        //void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        //{
        //    //Debug.Log("$ drag end" + Index.ToString());
        //    //UILord.Instance.CurBagUI.ClickOnItem(Index);

        //    //if (eventData.selectedObject)
        //    //{
        //    //    var item = eventData.selectedObject.GetComponent<InteractItemUI>();
        //    //    if (item)
        //    //    {
        //    //        Debug.Log("$ drag end selected object: " + item.Index.ToString());
        //    //    }
        //    //}
        //}
    }
}