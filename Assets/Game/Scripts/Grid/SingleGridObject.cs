using System;
using UnityEngine;

public enum TileType
{
    Flat,
    StairUp,
    StairDown
}

[Flags]
public enum TileAccess
{
    None = 0,
    PlayerPINK = 1 << 0,
    PlayerGREEN = 1 << 1,
    All = PlayerPINK | PlayerGREEN
}



public abstract class SingleGridObject : MonoBehaviour
{
    // Grid identity
    public Vector2Int GridPos { get; private set; }
    public int HeightLevel { get; protected set; }
    public TileType Type { get; protected set; }

    // A* data
    public int G;
    public int H;
    public int F => G + H;
    public SingleGridObject Parent;

    public IInteractable Interactable => _interactable;
    [SerializeField] private IInteractable _interactable;

    public bool IsWalkable => _isWalkable;
    private bool _isWalkable = true;
    [SerializeField] protected TileAccess Access = TileAccess.All;
    public bool HasInteractable => _interactable != null;

    public virtual void Initialize(Vector2Int key, int height, TileType type, Action onComplete, TileAccess tileAccess=TileAccess.All)
    {
        GridPos = key;
        HeightLevel = height;
        Type = type;
        Access = tileAccess;
        onComplete?.Invoke();
    }

    public bool IsAccessibleBy(TileAccess agent)
    {
        return (Access & agent) != 0;
    }



    public abstract void Initialize(Vector2Int key, Action onComplete, TileAccess access);

    public abstract void Destroy();
    public abstract Vector3 GetCenterPoint();
    public virtual void AddObject(IInteractable interObject)
    {
        _interactable = interObject;
        //_isWalkable = false;
    }

    public void ResetPathData()
    {
        G = 0;
        H = 0;
        Parent = null;
    }
}
