using System;
using System.Collections;
using UnityEngine;

public abstract class AScreenController : MonoBehaviour
{
    public abstract void Enable();
    public abstract void Disable();

    private void OnDestroy()
    {
        Disable();
    }
}
