using Framework.Factory;
using Framework.ObjectPool;
using UnityEngine;

namespace Framework.PoolFactory
{
    public class MonoPoolFactory<T> : AbstractPoolFactory<T> where T : MonoBehaviour, IPoolableProduct
    {
        public MonoPoolFactory(params object[] args) : base(args)
        {
            T prefab = null;
            Transform parent = null;
            if (args.Length >= 2)
            {
                prefab = args[0] as T;
                parent = args[1] as Transform;
            }

            pool = new MonoPool<T>(prefab, parent);
            factory = new MonoFactory<T>(parent, args);
        }
        /// <summary>
        /// 创建对象并放入对象池中，不返回
        /// </summary>

        public override void Create()
        {
            T obj = factory.Create();
            Return(obj);
        }
        /// <summary>
        /// 获取对象，如果对象池为空，则创建对象并返回
        /// </summary>
        /// <returns></returns>

        public override T Get()
        {
            if (pool.Count == 0)
            {
                Create();
            }

            return pool.Get();
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="obj"></param>
        public override void Return(T obj)
        {
            pool.Return(obj);
        }
        
    }
}