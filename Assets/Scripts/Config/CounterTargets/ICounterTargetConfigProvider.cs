namespace Config
{
    public interface ICounterTargetConfigProvider
    {
        ACounterTargetSO GetSO(int id);
    }
}