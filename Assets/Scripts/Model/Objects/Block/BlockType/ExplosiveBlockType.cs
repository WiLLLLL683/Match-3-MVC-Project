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

        private readonly IBlockDestroyService destroyService;
        private readonly IConfigProvider configProvider;
        private bool isActivated;

        public ExplosiveBlockType(IBlockDestroyService destroyService, IConfigProvider configProvider)
        {
            this.destroyService = destroyService;
            this.configProvider = configProvider;
        }

        public async UniTask<bool> Activate(Vector2Int position, Directions direction)
        {
            if (isActivated)
                return false;

            int explosionRadius = configProvider.Block.bonusBlock_explosionRadius;
            Vector2Int minBound = position - new Vector2Int(explosionRadius, explosionRadius);
            Vector2Int maxBound = position + new Vector2Int(explosionRadius, explosionRadius);

            destroyService.MarkToDestroyRect(minBound, maxBound);
            isActivated = true;
            return true;
        }
    }
}