using System;
using System.Collections;
using Model.Objects;
using UnityEngine;
using Utils;

namespace Model.Infrastructure
{
    public abstract class AModelState : IState
    {
        public abstract void OnEnter();
        public abstract void OnExit();
        public virtual void OnInputMoveBlock(Vector2Int startPos, Directions direction) { }
        public virtual void OnInputActivateBlock(Vector2Int startPos) { }
        public virtual void OnInputBooster(IBooster booster) { }
    }
}