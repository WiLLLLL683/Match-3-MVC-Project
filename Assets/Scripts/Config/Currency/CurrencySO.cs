using Model.Objects;
using UnityEngine;

namespace Config
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New Currency", menuName = "Config/Currency/Currency")]
    public class CurrencySO : ScriptableObject
    {
        public Sprite icon;
        public CurrencyType type;
        public int defaultAmount;
    }
}