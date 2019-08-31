using System;
using System.Collections.Generic;
using System.IO;

namespace Config {
    class ItemLoader {
        public List<Item> DataList = new List<Item> ();

        public Dictionary<int, Item> DataDic = new Dictionary<int, Item>();

        public List<Item> LoadConfigData(string path) {
            using (StreamReader reader = new StreamReader(path, System.Text.Encoding.Default, false)) {
                while (!reader.EndOfStream) {
                    string str = reader.ReadLine();
                    string[] split = str.Split(',');
                    Item data = new Item();
                    int.TryParse(split[0], out data.ID);
                    data.Name= split[1];
                    data.Des= split[2];
                    DataList.Add(data);
                }
                reader.Close();
            }
            return DataList;
        }

        public Dictionary<int, Item> LoadConfigDataByTxt(string str)
        {
            string[] periods = str.Split('\n');

            int index = 0;
            while (index < periods.Length)
            {
                string[] split = periods[index].Split(',');
                if (split.Length == 3)
                {
                    Item data = new Item();
                    int.TryParse(split[0], out data.ID);
                    data.Name = split[1];
                    data.Des = split[2];

                    DataDic.Add(data.ID, data);
                }

                index++;
            }

            return DataDic;
        }
    }
}
