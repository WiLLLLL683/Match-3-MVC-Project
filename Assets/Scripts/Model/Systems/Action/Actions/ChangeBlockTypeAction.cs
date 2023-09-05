using Model.Objects;
using UnityEngine;

namespace Model.Systems
{
    /// <summary>
    /// смена типа блока
    /// </summary>
    public class ChangeBlockTypeAction : IAction
    {
        private Block block;
        private IBlockType targetType;
        private IBlockType previousType;

        public ChangeBlockTypeAction(IBlockType _type, Block _block)
        {
            block = _block;
            targetType = _type;
            if (block != null)
            {
                previousType = block.Type;
            }
        }

        public void Execute()
        {
            if (targetType == null || previousType == null || block == null)
            {
                Debug.LogError("Invalid input data");
                return;
            }

            block.ChangeType(targetType);
        }

        public void Undo()
        {
            block.ChangeType(previousType);
        }
    }
}