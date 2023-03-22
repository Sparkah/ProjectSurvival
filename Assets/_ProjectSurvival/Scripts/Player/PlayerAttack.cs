using _ProjectSurvival.Scripts.Audio;
using _ProjectSurvival.Scripts.Stats;
using _ProjectSurvival.Scripts.Weapons.ActiveWeapons;
using _ProjectSurvival.Scripts.Weapons.Projectiles;
using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private PlayerMover _playerMover; //Bad dependency, find better solution
        private ActiveStats _activeStats;
        private ActiveWeapons _activeWeapons;
        private float _damageIncreasePercent;

        [Inject]
        private void Construct(ActiveWeapons activeWeapons, ActiveStats activeStats)
        {
            _activeWeapons = activeWeapons;
            _activeStats = activeStats;
            _activeStats.OnBaseDamageStatChanged += SetDamageIncreasePercent;
        }

        private void OnDestroy()
        {
            _activeWeapons.OnFire -= Fire;
            _activeStats.OnBaseDamageStatChanged -= SetDamageIncreasePercent;
        }

        public void StartFire()
        {
            _activeWeapons.OnFire += Fire;
        }

        private void Fire(ActiveWeapon activeWeapon)
        {
            int attackLevel = activeWeapon.Level;
            int projectilesCount = activeWeapon.WeaponType.GetAppearAmount(attackLevel);
            WeaponProjectile attackProjectile;
            for (int i = 0; i < projectilesCount; i++)
            {
                attackProjectile = activeWeapon.GetProjectile();
                attackProjectile.PrepareForLaunch(activeWeapon.WeaponType.GetProjectileSettings(attackLevel), _damageIncreasePercent);
                attackProjectile.Launch(_firePoint.position, _playerMover.MovementDirection);
                AudioPlayer.Audio.PlayOneShotSound(activeWeapon.WeaponType.ShootSound);
            }
        }

        private void SetDamageIncreasePercent(float percent)
        {
            _damageIncreasePercent = percent;
        }
    }
}
