using UnityEngine;

namespace View
{
    public interface IHeaderView
    {
        ICounterView StarsCounter { get; }
    }
}