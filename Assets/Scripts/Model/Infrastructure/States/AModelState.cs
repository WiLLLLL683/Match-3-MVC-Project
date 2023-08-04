using Model.Objects;
using System;
using System.Collections;
using UnityEngine;
using Utils;

namespace Model.Infrastructure
{
    public abstract class AModelState : IState
    {
        public abstract void OnStart();
        public abstract void OnEnd();
        public virtual void OnInputMoveBlock(Vector2Int startPos, Directions direction) { }
        public virtual void OnInputActivateBlock(Vector2Int startPos) { }
        public virtual void OnInputBooster(IBooster booster) { }
    }
}