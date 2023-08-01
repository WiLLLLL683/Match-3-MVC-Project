using System;
using UnityEngine;

namespace View
{
    public abstract class AHeaderView : MonoBehaviour
    {
        public abstract Transform ScoreParent { get; }
    }
}