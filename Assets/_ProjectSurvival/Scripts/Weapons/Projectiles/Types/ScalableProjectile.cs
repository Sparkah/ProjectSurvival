using DG.Tweening;
using UnityEngine;

public class ScalableProjectile : WeaponProjectile
{
    [Tooltip("Uncheck if appear size should not be default prefab size (1,1,1)")]
    [SerializeField] private bool _hasDefaultInitSize = true;
    [Tooltip("Check if after scaling to custom size, projectile should scale back to appear size")]
    [SerializeField] private bool _isScalingBack;

    private void OnDestroy()
    {
        transform.DOKill();
    }

    public override void CustomPrepareForLaunch(ProjectileSettings projectileSettings)
    {
        transform.localScale = _hasDefaultInitSize ? Vector3.one : new Vector3(Size, Size, Size);
    }

    public override void CustomLaunch(Vector3 launchPosition, Vector3 forwardDirection)
    {
        var scalingTweening = transform.DOScale(Size, Speed).SetEase(Ease.InSine);
        if (_isScalingBack)
            scalingTweening.SetLoops(2, LoopType.Yoyo);
        scalingTweening.OnComplete(ReturnToPool);
    }

    protected override float CalculateDamage()
    {
        return BaseDamage;
    }

    protected override void PrepareForReturningToPool()
    {
        transform.DOKill();
    }
}
