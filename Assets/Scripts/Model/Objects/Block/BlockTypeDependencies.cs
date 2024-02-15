using Config;
using Model.Services;
using System;

namespace Model.Objects
{
    public class BlockTypeDependencies
    {
        public Game model;
        public IBlockDestroyService destroyService;
        public IConfigProvider configProvider;
        public IBlockMoveService moveService;
        public IValidationService validationService;

        public BlockTypeDependencies(Game model,
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
    }
}