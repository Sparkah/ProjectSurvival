using _ProjectSurvival.Scripts.Audio;
using _ProjectSurvival.Scripts.Weapons.ActiveWeapons;
using _ProjectSurvival.Scripts.Weapons.Projectiles;
using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Player
{

    public class PlayerAttack : MonoBehaviour
    {
        [Inject] private ActiveWeapons _activeWeapons;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private PlayerMover _playerMover; //Bad dependency, find better solution

        public void StartFire()
        {
            _activeWeapons.OnFire += Fire;
        }

        private void OnDestroy()
        {
            _activeWeapons.OnFire -= Fire;
        }

        private void Fire(ActiveWeapon activeWeapon)
        {
            int attackLevel = activeWeapon.Level;
            int projectilesCount = activeWeapon.WeaponType.GetAppearAmount(attackLevel);
            WeaponProjectile attackProjectile;
            for (int i = 0; i < projectilesCount; i++)
            {
                attackProjectile = activeWeapon.GetProjectile();
                attackProjectile.PrepareForLaunch(activeWeapon.WeaponType.GetProjectileSettings(attackLevel));
                attackProjectile.Launch(_firePoint.position, _playerMover.MovementDirection);
                AudioPlayer.Audio.PlaySound(activeWeapon.WeaponType.ShootSound);
            }
        }
    }
}
