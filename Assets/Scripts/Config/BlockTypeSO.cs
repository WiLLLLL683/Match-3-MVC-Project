using Model.Objects;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "NewBlockType", menuName = "Config/BlockType")]
    public class BlockTypeSO : ScriptableObject
    {
        public Sprite icon;
        public ParticleSystem destroyEffect;
        [SerializeReference, SubclassSelector] public IBlockType blockType;
    }
}
