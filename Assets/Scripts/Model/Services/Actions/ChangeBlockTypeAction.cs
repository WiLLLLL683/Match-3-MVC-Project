using UnityEngine;
using Model.Objects;

namespace Model.Services
{
    /// <summary>
    /// смена типа блока
    /// </summary>
    public class ChangeBlockTypeAction : IAction
    {
        private Block block;
        private IBlockType targetType;
        private IBlockType previousType;

        public ChangeBlockTypeAction(IBlockType targetType, Block block)
        {
            this.block = block;
            this.targetType = targetType;

            if (this.block != null)
            {
                previousType = this.block.Type;
            }
        }

        public void Execute()
        {
            if (targetType == null || previousType == null || block == null)
            {
                Debug.LogError("Invalid input data");
                return;
            }

            block.SetType(targetType);
        }

        public void Undo()
        {
            block.SetType(previousType);
        }
    }
}