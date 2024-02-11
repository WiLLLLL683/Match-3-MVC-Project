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
        private readonly IValidationService validationService;

        public BlockActivateService(IValidationService validationService)
        {
            this.validationService = validationService;
        }

        public async UniTask<bool> TryActivateBlock(Vector2Int position, Directions direction)
        {
            Block block = validationService.TryGetBlock(position);

            if (block == null)
                return false;

            return await block.Type.Activate(position, direction);
        }
    }
}