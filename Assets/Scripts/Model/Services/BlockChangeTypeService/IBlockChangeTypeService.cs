using Model.Objects;
using System;
using UnityEngine;

namespace Model.Services
{
    public interface IBlockChangeTypeService
    {
        event Action<Block> OnTypeChange;

        void ChangeBlockType(Vector2Int position, IBlockType targetType);
        void ChangeBlockType(Cell cell, IBlockType targetType);
    }
}