using Model.Objects;
using System;
using System.Collections;
using UnityEngine;

namespace Model.Systems
{
    public interface ISystem
    {
        /// <summary>
        /// Обновить данные об уровне
        /// </summary>
        public void SetLevel(Level level);
    }
}