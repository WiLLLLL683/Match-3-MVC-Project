using UnityEngine;

namespace View
{
    public class HudView : MonoBehaviour, IHudView
    {
        [SerializeField] private Transform goalsParent;
        [SerializeField] private Transform restrictionsParent;

        public Transform GoalsParent => goalsParent;
        public Transform RestrictionsParent => restrictionsParent;
    }
}