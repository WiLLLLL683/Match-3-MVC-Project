using Model.Objects;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "NewBlockType", menuName = "Config/BlockType")]
    public class BlockTypeSO : ACounterTargetSO
    {
        public ParticleSystem destroyEffect;
        [SerializeReference, SubclassSelector] public BlockType type = new BasicBlockType(0);
        
        public override ICounterTarget CounterTarget => type;
    }
}
