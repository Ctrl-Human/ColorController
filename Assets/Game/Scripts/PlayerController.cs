using System;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
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

    private void Awake()
    {
        Instance = this;
    }

    public void MoveToCell(FloorTile tile)
    {
        _playerMovementModule.MoveToCell(tile);
    }
}
