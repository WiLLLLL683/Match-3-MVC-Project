using Model.Objects;

namespace Model.Readonly
{
    public interface IBoosterInventory_Readonly
    {
        int GetBoosterAmount<T>() where T : IBooster;
    }
}