using UnityEngine;

namespace View
{
    public class BoosterInventoryView : BoosterInventoryViewBase
    {
        [SerializeField] private Transform boostersParent;

        public override Transform BoostersParent => boostersParent;
    }
}