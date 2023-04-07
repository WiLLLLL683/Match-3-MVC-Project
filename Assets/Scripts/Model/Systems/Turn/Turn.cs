using Model.Objects;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Model.Systems
{
    /// <summary>
    /// Объект игрового хода
    /// </summary>
    [Serializable]
    public class Turn : ICounterTarget
    {
        private List<IAction> actions = new();

        /// <summary>
        /// Добавить действие в ход
        /// </summary>
        /// <param name="_action"></param>
        public void AddAction(IAction _action)
        {
            actions.Add(_action);
        }

        /// <summary>
        /// Выполнить все действия в ходу
        /// </summary>
        public void Execute()
        {
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].Execute();
            }
        }

        /// <summary>
        /// Отменить все действия в ходу
        /// </summary>
        public void Undo()
        {
            for (int i = actions.Count - 1; i >= 0; i--)
            {
                actions[i].Undo();
            }
        }
    }
}
