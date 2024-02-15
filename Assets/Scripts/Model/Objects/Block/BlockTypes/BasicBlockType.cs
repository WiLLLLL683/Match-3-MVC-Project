using Cysharp.Threading.Tasks;
using Model.Services;
using System;
using UnityEngine;
using Utils;

namespace Model.Objects
{
    /// <summary>
    /// Базовый тип блока, без действия по активации
    /// </summary>
    [Serializable]
    public class BasicBlockType : IBlockType
    {
        [field: SerializeField] public int Id { get; set; }

        public async UniTask<bool> Activate(Vector2Int position, Directions direction, BlockTypeDependencies dependencies) => false;

        public IBlockType Clone() => (IBlockType)MemberwiseClone();
    }
}