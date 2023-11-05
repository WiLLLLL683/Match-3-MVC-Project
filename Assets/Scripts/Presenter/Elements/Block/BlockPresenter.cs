using Config;
using Model.Infrastructure;
using Model.Objects;
using Model.Services;
using UnityEngine;
using View;
using Zenject;

namespace Presenter
{
    public class BlockPresenter : IBlockPresenter
    {
        public class Factory : PlaceholderFactory<Block, ABlockView, BlockTypeSO, BlockTypeSetSO, BlockPresenter> { }

        private readonly Block model;
        private readonly ABlockView view;
        private readonly BlockTypeSetSO allTypeSO;
        private readonly IGame game;
        private readonly IBlockDestroyService destroyService;
        private readonly IBlockChangeTypeService changeTypeService;
        private readonly IBlockMoveService moveService;
        private BlockTypeSO typeSO;

        public BlockPresenter(Block model, ABlockView view, BlockTypeSO typeSO, BlockTypeSetSO allTypeSO, IGame game,
            IBlockDestroyService destroyService, IBlockChangeTypeService changeTypeService, IBlockMoveService moveService)
        {
            this.model = model;
            this.view = view;
            this.allTypeSO = allTypeSO;
            this.game = game;
            this.destroyService = destroyService;
            this.changeTypeService = changeTypeService;
            this.moveService = moveService;
            this.typeSO = typeSO;
        }

        public void Enable()
        {
            view.OnMove += Move;
            view.OnActivate += Activate;

            destroyService.OnDestroy += Destroy;
            moveService.OnPositionChange += SyncPosition;
            changeTypeService.OnTypeChange += ChangeType;

            view.Init(typeSO.icon, typeSO.destroyEffect, model.Position);
            Debug.Log($"{this} enabled");
        }
        public void Disable()
        {
            view.OnMove -= Move;
            view.OnActivate -= Activate;

            destroyService.OnDestroy -= Destroy;
            moveService.OnPositionChange -= SyncPosition;
            changeTypeService.OnTypeChange -= ChangeType;
            Debug.Log($"{this} disabled");
        }
        public void Destroy() => Destroy(model);
        public void Move(Directions direction)
        {
            direction = direction.InvertUpDown();
            game.MoveBlock(model.Position, direction);
        }
        public void Activate()
        {
            Debug.Log("Activate");
            view.PlayClickAnimation();
            game.ActivateBlock(model.Position);
        }

        private void SyncPosition(Block model)
        {
            if (this.model != model)
                return;

            view.ChangeModelPosition(model.Position);
        }
        private void ChangeType(Block model)
        {
            if (this.model != model)
                return;

            typeSO = allTypeSO.GetSO(model.Type.Id);
            view.ChangeType(typeSO.icon, typeSO.destroyEffect);
        }
        private void Destroy(Block model)
        {
            if (this.model != model)
                return;

            Disable();
            view.PlayDestroyEffect();
            GameObject.Destroy(view.gameObject);
        }
    }
}