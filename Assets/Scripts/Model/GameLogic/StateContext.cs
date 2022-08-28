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
        public GenerationSystem GenerationSystem { get; private set; }
        public MatchSystem MatchSystem { get; private set; }
        public GravitySystem GravitySystem { get; private set; }
        public SwitchSystem SwitchSystem { get; private set; }

        public StateContext(Level _level)
        {
            Level = _level;
            GenerationSystem = new GenerationSystem(Level);
            MatchSystem = new MatchSystem(Level);
            GravitySystem = new GravitySystem(Level.gameBoard);
            SwitchSystem = new SwitchSystem(Level.gameBoard);
        }
    }
}