using Model.Objects;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Utils;

namespace Config
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "NewCellTypeSet", menuName = "Config/Cell/CellTypeSet")]
    public class CellTypeSetSO : ScriptableObject
    {
        public List<CellTypeSO> cellTypes = new();
        public CellTypeSO defaultCellType;
        public CellTypeSO hiddenCellType;

#if UNITY_EDITOR

        private readonly AssetFinder assetFinder = new();
        private readonly UniqueIdChecker idChecker = new();

        [Button] public void CheckUniqueId() => idChecker.CheckCounterTarget(cellTypes);
        [Button] public void FindAllCellTypesInProject() => assetFinder.FindAllAssetsOfType(ref cellTypes, this);
#endif
    }
}