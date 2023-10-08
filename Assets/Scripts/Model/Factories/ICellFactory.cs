﻿using Model.Objects;
using UnityEngine;

namespace Model.Factories
{
    public interface ICellFactory
    {
        Cell Create(Vector2Int position, CellType type);
        Cell CreateInvisible(Vector2Int position);
    }
}