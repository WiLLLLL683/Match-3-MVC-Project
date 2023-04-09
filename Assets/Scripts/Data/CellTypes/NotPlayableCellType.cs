using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Data
{
    /// <summary>
    /// Тип клетки которая не может содержать блок
    /// </summary>
    [CreateAssetMenu(fileName = "NotPlayableCellType", menuName = "CellTypes/NotPlayableCellType")]
    public class NotPlayableCellType : ACellType
    {
        public override bool CanContainBlock => false;
        public override void DestroyCellMaterial()
        {
            
        }
    }
}
