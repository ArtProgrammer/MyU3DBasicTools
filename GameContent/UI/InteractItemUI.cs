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
        None
    }

    public class InteractItemUI : MonoBehaviour
    {
        public int Index = 0;

        public InteractItemUIType Belong = InteractItemUIType.None;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}