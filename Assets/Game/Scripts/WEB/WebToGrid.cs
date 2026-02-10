using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class WebToGrid : MonoBehaviour
{
    [SerializeField] private PlayerController _playerGREEN;
    [SerializeField] private PlayerController _playerPINK;

    [SerializeField] private GameObject _playerTest;
    [SerializeField] private float followSpeed = 5f; // units per second
    private Vector3 targetPosition;
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private NPC _npc;

    private const int GRID_SIZE = 20;      // logical grid
    private const float CELL_WORLD_SIZE = 2f; // each cell is 2x2 Unity units

    public void Initialize(GameManager manager)
    {
        _gameManager = manager;
    }

    internal (SingleGridObject _green, SingleGridObject _pink) MapWebDataToGrid(PayloadColorData data)
    {

        float srcWidth = 1920;   // pixel width of your camera/frame
        float srcHeight = 1080;  // pixel height of your camera/frame

        
             Vector2Int cellKeyPink = PixelToGrid(
                 new Vector2(data.pink.centroid.X, data.pink.centroid.Y),
                 srcWidth,
                 srcHeight,
                 _gameManager.LevelXoffset , 10, _gameManager.LevelXoffset+10, 14
             );

    Vector2Int cellKeyGreen = PixelToGrid(
                 new Vector2(data.green.centroid.X, data.green.centroid.Y),
                 srcWidth,
                 srcHeight,
                 _gameManager.LevelXoffset, 0, _gameManager.LevelXoffset+10, 4
             );
        Debug.Log(cellKeyPink);

        SingleGridObject _tilePink =  GridManager.Instance.GetTileAt(cellKeyPink.x, cellKeyPink.y);
        SingleGridObject _tileGreen = GridManager.Instance.GetTileAt(cellKeyGreen.x, cellKeyGreen.y);

        FloorTile floorTileGreen = _tileGreen as FloorTile;
        FloorTile floorTilePink = _tilePink as FloorTile;

        //_gameManager.MovePlayerOnGrid(floorTilePink, _playerPINK);
        //_gameManager.MovePlayerOnGrid(floorTileGreen, _playerGREEN);


        return ( floorTileGreen , floorTilePink );

    }

    public static Vector2Int NormalizedToGrid(Vector2 n, int _gridX, int _gridZ, int _gridX2, int _gridZ2)
    {
        int gx = Mathf.FloorToInt(n.x * (_gridX2 - _gridX));
        int gy = Mathf.FloorToInt(n.y * (_gridZ2 - _gridZ));

        gx = Mathf.Clamp(gx, _gridX, _gridX2);
        gy = Mathf.Clamp(gy, _gridZ, _gridZ2);

        return new Vector2Int(gx, gy);
    }
    public static Vector2 PixelToNormalized(Vector2 p, float srcWidth, float srcHeight)
    {
        float nx = 1f - (p.x / srcWidth);  
        float ny = 1f - (p.y / srcHeight);  

        return new Vector2(nx, ny);
    }


    public static Vector2Int PixelToGrid(
        Vector2 pixel,
        float srcWidth,
        float srcHeight,
        int startX,
        int startZ,
        int endX,
        int endZ,
        bool flipX = true,
        bool flipY = true
    )
    {
        float nx = flipX ? 1f - (pixel.x / srcWidth) : pixel.x / srcWidth;
        float nz = flipY ? 1f - (pixel.y / srcHeight) : pixel.y / srcHeight;

        int cellsX = endX - startX + 1;
        int cellsZ = endZ - startZ + 1;

        int gx = Mathf.FloorToInt(nx * cellsX);
        int gz = Mathf.FloorToInt(nz * cellsZ);

        gx = Mathf.Clamp(gx, 0, cellsX - 1) + startX;
        gz = Mathf.Clamp(gz, 0, cellsZ - 1) + startZ;

        return new Vector2Int(gx, gz);
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


}
