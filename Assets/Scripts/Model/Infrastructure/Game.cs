using Data;
using Model.Objects;
using Model.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model.Infrastructure
{
    /// <summary>
    /// �������� ������ ������ ����
    /// </summary>
    public class Game
    {
        //meta game
        public LevelSelection LevelSelection { get; private set; }
        public CurrencyInventory CurrencyInventory { get; private set; }
        //core game
        public Level Level { get; private set; }
        public BoosterInventory BoosterInventory { get; private set; }
        public PlayerSettings PlayerSettings { get; private set; }

        private StateMachine stateMachine;
        private AllSystems systems;

        public Game(LevelData[] allLevels, int currentLevelIndex)
        {
            CurrencyInventory = new CurrencyInventory();
            BoosterInventory = new BoosterInventory();
            LevelSelection = new LevelSelection(allLevels, currentLevelIndex);
            PlayerSettings = new(true, false); //TODO �������� �� ����������

            systems = new AllSystems();
            systems.AddSystem<ISpawnSystem>(new SpawnSystem());
            systems.AddSystem<IMatchSystem>(new MatchSystem());
            systems.AddSystem<IGravitySystem>(new GravitySystem());
            systems.AddSystem<IMoveSystem>(new MoveSystem());

            stateMachine = new StateMachine();
            stateMachine.AddState(new LoadGameState(this, stateMachine));
            stateMachine.AddState(new MetaGameState());
            stateMachine.AddState(new LoadLevelState(this, stateMachine, systems));
            stateMachine.AddState(new WaitState(this, stateMachine, systems)); //TODO ����� ������ �� �����/����������, ��� �������� �� �������
            stateMachine.AddState(new TurnState(this, stateMachine, systems));
            stateMachine.AddState(new BoosterState(this, stateMachine, systems, BoosterInventory));
            stateMachine.AddState(new SpawnState(this, stateMachine, systems));
            stateMachine.AddState(new HintState(stateMachine, systems));
            stateMachine.AddState(new LoseState(stateMachine, systems));
            stateMachine.AddState(new WinState(stateMachine, systems));
            stateMachine.AddState(new BonusState(stateMachine, systems));
            stateMachine.AddState(new ExitState(stateMachine, systems));
        }

        /// <summary>
        /// �������� ����
        /// </summary>
        public void LoadGame() => stateMachine.SetState<LoadGameState>();
        /// <summary>
        /// ������ ����-����
        /// </summary>
        public void StartMetaGame() => stateMachine.SetState<MetaGameState>();
        /// <summary>
        /// ������ ���������� ������ ���-����
        /// </summary>
        public void StartCoreGame(LevelData levelData)
        {
            stateMachine.GetState<LoadLevelState>().SetLevelData(levelData);
            stateMachine.SetState<LoadLevelState>();
        }
        /// <summary>
        /// �������� ������ �� ������
        /// </summary>
        public void SetLevel(Level _level) => Level = _level;
    }
}