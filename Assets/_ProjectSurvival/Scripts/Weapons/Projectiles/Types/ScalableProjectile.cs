using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace _ProjectSurvival.Scripts.Weapons.Projectiles.Types
{
    public class ScalableProjectile : WeaponProjectile
    {
        [Tooltip("Uncheck if appear size should not be default prefab size (1,1,1)")]
        [SerializeField] private bool _hasDefaultInitSize = true;
        [Tooltip("Check if after scaling to custom size, projectile should scale back to appear size")]
        [SerializeField] private bool _isScalingBack;

        public event UnityAction OnFiringComplete;

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
            var scalingTweening = transform.DOScale(Size, Speed * 0.05f).SetEase(Ease.InSine);
            if (_isScalingBack)
                scalingTweening.SetLoops(2, LoopType.Yoyo);
            
            scalingTweening.OnComplete(ReturnToPool);
            if (OnFiringComplete != null) scalingTweening.OnComplete(OnFiringComplete.Invoke);
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
}
