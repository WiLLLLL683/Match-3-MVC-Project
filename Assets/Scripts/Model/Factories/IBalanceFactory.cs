using Data;
using Model.Objects;

public interface IBalanceFactory
{
    Balance Create(BalanceSO balanceSO);
}