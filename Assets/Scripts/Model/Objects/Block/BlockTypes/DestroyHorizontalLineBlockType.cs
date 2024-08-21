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

        private bool isActivated;

        public async UniTask<bool> Activate(Vector2Int position, Directions direction, BlockTypeContext dependencies)
        {
            if (isActivated)
                return false;

            dependencies.destroyService.MarkToDestroyHorizontalLine(position.y);
            isActivated = true;
            return true;
        }

        public IBlockType Clone() => (IBlockType)MemberwiseClone();
    }
}