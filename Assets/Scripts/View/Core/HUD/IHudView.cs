using UnityEngine;

namespace View
{
    public interface IHudView
    {
        public abstract Transform GoalsParent { get; }
        public abstract Transform RestrictionsParent { get; }
    }
}