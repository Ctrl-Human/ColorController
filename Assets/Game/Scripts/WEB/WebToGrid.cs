using System;
using System.Linq;
using UnityEngine;

public class WebToGrid : MonoBehaviour
{
    [SerializeField] private GameObject _playerTest;
    [SerializeField] private float followSpeed = 5f; // units per second
    private Vector3 targetPosition;
    [SerializeField] private PlayerController _playerPink;
    [SerializeField] private PlayerController _playerGreen;
    [SerializeField] private NPC _npc;

    private const int GRID_SIZE = 20;      // logical grid
    private const float CELL_WORLD_SIZE = 2f; // each cell is 2x2 Unity units

    internal void UpdateWebDataToGrid(PayloadColorData data)
    {
       // if (data?.client?.blobs == null || data.client.blobs.Count() == 0) return;

        float srcWidth = 1920;   // pixel width of your camera/frame
        float srcHeight = 1080;  // pixel height of your camera/frame
        Debug.Log("cent pink: " + data.pink.centroid.X );
        Debug.Log("cent green: " + data.green.centroid.X);
        
             Vector2Int cellKeyPink = PixelToGrid(
                 new Vector2(data.pink.centroid.X, data.pink.centroid.Y),
                 srcWidth,
                 srcHeight
             );

        Vector2Int cellKeyGreen = PixelToGrid(
    new Vector2(data.green.centroid.X, data.green.centroid.Y),
    srcWidth,
    srcHeight
);

     //   FloorTile _tilePink =  GridManager.Instance.GetTileAt(cellKeyPink.x, cellKeyPink.y);
      // FloorTile _tileGreen = GridManager.Instance.GetTileAt(cellKeyGreen.x, cellKeyGreen.y);


    }


    public static Vector2Int PixelToGrid(Vector2 p, float srcWidth, float srcHeight)
    {
        // Clamp to pixel bounds
        p.x = Mathf.Clamp(p.x, 0, srcWidth - 1);
        p.y = Mathf.Clamp(p.y, 0, srcHeight - 1);

        // Compute cell size in pixels
        float cellW = srcWidth / GRID_SIZE;
        float cellH = srcHeight / GRID_SIZE;


        int gx = Mathf.FloorToInt(p.x / cellW);
        int gy = Mathf.FloorToInt((srcHeight - p.y) / cellH);

        // Clamp to grid bounds
        gx = Mathf.Clamp(gx, 0, GRID_SIZE - 1);
        gy = Mathf.Clamp(gy, 0, GRID_SIZE - 1);

        return new Vector2Int(gx, gy);
    }


    public static Rect PixelRectToGridRect(Rect pixelRect, float srcWidth, float srcHeight)
    {
        float unitX = srcWidth / GRID_SIZE;
        float unitY = srcHeight / GRID_SIZE;

        float gridX = pixelRect.x / unitX;
        float gridY = (srcHeight - pixelRect.y - pixelRect.height) / unitY; // flip Y
        float gridWidth = pixelRect.width / unitX;
        float gridHeight = pixelRect.height / unitY;

        return new Rect(gridX, gridY, gridWidth, gridHeight);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SingleGridObject _tileGreen = GridManager.Instance.GetTileAt(5,5);
            _playerGreen.MoveToCell(_tileGreen, (floorTile) =>
            {
                Debug.Log("angekommen");
                if(floorTile.Interactable != null)
                {
                    floorTile.Interactable.Interact();
                }
            });
        }

        }


}
