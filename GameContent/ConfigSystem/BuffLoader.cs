using System;
using System.Collections.Generic;
using System.IO;

namespace Config {
    class BuffLoader {
        public Dictionary<int, Buff> Datas = new Dictionary<int, Buff> ();

        public Dictionary<int, Buff> LoadConfigData(string str) {
            string[] periods = str.Split('\n');
            int index = 0;
            while (index < periods.Length) {
                string[] split = periods[index].Split(',');
                if (split.Length == 3) {
                    Buff data = new Buff();
                    int.TryParse(split[0], out data.ID);
                    data.Name= split[1];
                    data.Path= split[2];
                    Datas.Add(data.ID, data);
                }
                index++;
                }
            return Datas;
        }
    }
}
