using Config;
using Cysharp.Threading.Tasks;
using Model.Services;
using System;
using UnityEngine;
using Utils;

namespace Model.Objects
{
    [Serializable]
    public class ExplosiveBlockType : IBlockType
    {
        [field: SerializeField] public int Id { get; set; }

        private IBlockDestroyService destroyService;
        private IConfigProvider configProvider;
        private bool isActivated;

        public async UniTask<bool> Activate(Vector2Int position, Directions direction, BlockTypeContext dependencies)
        {
            if (isActivated)
                return false;

            destroyService = dependencies.destroyService;
            configProvider = dependencies.configProvider;

            int explosionRadius = configProvider.Block.bonusBlock_explosionRadius;
            Vector2Int minBound = position - new Vector2Int(explosionRadius, explosionRadius);
            Vector2Int maxBound = position + new Vector2Int(explosionRadius, explosionRadius);

            destroyService.MarkToDestroyRect(minBound, maxBound);
            isActivated = true;
            return true;
        }

        public IBlockType Clone() => (IBlockType)MemberwiseClone();
    }
}