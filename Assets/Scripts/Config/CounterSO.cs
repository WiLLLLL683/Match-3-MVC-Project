using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Counter", menuName = "Config/Counter")]
public class CounterSO : ScriptableObject
{
    [SerializeReference, SubclassSelector] public ICounterTarget target;
    [SerializeField] public int count;
}
