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
        public bool IsActivatable => true;

        private readonly IBlockDestroyService destroyService;
        private bool isActivated = false;

        public DestroyVerticalLineBlockType(IBlockDestroyService destroyService)
        {
            this.destroyService = destroyService;
        }

        public async UniTask Activate(Vector2Int position, Directions direction)
        {
            if (isActivated)
                return;

            destroyService.MarkToDestroyVerticalLine(position.x);
            isActivated = true;
            return;
        }
    }
}