﻿using System;
using System.Collections.Generic;

using GameContent.Item;
using GameContent.UsableItem;

namespace GameContent
{
    interface IConfigMgr
    {
        int TotalCount { set; get; }
        void Load();
    }

    /// <summary>
    /// Generated by scripts.
    /// </summary>
    class SkillCfgData : BaseUsableData
    {
    }

    /// <summary>
    /// Generated by scripts.
    /// </summary>
    class SkillCfgDataLoader
    {
        // this can not afford to the change of 
        // the count of rows in config file update only.
        // should be change for flexibility.
        public static int Count;

        public SkillCfgData LoadSingleData(int row)
        {
            return null;
        }
    }
    
    /// <summary>
    /// This is defined by programmer.
    /// </summary>
    class SkillCfgDataMgr : IConfigMgr
    {
        public int TotalCount { set; get; }

        private Dictionary<int, SkillCfgData> CfgDatas =
            new Dictionary<int, SkillCfgData>();

        public void Load()
        {
            SkillCfgDataLoader scdl = new SkillCfgDataLoader();
            for (int i = 0; i < SkillCfgDataLoader.Count; i++)
            {
                SkillCfgData scdata = scdl.LoadSingleData(i);
                scdata.Catalog = UsableCatalog.Skill;
                CfgDatas.Add(scdata.ID, scdata);
            }
        }

        public SkillCfgData GetCfgDataByID(int id)
        {
            if (CfgDatas.ContainsKey(id))
            {
                return CfgDatas[id];
            }

            return null;
        }
    }

    enum CfgMgrType
    {
        Skill,
        Item
    }

    class ConfigManager
    {
        private static ConfigManager TheInstance = new ConfigManager();

        public static ConfigManager Instance
        {
            get
            {
                return TheInstance;
            }
        }

        public Dictionary<CfgMgrType, IConfigMgr> CfgMgrs =
            new Dictionary<CfgMgrType, IConfigMgr>();

        public void AddLoader(CfgMgrType cmtype, IConfigMgr loader)
        {
            if (!CfgMgrs.ContainsKey(cmtype))
            {
                CfgMgrs.Add(cmtype, loader);
            }
        }

        public void Load()
        {
            foreach (var item in CfgMgrs)
            {
                item.Value.Load();
            }
        }

        public IConfigMgr GetCfgMgrByType(CfgMgrType cmtype)
        {
            if (CfgMgrs.ContainsKey(cmtype))
            {
                return CfgMgrs[cmtype];
            }

            return null;
        }
    }
}
