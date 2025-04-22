using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Framework.Singleton;
using UnityEditor;

namespace Framework.Factory
{
    public class PrefabFactory : Singleton<PrefabFactory>
    {
        private Dictionary<string, GameObject> prefabDict;

        public PrefabFactory()
        {
            prefabDict = new Dictionary<string, GameObject>();
            GameObject[] allPrefabs = Resources.LoadAll<GameObject>("Prefabs");
            foreach (GameObject prefab in allPrefabs)
            {
                string targetName;
                //如果预制体名称重复，则键为文件夹名称/预制体名称
                if (!prefabDict.TryAdd(prefab.name, prefab))
                {
                    string assetPath = AssetDatabase.GetAssetPath(prefab);
                    string parentFolderPath = Path.GetDirectoryName(assetPath);
                    string parentFolderName = Path.GetFileName(parentFolderPath);
                    targetName = parentFolderName + "/" + prefab.name;

                    prefabDict.Add(targetName, prefab);
                }
                
            }
        }

        /// <summary>
        /// 通过键：预制体名称获取预制体，如果预制体名称重复，则键为：父文件夹名称/预制体名称
        /// </summary>
        /// <param name="prefabKey"></param>
        /// <returns>预制体</returns>
        public GameObject GetPrefab(string prefabKey)
        {
            if (prefabDict.TryGetValue(prefabKey, out var value))
            {
                return value;
            }
            else
            {
                Debug.LogWarning("Prefab " + prefabKey + " not found ");
                return null;
            }
        }

        /// <summary>
        /// 通过键：预制体名称实例化预制体，如果预制体名称重复，则键为：父文件夹名称/预制体名称
        /// </summary>
        /// <param name="prefabKey"> 预制体名称</param>
        /// <param name="defaultActive"> 是否默认激活</param>
        /// <param name="parent"> 父物体</param>
        /// <param name="worldPositionStays"> 是否保持世界坐标</param>
        /// <returns></returns>
        public GameObject Create(string prefabKey, bool defaultActive = true, Transform parent = null,
            bool worldPositionStays = false)
        {
            if (prefabDict.TryGetValue(prefabKey, out var value))
            {
                var go = GameObject.Instantiate(value, parent, worldPositionStays);
                go.SetActive(defaultActive);
                return go;
            }

            Debug.LogWarning("Prefab " + prefabKey + " not found ");
            return null;
        }

        public List<string> GetAllKeys()
        {
            return new List<string>(prefabDict.Keys);
        }
    }
}