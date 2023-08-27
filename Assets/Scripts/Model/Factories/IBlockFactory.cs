using Model.Objects;

namespace Model.Factories
{
    public interface IBlockFactory
    {
        Block Create(IBlockType type, Cell cell);
    }
}