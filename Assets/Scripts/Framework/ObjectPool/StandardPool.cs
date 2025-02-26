namespace Framework.ObjectPool
{
    public class StandardPool<T>:AbstractPool<T> where T : class, IPoolable
    {
        public StandardPool(params object[] args)
        {
            this.args = args;
            PoolManager.Instance.RegisterPool<T>(this);
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
        /// 回收对象
        /// </summary>
        /// <param name="product"></param>
        public override void Return(IPoolable product)
        {
            product.Disable();
            pool.Enqueue(product as T);
        }
    }
}