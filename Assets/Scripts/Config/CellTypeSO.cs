using UnityEngine;
using Model.Objects;

namespace Config
{
    [CreateAssetMenu(fileName = "NewCellType", menuName = "Config/CellType")]
    public class CellTypeSO : ScriptableObject
    {
        [SerializeReference, SubclassSelector] public ICellType type;
    }
}
