using Config;
using Model.Objects;
using System;
using UnityEngine;
using Utils;

namespace Model.Infrastructure
{
    public class ModelInput : IModelInput
    {
        private readonly IStateMachine stateMachine;

        public ModelInput(IStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void StartLevel(LevelSO levelData) => stateMachine.EnterState<LoadLevelState, LevelSO>(levelData);
        public void ExitLevel() => stateMachine.EnterState<ExitState>();

        public void ActivateBlock(Vector2Int position) => stateMachine.EnterState<InputActivateBlockState, Vector2Int>(position);
        public void MoveBlock(Vector2Int position, Directions direction)
        {
            var touple = (position, direction);
            stateMachine.EnterState<InputMoveBlockState, (Vector2Int, Directions)>(touple);
        }
        //TODO нужен более надежный способ получения конкретного типа бустера, например id
        public void ActivateBooster(IBooster booster) => stateMachine.EnterState<InputBoosterState, IBooster>(booster);
    }
}