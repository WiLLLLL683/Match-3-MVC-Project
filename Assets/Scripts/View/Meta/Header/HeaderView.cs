using UnityEngine;

namespace View
{
    public class HeaderView : MonoBehaviour, IHeaderView
    {
        [SerializeField] private Transform scoreParent;

        public Transform ScoreParent => scoreParent;
    }
}