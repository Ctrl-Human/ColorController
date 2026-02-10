using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;


public enum PlayerControllerState
{
    multiplayer,
    bus
}


public class GameManager : MonoBehaviour
{
    public int LevelXoffset => _levelXOffset;
    [SerializeField] private MouseManager _mouseManager;
    [SerializeField] private GridManager _gridManager;
    [SerializeField] private PlayerController _playerGREEN;
    [SerializeField] private PlayerController _playerPINK;
    [SerializeField] private WebsocketController _webSocketController;
    [SerializeField] private WebToGrid _webToGrid;
    [SerializeField] private int _levelXOffset = 0;
    [SerializeField] private BusController _busController;

    [SerializeField] private IInteractable _greenInteractable;
    [SerializeField] private IInteractable _pinkInteractable;


    private PlayerControllerState _playerState = PlayerControllerState.bus;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _webToGrid.Initialize(this);
        _webSocketController.Initialize(this);
        // _playerPINK.Initialize()
        _gridManager.SpawnGrid(() =>
        {
            _playerGREEN.Initialize(_gridManager.GetStartTile(_playerGREEN));
            _playerPINK.Initialize(_gridManager.GetStartTile(_playerPINK));
        }, _levelXOffset);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            CameraManager.Instance.SwitchSplitMainCamera();
        }
    }

    internal void GetWebDataColor(PayloadColorData data)
    {
        switch(_playerState) 
        {
            case PlayerControllerState.multiplayer:
            (SingleGridObject green, SingleGridObject pink) = _webToGrid.MapWebDataToGrid(data);
            MovePlayerOnGrid(green, _playerGREEN);
            MovePlayerOnGrid(pink, _playerPINK);
                break;
            case PlayerControllerState.bus:
                _busController.StartDriving();
                _busController.Steer(data.pink.centroid.X);
                float uiX = (Screen.width * 0.5f) - data.green.centroid.X;
                float uiY = (Screen.height * 0.5f) - data.green.centroid.Y;
                UIManager.Instance.UpdateCrossfade(new Vector2(uiX, uiY));

                _busController.Shoot(data.green.centroid.X, data.green.centroid.Y);
                break;
        }
      

    }

    public void MoveCrossfade(Vector2 target)
    {
        UIManager.Instance.UpdateCrossfade(target);
    }

    public void MovePlayerOnGrid(SingleGridObject target, PlayerController _player)
    {
        if (!_player.IsMoving && !_player.IsInteracting )
        {

            SingleGridObject startCell = _player.GetTileAtPosition();
            _player.FollowAStarPath(GridManager.Instance.GetAStarPath(startCell, target), (SingleGridObject _cell) => { }, (SingleGridObject _cell) =>
            {
                if (_cell.HasInteractable)
                {
                  
                    _player.ToogleIsInteracting(true);
                    if (_playerGREEN.IsInteracting && _playerPINK.IsInteracting)
                    {
                        _cell.Interactable.Interact();
                        _playerGREEN.CurrentCell.Interactable.Interact();
                        _playerPINK.CurrentCell.Interactable.Interact();
                    }
                    
                }

                _player.ToggleIsMoving(false);

            /*    if (_cell.GridPos.x > _levelXOffset + 8)
                {
                    _levelXOffset += 10;
                    _gridManager.SpawnGrid(() => { }, _levelXOffset);
                }

                if (_cell.GridPos.x > 20)
                {
                    CameraManager.Instance.SwitchToBusCam();
                    _playerState = PlayerControllerState.bus;
                    _busController.StartDriving();
                }*/
            });

            //_player.ToggleIsMoving(true);
        }

    }



}
