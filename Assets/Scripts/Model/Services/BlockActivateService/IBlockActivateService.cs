using UnityEngine;

namespace Model.Services
{
    public interface IBlockActivateService
    {
        bool TryActivateBlock(Vector2Int position);
    }
}