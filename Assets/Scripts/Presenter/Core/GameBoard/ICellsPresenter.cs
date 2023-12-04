using System;
using UnityEngine;
using View;

namespace Presenter
{
    public interface ICellsPresenter : IPresenter
    {
        public abstract ICellView GetCellView(Vector2Int modelPosition);
    }
}