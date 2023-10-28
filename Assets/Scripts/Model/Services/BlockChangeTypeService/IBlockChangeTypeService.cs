﻿using Model.Objects;
using System;
using UnityEngine;

namespace Model.Services
{
    public interface IBlockChangeTypeService
    {
        event Action<Block> OnTypeChange;

        void SetLevel(GameBoard gameBoard);
        void ChangeBlockType(Vector2Int position, BlockType targetType);
        void ChangeBlockType(Cell cell, BlockType targetType);
    }
}