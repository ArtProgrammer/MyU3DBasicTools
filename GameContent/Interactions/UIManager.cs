using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using SimpleAI.Utils;

namespace GameContent.Interaction
{
    public enum GameUIKind
    { 
        Bag,
        Skill,
        ItemUse,
        PlayerInfo,
        None
    }

    public class UIManager : SingletonAsComponent<UIManager>
    {
        public static UIManager Instance
        { 
            get
            {
                return (UIManager)InsideInstance;
            }
        }

        public RectTransform HealthBarPanel = null;
        List<GameObject> Panels = new List<GameObject>();

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ShowMainUI(GameUIKind kind)
        { 

        }

        public void HideMainUI(GameUIKind kind)
        { 

        }
    }
}