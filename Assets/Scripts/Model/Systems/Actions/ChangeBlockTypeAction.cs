using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Systems
{
    /// <summary>
    /// смена типа блока
    /// </summary>
    public class ChangeBlockTypeAction : IAction
    {
        private Block block;
        private ABlockType targetType;
        private ABlockType previousType;

        public ChangeBlockTypeAction(ABlockType _type, Block _block)
        {
            block = _block;
            targetType = _type;
            if (block != null)
            {
                previousType = block.type;
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