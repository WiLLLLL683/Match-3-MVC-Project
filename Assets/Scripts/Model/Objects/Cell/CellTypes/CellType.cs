using System;

namespace Model.Objects
{
    [Serializable]
    public abstract class CellType : ICounterTarget
    {
        public int Id;
        public bool CanContainBlock;
        public bool IsPlayable;

        int ICounterTarget.Id => Id;

        public abstract void DestroyCellMaterial();

        /// <summary>
        /// Memberwise clone
        /// </summary>
        public CellType Clone() => (CellType)MemberwiseClone();
    }
}
