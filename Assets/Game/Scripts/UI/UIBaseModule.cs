using System;
using UnityEngine;

public abstract class UIBaseModule : MonoBehaviour
{
    public abstract void Initialize(UIManager uiManager, Action onComplete);
}
