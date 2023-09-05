using Config;

namespace Model.Readonly
{
    public interface ILevelSelection_Readonly
    {
        LevelSO[] AllLevels { get; }
        LevelSO CurrentLevelData { get; }
        int CurrentLevelIndex { get; }
    }
}