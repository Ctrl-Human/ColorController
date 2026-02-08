using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovementModule : ModuleBase
{
    private PlayerController _playerController;
    private FloorTile _targetTile;
    [SerializeField] private float _speed = 6f;
    public override void Initialize(PlayerController playercontroller, Action onComplete)
    {
        _playerController = playercontroller;
        onComplete?.Invoke();
    }

    internal void MoveToCell(FloorTile targetCell)
    {
       _targetTile = targetCell;
    }

    // Update is called once per frame
    void Update()
    {

        /* if (_targetCell.x >= (GridManager.Instance.GridSize - 1) * -1 && targetPosition.x <= GridManager.Instance.GridSize - 1 && targetPosition.z <= GridManager.Instance.GridSize - 1 && targetPosition.y >= (GridManager.Instance.GridSize - 1) * -1)
         {*/
       if (_targetTile)
        {
            transform.position = Vector3.Lerp(
                transform.position,
                _targetTile.transform.position,
                _speed * Time.deltaTime
            );
        }

    }

}
