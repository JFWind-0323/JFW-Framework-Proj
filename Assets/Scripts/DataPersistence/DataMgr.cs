using System.IO;
using Singleton;
using UnityEngine;

namespace DataPersistence
{
    public class DataMgr : Singleton<DataMgr>
    {
        private readonly GameData gameData = new();
        private readonly string jsonpath = Application.persistentDataPath;

        public void SaveData(int count)
        {
            gameData.Count = count;
            var json = JsonUtility.ToJson(gameData);
            var path = jsonpath + $"/GameData{count}.json";
            File.WriteAllText(path, json);
            Debug.Log("Save Data Success:" + path);
        }

        public GameData LoadData(int count)
        {
            var data = JsonUtility.FromJson<GameData>(File.ReadAllText(jsonpath + $"/GameData{count}.json"));
            return data;
        }
    }
}