using System;

namespace Framework.Factory
{
    public class StandardFactory<T> : AbstractFactory<T> where T : class, IProduct
    {
        public StandardFactory(params object[] args)
        {
            this.args = args;
            FactoryManager.Instance.RegisterFactory(this);
        }
        /// <summary>
        /// 创建产品
        /// </summary>
        /// <returns></returns>
        public override T Create()
        {
            var product = Activator.CreateInstance(typeof(T), args) as T;
            PostProcess(product);
            return product;
        }

        
        protected override void PostProcess(T product)
        {
            product.Construct(args);
        }
    }
}