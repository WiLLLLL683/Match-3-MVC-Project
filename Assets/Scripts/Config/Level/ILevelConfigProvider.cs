namespace Config
{
    public interface ILevelConfigProvider
    {
        int LastLevelIndex { get; }

        LevelSO GetSO(int index);
    }
}