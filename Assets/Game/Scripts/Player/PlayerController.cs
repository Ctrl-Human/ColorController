using DG.Tweening;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool IsMoving => _isMoving;
    public bool IsInteracting => _isInteracting;

    public SingleGridObject CurrentCell => _currentCell;
    public TileAccess TileAccess => _playerAccess;
    [SerializeField] private bool _isMoving = false;
    [SerializeField] private Vector3 _targetCell;
    [SerializeField] private LayerMask _floorLayer;
    [SerializeField] private TileAccess _playerAccess;
    [SerializeField] private SingleGridObject _currentCell;
    [SerializeField] private float _speed = 6f;
    private bool _isInteracting = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    [SerializeField] private PlayerMovementModule _playerMovementModule;


    public void Initialize(SingleGridObject startCell)
    {
        _playerMovementModule.Initialize(this,() =>
        {

        });
        _currentCell = startCell;
        _playerMovementModule.AssignStartCell(startCell);
    }

    public SingleGridObject GetTileAtPosition()
    {
        Ray ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, 2f, _floorLayer))
        {
            FloorTile tile = hit.collider.GetComponent<FloorTile>();
          //  Debug.Log("player " + tile.Key);
            return tile;
        }
        return null;
    }
    public void MoveToCell(SingleGridObject tile, Action<SingleGridObject> onComplete)
    {
       // _playerMovementModule.MoveToCell(tile, onComplete);
        //_playerMovementModule.MoveToCell(tile);
    }

    public void FollowAStarPath(
        List<SingleGridObject> path,
        Action<SingleGridObject> onStepComplete = null,
        Action<SingleGridObject> onAllComplete = null
    )
    {
        Debug.Log("shall astar follow");
            _playerMovementModule.MoveToCell(path, (SingleGridObject cell) =>
            {
                _currentCell = cell;
            }, onAllComplete);

    }

   internal void ToggleIsMoving(bool value)
    {
        _isMoving = value;
    }

    internal void ToogleIsInteracting(bool value)
    {
        _isInteracting = value;
    }

}
