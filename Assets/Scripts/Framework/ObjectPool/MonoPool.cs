using UnityEngine;

namespace Framework.ObjectPool
{
    public class MonoPool<T> : AbstractPool<T> where T : MonoBehaviour, IPoolable
    {
        
        private T prefab;
        private Transform parent;

        public MonoPool(T prefab, Transform parent)
        {
            this.prefab = prefab;
            this.parent = parent;
            
            PoolManager.Instance.RegisterPool<T>(this);
        }

        protected override void WarmPool(int count)
        {
            for (int i = 0; i < count; i++)
            {
                //创建对象
                T obj = GameObject.Instantiate(prefab, parent);
                //初始不激活对象
                obj.Disable();
                //添加到池中等待被使用
                pool.Enqueue(obj);
            }
        }


        /// <summary>
        /// 获取对象
        /// </summary>
        /// <returns></returns>
        public override T Get()
        {
            if (pool.Count == 0)
            {
                WarmPool(1);
            }

            T obj = pool.Dequeue();
            obj.Enable();
            return obj;
        }

        /// <summary>
        /// 归还对象到池中
        /// </summary>
        /// <param dataName="obj"></param>
        public  override void Return(IPoolable obj)
        {
            //将对象设置为不激活
            obj.Disable();
            //将对象归还到池中
            pool.Enqueue(obj as T);
        }
        
    }
}