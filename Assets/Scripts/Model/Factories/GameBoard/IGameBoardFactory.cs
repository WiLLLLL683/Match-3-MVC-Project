using Config;
using Model.Objects;

namespace Model.Factories
{
    public interface IGameBoardFactory
    {
        GameBoard Create(LevelSO config);
    }
}