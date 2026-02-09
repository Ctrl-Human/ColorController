using System;
using UnityEngine;

public enum TileType
{
    Flat,
    StairUp,
    StairDown
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

    public bool HasInteractable => _interactable != null;

    public virtual void Initialize(Vector2Int key, int height, TileType type, Action onComplete)
    {
        GridPos = key;
        HeightLevel = height;
        Type = type;

        onComplete?.Invoke();
    }

    public abstract void Initialize(Vector2Int key, Action onComplete);

    public abstract void Destroy();
    public abstract Vector3 GetCenterPoint();
    public virtual void AddObject(IInteractable interObject)
    {
        _interactable = interObject;
        _isWalkable = false;
    }

    public void ResetPathData()
    {
        G = 0;
        H = 0;
        Parent = null;
    }
}
