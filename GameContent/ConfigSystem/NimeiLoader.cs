using System;
using System.Collections.Generic;
using System.IO;

namespace Config {
    class NimeiLoader {
        public Dictionary<int, Nimei> Datas = new Dictionary<int, Nimei> ();

        public Dictionary<int, Nimei> LoadConfigData(string str) {
            string[] periods = str.Split('\n');
            int index = 0;
            while (index < periods.Length) {
                string[] split = periods[index].Split(',');
                if (split.Length == 3) {
                    Nimei data = new Nimei();
                    int.TryParse(split[0], out data.ID);
                    data.Name= split[1];
                    data.Des= split[2];
                    Datas.Add(data.ID, data);
                }
                index++;
                }
            return Datas;
        }
    }
}
