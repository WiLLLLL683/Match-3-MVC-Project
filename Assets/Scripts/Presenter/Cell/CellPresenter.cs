using System;
using System.Collections.Generic;
using UnityEngine;
using Model.Objects;
using View;
using Data;

namespace Presenter
{
    public class CellPresenter : ICellPresenter
    {
        private Cell model;
        private ICellView view;

        public CellPresenter(Cell model, ICellView view)
        {
            this.model = model;
            this.view = view;
        }

        public void Init()
        {
            view.Init(model.Position, model.Type);

            model.OnDestroy += Destroy;
            model.OnEmpty += Empty;
            model.OnTypeChange += ChangeType;
        }
        public void Destroy(Cell cell)
        {
            view.PlayDestroyEffect();

            model.OnDestroy -= Destroy;
            model.OnEmpty -= Empty;
            model.OnTypeChange -= ChangeType;
        }



        private void ChangeType(ACellType type) => view.ChangeType(type);
        private void Empty(Cell cell) => view.PlayEmptyEffect();
    }
}