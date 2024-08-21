using Config;
using Model.Services;
using System;

namespace Model.Objects
{
    public class BlockTypeContext
    {
        public Game model;
        public IConfigProvider configProvider;
        public IValidationService validationService;
        public IBlockDestroyService destroyService;
        public IBlockMoveService moveService;

        public BlockTypeContext(Game model,
            IConfigProvider configProvider,
            IValidationService validationService,
            IBlockDestroyService destroyService,
            IBlockMoveService moveService)
        {
            this.model = model;
            this.configProvider = configProvider;
            this.validationService = validationService;
            this.destroyService = destroyService;
            this.moveService = moveService;
        }
    }
}