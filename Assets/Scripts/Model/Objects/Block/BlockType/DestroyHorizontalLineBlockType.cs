using Cysharp.Threading.Tasks;
using Model.Services;
using System;
using UnityEngine;
using Utils;

namespace Model.Objects
{
    [Serializable]
    public class DestroyHorizontalLineBlockType : IBlockType
    {
        [field: SerializeField] public int Id { get; set; }

        private readonly IBlockDestroyService destroyService;
        private bool isActivated;

        public DestroyHorizontalLineBlockType(IBlockDestroyService destroyService)
        {
            this.destroyService = destroyService;
        }

        public async UniTask<bool> Activate(Vector2Int position, Directions direction)
        {
            if (isActivated)
                return false;

            destroyService.MarkToDestroyHorizontalLine(position.y);
            isActivated = true;
            return true;
        }
    }
}