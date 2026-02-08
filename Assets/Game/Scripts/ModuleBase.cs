using System;
using UnityEngine;

public abstract class ModuleBase: MonoBehaviour
{
    public abstract void Initialize(PlayerController playercontroller, Action onComplete);
}
