using Model.Objects;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "NewBlockType", menuName = "Config/BlockType")]
    public class BlockTypeSO : ScriptableObject
    {
        [SerializeReference, SubclassSelector] public IBlockType blockType;
    }
}
