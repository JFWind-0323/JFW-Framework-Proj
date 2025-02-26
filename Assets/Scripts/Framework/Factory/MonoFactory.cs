using UnityEngine;

namespace Framework.Factory
{
    public class MonoFactory<T> : AbstractFactory<T> where T : MonoBehaviour, IProduct
    {
        private Transform parent;

        public MonoFactory(Transform parent = null, params object[] args)
        {
            this.args = args;
            this.parent = parent;
            FactoryManager.Instance.RegisterFactory(this);
        }

        public override T Create()
        {
            var obj = new GameObject(typeof(T).Name).AddComponent<T>();
            PostProcess(obj);
            return obj;
        }

        protected override void PostProcess(T product)
        {
            if (parent)
                product.transform.SetParent(parent);
            product.Construct(args);
        }
    }
}