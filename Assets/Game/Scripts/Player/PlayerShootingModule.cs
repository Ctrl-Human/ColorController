using System;
using UnityEngine;

public class PlayerShootingModule : PlayerBaseModule
{
    [SerializeField] private PlayerController _playerController;

    public override void Initialize(PlayerController playercontroller, Action onComplete)
    {
        _playerController = playercontroller;
        onComplete?.Invoke();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
