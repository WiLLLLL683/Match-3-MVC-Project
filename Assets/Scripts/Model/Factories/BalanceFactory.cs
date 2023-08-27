using Data;
using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BalanceFactory : IBalanceFactory
{
    public Balance Create(BalanceSO balanceSO)
    {
        List<(IBlockType type, int weight)> weights = new();
        for (int i = 0; i < balanceSO.typeWeights.Count; i++)
        {
            weights.Add((balanceSO.typeWeights[i].blockTypeSO.blockType, balanceSO.typeWeights[i].weight));
        }

        var balance = new Balance();
        balance.SetWeights(weights);
        balance.defaultBlockType = balanceSO.defaultBlockType.blockType;
        return balance;
    }
}
