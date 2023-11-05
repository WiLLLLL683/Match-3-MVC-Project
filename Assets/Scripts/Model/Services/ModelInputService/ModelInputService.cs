using Config;
using Model.Infrastructure;
using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model.Services
{
    public class ModelInputService : IModelInputService
    {
        private readonly IStateMachine<AModelState> stateMachine;

        public ModelInputService(IStateMachine<AModelState> stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void StartLevel(LevelSO levelData)
        {
            stateMachine.GetState<LoadLevelState>().SetLevelData(levelData);
            stateMachine.EnterState<LoadLevelState>();
        }

        public void MoveBlock(Vector2Int position, Directions direction) => stateMachine.CurrentState.OnInputMoveBlock(position, direction);
        public void ActivateBooster(IBooster booster) => stateMachine.CurrentState.OnInputBooster(booster); //TODO нужен более надежный способ получения конкретного типа бустера, например id
        public void ActivateBlock(Vector2Int position) => stateMachine.CurrentState.OnInputActivateBlock(position);
    }
}