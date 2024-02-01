using Model.Services;
using System;
using UnityEngine;

namespace Model.Objects
{
    [Serializable]
    public class ExplosiveBlockType : IBlockType
    {
        [SerializeField] private int id;
        [SerializeField] private int explosionRadius = 2;
        public int Id => id;

        public ExplosiveBlockType() { }
        public ExplosiveBlockType(int id) => this.id = id;

        public bool Activate(Vector2Int position, IBlockDestroyService destroyService)
        {
            Vector2Int minBound = position - new Vector2Int(explosionRadius, explosionRadius);
            Vector2Int maxBound = position + new Vector2Int(explosionRadius, explosionRadius);

            for (int x = minBound.x; x <= maxBound.x; x++)
            {
                for (int y = minBound.y; y <= maxBound.y; y++)
                {
                    destroyService.MarkToDestroy(new Vector2Int(x, y));
                }
            }

            return true;
        }

        public IBlockType Clone() => (IBlockType)MemberwiseClone();
    }
}