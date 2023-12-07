using System;
using System.Collections;
using UnityEngine;

namespace Utils
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator routine);
    }
}