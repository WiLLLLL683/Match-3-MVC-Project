using UnityEngine;
using Model.Objects;

namespace Config
{
    [CreateAssetMenu(fileName = "NewCellType", menuName = "Config/Cell/CellType")]
    public class CellTypeSO : ScriptableObject
    {
        public Sprite icon;
        public ParticleSystem destroyEffect;
        public ParticleSystem emptyEffect;
        [SerializeReference, SubclassSelector] public ICellType type;
    }
}
