using Config;
using Model.Objects;

namespace Model.Factories
{
    public interface IBalanceFactory
    {
        Balance Create(BalanceSO balanceSO);
    }
}