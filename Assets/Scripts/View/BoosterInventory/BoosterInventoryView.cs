using UnityEngine;

namespace View
{
    public class BoosterInventoryView : ABoosterInventoryView
    {
        [SerializeField] private Transform boostersParent;

        public override Transform BoostersParent => boostersParent;
    }
}