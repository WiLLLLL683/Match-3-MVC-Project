using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using Model.Objects;
using Model.Systems;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Model.Infrastructure.FunctionalTests
{
    public class GameTests
    {
        [Test]
        public void SetLevel_ValidLevel_LevelSetInGame()
        {
            Game game = new();
            Level level = new(1, 1);
            Assert.Null(game.Level);// AreEqual(null, game.Level);

            game.SetLevel(level);

            Assert.AreEqual(level, game.Level);
        }

        [Test]
        public void SetLevel_ValidLevel_LevelSetInSystems()
        {
            Game game = new();
            Level level = new(1, 1);
            var spawn = (SpawnSystem)game.Systems.SpawnSystem;
            var match = (MatchSystem)game.Systems.MatchSystem;
            var gravity = (GravitySystem)game.Systems.GravitySystem;
            var move = (MoveSystem)game.Systems.MoveSystem;
            Assert.Null(spawn.GetLevel());
            Assert.Null(match.GetLevel());
            Assert.Null(gravity.GetLevel());
            Assert.Null(move.GetLevel());

            game.SetLevel(level);

            Assert.AreEqual(level, spawn.GetLevel()); //TODO ��� �������� ������ ��������� ����� ������� - ����� �������� ���������������
            Assert.AreEqual(level, match.GetLevel());
            Assert.AreEqual(level, gravity.GetLevel());
            Assert.AreEqual(level, move.GetLevel());
        }

        [Test]
        public void SetLevel_ValidLevel_LevelSetInStates()
        {
            Game game = new();
            Level level = new(1, 1);
            var state = new TestState_ForGameTests(game);
            game.StateMachine.AddState(state);

            game.SetLevel(level);
            game.StateMachine.SetState<TestState_ForGameTests>();

            Assert.AreEqual(level, state.level);
        }

        [Test]
        public void SetLevel_InValidLevel_ReturnToMetaGameState()
        {
            Game game = new();
            Level level = null;

            game.SetLevel(level);

            Assert.AreEqual(typeof(MetaGameState), game.StateMachine.currentState.GetType());
        }

        [Test]
        public void LoadGame_ExistedSaveData_SaveDataLoaded()
        {
            Assert.Fail();
            //TODO ������� ����������
        }

        [Test]
        public void LoadGame_NoSaveData_NewSaveData()
        {
            Assert.Fail();
            //TODO ������� ����������
        }

        [Test]
        public void StartMetaGame_MetaGameStateLoaded() //TODO ������ �� ��� ���� �������? �������� ������ ��������� �������?
        {
            Game game = new();

            game.StartMetaGame();

            Assert.AreEqual(typeof(MetaGameState), game.StateMachine.currentState.GetType());
        }

        [Test]
        public void StartCoreGame_ValidLevelData_WaitStateLoaded()
        {
            Game game = new();
            LevelData levelData = new(new GameBoardData(), new CounterData[1], new CounterData[1], new BalanceData());

            game.StartCoreGame(levelData);

            Assert.AreEqual(typeof(WaitState), game.StateMachine.currentState.GetType());
        }

        [Test]
        public void StartCoreGame_InValidLevelData_ReturnToMetaGameState()
        {
            Game game = new();
            LevelData levelData = default;

            game.StartCoreGame(levelData);

            Assert.AreEqual(typeof(MetaGameState), game.StateMachine.currentState.GetType());
        }
    }
}