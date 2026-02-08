using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    void IInteractable.Initialize()
    {
    }

    void IInteractable.Interact()
    {
        Debug.Log("interacted");
    }


}
