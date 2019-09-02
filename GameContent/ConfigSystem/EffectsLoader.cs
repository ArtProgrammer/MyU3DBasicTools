using System;
using System.Collections.Generic;
using System.IO;

namespace Config {
    class EffectsLoader {
        public Dictionary<int, Effects> Datas = new Dictionary<int, Effects> ();

        public Dictionary<int, Effects> LoadConfigData(string str) {
            string[] periods = str.Split('\n');
            int index = 0;
            while (index < periods.Length) {
                string[] split = periods[index].Split(',');
                if (split.Length == 3) {
                    Effects data = new Effects();
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
