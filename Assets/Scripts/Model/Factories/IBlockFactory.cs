using Model.Objects;

public interface IBlockFactory
{
    Block Create(IBlockType type, Cell cell);
}