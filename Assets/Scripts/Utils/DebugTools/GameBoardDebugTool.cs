using Model.Objects;
using System;
using UnityEngine;
using Zenject;

namespace Utils
{
    public class GameBoardDebugTool : MonoBehaviour
    {
        public Game model;

        [Inject]
        public void Construct(Game model)
        {
            this.model = model;
        }
    }
}