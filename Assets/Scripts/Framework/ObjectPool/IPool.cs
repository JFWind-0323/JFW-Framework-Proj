using System;
namespace Framework.ObjectPool
{
    public interface IPool<out T> where T : IPoolable
    {
        Type ObjectType { get; }
        T Get();
        void Return(IPoolable product);
        
    }
}