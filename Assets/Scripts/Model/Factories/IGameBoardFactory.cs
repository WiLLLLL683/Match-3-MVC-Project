using Data;
using Model.Objects;

namespace Model.Factories
{
    public interface IGameBoardFactory
    {
        GameBoard Create(LevelConfig.CellConfig config);
    }
}