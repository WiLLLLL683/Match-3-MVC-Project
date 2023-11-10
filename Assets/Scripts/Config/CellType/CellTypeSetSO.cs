using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "NewCellTypeSet", menuName = "Config/Cell/CellTypeSet")]
    public class CellTypeSetSO : ScriptableObject
    {
        public List<CellTypeSO> cellTypes = new();
        public CellTypeSO defaultCellType;
        public CellTypeSO invisibleCellType;

        public CellTypeSO GetSO(int id)
        {
            for (int i = 0; i < cellTypes.Count; i++)
            {
                if (cellTypes[i].type.Id == id)
                    return cellTypes[i];
            }

            return defaultCellType;
        }
    }
}