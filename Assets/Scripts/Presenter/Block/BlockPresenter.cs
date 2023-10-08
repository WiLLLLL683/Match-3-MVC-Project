using UnityEngine;
using View;
using Model.Readonly;
using Utils;
using Model.Infrastructure;
using Config;

namespace Presenter
{
    public class BlockPresenter : IBlockPresenter
    {
        /// <summary>
        /// Реализация фабрики использующая класс презентера в котором находится.
        /// </summary>
        public class Factory : AFactory<IBlock_Readonly, ABlockView, IBlockPresenter>
        {
            private readonly IGame game;
            private readonly BlockTypeSetSO balanceSO;
            public Factory(ABlockView viewPrefab, IGame game, BlockTypeSetSO balanceSO) : base(viewPrefab)
            {
                this.game = game;
                this.balanceSO = balanceSO;
            }

            public override IBlockPresenter Connect(ABlockView existingView, IBlock_Readonly model)
            {
                BlockTypeSO typeSO = balanceSO.GetSO(model.Type_Readonly.Id);
                IBlockPresenter presenter = new BlockPresenter(model, existingView, typeSO, game, balanceSO);
                presenter.Enable();
                existingView.Init(typeSO.icon, typeSO.destroyEffect, model.Position);
                allPresenters.Add(presenter);
                return presenter;
            }
        }

        private readonly IBlock_Readonly model;
        private readonly ABlockView view;
        private readonly IGame game;
        private readonly BlockTypeSetSO balanceSO;
        private BlockTypeSO typeSO;

        public BlockPresenter(IBlock_Readonly model, ABlockView view, BlockTypeSO typeSO, IGame game, BlockTypeSetSO balanceSO)
        {
            this.model = model;
            this.view = view;
            this.game = game;
            this.typeSO = typeSO;
            this.balanceSO = balanceSO;
        }

        public void Enable()
        {
            view.OnMove += Move;
            view.OnActivate += Activate;

            model.OnDestroy_Readonly += Destroy;
            model.OnPositionChange += SyncPosition;
            model.OnTypeChange += ChangeType;
        }
        public void Disable()
        {
            view.OnMove -= Move;
            view.OnActivate -= Activate;

            model.OnDestroy_Readonly -= Destroy;
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
        private void Destroy(IBlock_Readonly block)
        {
            Disable();
            view.PlayDestroyEffect();
            GameObject.Destroy(view.gameObject);
        }
    }
}