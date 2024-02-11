using Cysharp.Threading.Tasks;
using Model.Services;
using System;
using UnityEngine;
using Utils;

namespace Model.Objects
{
    [Serializable]
    public class DestroyVerticalLineBlockType : IBlockType
    {
        [field: SerializeField] public int Id { get; set; }

        private readonly IBlockDestroyService destroyService;
        private bool isActivated;

        public DestroyVerticalLineBlockType(IBlockDestroyService destroyService)
        {
            this.destroyService = destroyService;
        }

        public async UniTask<bool> Activate(Vector2Int position, Directions direction)
        {
            if (isActivated)
                return false;

            destroyService.MarkToDestroyVerticalLine(position.x);
            isActivated = true;
            return true;
        }
    }
}