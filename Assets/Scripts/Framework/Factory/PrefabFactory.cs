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
                string targetName = prefab.name;
                if (!prefabDict.TryAdd(prefab.name, prefab))
                {
                    string assetPath = AssetDatabase.GetAssetPath(prefab);
                    string parentFolderPath = Path.GetDirectoryName(assetPath);
                    string parentFolderName = Path.GetFileName(parentFolderPath);
                    targetName = parentFolderName + "/" + prefab.name;

                    prefabDict.Add(targetName, prefab);
                }

                Debug.Log(targetName);
            }
        }

        public GameObject Get(string prefabName)
        {
            if (prefabDict.TryGetValue(prefabName, out var value))
            {
                return value;
            }
            else
            {
                Debug.LogWarning("Prefab " + prefabName + " not found ");
                return null;
            }
        }

        public GameObject Create(string prefabName)
        {
            if (prefabDict.TryGetValue(prefabName, out var value))
            {
                GameObject.Instantiate(value);
                return value;
            }

            Debug.LogWarning("Prefab " + prefabName + " not found ");
            return null;
        }
    }
}