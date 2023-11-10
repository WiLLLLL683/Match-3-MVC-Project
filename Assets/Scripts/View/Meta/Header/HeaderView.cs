using UnityEngine;

namespace View
{
    public class HeaderView : MonoBehaviour, IHeaderView
    {
        [SerializeField] private CounterView starsCounter;

        public ICounterView StarsCounter => starsCounter;
    }
}