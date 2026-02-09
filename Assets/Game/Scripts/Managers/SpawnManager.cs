using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private NPC _npc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SingleGridObject tile = GridManager.Instance.CheckIfCellExistsAndFree(new Vector2Int(2, 3));

            if (tile!=null)
            {
                var spawned = Instantiate(_npc, tile.transform.position, Quaternion.identity);
                IInteractable inter = spawned.GetComponent<IInteractable>();
                GridManager.Instance.AddInteractableToCell(inter,tile);
            }
        }
    }
}
