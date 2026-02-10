using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private NPC _npc;
    [SerializeField] private Door _door;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SingleGridObject tileP = GridManager.Instance.CheckIfCellExistsAndFree(new Vector2Int(9, 11));
            SingleGridObject tileG = GridManager.Instance.CheckIfCellExistsAndFree(new Vector2Int(9, 3));
            if (tileP!=null)
            {
                var spawned = Instantiate(_door, tileP.transform.position, Quaternion.identity);
                IInteractable inter = spawned.GetComponent<IInteractable>();
                GridManager.Instance.AddInteractableToCell(inter,tileP);
            }
            if (tileG != null)
            {
                var spawned = Instantiate(_door, tileG.transform.position, Quaternion.identity);
                IInteractable inter = spawned.GetComponent<IInteractable>();
                GridManager.Instance.AddInteractableToCell(inter, tileG);
            }
        }
    }
}
