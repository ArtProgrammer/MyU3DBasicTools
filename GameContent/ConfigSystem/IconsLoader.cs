using System;
using System.Collections.Generic;
using System.IO;

namespace Config {
    class IconsLoader {
        public Dictionary<int, Icons> Datas = new Dictionary<int, Icons> ();

        public Dictionary<int, Icons> LoadConfigData(string str) {
            string[] periods = str.Split('\n');
            int index = 0;
            while (index < periods.Length) {
                string[] split = periods[index].Split(',');
                if (split.Length == 3) {
                    Icons data = new Icons();
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
