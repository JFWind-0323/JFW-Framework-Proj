using System.IO;
using Framework.Singleton;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Framework.DataPersistence
{
    public class DataMgr : Singleton<DataMgr>
    {
        public string dataPath = Application.dataPath + "/Data";

        private Dictionary<string, object> CreateDataIntance(Dictionary<string, object> data)
        {
            return data;
        }

        public void SaveData(Dictionary<string, object> data, string fileName)
        {
            string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
            string filePath = $"{dataPath}/{fileName}.json";
            Debug.Log("正在保存至：" + filePath);
            File.WriteAllText(filePath, jsonData);
            Debug.Log("保存成功");
        }

        public Dictionary<string, object> LoadData(string fileName)
        {
            string filePath = $"{dataPath}/{fileName}.json";
            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);
                var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData);
                return data;
            }

            Debug.LogError("文件不存在：" + filePath);
            return null;
        }
    }
}