using Framework.Factory;
using Framework.ObjectPool;

namespace Framework.PoolFactory
{
    public class StandardPoolFactory<T> : AbstractPoolFactory<T> where T : class, IPoolableProduct
    {
        public StandardPoolFactory(params object[] args) : base(args)
        {
            pool = new StandardPool<T>(args);
            factory = new StandardFactory<T>(args);
        }

        public override void Create()
        {
            T product = factory.Create();
            Return(product);
        }

        /// <summary>
        /// 从池中取出对象，如果池为空，则创建新对象并放入池中
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

        public override void Return(T product)
        {
            pool.Return(product);
        }
    }
}