using UnityEngine;

namespace View
{
    public abstract class AHudView : MonoBehaviour
    {
        public abstract Transform GoalsParent { get; }
        public abstract Transform RestrictionsParent { get; }
    }
}