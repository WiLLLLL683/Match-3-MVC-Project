using Data;
using Model.Objects;

namespace Model.Factories
{
    public interface ILevelFactory
    {
        Level Create(LevelConfig levelData);
    }
}