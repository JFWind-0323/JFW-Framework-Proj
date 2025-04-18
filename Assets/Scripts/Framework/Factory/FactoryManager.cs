using System;
using System.Collections.Generic;
using Framework.Singleton;

namespace Framework.Factory
{
    public class FactoryManager : Singleton<FactoryManager>
    {
        private Dictionary<Type, IFactory<IProduct>> factories =
            new Dictionary<Type, IFactory<IProduct>>();

        /// <summary>
        /// 注册工厂
        /// </summary>
        /// <param dataName="factory">工厂</param>
        /// <typeparam dataName="T">产品对象类型</typeparam>
        public void RegisterFactory<T>(IFactory<T> factory) where T : IProduct
        {
            // Debug.Log(factory);
            // Debug.Log(typeof(T));
            // Debug.Log(factory as IFactory<IProduct>);
            factories.Add(typeof(T), factory as IFactory<IProduct>);
        }

        /// <summary>
        /// 创建产品对象
        /// </summary>
        /// <typeparam dataName="T">产品对象类型</typeparam>
        /// <returns> 创建的对象 </returns>
        /// <exception cref="ArgumentException"> 该类型未注册工厂 </exception>
        public T Create<T>()
        {
            if (factories.ContainsKey(typeof(T)))
            {
                return (T)factories[typeof(T)].Create();
            }
            else
            {
                throw new ArgumentException("该类型未注册工厂：" + typeof(T));
            }
        }
    }
}