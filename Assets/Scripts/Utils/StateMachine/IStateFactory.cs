namespace Utils
{
    public interface IStateFactory
    {
        T Create<T>() where T : IExitableState;
    }
}