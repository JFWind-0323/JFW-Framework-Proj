using System.Collections.Generic;
using Framework.Factory;
using Framework.Singleton;
using UnityEngine;

namespace Framework.PoolFactory
{
    public class PrefabFactoryPool : Singleton<PrefabFactoryPool>
    {
        private Dictionary<string, Queue<GameObject>> poolDict = new();

        public PrefabFactoryPool()
        {
            foreach (var keys in PrefabFactory.Instance.GetAllKeys())
            {
                poolDict.Add(keys, new Queue<GameObject>());
            }
        }

        /// <summary>
        /// 通过键：预制体名称 创建对象，如果预制体名称重复，则键为：父文件夹名称/预制体名称
        /// </summary>
        /// <param name="prefabNameKey">预制体键名</param>
        /// <param name="defaultActive">是否默认激活</param>
        /// <param name="parent">父物体</param>
        /// <param name="worldPositionStays">是否保持世界坐标</param>
        public GameObject Creat(string prefabNameKey, bool defaultActive = true, Transform parent = null,
            bool worldPositionStays = false)
        {
            var obj = PrefabFactory.Instance.Create(prefabNameKey, defaultActive, parent, worldPositionStays);
            //添加到池中等待被使用
            poolDict[prefabNameKey].Enqueue(obj);
            return obj;
        }


        /// <summary>
        /// 获取预制体，如果池中没有，则创建新的预制体
        /// </summary>
        /// <returns></returns>
        public GameObject Get(string prefabNameKey, bool defaultActive = false, Transform parent = null,bool worldPositionStays = false)
        {
            if (!poolDict.ContainsKey(prefabNameKey))
            {
                Debug.LogError($"不存在名为:{prefabNameKey}的预制体");
            }

            if (poolDict[prefabNameKey].Count == 0)
            {
                Creat(prefabNameKey, defaultActive, parent, worldPositionStays);
            }

            GameObject obj = poolDict[prefabNameKey].Dequeue();
            obj.gameObject.SetActive(defaultActive);
            return obj;
        }

        public void Return(GameObject obj)
        {
            string prefabNameKey = obj.name.Replace("(Clone)", "");

            //将对象设置为不激活
            obj.gameObject.SetActive(false);
            //将对象归还到池中
            poolDict[prefabNameKey].Enqueue(obj);
        }
    }
}