using System;
using System.Collections;
using UnityEngine;

namespace Data
{
    /// <summary>
    /// Красный тип блока, без действия по активации
    /// </summary>
    [CreateAssetMenu(fileName = "RedBlockType", menuName = "BlockTypes/RedBlockType")]
    public class RedBlockType : ABlockType
    {
        public override bool Activate()
        {
            return base.Activate();
        }
    }
}