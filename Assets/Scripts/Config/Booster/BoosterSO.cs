using Model.Objects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "NewBooster", menuName = "Config/Boosters/Booster")]
    public class BoosterSO : ScriptableObject
    {
        public string Name;
        public string Hint;
        public Sprite Icon;
        public int InitialAmount;
        [SerializeReference, SubclassSelector] public IBooster booster;
    }
}