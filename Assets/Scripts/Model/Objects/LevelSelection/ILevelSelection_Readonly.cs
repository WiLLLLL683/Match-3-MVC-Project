using Data;

namespace Model.Readonly
{
    public interface ILevelSelection_Readonly
    {
        LevelConfig[] AllLevels { get; }
        LevelConfig CurrentLevelData { get; }
        int CurrentLevelIndex { get; }
    }
}