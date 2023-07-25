using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Model.Infrastructure.FunctionalTests
{
    public class TestState_ForGameTests : IState
    {
        public Game game;
        public Level level;

        public TestState_ForGameTests(Game _game)
        {
            game = _game;
        }

        public void OnStart()
        {
            level = game.Level;
        }

        public void OnEnd()
        {

        }
    }
}