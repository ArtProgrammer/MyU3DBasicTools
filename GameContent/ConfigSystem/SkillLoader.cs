using System;
using System.Collections.Generic;
using System.IO;

namespace Config {
    class SkillLoader {
        public Dictionary<int, Skill> Datas = new Dictionary<int, Skill> ();

        public Dictionary<int, Skill> LoadConfigData(string str) {
            string[] periods = str.Split('\n');
            int index = 0;
            while (index < periods.Length) {
                string[] split = periods[index].Split(',');
                if (split.Length == 3) {
                    Skill data = new Skill();
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
