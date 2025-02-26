using Framework.Factory;
using Framework.ObjectPool;

namespace Framework.PoolFactory
{
    public interface IPoolableProduct: IPoolable, IProduct
    {
        
    }
}