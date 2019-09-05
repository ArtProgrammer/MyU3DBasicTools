using System;
using System.Collections.Generic;
using System.IO;

namespace Config {
    class BuffConfigLoader {
        public Dictionary<int, BuffConfig> Datas = new Dictionary<int, BuffConfig> ();

        public Dictionary<int, BuffConfig> LoadConfigData(string str) {
            string[] periods = str.Split('\n');
            int index = 0;
            while (index < periods.Length) {
                string[] split = periods[index].Split(',');
                if (split.Length == 8) {
                    BuffConfig data = new BuffConfig();
                    int.TryParse(split[0], out data.ID);
                    data.Name= split[1];
                    float.TryParse(split[2], out data.Delay);
                    float.TryParse(split[3], out data.LifeTime);
                    float.TryParse(split[4], out data.Duration);
                    int.TryParse(split[5], out data.KindType);
                    float.TryParse(split[6], out data.MaxTime);
                    int.TryParse(split[7], out data.EffectID);
                    Datas.Add(data.ID, data);
                }
                index++;
                }
            return Datas;
        }
        public BuffConfig GetDataByID(int id) {
            if (Datas.ContainsKey(id)) { 
                return Datas[id];
            }
            return null;
        }
    }
}
