using Cysharp.Threading.Tasks;
using Model.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model.Objects
{
    [Serializable]
    public class DestroySelectedTypeBlockType : IBlockType
    {
        [field: SerializeField] public int Id { get; set; }
        public bool IsActivatable => true;

        private readonly IValidationService validationService;
        private readonly IBlockDestroyService destroyService;
        private bool isActivated;

        public DestroySelectedTypeBlockType(IValidationService validationService, IBlockDestroyService destroyService)
        {
            this.validationService = validationService;
            this.destroyService = destroyService;
        }

        public async UniTask Activate(Vector2Int position, Directions direction)
        {
            if (isActivated)
                return;

            Block selectedBlock = GetSelectedBlock(position, direction);
            if (selectedBlock == null)
                return;

            List<Block> blocksToDestroy = validationService.FindAllBlockOfType(selectedBlock.Type.Id);
            destroyService.MarkToDestroy(blocksToDestroy);
            destroyService.MarkToDestroy(position);
            isActivated = true;

            return;
        }

        private Block GetSelectedBlock(Vector2Int position, Directions direction)
        {
            if (direction == Directions.Zero)
            {
                return FindNearbyBlock(position);
            }
            else
            {
                return validationService.TryGetBlock(position + direction.ToVector2Int());
            }
        }

        private Block FindNearbyBlock(Vector2Int position)
        {
            foreach (Directions direction in Enum.GetValues(typeof(Directions)))
            {
                if (direction == Directions.Zero)
                    continue;

                Block block = validationService.TryGetBlock(position + direction.ToVector2Int());
                if (block != null)
                    return block;
            }

            return null;
        }
    }
}