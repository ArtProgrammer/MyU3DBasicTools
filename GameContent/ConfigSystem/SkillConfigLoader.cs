using System;
using System.Collections.Generic;
using System.IO;

namespace Config {
    class SkillConfigLoader {
        public Dictionary<int, SkillConfig> Datas = new Dictionary<int, SkillConfig> ();

        public Dictionary<int, SkillConfig> LoadConfigData(string str) {
            string[] periods = str.Split('\n');
            int index = 0;
            while (index < periods.Length) {
                string[] split = periods[index].Split(',');
                if (split.Length == 14) {
                    SkillConfig data = new SkillConfig();
                    int.TryParse(split[0], out data.ID);
                    data.Name= split[1];
                    int.TryParse(split[2], out data.Kind);
                    int.TryParse(split[3], out data.BuffID);
                    int.TryParse(split[4], out data.TargetType);
                    int.TryParse(split[5], out data.Cost);
                    int.TryParse(split[6], out data.CostType);
                    int.TryParse(split[7], out data.EffectID);
                    float.TryParse(split[8], out data.UseRange);
                    float.TryParse(split[9], out data.EffectRange);
                    float.TryParse(split[10], out data.CoolDown);
                    int.TryParse(split[11], out data.IconID);
                    float.TryParse(split[12], out data.DelayTime);
                    float.TryParse(split[13], out data.LastTime);
                    Datas.Add(data.ID, data);
                }
                index++;
                }
            return Datas;
        }
        public SkillConfig GetDataByID(int id) {
            if (Datas.ContainsKey(id)) { 
                return Datas[id];
            }
            return null;
        }
    }
}
