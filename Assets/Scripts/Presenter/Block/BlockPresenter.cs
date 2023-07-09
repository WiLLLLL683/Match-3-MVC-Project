using Data;
using Model;
using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;
using View;

namespace Presenter
{
    public class BlockPresenter : IBlockPresenter
    {
        private Block model;
        private IBlockView view;

        public BlockPresenter(Block model, IBlockView view)
        {
            this.model = model;
            this.view = view;
        }

        public void Init()
        {
            view.Init(model.Type, model.Position, this);

            model.OnDestroy += Destroy;
            model.OnPositionChange += SyncPosition;
            model.OnTypeChange += ChangeType;
        }
        public void Destroy(Block block)
        {
            model.OnDestroy -= Destroy;
            model.OnPositionChange -= SyncPosition;
            model.OnTypeChange -= ChangeType;

            view.PlayDestroyEffect();
            GameObject.Destroy(view.gameObject);
        }
        public void Drag(Vector2 deltaPosition) => view.Drag(deltaPosition);
        public void Move(Directions direction)
        {
            Debug.Log("Move");
            view.ReturnToModelPosition();
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

        private void SyncPosition(Vector2 modelPosition) => view.SetModelPosition(modelPosition);
        private void ChangeType(ABlockType type) => view.ChangeType(type);
    }
}