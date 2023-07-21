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

        public void Init()
        {
            view.OnMove += Move;
            view.OnActivate += Activate;

            model.OnDestroy_Readonly += Destroy;
            model.OnPositionChange += SyncPosition;
            model.OnTypeChange += ChangeType;
        }
        public void Destroy(IBlock_Readonly block)
        {
            view.OnMove -= Move;
            view.OnActivate -= Activate;

            model.OnDestroy_Readonly -= Destroy;
            model.OnPositionChange -= SyncPosition;
            model.OnTypeChange -= ChangeType;

            view.PlayDestroyEffect();
            GameObject.Destroy(view.gameObject);
        }
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
            //TODO исключить возможность запуска блока в обход систем модели
            //model.Activate();
        }

        private void SyncPosition(Vector2Int modelPosition) => view.ChangeModelPosition(modelPosition);
        private void ChangeType(ABlockType type) => view.ChangeType(type);
    }
}