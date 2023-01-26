using Array2DEditor;
using UnityEngine;

namespace Array2DEditor
{
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
