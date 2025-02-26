using System;
using System.Collections.Generic;
using Framework.Singleton;
using UnityEngine;

namespace Framework.PoolFactory
{
    public class PoolFactoryManager : Singleton<PoolFactoryManager>
    {
        private Dictionary<Type, IPoolFactory<IPoolableProduct>> poolFactories =
            new Dictionary<Type, IPoolFactory<IPoolableProduct>>();

        /// <summary>
        /// 注册池工厂
        /// </summary>
        /// <param name="poolFactory"> 池工厂 </param>
        /// <typeparam name="T"> 池对象类型 </typeparam>
        public void RegisterPoolFactory<T>(IPoolFactory<IPoolableProduct> poolFactory) where T : class, IPoolableProduct
        {
            poolFactories.Add(typeof(T), poolFactory);
        }

        /// <summary>
        /// 通过工厂创建MonoBehaviour对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        public void CreateMono<T>() where T : MonoBehaviour, IPoolableProduct
        {
            if (!poolFactories.ContainsKey(typeof(T)))
            {
                MonoPoolFactory<T> monoPoolFactory = new MonoPoolFactory<T>();
                RegisterPoolFactory<T>(monoPoolFactory);
            }

            poolFactories[typeof(T)].Create();
        }

        /// <summary>
        /// 通过工厂创建普通对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        public void CreateStandard<T>() where T : class, IPoolableProduct
        {
            if (!poolFactories.ContainsKey(typeof(T)))
            {
                StandardPoolFactory<T> standardPoolFactory = new StandardPoolFactory<T>();
                RegisterPoolFactory<T>(standardPoolFactory);
            }

            poolFactories[typeof(T)].Create();
        }

        /// <summary>
        /// 获取MonoBehaviour对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns>对象</returns>
        public T GetMono<T>() where T : MonoBehaviour, IPoolableProduct
        {
            if (!poolFactories.ContainsKey(typeof(T)))
            {
                CreateMono<T>();
            }
            return (T)poolFactories[typeof(T)].Get();
        }

        /// <summary>
        /// 获取普通对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <returns>对象</returns>
        public T GetStandard<T>() where T : class, IPoolableProduct
        {
            if (!poolFactories.ContainsKey(typeof(T)))
            {
               CreateStandard<T>();
            }

            return (T)poolFactories[typeof(T)].Get();
        }
    }
}