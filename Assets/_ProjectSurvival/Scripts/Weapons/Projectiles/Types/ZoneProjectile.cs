using DG.Tweening;
using UnityEngine;

public class ZoneProjectile : WeaponProjectile
{
    private float _speed;
    private float _baseDamage;
    private float _size;

    private void OnDestroy()
    {
        transform.DOKill();
    }

    public override void PrepareForLaunch(ProjectileSettings projectileSettings)
    {
        _baseDamage = projectileSettings.Damage;
        _speed = projectileSettings.Speed;
        _size = projectileSettings.Size;
        transform.localScale = Vector3.one;
        SetDurability(projectileSettings.Durability);
    }

    public override void Launch(Vector3 launchPosition, Vector3 forwardDirection)
    {
        transform.position = launchPosition;
        transform.rotation = Quaternion.identity;

        transform.DOScale(_size, _speed)
            .SetEase(Ease.InSine)
            .SetLoops(2, LoopType.Yoyo)
            .OnComplete(ReturnToPool);
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
