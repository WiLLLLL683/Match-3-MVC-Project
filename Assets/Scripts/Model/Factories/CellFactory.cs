using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CellFactory
{
    private readonly ICellType invisibleCellType;

    public CellFactory(ICellType invisibleCellType)
    {
        this.invisibleCellType = invisibleCellType;
    }

    public Cell Create(Vector2Int position, ICellType type)
    {
        return new Cell(type, position);
    }

    public Cell CreateInvisible(Vector2Int position)
    {
        return new Cell(invisibleCellType, position);
    }
}
