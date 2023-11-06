using Config;
using Model.Objects;
using UnityEngine;

namespace Model.Infrastructure
{
    /// <summary>
    /// Фасад обрабатывающий инпут приходящий в слой модели
    /// Все вызовы модели из презентеров должны проходить через этот сервис
    /// </summary>
    public interface IModelInput
    {
        /// <summary>
        /// Завершение запущенного уровня, для безопасной выгрузки сцены
        /// </summary>
        void ExitLevel();

        /// <summary>
        /// Запуск выбранного уровня кор-игры
        /// </summary>
        void StartLevel(LevelSO levelData);

        /// <summary>
        /// Попытаться активировать блок в заданных координатах
        /// </summary>
        void ActivateBlock(Vector2Int blockPosition);

        /// <summary>
        /// Попытаться сдвинуть блок в заданных координатах в заданном направлении
        /// </summary>
        void MoveBlock(Vector2Int blockPosition, Directions direction);

        /// <summary>
        /// Активировать выбранный бустер
        /// </summary>
        void ActivateBooster(IBooster booster);
    }
}