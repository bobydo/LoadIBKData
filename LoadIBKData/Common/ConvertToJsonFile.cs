using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;

namespace LoadIBKData.Common
{
    public static class ConvertToJsonFile
    {
        public static void ToJsonFile<T>(T data, string fileName)
        {
            //convert object to json string.
            string json = JsonConvert.SerializeObject(data);
            if (!File.Exists(fileName))
            {
                using (TextWriter tw = new StreamWriter(fileName))
                {
                    tw.WriteLine(json);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(fileName))
                {
                    sw.WriteLine(json);
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
