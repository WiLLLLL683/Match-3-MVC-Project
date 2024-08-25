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
        public bool IsActivatable => false;

        public async UniTask Activate(Vector2Int position, Directions direction) { }
    }
}