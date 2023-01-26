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

    public AllSystems()
    {
        SpawnSystem = new SpawnSystem();
        MatchSystem = new MatchSystem();
        GravitySystem = new GravitySystem();
        MoveSystem = new MoveSystem();
    }

    public void SetLevel(Level _level)
    {
        SpawnSystem.SetLevel(_level);
        MatchSystem.SetLevel(_level);
        GravitySystem.SetLevel(_level);
        MoveSystem.SetLevel(_level);
    }
}
