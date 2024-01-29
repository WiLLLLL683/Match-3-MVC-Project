using Model.Objects;

namespace Model.Factories
{
    public interface IBoosterFactory
    {
        IBooster Create(int id);
    }
}