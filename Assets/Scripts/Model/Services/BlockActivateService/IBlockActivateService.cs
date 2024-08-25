using Cysharp.Threading.Tasks;
using Model.Objects;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model.Services
{
    public interface IBlockActivateService
    {
        UniTask ActivateBlock(Vector2Int position, Directions direction);
        UniTask ActivateMarkedBlocks();
        List<Block> FindMarkedBlocks();
    }
}