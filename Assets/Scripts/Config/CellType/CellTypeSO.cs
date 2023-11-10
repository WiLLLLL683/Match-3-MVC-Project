using UnityEngine;
using Model.Objects;

namespace Config
{
    [CreateAssetMenu(fileName = "NewCellType", menuName = "Config/Cell/CellType")]
    public class CellTypeSO : ACounterTargetSO
    {
        public ParticleSystem destroyEffect;
        public ParticleSystem emptyEffect;
        [SerializeReference, SubclassSelector] public CellType type;

        public override ICounterTarget CounterTarget => type;
    }
}
