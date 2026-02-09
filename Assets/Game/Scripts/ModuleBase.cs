using System;
using UnityEngine;

public abstract class PlayerBaseModule: MonoBehaviour
{
    public abstract void Initialize(PlayerController playercontroller, Action onComplete);
}
