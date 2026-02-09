using System;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector3 _targetCell;
    [SerializeField] private float _speed = 6f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    [SerializeField] private PlayerMovementModule _playerMovementModule;


    public void Initialize()
    {
        _playerMovementModule.Initialize(this,() =>
        {

        });
    }


    public void MoveToCell(SingleGridObject tile, Action<SingleGridObject> onComplete)
    {
        _playerMovementModule.MoveToCell(tile, onComplete);
        //_playerMovementModule.MoveToCell(tile);
    }
}
