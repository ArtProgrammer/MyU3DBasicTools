using System;
using System.Collections.Generic;
using System.IO;

namespace Config {
    class NimeiLoader {
        public List<Nimei> DataList = new List<Nimei> ();

        public List<Nimei> LoadConfigData(string path) {
            using (StreamReader reader = new StreamReader(path, System.Text.Encoding.Default, false)) {
                while (!reader.EndOfStream) {
                    string str = reader.ReadLine();
                    string[] split = str.Split(',');
                    Nimei data = new Nimei();
                    int.TryParse(split[0], out data.ID);
                    data.Name= split[1];
                    data.Des= split[2];
                    DataList.Add(data);
                }
                reader.Close();
            }
            return DataList;
        }
    }
}
