using System;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "BlockType", menuName = "Config/BlockType")]
    public class BlockTypeSO : ScriptableObject
    {
        [SerializeReference, SubclassSelector] public IBlockType blockType;
    }
}
