using UnityEngine;
using View;
using Model.Readonly;
using Utils;
using Model.Infrastructure;
using Config;
using Model.Services;
using Model.Objects;

namespace Presenter
{
    public class BlockPresenter : IBlockPresenter
    {
        /// <summary>
        /// Реализация фабрики использующая класс презентера в котором находится.
        /// </summary>
        public class Factory : AFactory<Block, ABlockView, IBlockPresenter>
        {
            private readonly IGame game;
            private readonly IBlockDestroyService destroyService;
            private readonly BlockTypeSetSO balanceSO;

            public Factory(ABlockView viewPrefab, IGame game, IBlockDestroyService destroyService, BlockTypeSetSO balanceSO) : base(viewPrefab)
            {
                this.game = game;
                this.destroyService = destroyService;
                this.balanceSO = balanceSO;
            }

            public override IBlockPresenter Connect(ABlockView existingView, Block model)
            {
                BlockTypeSO typeSO = balanceSO.GetSO(model.Type_Readonly.Id);
                IBlockPresenter presenter = new BlockPresenter(model, existingView, typeSO, game, destroyService, balanceSO);
                presenter.Enable();
                existingView.Init(typeSO.icon, typeSO.destroyEffect, model.Position);
                allPresenters.Add(presenter);
                return presenter;
            }
        }

        private readonly Block model;
        private readonly ABlockView view;
        private readonly IGame game;
        private readonly IBlockDestroyService destroyService;
        private readonly BlockTypeSetSO balanceSO;
        private BlockTypeSO typeSO;

        public BlockPresenter(Block model, ABlockView view, BlockTypeSO typeSO, IGame game, IBlockDestroyService destroyService, BlockTypeSetSO balanceSO)
        {
            this.model = model;
            this.view = view;
            this.game = game;
            this.destroyService = destroyService;
            this.typeSO = typeSO;
            this.balanceSO = balanceSO;
        }

        public void Enable()
        {
            view.OnMove += Move;
            view.OnActivate += Activate;

            destroyService.OnDestroy += Destroy;
            model.OnPositionChange += SyncPosition;
            model.OnTypeChange += ChangeType;
        }
        public void Disable()
        {
            view.OnMove -= Move;
            view.OnActivate -= Activate;

            destroyService.OnDestroy -= Destroy;
            model.OnPositionChange -= SyncPosition;
            model.OnTypeChange -= ChangeType;
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

        private void SyncPosition(Vector2Int modelPosition) => view.ChangeModelPosition(modelPosition);
        private void ChangeType(IBlockType_Readonly type)
        {
            typeSO = balanceSO.GetSO(type.Id);
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