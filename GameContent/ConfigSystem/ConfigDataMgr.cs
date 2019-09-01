using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Config;

namespace GameContent
{
    class ConfigDataMgr
    {
        private static ConfigDataMgr TheInstance = new ConfigDataMgr();

        public static ConfigDataMgr Instance
        {
            get
            {
                return TheInstance;
            }
        }

        public IconsLoader IconLoader = new IconsLoader();

        public void Initialize()
        {
            LoadConfig();
            LoadAssets();
        }

        public void LoadConfig()
        {
            TextAsset ta = Resources.Load("TextAssets/Icons") as TextAsset;
            IconLoader.LoadConfigData(ta.text);
        }

        public void LoadAssets()
        {
            foreach (var item in IconLoader.Datas)
            {
                IconsAssetHolder.Instance.AddIcon(item.Key, item.Value.Path);
            }            
        }

        public void Clear()
        {

        }
    }
}
