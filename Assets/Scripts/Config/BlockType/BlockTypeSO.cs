using Model.Objects;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "NewBlockType", menuName = "Config/Block/BlockType")]
    public class BlockTypeSO : ACounterTargetSO
    {
        public ParticleSystem destroyEffect;
        [SerializeReference, SubclassSelector] public IBlockType type = new BasicBlockType(0);
        
        public override ICounterTarget CounterTarget => type;
    }
}
