using UnityEngine;

namespace View
{
    public abstract class ABoosterInventoryView : MonoBehaviour
    {
        public abstract Transform BoostersParent { get; }
    }
}