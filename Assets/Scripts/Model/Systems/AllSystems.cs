using Model.Objects;
using Model.Systems;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AllSystems
{
    public ISpawnSystem SpawnSystem { get; private set; }
    public IMatchSystem MatchSystem { get; private set; }
    public IGravitySystem GravitySystem { get; private set; }
    public IMoveSystem MoveSystem { get; private set; }

    public void UpdateSystems(Level _level)
    {
        SpawnSystem = new SpawnSystem(_level);
        MatchSystem = new MatchSystem(_level);
        GravitySystem = new GravitySystem(_level.gameBoard);
        MoveSystem = new MoveSystem(_level.gameBoard);
    }
}
