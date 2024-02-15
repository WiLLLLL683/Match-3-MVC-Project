using Config;
using Cysharp.Threading.Tasks;
using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model.Services
{
    public class BlockActivateService : IBlockActivateService //TODO удалить сервис за ненадобностью?
    {
        private readonly Game model;
        private readonly IBlockDestroyService destroyService;
        private readonly IConfigProvider configProvider;
        private readonly IBlockMoveService moveService;
        private readonly IValidationService validationService;

        public BlockActivateService(Game model,
            IBlockDestroyService destroyService,
            IConfigProvider configProvider,
            IBlockMoveService moveService,
            IValidationService validationService)
        {
            this.model = model;
            this.destroyService = destroyService;
            this.configProvider = configProvider;
            this.moveService = moveService;
            this.validationService = validationService;
        }

        public async UniTask<bool> TryActivateBlock(Vector2Int position, Directions direction)
        {
            Block block = validationService.TryGetBlock(position);

            if (block == null)
                return false;

            return await block.Type.Activate(position, direction, new(model, destroyService, configProvider, moveService, validationService));
        }
    }
}