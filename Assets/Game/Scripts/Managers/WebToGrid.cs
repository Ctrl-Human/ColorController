using System;
using System.Linq;
using UnityEngine;

public class WebToGrid : MonoBehaviour
{
    [SerializeField] private GameObject _playerTest;
    [SerializeField] private float followSpeed = 5f; // units per second
    private Vector3 targetPosition;

    private const int GRID_SIZE = 20;      // logical grid
    private const float CELL_WORLD_SIZE = 2f; // each cell is 2x2 Unity units

    /// <summary>
    /// Call this with the latest WebData from the server
    /// </summary>
    internal void UpdateWebDataToGrid(WebData data)
    {
       // if (data?.client?.blobs == null || data.client.blobs.Count() == 0) return;

        float srcWidth = 1920;   // pixel width of your camera/frame
        float srcHeight = 1080;  // pixel height of your camera/frame
        Debug.Log("cent pink: " + data.payload.pink.centroid.x);
        Debug.Log("cent green: " + data.payload.green.centroid.x);
        // Only process the biggest blob (first one)
        //     var blob = data.client.blobs[0];
        
             Vector2Int cellKey = PixelToGrid(
                 new Vector2(data.payload.pink.centroid.x, data.payload.pink.centroid.y),
                 srcWidth,
                 srcHeight
             );

            FloorTile _tile =  GridManager.Instance.GetTileAt(cellKey.x, cellKey.y);

            // Debug.Log($"Blob centroid at pixel ({blob.centroid.x},{blob.centroid.y}) -> Grid cell ({cell.x},{cell.y})");

            // Vector3 worldPos = GridToWorld(cell);
            // targetPosition = worldPos; // update target, but don't set transform.position directly

             PlayerController.Instance.MoveToCell(_tile);
             // Spawn a cell in GridManager, scaled by CELL_WORLD_SIZE
             //GridManager.Instance.CreateSingleCell(cell.x, cell.y, CELL_WORLD_SIZE);
        
    }

    /// <summary>
    /// Convert a pixel coordinate (origin top-left) to a 20x20 grid cell
    /// </summary>
    public static Vector2Int PixelToGrid(Vector2 p, float srcWidth, float srcHeight)
    {
        // Clamp to pixel bounds
        p.x = Mathf.Clamp(p.x, 0, srcWidth - 1);
        p.y = Mathf.Clamp(p.y, 0, srcHeight - 1);

        // Compute cell size in pixels
        float cellW = srcWidth / GRID_SIZE;
        float cellH = srcHeight / GRID_SIZE;

        // Flip Y for Unity (pixel top-left -> bottom-left)
        int gx = Mathf.FloorToInt(p.x / cellW);
        int gy = Mathf.FloorToInt((srcHeight - p.y) / cellH);

        // Clamp to grid bounds
        gx = Mathf.Clamp(gx, 0, GRID_SIZE - 1);
        gy = Mathf.Clamp(gy, 0, GRID_SIZE - 1);

        return new Vector2Int(gx, gy);
    }

    /// <summary>
    /// Optional: Convert a blob rect to a grid rectangle
    /// Useful if you want to mark the full area of the blob
    /// </summary>
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
  
    }

    private Vector3 GridToWorld(Vector2Int cell)
    {
        float offset = GRID_SIZE * 0.5f * CELL_WORLD_SIZE;

        return new Vector3(
            (cell.x + 0.5f) * CELL_WORLD_SIZE - offset,
            0, // y-axis up
            (cell.y + 0.5f) * CELL_WORLD_SIZE - offset
        );
    }

}
