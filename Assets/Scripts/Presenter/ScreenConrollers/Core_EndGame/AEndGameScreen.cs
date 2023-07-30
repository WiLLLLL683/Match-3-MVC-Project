using Model.Infrastructure;
using System;
using UnityEngine;
using View;

namespace Presenter
{
    public abstract class AEndGameScreen : AScreenController
    {
        public abstract void Init(Game game, IInput input);
        public abstract void OnLevelComplete();
        public abstract void OnDefeat();
    }
}