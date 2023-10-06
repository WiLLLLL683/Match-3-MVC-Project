using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "AllCellTypes", menuName = "Config/Cell/AllCellTypes")]
    public class AllCellTypeSO : ScriptableObject
    {
        public List<CellTypeSO> cellTypes = new();
        public CellTypeSO defaultCellType;

        private readonly Dictionary<int, CellTypeSO> idTypePairs = new();

        private void OnValidate()
        {
            idTypePairs.Clear();
            for (int i = 0; i < cellTypes.Count; i++)
            {
                idTypePairs.Add(cellTypes[i].type.Id, cellTypes[i]);
            }
        }

        public CellTypeSO GetSO(int id)
        {
            if (!idTypePairs.ContainsKey(id))
                return defaultCellType;

            return idTypePairs[id];
        }
    }
}