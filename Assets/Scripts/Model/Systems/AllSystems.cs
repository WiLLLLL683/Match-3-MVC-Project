using Model.Objects;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Systems
{
    /// <summary>
    /// Контейнер для игровых систем
    /// </summary>
    public class AllSystems
    {
        private Dictionary<Type, ISystem> systems = new();

        public T GetSystem<T>() where T : ISystem
        {
            Type type = typeof(T);

            Debug.Log(type);
            foreach (var item in systems)
            {
                Debug.Log(item.Value.GetType());
            }

            if (!systems.ContainsKey(type))
            {
                Debug.LogError("There is no " + typeof(T) + " registered");
                return default;
            }

            return (T)systems[typeof(T)];
        }

        public void AddSystem<T>(T _system) where T : ISystem
        {
            Type type = typeof(T);

            if (systems.ContainsKey(type))
            {
                systems[type] = _system;
            }
            else
            {
                systems.Add(type, _system);
            }
        }

        public void SetLevel(Level _level)
        {
            foreach (var item in systems)
            {
                item.Value.SetLevel(_level);
            }
        }
    }
}