using _ProjectSurvival.Scripts.Weapons.Projectiles;
using _ProjectSurvival.Scripts.Weapons.WeaponTypes;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine.Events;

namespace _ProjectSurvival.Scripts.Weapons.ActiveWeapons
{
    public class ActiveWeapon
    {
        private WeaponTypeSO _attackType;
        private int _level;
        private ProjectilesPool _pool;
        private CancellationTokenSource _cancellationTokenSource;

        public WeaponTypeSO WeaponType => _attackType;
        public int Level => _level;

        public event UnityAction<ActiveWeapon> OnFire;

        public ActiveWeapon(WeaponTypeSO attackType, ProjectilesPool pool)
        {
            _pool = pool;
            _attackType = attackType;
            _level = 1;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void LevelUp()
        {
            _level++;
        }

        public bool HasMaximumLevel()
        {
            return _level == _attackType.MaximumLevel;
        }

        public WeaponProjectile GetProjectile()
        {
            return _pool.Pool.Get();
        }

        public void StartFiring()
        {
            Firing().Forget();
        }

        public void CancelFiring()
        {
            _cancellationTokenSource.Cancel();
        }

        private async UniTaskVoid Firing()
        {
            while (true)
            {
                OnFire?.Invoke(this);
                await UniTask.Delay(
                    TimeSpan.FromSeconds(_attackType.GetAppearFrequency(_level)),
                    ignoreTimeScale: false,
                    cancellationToken: _cancellationTokenSource.Token);
            }
        }
    }
}
