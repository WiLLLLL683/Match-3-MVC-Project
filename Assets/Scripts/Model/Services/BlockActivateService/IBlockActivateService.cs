using Cysharp.Threading.Tasks;
using UnityEngine;
using Utils;

namespace Model.Services
{
    public interface IBlockActivateService
    {
        UniTask<bool> TryActivateBlock(Vector2Int position, Directions direction);
    }
}