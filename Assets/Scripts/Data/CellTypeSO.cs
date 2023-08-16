using UnityEngine;
using Model.Objects;

namespace Data
{
    [CreateAssetMenu(fileName = "NewCellType", menuName = "Config/CellType")]
    public class CellTypeSO : ScriptableObject
    {
        [SerializeReference, SubclassSelector] public ICellType type;
    }
}
