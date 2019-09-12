using System;
using System.Collections.Generic;
using System.IO;

namespace Config {
    class NPCConfigLoader {
        public Dictionary<int, NPCConfig> Datas = new Dictionary<int, NPCConfig> ();

        public Dictionary<int, NPCConfig> LoadConfigData(string str) {
            string[] periods = str.Split('\n');
            int index = 0;
            while (index < periods.Length) {
                string[] split = periods[index].Split(',');
                if (split.Length == 12) {
                    NPCConfig data = new NPCConfig();
                    int.TryParse(split[0], out data.ID);
                    data.Name= split[1];
                    int.TryParse(split[2], out data.IconID);
                    int.TryParse(split[3], out data.PrefabID);
                    int.TryParse(split[4], out data.Kind);
                    int.TryParse(split[5], out data.Xue);
                    int.TryParse(split[6], out data.Qi);
                    float.TryParse(split[7], out data.Speed);
                    int.TryParse(split[8], out data.Defence);
                    int.TryParse(split[9], out data.Attack);
                    int.TryParse(split[10], out data.YinYang);
                    int.TryParse(split[11], out data.WuXing);
                    Datas.Add(data.ID, data);
                }
                index++;
                }
            return Datas;
        }
        public NPCConfig GetDataByID(int id) {
            if (Datas.ContainsKey(id)) { 
                return Datas[id];
            }
            return null;
        }
    }
}
