using System;
using UnityEngine;

namespace Model.Services
{
    /// <summary>
    /// Сервис для работы с бустерами.
    /// </summary>
    public interface IBoosterService
    {
        /// <summary>
        /// Id, Amount
        /// </summary>
        event Action<int, int> OnAmountChanged;

        /// <summary>
        /// Добавить бустер определенного типа.
        /// </summary>
        void AddBooster(int id, int ammount);

        /// <summary>
        /// Забрать бустер определенного типа без использования.
        /// </summary>
        void RemoveBooster(int id, int ammount);

        /// <summary>
        /// Использовать бустер определенного типа и забрать 1 штуку.
        /// Возвращает успех операции.
        /// </summary>
        bool UseBooster(int id, Vector2Int startPosition);

        /// <summary>
        /// Получить количество бустеров определенного типа.
        /// </summary>
        int GetBoosterAmount(int id);
    }
}