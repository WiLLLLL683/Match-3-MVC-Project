using System;
using System.Collections;
using UnityEngine;

namespace Data
{
    /// <summary>
    /// Базовый тип блока, без действия по активации
    /// </summary>
    [CreateAssetMenu(fileName = "BasicBlockType", menuName = "BlockTypes/BasicBlockType")]
    public class BasicBlockType : ABlockType
    {
        public override bool Activate()
        {
            return base.Activate();
        }
    }
}