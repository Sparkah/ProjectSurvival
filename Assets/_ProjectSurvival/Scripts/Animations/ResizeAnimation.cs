using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeAnimation : MonoBehaviour
{
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private float _delta = 0.1f;
    private float _defaultSize;

    private void Awake()
    {
        _defaultSize = transform.localScale.x;
    }

    private void OnEnable()
    {
        transform.localScale = new Vector3(_defaultSize, _defaultSize, _defaultSize);
        transform.DOScaleX(_defaultSize - _defaultSize * _delta, _duration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        transform.DOKill();
    }
}
