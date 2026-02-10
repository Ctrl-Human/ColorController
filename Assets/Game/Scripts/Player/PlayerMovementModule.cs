using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovementModule : PlayerBaseModule
{
    private PlayerController _playerController;
    private Sequence _activeMoveSequence;
    private FloorTile _targetTile;
    private bool _isMoving = false;
    [SerializeField] private float _speed = 6f;
    private Vector3 _targetPosition;
    public override void Initialize(PlayerController playercontroller, Action onComplete)
    {
        _playerController = playercontroller;
        
        onComplete?.Invoke();

    }
    internal void AssignStartCell(SingleGridObject _cell)
    {
        transform.position = _cell.GetCenterPoint() + new Vector3(0, 1f, 0);
    }
    internal void MoveToCell(List<SingleGridObject> cellPath, Action<SingleGridObject> onTileReached, Action<SingleGridObject> onGoalReached)
    {
        if (cellPath == null || cellPath.Count == 0) return;

        // Kill any previous movement
        if (_activeMoveSequence != null && _activeMoveSequence.IsActive())
        {
            _activeMoveSequence.Kill();
            _isMoving = false;
            _playerController.ToggleIsMoving(false);
            _playerController.ToogleIsInteracting(false);
        }

        _isMoving = true;
        _playerController.ToggleIsMoving(true);

        Sequence seq = DOTween.Sequence();
        _activeMoveSequence = seq; // store active sequence
        Vector3 prevPos = transform.position;

        bool interacted = false;

        for (int i = 0; i < cellPath.Count; i++)
        {
            var currentTile = cellPath[i];
            Vector3 target = currentTile.GetCenterPoint() + Vector3.up;
            float distance = Vector3.Distance(prevPos, target);
            float duration = distance / _speed;

            Vector3 direction = (target - prevPos);
            direction.y = 0f;

            // Move tween
            seq.Append(transform.DOMove(target, duration).SetEase(Ease.Linear));

            if (direction != Vector3.zero)
            {
                Quaternion lookRot = Quaternion.LookRotation(direction);
                seq.Join(transform.DORotateQuaternion(lookRot, Mathf.Min(0.25f, duration)));
            }

            seq.AppendCallback(() =>
            {
                onTileReached?.Invoke(currentTile);

                if (!interacted && currentTile.HasInteractable)
                {

                    _playerController.ToogleIsInteracting(true);

                    // Stop sequence after interacting
                    seq.Kill();
                    _isMoving = false;
                    _playerController.ToggleIsMoving(false);
                    onGoalReached?.Invoke(currentTile);
                }
            });

            prevPos = target;

            if (i == cellPath.Count - 1)
            {
                seq.AppendCallback(() =>
                {
                    if (!interacted)
                    {
                        _isMoving = false;
                        _playerController.ToggleIsMoving(false);
                        onGoalReached?.Invoke(currentTile);
                    }
                });
            }
        }

        seq.SetLink(gameObject);
        seq.Play();
    }



    // Update is called once per frame
    void Update()
    {



    }

}
