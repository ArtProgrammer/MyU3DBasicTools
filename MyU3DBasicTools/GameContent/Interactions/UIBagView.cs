using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameContent.Item;

namespace GameContent.Interaction
{
    public class UIBagView : MonoBehaviour
    {
        private UIBagInteractor Inter = null;

        // Start is called before the first frame update
        void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            Inter = GetComponent<UIBagInteractor>();
        }

        public void OnAddItem(int index, BaseItem item)
        { 
        }

        public void OnRemoveItem(int index)
        {
            //
        }
    }
}