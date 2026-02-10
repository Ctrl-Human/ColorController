using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform _interactPosition;
    [SerializeField] private AudioSource _audio;
    public Vector3 GetInteractPosition()
    {
        return _interactPosition.position;
    }

    void IInteractable.Initialize()
    {
    }

    void IInteractable.Interact()
    {
        _audio.Play();
        Debug.Log("interacted");
    }


}
