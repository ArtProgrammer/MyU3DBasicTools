using System;
using System.Collections.Generic;
using System.IO;

namespace Config {
    class EffectsConfigLoader {
        public Dictionary<int, EffectsConfig> Datas = new Dictionary<int, EffectsConfig> ();

        public Dictionary<int, EffectsConfig> LoadConfigData(string str) {
            string[] periods = str.Split('\n');
            int index = 0;
            while (index < periods.Length) {
                string[] split = periods[index].Split(',');
                if (split.Length == 3) {
                    EffectsConfig data = new EffectsConfig();
                    int.TryParse(split[0], out data.ID);
                    data.Name= split[1];
                    data.Path= split[2];
                    Datas.Add(data.ID, data);
                }
                index++;
                }
            return Datas;
        }
        public EffectsConfig GetDataByID(int id) {
            if (Datas.ContainsKey(id)) { 
                return Datas[id];
            }
            return null;
        }
    }
}
