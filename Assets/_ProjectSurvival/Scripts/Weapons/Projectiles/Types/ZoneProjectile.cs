using DG.Tweening;
using UnityEngine;

public class ZoneProjectile : WeaponProjectile
{
    [Tooltip("Uncheck if appear size should not be default prefab size (1,1,1)")]
    [SerializeField] private bool _hasDefaultInitSize = true;
    [Tooltip("Check if after scaling to custom size, projectile should scale back to appear size")]
    [SerializeField] private bool _isScalingBack;
    private float _speed;
    private float _baseDamage;
    private float _size;

    protected float Speed => _speed;
    protected float Size => _size;

    private void OnDestroy()
    {
        transform.DOKill();
    }

    public override void PrepareForLaunch(ProjectileSettings projectileSettings)
    {
        _baseDamage = projectileSettings.Damage;
        _speed = projectileSettings.Speed;
        _size = projectileSettings.Size;
        transform.localScale = _hasDefaultInitSize ? Vector3.one : new Vector3(_size, _size, _size);
        SetDurability(projectileSettings.Durability);
    }

    public override void Launch(Vector3 launchPosition, Vector3 forwardDirection)
    {
        transform.position = launchPosition;
        transform.rotation = Quaternion.LookRotation(forwardDirection);

        var scalingTweening = transform.DOScale(_size, _speed).SetEase(Ease.InSine);
        if (_isScalingBack)
            scalingTweening.SetLoops(2, LoopType.Yoyo);
        scalingTweening.OnComplete(ReturnToPool);
    }

    protected override float CalculateDamage()
    {
        return _baseDamage;
    }

    protected override void PrepareForReturningToPool()
    {
        transform.DOKill();
    }
}
