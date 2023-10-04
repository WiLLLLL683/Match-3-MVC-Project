using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model.Services
{
    public interface IAction
    {
        public void Execute();
        public void Undo();
    }
}