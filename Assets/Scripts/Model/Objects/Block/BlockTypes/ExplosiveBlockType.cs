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
        public bool IsActivatable => true;

        private readonly IConfigProvider configProvider;
        private readonly IBlockDestroyService destroyService;
        private bool isActivated;

        public ExplosiveBlockType(IConfigProvider configProvider, IBlockDestroyService destroyService)
        {
            this.configProvider = configProvider;
            this.destroyService = destroyService;
        }

        public async UniTask Activate(Vector2Int position, Directions direction)
        {
            if (isActivated)
                return;

            int explosionRadius = configProvider.Block.bonusBlock_explosionRadius;
            Vector2Int minBound = position - new Vector2Int(explosionRadius, explosionRadius);
            Vector2Int maxBound = position + new Vector2Int(explosionRadius, explosionRadius);

            destroyService.MarkToDestroyRect(minBound, maxBound);
            isActivated = true;
            return;
        }

        public IBlockType Clone() => (IBlockType)MemberwiseClone();
    }
}