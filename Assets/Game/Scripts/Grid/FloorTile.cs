using DG.Tweening;
using System;
using TMPro;
using UnityEditor.Build.Content;
using UnityEngine;

public class FloorTile : SingleGridObject
{


    [SerializeField] private GameObject _floorMesh;
    [SerializeField] public Vector2Int Key { get; private set; }
    [SerializeField] private TextMeshProUGUI _visualKey;

    [Header("Tile Logic")]
    [SerializeField] private int _heightLevel=0;
    [SerializeField] private TileType _tileType = TileType.Flat;

    public override void Destroy()
    {
        throw new NotImplementedException();
    }

    public override void Initialize(Vector2Int key, Action onComplete)
    {
        base.Initialize(key, _heightLevel, _tileType , onComplete);

        _visualKey.text = $"{key}\nH:{HeightLevel}";
        StartAnimation(null);
    }

    private void StartAnimation(Action onComplete)
    {

        Sequence seq = DOTween.Sequence();

        seq.Join(_floorMesh.transform.DORotate(new Vector3(0, 180f, 0), 1f))
            .Join(_floorMesh.transform.DOPunchPosition(new Vector3(0, -5f, 0), 1.5f, 2, 2.5f))
               .Append(_floorMesh.transform.DOLocalMove(Vector3.zero, 0.3f))
            .SetLink(gameObject)
            .OnComplete(() =>
            {
                onComplete?.Invoke();
            });
    }




    public override Vector3 GetCenterPoint()
    {
        Vector3 _pos = new Vector3(
            transform.position.x + GridManager.Instance.CellSize * 0.25f,
            transform.position.y,
            transform.position.z + GridManager.Instance.CellSize * 0.25f);
        return _pos;
    }


}
