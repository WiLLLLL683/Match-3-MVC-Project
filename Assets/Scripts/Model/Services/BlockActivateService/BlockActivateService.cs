using Config;
using Cysharp.Threading.Tasks;
using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model.Services
{
    public class BlockActivateService : IBlockActivateService
    {
        private readonly BlockTypeContext context;
        private readonly IValidationService validationService;

        public BlockActivateService(Game model,
            IBlockDestroyService destroyService,
            IConfigProvider configProvider,
            IBlockMoveService moveService,
            IValidationService validationService)
        {
            this.context = new(model, configProvider, validationService, destroyService, moveService);
            this.validationService = validationService;
        }

        public async UniTask<bool> TryActivateBlock(Vector2Int position, Directions direction)
        {
            Block block = validationService.TryGetBlock(position);

            if (block == null)
                return false;

            return await block.Type.Activate(position, direction, context);
        }
    }
}