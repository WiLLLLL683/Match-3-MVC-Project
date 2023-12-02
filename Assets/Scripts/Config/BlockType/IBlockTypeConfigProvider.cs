using Model.Services;
using System.Collections.Generic;
using View;

namespace Config
{
    public interface IBlockTypeConfigProvider
    {
        BlockView Prefab { get; }
        BlockTypeSO GetSO(int id);
        List<BlockType_Weight> GetWeights();
    }
}