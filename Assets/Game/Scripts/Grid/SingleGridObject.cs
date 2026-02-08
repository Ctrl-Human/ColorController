using System;
using UnityEngine;

public abstract class SingleGridObject : MonoBehaviour
{
    public abstract void Initialize(Vector2Int key, Action onComplete);
    public abstract void Destroy();

    public abstract void AddObject(IInteractable interObject);

}
