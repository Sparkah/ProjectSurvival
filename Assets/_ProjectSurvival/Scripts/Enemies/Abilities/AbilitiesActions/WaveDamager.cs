using System;
using System.Collections;
using System.Threading;
using _ProjectSurvival.Scripts.Weapons.Projectiles.Types;
using _ProjectSurvival.Scripts.Weapons.WeaponTypes;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _ProjectSurvival.Scripts.Enemies.Abilities.AbilitiesActions
{
    public class WaveDamager : Ability
    {
        [SerializeField] private ScalableProjectile _projectile;
        [SerializeField] private WeaponTypeSO _weapon;
        [SerializeField] private float _cooldown = 2f;
        
        private CancellationTokenSource _cancellationTokenSource;
        private bool _startFiring = true;

        private void Awake()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _projectile.OnFiringComplete += ReturnToPool;
        }

        private void ReturnToPool()
        {
            _projectile.transform.localScale = Vector3.zero;
        }

        public override void ImplementAbility(EnemyAbilities ability)
        {
            if (ability != EnemyAbilities.WaveDamager) return;
            _projectile.PrepareForLaunch(_weapon.GetProjectileSettings(2), 0);
            _projectile.CustomPrepareForLaunch(_weapon.GetProjectileSettings(2));
            _projectile.CustomLaunch(transform.position,transform.position);

            if (_startFiring)
            {
                Firing().Forget();
                _startFiring = false;
            }
        }
        
        private async UniTaskVoid Firing()
        {
            while (true)
            {
                await UniTask.Delay(
                    TimeSpan.FromSeconds(_cooldown),
                    ignoreTimeScale: false,
                    cancellationToken: _cancellationTokenSource.Token);
                
                ImplementAbility(EnemyAbilities.WaveDamager);
            }
        }
        
        

        private void OnDestroy()
        {
            _cancellationTokenSource.Cancel();
            _projectile.OnFiringComplete -= ReturnToPool;
        }

        private void OnDisable()
        {
            ReturnToPool();
        }
    }
}