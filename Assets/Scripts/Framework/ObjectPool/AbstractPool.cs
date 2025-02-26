using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.ObjectPool
{
    public abstract class AbstractPool<T> : IPool<T> where T : class, IPoolable
    {
        public virtual Type ObjectType=> typeof(T);
        public int Count => pool.Count;
        protected Queue<T> pool= new Queue<T>();
        protected object[] args;
        public abstract T Get();

        public abstract void Return(IPoolable product);

        protected virtual void WarmPool(int count)
        {
            for (int i = 0; i < count; i++)
            {
                T product = Activator.CreateInstance(ObjectType, args) as T;
                if (product == null)
                {
                    Debug.LogError("Failed to create instance of " + ObjectType.Name);
                    continue;
                }
                product.Disable();
                pool.Enqueue(product);
            }
        }
    }
}