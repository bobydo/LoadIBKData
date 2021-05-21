using LoadIBKData.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace LoadIBKData.Common
{
    public static class ConvertToJsonFile
    {
        public static void ToJsonFile<T>(List<T> data, string fileName)
        {
            //https://stackoverflow.com/questions/20626849/how-to-append-a-json-file-without-disturbing-the-formatting/20627195
            //convert object to json string.
            string jsonData;
            if (File.Exists(fileName))
            {
                var getData = System.IO.File.ReadAllText(fileName);
                var priceList = JsonConvert.DeserializeObject<List<T>>(getData) ?? new List<T>();
                foreach (var p in data)
                    priceList.Add(p);
                jsonData = JsonConvert.SerializeObject(priceList);
                System.IO.File.WriteAllText(fileName, jsonData);
            }
            else
            {
                jsonData = JsonConvert.SerializeObject(data);
                using (TextWriter tw = new StreamWriter(fileName))
                {
                    tw.WriteLine(jsonData);
                }
            }
        }

        public static IList<T> JsonFileToObj<T>(string fileName)
        {
            List<T> list = new List<T>();
            //convert json string to objects
            using (TextReader tr = new StreamReader(fileName))
            {
                var objsString = tr.ReadToEnd();
                //https://stackoverflow.com/questions/33608473/unable-to-cast-object-of-type-newtonsoft-json-linq-jarray-to-type-system-coll
                JArray jsonResponse = JArray.Parse(objsString);
                foreach (var item in jsonResponse)
                {
                    T rowsResult = JsonConvert.DeserializeObject<T>(item.ToString());
                    list.Add(rowsResult);
                }
                return list;
            };
        }
    }
}
