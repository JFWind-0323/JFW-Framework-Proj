using System;
using System.Collections.Generic;
using Framework.Singleton;

namespace Framework.ObjectPool
{
    public class PoolManager : Singleton<PoolManager>
    {
        private Dictionary<Type, IPool<IPoolable>> Pools = new Dictionary<Type, IPool<IPoolable>>();

        /// <summary>
        /// 注册对象池
        /// </summary>
        /// <param name="pool">对象池</param>
        /// <typeparam name="T">对象类型</typeparam>
        public void RegisterPool<T>(IPool<IPoolable> pool) where T : IPoolable
        {
            if (!Pools.ContainsKey(typeof(T)))
                Pools.Add(typeof(T), pool);
        }

        /// <summary>
        /// 获取对象池
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns></returns>
        public IPool<T> GetPool<T>() where T : IPoolable
        {
            if (Pools.ContainsKey(typeof(T)))
            {
                return (IPool<T>)Pools[typeof(T)];
            }

            return null;
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns></returns>
        public T Get<T>() where T : IPoolable
        {
            if (Pools.ContainsKey(typeof(T)))
            {
                return (T)Pools[typeof(T)].Get();
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        /// <param name="obj">对象</param>
        /// <typeparam name="T"></typeparam>
        public void Return<T>(IPoolable obj) where T : IPoolable
        {
            Pools[typeof(T)].Return(obj);
        }

        /// <summary>
        /// 清空所有对象池
        /// </summary>
        public void Clear()
        {
            Pools.Clear();
        }
    }
}