using DG.Tweening;
using UnityEngine;

public class Door : MonoBehaviour,IInteractable
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private Transform _doorPivot;
    [SerializeField] private float _openAngle = 90f;
    [SerializeField] private float _duration = 1.5f;

    private bool _isOpen = false;
    private Tween _rotateTween;


    public Vector3 GetInteractPosition()
    {
      return _interactionPoint.position;
    }

    public void Initialize()
    {
        
    }

    public void Interact()
    {
        if (_rotateTween != null && _rotateTween.IsActive()) return;

        float targetY = _isOpen ? 0f : _openAngle;

        _rotateTween = _doorPivot
            .DOLocalRotate(new Vector3(-90, targetY, 0), _duration)
            .SetEase(Ease.OutCubic)
            .OnComplete(() => _isOpen = !_isOpen);
    }


}
