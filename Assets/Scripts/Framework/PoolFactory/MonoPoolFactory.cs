using Framework.Factory;
using Framework.ObjectPool;
using UnityEngine;

namespace Framework.PoolFactory
{
    public class MonoPoolFactory<T> : AbstractPoolFactory<T> where T : MonoBehaviour, IPoolableProduct
    {
        public MonoPoolFactory(T prefab=null, Transform parent = null, params object[] args) : base(args)
        {
            pool = new MonoPool<T>(prefab, parent);
            factory = new MonoFactory<T>(parent, args);
        }

        public override void Create()
        {
            T obj = factory.Create();
            Return(obj);
        }

        public override T Get()
        {
            Create();
            return pool.Get();
        }

        public override void Return(T obj)
        {
            pool.Return(obj);
        }
    }
}