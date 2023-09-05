using Model.Objects;

namespace Model.Factories
{
    public class BlockFactory : IBlockFactory
    {
        public Block Create(IBlockType type, Cell cell)
        {
            return new Block(type, cell);
        }
    }
}