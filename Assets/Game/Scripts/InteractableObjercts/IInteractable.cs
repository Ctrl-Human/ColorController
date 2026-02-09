using UnityEngine;

public interface IInteractable
{
    void Initialize();
    void Interact();

    Vector3 GetInteractPosition();
}
