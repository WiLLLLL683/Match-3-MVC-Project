using Model.Infrastructure;
using System;
using UnityEngine;
using View;

namespace Presenter
{
    public interface IGameBoardPresenter
    {
        public GameObject gameObject { get; }

        void Init(Game game);
        void SpawnBlocks();
        void SpawnCells();
    }
}