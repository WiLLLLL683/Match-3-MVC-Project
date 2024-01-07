using Model.Services;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Objects
{
    [Serializable]
    public class DestroyOneBlockBooster : IBooster
    {
        [SerializeField] private int id;
        public int Id => id;

        public bool Execute(Vector2Int startPosition, IBlockDestroyService destroyService, IValidationService validationService)
        {
            if (!validationService.BlockExistsAt(startPosition))
                return false;

            destroyService.DestroyAt(startPosition);
            return true;
        }

        public IBooster Clone() => (IBooster)MemberwiseClone();
    }
}