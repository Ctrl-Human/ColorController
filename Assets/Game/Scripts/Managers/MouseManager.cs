using Unity.Burst.CompilerServices;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public static MouseManager Instance { get; private set; }


    [SerializeField] private Vector3 _mousePosition;
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
                FloorTile tile = _hit.transform.GetComponent<FloorTile>();
                if (tile != null)
                {

                    //GridManager.Instance.SpawnInteractableAtCell(_npc, tile);
                }
            }
        }
    }



}
