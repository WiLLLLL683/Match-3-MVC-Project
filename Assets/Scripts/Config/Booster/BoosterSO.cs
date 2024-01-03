using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "NewBooster", menuName = "Config/Booster")]
    public class BoosterSO : ScriptableObject
    {
        public string Name;
        public Sprite Icon;
        [SerializeReference, SubclassSelector] public IBooster booster;
        public int InitialAmount;
    }
}