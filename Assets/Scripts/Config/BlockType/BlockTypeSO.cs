using DG.Tweening;
using Model.Objects;
using NaughtyAttributes;
using System;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(fileName = "NewBlockType", menuName = "Config/Block/BlockType")]
    public class BlockTypeSO : ACounterTargetSO
    {
        public BlockTypeConfig typeConfig;
        [SerializeReference, SubclassSelector] public IBlockType type = new BasicBlockType();

        public override ICounterTarget CounterTarget => type;
    }
}
