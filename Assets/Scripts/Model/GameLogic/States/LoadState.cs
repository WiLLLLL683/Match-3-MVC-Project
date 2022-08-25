using Data;
using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.GameLogic
{
    public class LoadState : AState
    {
        private StateContext context;
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
                //�������� ������
                Level level = new Level(levelData);
                //�������� ������
                context = new StateContext(level);
                stateMachine.ChangeState(new WaitState(stateMachine,context));
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