using Model.Objects;
using Model.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.GameLogic
{
    public class StateContext
    {
        public Level Level { get; private set; }
        public SpawnSystem SpawnSystem { get; private set; }
        public MatchSystem MatchSystem { get; private set; }
        public GravitySystem GravitySystem { get; private set; }
        public MoveSystem MoveSystem { get; private set; }

        public StateContext(Level _level)
        {
            Level = _level;
            SpawnSystem = new SpawnSystem(Level);
            MatchSystem = new MatchSystem(Level);
            GravitySystem = new GravitySystem(Level.gameBoard);
            MoveSystem = new MoveSystem(Level.gameBoard);
        }
    }
}