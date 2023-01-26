using Model.Objects;

namespace Model.Systems
{
    public interface ISpawnSystem
    {
        void SpawnBonusBlock(ABlockType _type, Cell _cell);
        void SpawnTopLine();
        void SetLevel(Level level);
    }
}