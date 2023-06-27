using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;
using ViewElements;

namespace Controller
{
    public class BlockController
    {
        private Block model;
        private BlockView view;

        public BlockController(Block model, BlockView view)
        {
            this.model = model;
            this.view = view;
        }

        public void Activate()
        {
            view.Init(model.Type, model.Position);

            model.OnPositionChange += view.SetModelPosition;
            model.OnTypeChange += view.ChangeType;
            model.OnDestroy += Destroy;
        }

        public void Destroy(Block block)
        {
            model.OnPositionChange -= view.SetModelPosition;
            model.OnTypeChange -= view.ChangeType;
            model.OnDestroy -= Destroy;

            view.PlayDestroyEffect();
            GameObject.Destroy(view.gameObject);
        }
    }
}