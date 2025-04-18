using Framework.Factory;
using Framework.ObjectPool;

namespace Framework.PoolFactory
{
    public abstract class AbstractPoolFactory<T> : IPoolFactory<T> where T : class, IPoolableProduct
    {
        /*
         * 由工厂统一创建对象，并将对象放入池中，供其他地方使用
         */
        protected AbstractPool<T> pool;
        protected AbstractFactory<T> factory;
        protected object[] args;

        protected AbstractPoolFactory(params object[] args)
        {
            this.args = args;
        }

        public abstract void Create();

        public void UpdateArgs(params object[] args)
        {
            this.args = args;
            factory.UpdateArgs(args);
        }

        public abstract T Get();
        public abstract void Return(T obj);
    }
}