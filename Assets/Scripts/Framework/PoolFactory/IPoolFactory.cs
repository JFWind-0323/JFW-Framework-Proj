using Framework.Factory;
using Framework.ObjectPool;

namespace Framework.PoolFactory
{
    public interface IPoolFactory<out T> where T : IProduct,IPoolable
    {
        void Create();
        void UpdateArgs(params object[] args);
        T Get();
    }
}