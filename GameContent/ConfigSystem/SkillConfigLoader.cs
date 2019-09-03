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
                if (split.Length == 3) {
                    SkillConfig data = new SkillConfig();
                    int.TryParse(split[0], out data.ID);
                    data.Name= split[1];
                    data.Path= split[2];
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
