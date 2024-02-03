using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Services
{
    public class BlockActivateService : IBlockActivateService
    {
        private readonly IValidationService validationService;
        private readonly IBlockDestroyService destroyService;

        public BlockActivateService(IValidationService validationService, IBlockDestroyService destroyService)
        {
            this.validationService = validationService;
            this.destroyService = destroyService;
        }

        public bool TryActivateBlock(Vector2Int position)
        {
            Block block = validationService.TryGetBlock(position);

            if (block == null)
                return false;

            return block.Type.Activate(position, destroyService);
        }
    }
}