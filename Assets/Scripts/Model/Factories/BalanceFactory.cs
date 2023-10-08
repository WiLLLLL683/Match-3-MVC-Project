using Config;
using Model.Objects;
using System.Collections.Generic;

namespace Model.Factories
{
    public class BalanceFactory : IBalanceFactory
    {
        public Balance Create(BlockTypeSetSO balanceSO)
        {
            List<BlockType_Weight> weights = new();
            for (int i = 0; i < balanceSO.typeWeights.Count; i++)
            {
                weights.Add(new(balanceSO.typeWeights[i].blockTypeSO.type, balanceSO.typeWeights[i].weight));
            }

            var balance = new Balance();
            balance.SetWeights(weights);
            balance.defaultBlockType = balanceSO.defaultBlockType.type;
            return balance;
        }
    }
}