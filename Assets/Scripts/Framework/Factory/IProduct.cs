namespace Framework.Factory
{
    public interface IProduct
    {
        void Construct(params object[] args);
    }
}