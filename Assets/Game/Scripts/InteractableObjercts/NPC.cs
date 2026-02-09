using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _interactPosition;
    public Vector3 GetInteractPosition()
    {
        return _interactPosition.position;
    }

    void IInteractable.Initialize()
    {
    }

    void IInteractable.Interact()
    {
        Debug.Log("interacted");
    }


}
