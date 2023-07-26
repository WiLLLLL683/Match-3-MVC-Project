using Data;

namespace Model.Readonly
{
    public interface ILevelSelection_Readonly
    {
        LevelData[] AllLevels { get; }
        LevelData CurrentLevelData { get; }
        int CurrentLevelIndex { get; }
    }
}