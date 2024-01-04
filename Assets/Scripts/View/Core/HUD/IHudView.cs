using UnityEngine;

namespace View
{
    public interface IHudView
    {
        Transform GoalsParent { get; }
        Transform RestrictionsParent { get; }
    }
}