using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Objects
{
    /// <summary>
    /// Базовый тип клетки, которая может содержать блок
    /// </summary>
    [Serializable]
    public class BasicCellType : ACellType
    {
        public override void DestroyCellMaterial()
        {
            
        }
    }
}
