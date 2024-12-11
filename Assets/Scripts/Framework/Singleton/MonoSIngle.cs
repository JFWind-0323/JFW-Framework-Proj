using UnityEngine;

namespace Framework.Singleton
{
    public abstract class MonoSingle<T> : MonoBehaviour where T : MonoSingle<T>
    {
        private static T instance;
        private static readonly object lockObject = new();

        public static T Instance
        {
            get
            {
                if (instance == null)
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            var obj = GameObject.Find("MonoSingle");
                            if (obj == null) obj = new GameObject("MonoSingle");

                            instance = obj.GetComponent<T>();
                            if (instance == null) instance = obj.AddComponent<T>();
                        }
                    }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject); // 保持单例在切换场景时不被销毁
                
            }
            else if (instance != this)
            {
                Destroy(gameObject); // 确保只存在一个实例
            }
            //DontDestroyOnLoad(Instance.gameObject);
        }

        protected virtual void OnDestroy()
        {
            if (instance == this) instance = null; // 清理引用
        }
    }
}