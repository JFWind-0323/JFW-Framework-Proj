using UnityEngine;

namespace Framework.Singleton
{
    public abstract class MonoSingle<T> : MonoBehaviour where T : MonoSingle<T>
    {
        /*
         * Monobehaviour 单例
         * 调用时自动创建
         */
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

        private void Awake()
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
            WhenInit();
            //DontDestroyOnLoad(Instance.gameObject);
        }

        private void OnDestroy()
        {
            if (instance == this) instance = null; // 清理引用
            WhenDestroy();
        }

        protected virtual void WhenInit()
        {
            
        }

        protected virtual void WhenDestroy()
        {
            
        }
        
    }
}