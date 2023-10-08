using Model.Readonly;
using System;
using UnityEngine;

namespace Model.Objects
{
    [Serializable]
    public abstract class CellType : ICellType_Readonly, ICounterTarget
    {
        [SerializeField] protected int id;
        [SerializeField] protected bool canContainBlock;
        [SerializeField] protected bool isPlayable;
        public int Id => id;
        public bool CanContainBlock => canContainBlock;
        public bool IsPlayable => isPlayable;

        public abstract void DestroyCellMaterial();

        /// <summary>
        /// Memberwise clone
        /// </summary>
        public CellType Clone() => (CellType)MemberwiseClone();
    }
}
