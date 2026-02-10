using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIShootModule : UIBaseModule
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private Image _cross;

    public override void Initialize(UIManager uiManager, Action onComplete)
    {
        _uiManager = uiManager;
        onComplete?.Invoke();
    }

    internal void MoveCrossfade(Vector2 target)
    {
        _cross.rectTransform
            .DOAnchorPos(new Vector2(target.x, target.y), 0.15f)
            .SetEase(Ease.OutSine);
    }
}
