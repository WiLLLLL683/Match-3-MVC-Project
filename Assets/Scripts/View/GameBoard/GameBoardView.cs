﻿using System;
using System.Collections;
using UnityEngine;

public class GameBoardView : IGameBoardView
{
    [SerializeField] private Transform blocksParent;
    [SerializeField] private Transform cellsParent;

    public override Transform BlocksParent => blocksParent;
    public override Transform CellsParent => cellsParent;

}
