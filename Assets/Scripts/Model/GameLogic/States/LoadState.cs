using Data;
using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.GameLogic
{
    public class LoadState : AState
    {
        private StateContext stateContext;
        private LevelData levelData;

        public LoadState(GameStateMachine _stateMachine, LevelData _levelData) : base(_stateMachine)
        {
            levelData = _levelData;
        }

        public override void OnStart()
        {
            base.OnStart();

            if (levelData.ValidCheck())
            {
                //загрузка уровня
                Level level = new Level(levelData);
                //загрузка систем
                stateContext = new StateContext(level);
                stateMachine.ChangeState(new WaitState(stateMachine));
            }
            else
            {
                Debug.LogError("Invalid LevelData");
                stateMachine.ChangeState(new MetaState(stateMachine));
            }
        }

        public override void OnEnd()
        {
            base.OnEnd();
        }
    }
}