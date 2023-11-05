using System;
using UnityEngine;

namespace View
{
    public class BoosterInventoryView : MonoBehaviour, IBoosterInventoryView
    {
        [SerializeField] private Transform boostersParent;

        public Transform BoostersParent => boostersParent;
    }
}