using UnityEngine;
using View;
using Data;
using Model.Readonly;
using Utils;
using Model.Infrastructure;

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
            public Factory(ABlockView viewPrefab, IGame game) : base(viewPrefab)
            {
                this.game = game;
            }

            public override IBlockPresenter Connect(ABlockView existingView, IBlock_Readonly model)
            {
                var presenter = new BlockPresenter(model, existingView, game);
                presenter.Enable();
                existingView.Init(model.Type, model.Position);
                allPresenters.Add(presenter);
                return presenter;
            }
        }
        
        private IBlock_Readonly model;
        private ABlockView view;
        private readonly IGame game;

        public BlockPresenter(IBlock_Readonly model, ABlockView view, IGame game)
        {
            this.model = model;
            this.view = view;
            this.game = game;
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
        private void ChangeType(ABlockType type) => view.ChangeType(type);
        private void Destroy(IBlock_Readonly block)
        {
            Disable();
            view.PlayDestroyEffect();
            GameObject.Destroy(view.gameObject);
        }
    }
}