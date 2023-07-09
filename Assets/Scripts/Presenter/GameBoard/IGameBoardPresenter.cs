using Model.Infrastructure;
using System;
using UnityEngine;

namespace Presenter
{
    public interface IGameBoardPresenter
    {
        public GameObject gameObject { get; }

        void Init(Game game, IInput input);
        void SpawnBlocks();
        void SpawnCells();
    }
}