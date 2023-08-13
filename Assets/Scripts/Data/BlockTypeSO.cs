using Model.Objects;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "NewBlockType", menuName = "Config/BlockType")]
    public class BlockTypeSO : ScriptableObject
    {
        [SerializeReference, SubclassSelector] public IBlockType blockType;
    }
}
