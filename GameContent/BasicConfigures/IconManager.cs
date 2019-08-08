using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SimpleAI.Utils;
using SimpleAI.Logger;

namespace GameContent
{
    public class IconData
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Path { set; get; }
    }

    public class IconManager : SingletonAsComponent<IconManager>
    {
        public static IconManager Instance
        {
            get
            {
                return (IconManager)InsideInstance;
            }
        }

        private Dictionary<int, IconData> IconDatas =
            new Dictionary<int, IconData>();

        private void Awake()
        {
            InitIcons();
            TinyLogger.Instance.DebugLog("Icons configure init.");
        }

        public bool InitIcons()
        {
            {
                IconData icd = new IconData();
                icd.ID = 1;
                icd.Name = "red_cross.png";
                icd.Path = Application.dataPath + "/AssetBundles/skillicons";

                IconDatas.Add(icd.ID, icd);
            }

            //{
            //    IconData icd = new IconData();
            //    icd.ID = 1;
            //    icd.Name = "";
            //    icd.Path = Application.dataPath + "/AssetBundles/skillicons";

            //    IconDatas.Add(icd.ID, icd);
            //}

            {
                IconData icd = new IconData();
                icd.ID = 2;
                icd.Name = "Board-Games.png";
                icd.Path = Application.dataPath + "/AssetBundles/skillicons";

                IconDatas.Add(icd.ID, icd);
            }

            return true;
        }

        public IconData GetIconData(int id)
        {
            if (IconDatas.ContainsKey(id))
            {
                return IconDatas[id];
            }

            return null;
        }
    }
}
