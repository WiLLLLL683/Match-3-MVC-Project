using UnityEngine;
using View;
using Data;
using Model.Readonly;

namespace Presenter
{
    public class BlockPresenter : IBlockPresenter
    {
        private IBlock_Readonly model;
        private IBlockView view;

        public BlockPresenter(IBlock_Readonly model, IBlockView view)
        {
            this.model = model;
            this.view = view;
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
            Debug.Log("Move");
            //TODO обращение к модели для запуска систем
        }
        public void Activate()
        {
            Debug.Log("Activate");
            view.PlayClickAnimation();
            //TODO обращение к модели для запуска систем
            //model.Activate();
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