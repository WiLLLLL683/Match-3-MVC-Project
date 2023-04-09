using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    /// <summary>
    /// Базовый тип клетки, которая может содержать блок
    /// </summary>
    [CreateAssetMenu(fileName = "BasicCellType", menuName = "CellTypes/BasicCellType")]
    public class BasicCellType : ACellType
    {
        public override void DestroyCellMaterial()
        {
            
        }
    }
}
