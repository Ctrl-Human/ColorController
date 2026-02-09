using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovementModule : PlayerBaseModule
{
    private PlayerController _playerController;
    private FloorTile _targetTile;
    private bool _isMoving = false;
    [SerializeField] private float _speed = 6f;
    private Vector3 _targetPosition;
    public override void Initialize(PlayerController playercontroller, Action onComplete)
    {
        _playerController = playercontroller;
        onComplete?.Invoke();
    }

    internal void MoveToCell(SingleGridObject pos, Action<SingleGridObject> onComplete)
    {

        if (pos.HasInteractable)
        {
            _targetPosition = pos.Interactable.GetInteractPosition();
        }
        else
        {
            _targetPosition = pos.transform.position;
        }
        float distance = Vector3.Distance(transform.position, _targetPosition);
        float duration = distance / _speed; // duration = distance / speed

        transform.DOKill();
        transform.DOMove(_targetPosition, duration).SetEase(Ease.InOutSine).OnComplete(() =>
        {
            onComplete?.Invoke(pos);
        });
    }

    // Update is called once per frame
    void Update()
    {



    }

}
