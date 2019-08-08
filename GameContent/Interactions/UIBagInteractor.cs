using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleAI.Inputs;

namespace GameContent.Interaction
{
    public class UIBagInteractor : MonoBehaviour
    {
        private UIBagData BagData = null;

        // Start is called before the first frame update
        void Start()
        {
            Initialize();
        }

        public void UpdateCirclePos(Vector3 pos)
        { 

        }

        public void FireCurSkill(Vector3 pos)
        { 

        }

        public void LoadContent()
        { 

        }

        public void Initialize()
        {
            LoadContent();

            InputKeeper.Instance.OnLeftClickPos += UpdateCirclePos;
            InputKeeper.Instance.OnLeftClickPos += FireCurSkill;
        }

        public void TryItem(int index)
        { 

        }

        public void AddItem(int index)
        {

        }

        public void RemoveItem(int index)
        {
        }

        public void SweepItems(int srcIndex, int dstIndex)
        {
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}