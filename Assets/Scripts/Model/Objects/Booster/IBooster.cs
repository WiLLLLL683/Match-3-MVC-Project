using Model.Services;
using UnityEngine;

namespace Model.Objects
{
    public interface IBooster
    {
        public int Id { get; }
        public bool Execute(Vector2Int startPosition, IBlockDestroyService destroyService, IValidationService validationService);
        public IBooster Clone();
    }
}