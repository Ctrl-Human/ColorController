using Unity.Burst.CompilerServices;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public static MouseManager Instance { get; private set; }

    [SerializeField] private PlayerController _playerControllerPINK;
    [SerializeField] private PlayerController _playerControllerGREEN;
    [SerializeField] private Vector3 _mousePosition;
    [SerializeField] private GameManager _gameManager;
    private Ray _mouseRay;
    private RaycastHit _hit;
    [SerializeField] private MonoBehaviour _npc;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        _mousePosition = Input.mousePosition;
        _mouseRay = Camera.main.ScreenPointToRay(_mousePosition);

        if (Input.GetMouseButtonDown(0)) // left mouse button
        {
            if (Physics.Raycast(_mouseRay, out _hit))
            {
                // _hit.transform is the FloorTile you clicked
                FloorTile tile = _hit.transform.GetComponentInParent<FloorTile>();
                Debug.Log($"Hit: {_hit.collider.name} | Root: {_hit.collider.transform.root.name}");

                if (tile != null)
                {
                    Debug.Log(tile.Key);
                    _gameManager.MovePlayerOnGrid(tile, _playerControllerGREEN);
                }
            }
        }

        if (Input.GetMouseButtonDown(1)) // left mouse button
        {
            if (Physics.Raycast(_mouseRay, out _hit))
            {
                // _hit.transform is the FloorTile you clicked
                FloorTile tile = _hit.transform.GetComponentInParent<FloorTile>();
                Debug.Log($"Hit: {_hit.collider.name} | Root: {_hit.collider.transform.root.name}");

                if (tile != null)
                {
                    Debug.Log(tile.Key);
                    _gameManager.MovePlayerOnGrid(tile, _playerControllerPINK);

                }
            }
        }
    }



}
