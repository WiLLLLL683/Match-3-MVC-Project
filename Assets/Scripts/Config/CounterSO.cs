using Model.Objects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Counter", menuName = "Config/Counter")]
public class CounterSO : ScriptableObject
{
    public Sprite icon;
    public int count;
    [SerializeReference, SubclassSelector] public ICounterTarget target;
}
