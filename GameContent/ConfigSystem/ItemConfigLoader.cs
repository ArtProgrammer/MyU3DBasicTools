using System;
using System.Collections.Generic;
using System.IO;

namespace Config {
    class ItemConfigLoader {
        public Dictionary<int, ItemConfig> Datas = new Dictionary<int, ItemConfig> ();

        public Dictionary<int, ItemConfig> LoadConfigData(string str) {
            string[] periods = str.Split('\n');
            int index = 0;
            while (index < periods.Length) {
                string[] split = periods[index].Split(',');
                if (split.Length == 8) {
                    ItemConfig data = new ItemConfig();
                    int.TryParse(split[0], out data.ID);
                    data.Name= split[1];
                    data.Des= split[2];
                    int.TryParse(split[3], out data.IconID);
                    int.TryParse(split[4], out data.PrefabID);
                    int.TryParse(split[6], out data.Kind);
                    int.TryParse(split[7], out data.TargetType);
                    Datas.Add(data.ID, data);
                }
                index++;
                }
            return Datas;
        }
        public ItemConfig GetDataByID(int id) {
            if (Datas.ContainsKey(id)) { 
                return Datas[id];
            }
            return null;
        }
    }
}
