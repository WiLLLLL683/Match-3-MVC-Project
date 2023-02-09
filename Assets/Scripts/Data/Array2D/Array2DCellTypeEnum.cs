using Array2DEditor;
using UnityEngine;

namespace Array2DEditor
{
    /// <summary>
    /// Класс для отображения в инспекторе 2-х мерного массива с типами клеток
    /// </summary>
    [System.Serializable]
    public class Array2DCellTypeEnum : Array2D<CellTypeEnum>
    {
        [SerializeField]
        CellRowCellTypeEnum[] cells = new CellRowCellTypeEnum[Consts.defaultGridSize];

        protected override CellRow<CellTypeEnum> GetCellRow(int idx)
        {
            return cells[idx];
        }

        public void SetSize()
        {

        }
    }

    [System.Serializable]
    public class CellRowCellTypeEnum : CellRow<CellTypeEnum> { }
}
