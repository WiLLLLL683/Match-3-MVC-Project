﻿using Model.Objects;
using System;
using System.Collections.Generic;
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
        /// Возвращает клетки для уничтожения блоков в них.
        /// </summary>
        void UseBooster(int id, Vector2Int startPosition);

        /// <summary>
        /// Получить количество бустеров определенного типа.
        /// </summary>
        int GetBoosterAmount(int id);
    }
}