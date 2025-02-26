namespace Framework.Factory
{
    public interface IFactory<out T> where T :IProduct
    {
        T Create();
    }
}