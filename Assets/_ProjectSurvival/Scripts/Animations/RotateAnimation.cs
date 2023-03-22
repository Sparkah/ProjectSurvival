using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAnimation : MonoBehaviour
{
    [SerializeField] private float _duration = 0.5f;
    [SerializeField] private float _delta = 30;
    private Vector3 _defaultEuler;

    private void Awake()
    {
        _defaultEuler = transform.localRotation.eulerAngles;
    }

    private void OnEnable()
    {
        transform.localRotation = Quaternion.Euler(_defaultEuler.x, _defaultEuler.y, -_delta);
        transform.DOLocalRotate(new Vector3(_defaultEuler.x, _defaultEuler.y, _delta), _duration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDisable()
    {
        transform.DOKill();
    }
}
