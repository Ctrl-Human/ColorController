using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class FloorTile : SingleGridObject
{
    [SerializeField] private GameObject _floorMesh;
    [SerializeField] private IInteractable _interactable;
    [SerializeField] public Vector2Int Key { get; private set; }
    [SerializeField] private TextMeshProUGUI _visualKey;

    public override void Destroy()
    {
        throw new NotImplementedException();
    }

    public override void Initialize(Vector2Int key, Action onComplete)
    {
        Key = key;
        _visualKey.text = Key.ToString();
        StartAnimation(onComplete);
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

    public void SetInteractable(IInteractable interactable)
    {
        _interactable = interactable;
        _interactable.Initialize();
    }

    public override void AddObject(IInteractable interObject)
    {
        throw new NotImplementedException();
    }

    public bool HasInteractable => _interactable != null;
}
