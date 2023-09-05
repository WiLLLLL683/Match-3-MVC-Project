using Config;
using Model.Objects;

namespace Model.Factories
{
    public interface ILevelFactory
    {
        Level Create(LevelSO levelData);
    }
}