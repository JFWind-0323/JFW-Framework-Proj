using Framework.PoolFactory;
using UnityEngine;

namespace Framework.Factory
{
    public class MonoProduct : MonoBehaviour, IPoolableProduct
    {
        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        public virtual void Construct(params object[] args)
        {
            gameObject.AddComponent<Rigidbody>();
            Debug.Log("MonoProduct Construct");
        }

        public void DoSomething()
        {
            Debug.Log("MonoProduct DoSomething");
        }
    }
}