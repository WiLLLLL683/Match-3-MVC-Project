using Data;
using Model.Objects;

namespace Model.Systems
{
    public interface ISpawnSystem : ISystem
    {
        void SpawnBonusBlock(ABlockType _type, Cell _cell);
        void SpawnTopLine();
    }
}