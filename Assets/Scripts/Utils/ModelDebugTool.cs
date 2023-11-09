using Model.Objects;
using Model.Services;
using NaughtyAttributes;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Utils
{
    public class ModelDebugTool : MonoBehaviour
    {
        public Game game;

        private IBlockChangeTypeService blockChangeTypeService;
        private IBlockDestroyService blockDestroyService;
        private IBlockMoveService blockMoveService;
        private IBlockSpawnService blockSpawnService;
        private IGravityService gravityService;
        private IMatchService matchService;
        private IRandomBlockTypeService randomBlockTypeService;
        private IValidationService validationService;
        private ICellChangeTypeService cellChangeTypeService;
        private ICellSetBlockService cellSetBlockService;
        private ICellDestroyService cellDestroyService;
        private IBoosterService boosterService;
        private ICounterService counterService;
        private ICurrencyService currencyService;
        private IWinLoseService winLoseService;

        [Inject]
        public void Construct(Game game, 
            IBlockChangeTypeService blockChangeTypeService,
            IBlockDestroyService blockDestroyService,
            IBlockMoveService blockMoveService,
            IBlockSpawnService blockSpawnService,
            IGravityService gravityService,
            IMatchService matchService,
            IRandomBlockTypeService randomBlockTypeService,
            IValidationService validationService,
            ICellChangeTypeService cellChangeTypeService,
            ICellSetBlockService cellSetBlockService,
            ICellDestroyService cellDestroyService,
            IBoosterService boosterService,
            ICounterService counterService,
            ICurrencyService currencyService,
            IWinLoseService winLoseService)
        {
            this.game = game;
            this.blockChangeTypeService = blockChangeTypeService;
            this.blockDestroyService = blockDestroyService;
            this.blockMoveService = blockMoveService;
            this.blockSpawnService = blockSpawnService;
            this.gravityService = gravityService;
            this.matchService = matchService;
            this.randomBlockTypeService = randomBlockTypeService;
            this.validationService = validationService;
            this.cellChangeTypeService = cellChangeTypeService;
            this.cellSetBlockService = cellSetBlockService;
            this.cellDestroyService = cellDestroyService;
            this.boosterService = boosterService;
            this.counterService = counterService;
            this.currencyService = currencyService;
            this.winLoseService = winLoseService;
        }

        [Button] public void Currency_Add10Stars() => currencyService.AddCurrency(CurrencyType.Star, 10);
        [Button] public void Currency_Spend10Stars() => currencyService.SpendCurrency(CurrencyType.Star, 10);
        [Button] public void Currency_GetAmountOfStars() => Debug.Log($"You have {currencyService.GetAmount(CurrencyType.Star)} Stars");
    }
}