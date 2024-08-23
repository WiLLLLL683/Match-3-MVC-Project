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
        public bool IsActivatable => true;

        private readonly IBlockDestroyService destroyService;
        private bool isActivated;

        public DestroyHorizontalLineBlockType(IBlockDestroyService destroyService)
        {
            this.destroyService = destroyService;
        }

        public async UniTask Activate(Vector2Int position, Directions direction)
        {
            if (isActivated)
                return;

            destroyService.MarkToDestroyHorizontalLine(position.y);
            isActivated = true;
            return;
        }

        public IBlockType Clone() => (IBlockType)MemberwiseClone();
    }
}