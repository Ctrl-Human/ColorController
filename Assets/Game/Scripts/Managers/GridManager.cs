using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    // Singleton
    public static GridManager Instance { get; private set; }

    //  [SerializeField] private

    [Header("Grid Settings")]
    [SerializeField] private SingleGridObject _tile;
    [SerializeField] private int _gridSize = 20;
    [SerializeField] private int _cellSize = 2;

    [SerializeField] private AStarGridModule _astarGridModule;

    public int CellSize => _cellSize;
    public int GridSize => _gridSize;



    private Vector2Int[][] _spawnDirections;
    private Dictionary<Vector2Int, SingleGridObject> _grid;
    public IEnumerable<SingleGridObject> AllTiles => _grid.Values;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // prevent duplicates
            return;
        }
        Instance = this;
        _grid = new Dictionary<Vector2Int, SingleGridObject>();

        _spawnDirections = new Vector2Int[3][];
        _spawnDirections[0] = new Vector2Int[1] { new Vector2Int(0, 0) };
        _spawnDirections[1] = new Vector2Int[4] {
    new Vector2Int(1, 0),
    new Vector2Int(-1, 0),
    new Vector2Int(0, 1),
    new Vector2Int(0, -1)
};
        _spawnDirections[2] = new Vector2Int[4] {
    new Vector2Int(-1, 1),
    new Vector2Int(1, 1),
    new Vector2Int(-1, -1),
    new Vector2Int(1, -1)
};

    }

    private void Start()
    {
       StartCoroutine(SpawnFullGridCoroutine());
        // Optional: spawn full grid on start
        // StartCoroutine(SpawnFullGridCoroutine());
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("jo");
            _grid.TryGetValue(new Vector2Int(2, 2), out SingleGridObject startCell);
            _grid.TryGetValue(new Vector2Int(8, 13), out SingleGridObject endCell);
          List<SingleGridObject> _path =  _astarGridModule.FindPath(startCell,endCell);
            Debug.Log(_path);

            foreach (SingleGridObject path in _path)
            {
                path.gameObject.SetActive(false);
            }
        }
    }
    #region Grid Helpers

    // Converts grid coordinates to world position (center of cell)
    private Vector3 GetWorldPosition(Vector2Int gridPos)
    {
        float offsetX = _gridSize * 0.5f * _cellSize;
        float offsetZ = _gridSize * 0.5f * _cellSize;

        return new Vector3(
            (gridPos.x + 0.5f) * _cellSize - offsetX,
            0,
            (gridPos.y + 0.5f) * _cellSize - offsetZ
        );
    }


   public IEnumerable<SingleGridObject> GetNeighbors(SingleGridObject tile)
    {
        Vector2Int[] dirs =
        {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

        foreach (var d in dirs)
        {
            Vector2Int p = tile.GridPos + d;
            if (_grid.TryGetValue(p, out var neighbor))
                
                yield return neighbor;
        }
    }

    #endregion

    #region Spawn Grid

    // Coroutine to spawn a full grid (optional)
    private IEnumerator SpawnFullGridCoroutine()
    {
        for (int z = 0; z < _gridSize; z++)
        {
            for (int x = 0; x < _gridSize; x++)
            {
                Vector2Int pos = new Vector2Int(x, z);
                SpawnCell(pos);
                yield return new WaitForEndOfFrame(); // small delay if you want a "wave spawn"
            }
        }
    }

    // Creates a single cell at grid position
    private void SpawnCell(Vector2Int gridPos)
    {
       // Debug.Log("neu: " + gridPos);
        if (_grid.ContainsKey(gridPos))
        {
           // Debug.LogWarning($"Cell at {gridPos} already exists!");
            return;
        }
        if (!_grid.ContainsKey(gridPos))
        {
            Vector3 worldPos = GetWorldPosition(gridPos);
           // Debug.Log(worldPos + " jo " + gridPos);
          SingleGridObject cell = Instantiate(_tile, worldPos, Quaternion.identity);
            cell.Initialize(gridPos, () =>
            {
                
                //_grid.Add(gridPos, cell);
                //Debug.Log($"Initialized cell at {gridPos}");
            });
            _grid.Add(gridPos, cell);

            //return cell;
        }
    }

    #endregion

    #region Public API

    // Create multiple cells in a row at runtime
    public void CreateCellsAtSpot(int x, int z)
    {
       StartCoroutine(SpawnRoutineCo(x, z));
    }

    public SingleGridObject CheckIfCellExistsAndFree(Vector2Int key)
    {
        
        if(!_grid.ContainsKey(key))
        {
            Debug.LogWarning("SpawnInteractableAtCell: tile is null");
            return null;
        }

        SingleGridObject _tile = _grid[key];
        // Prevent spawning multiple interactables
        if (_tile.HasInteractable)
        {
            Debug.LogWarning("Tile already has an interactable!");
            return null;
        }
        return _tile;
    }

    public void AddInteractableToCell(IInteractable _inter, SingleGridObject tile)
    {


        // Spawn the prefab at the tile's world position

        // Assign it to the tile
        tile.AddObject(_inter);

    }

    public void CreateSingleCell(int x, int y, float cellSize)
    {
        Vector3 pos = new Vector3(x * cellSize, 0, y * cellSize);
        // spawn or activate your grid marker here
        SpawnCell(new Vector2Int(x,y));
    }

    IEnumerator SpawnRoutineCo(int x, int z)
    {
        for (int i = 0; i < _spawnDirections.Length; i++) // outer loop (groups)
        {
            for (int j = 0; j < _spawnDirections[i].Length; j++) // inner loop (directions)
            {
                Vector2Int offset = _spawnDirections[i][j];  // THIS IS THE OFFSET
                Vector2Int spawnPos = new Vector2Int(x + offset.x, z + offset.y);

                Debug.Log($"Spawning at {spawnPos} with offset {offset}");
                SpawnCell(spawnPos);

            }
            yield return new WaitForSeconds(1f);
        }
        yield return null;
    }

    internal SingleGridObject GetTileAt(int cellX, int cellZ)
    {
        Vector2Int key = new Vector2Int(cellX, cellZ);

        if (_grid.TryGetValue(key, out SingleGridObject tile))
        {
            return tile;
        }
        else
        {
            Debug.LogWarning($"GetTileAt: no tile found at ({cellX},{cellZ})");
            return null;
        }
    }

    #endregion
}
