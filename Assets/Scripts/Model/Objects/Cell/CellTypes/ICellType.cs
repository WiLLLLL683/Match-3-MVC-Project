using Model.Readonly;
using System;

namespace Model.Objects
{
    public interface ICellType : ICellType_Readonly, ICounterTarget
    {
        public void DestroyCellMaterial();
    }
}
