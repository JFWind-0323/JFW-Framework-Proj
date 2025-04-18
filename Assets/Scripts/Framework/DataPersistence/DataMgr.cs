using System.IO;
using Framework.Singleton;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Framework.DataPersistence
{
    /*
     * 为了保证架构的通用性，我没有自定义一个类进行JSON的序列化和反序列化
     * 而是选择直接使用键值对的形式（参考了VS Code 的配置文件格式）
     * 这样做的好处是灵活通用，坏处是数据不够稳定（因为没有模板数据类）
     * 如果需要更严格的类型检查，可以考虑使用自定义的类进行序列化和反序列化
     */
    /// <summary>
    /// 数据管理器类，继承自单例模式，用于处理数据的保存和加载。
    /// </summary>
    public class DataMgr : Singleton<DataMgr>
    {
        private readonly string dataPath = Path.Combine(Application.dataPath, "Data");
        
        /// <summary>
        /// 将字典类型的数据保存为JSON文件。
        /// </summary>
        /// <param dataName="data">要保存的数据字典。</param>
        /// <param dataName="fileName">保存的文件名。</param>
        public void SaveDataByDictionary(Dictionary<string, object> data, string fileName)
        {
            if (data == null)
            {
                Debug.LogError("尝试保存空数据");
                return;
            }

            if (string.IsNullOrEmpty(fileName))
            {
                Debug.LogError("文件名不能为空");
                return;
            }

            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }

            try
            {
                string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
                string filePath = Path.Combine(dataPath, $"{fileName}.json");
                Debug.Log("正在保存至：" + filePath);
                File.WriteAllText(filePath, jsonData);
                Debug.Log("保存成功");
            }
            catch (System.Exception ex)
            {
                Debug.LogError("保存数据时出错: " + ex.Message);
            }
        }

        /// <summary>
        /// 从指定的JSON文件加载数据到字典。
        /// </summary>
        /// <param dataName="fileName">要加载的文件名。</param>
        /// <returns>加载到的数据字典，如果文件不存在或发生错误则返回null。</returns>
        public Dictionary<string, object> LoadDataByDictionary(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                Debug.LogError("文件名不能为空");
                return null;
            }

            string filePath = Path.Combine(dataPath, $"{fileName}.json");
            if (File.Exists(filePath))
            {
                try
                {
                    string jsonData = File.ReadAllText(filePath);
                    var data = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData);
                    return data;
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("加载数据时出错: " + ex.Message);
                }
            }
            else
            {
                Debug.LogError("文件不存在：" + filePath);
            }

            return null;
        }
    }
}
