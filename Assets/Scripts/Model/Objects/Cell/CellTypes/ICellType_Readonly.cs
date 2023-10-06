using System;
using UnityEngine;

namespace Model.Readonly
{
    public interface ICellType_Readonly
    {
        public int Id { get; }
        public bool CanContainBlock { get; }
        public bool IsPlayable { get; }
    }
}