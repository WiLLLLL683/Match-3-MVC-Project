using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Systems
{
    public abstract class ASequence : IAction
    {
        protected List<IAction> actions;

        public void Execute()
        {
            for (int i = 0; i < actions.Count; i++)
            {
                actions[i].Execute();
            }
        }

        public void Undo()
        {
            for (int i = actions.Count-1; i > -1; i--)
            {
                actions[i].Undo();
            }
        }
    }
}