using Model.Readonly;
using System;
using UnityEngine;

namespace Model.Objects
{
    /// <summary>
    /// Модель для блока на игровом поле,
    /// Блок должен находиться в Клетке
    /// </summary>
    [Serializable]
    public class Block : IBlock_Readonly
    {
        public BlockType Type { get; private set; }
        public Vector2Int Position { get; private set; }
        public IBlockType_Readonly Type_Readonly => Type;

        public event Action<Block> OnDestroy;
        public event Action<IBlock_Readonly> OnDestroy_Readonly;
        public event Action<IBlockType_Readonly> OnTypeChange;
        public event Action<Vector2Int> OnPositionChange;

        public Block(BlockType type, Vector2Int position)
        {
            Type = type;
            Position = position;
        }

        /// <summary>
        /// Задать положение блока
        /// </summary>
        public void SetPosition(Vector2Int position)
        {
            Position = position;
            OnPositionChange?.Invoke(Position);
        }

        /// <summary>
        /// Задать новый тип блока
        /// </summary>
        public void SetType(BlockType type)
        {
            Type = type;
            OnTypeChange?.Invoke(type);
        }

        /// <summary>
        /// Активировать блок, зависит от типа блока
        /// </summary>
        public bool Activate() => Type.Activate();

        /// <summary>
        /// уничтожить блок
        /// ВНИМАНИЕ: метод следует вызывать только из Клетки
        /// </summary>
        public void Destroy()
        {
            OnDestroy?.Invoke(this);
            OnDestroy_Readonly?.Invoke(this);
        }
    }
}