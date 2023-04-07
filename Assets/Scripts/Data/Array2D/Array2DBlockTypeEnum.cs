using Array2DEditor;
using UnityEngine;

namespace Array2DEditor
{
    /// <summary>
    /// Класс для отображения в инспекторе 2-х мерного массива с типами блоков
    /// </summary>
    [System.Serializable]
    public class Array2DBlockTypeEnum : Array2D<BlockTypeEnum>
    {
        [SerializeField]
        CellRowBlockTypeEnum[] cells = new CellRowBlockTypeEnum[Consts.defaultGridSize];

        protected override CellRow<BlockTypeEnum> GetCellRow(int idx)
        {
            return cells[idx];
        }
    }

    [System.Serializable]
    public class CellRowBlockTypeEnum : CellRow<BlockTypeEnum> { }
}
