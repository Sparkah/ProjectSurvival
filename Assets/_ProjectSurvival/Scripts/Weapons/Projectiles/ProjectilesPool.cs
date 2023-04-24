using _ProjectSurvival.Scripts.Pool;
using _ProjectSurvival.Scripts.Weapons.ActiveWeapons;
using _ProjectSurvival.Scripts.Weapons.Projectiles.Types;
using Zenject;

namespace _ProjectSurvival.Scripts.Weapons.Projectiles
{
    public class ProjectilesPool : GameObjectPool<WeaponProjectile>
    {
        [Inject] private ActiveWeapons.ActiveWeapons _activeWeapon;

        private void Start()
        {
            CheckForUpgradeType(null);
            _activeWeapon.OnFire += CheckForUpgradeType;
        }

        private void CheckForUpgradeType(ActiveWeapon activeWeapon)
        {
            var projectiles = GetComponentsInChildren<CircularProjectile>(true);
            if (projectiles.Length == 0) return;
            
            SetUpCircualarProjectiles(projectiles);
        }

        private void SetUpCircualarProjectiles(CircularProjectile[] projectiles)
        {
            var index = 0;
            if (projectiles == null) return;
            foreach (var projectile in projectiles)
            {
                projectile.SetUpProjectiles(index, projectiles.Length);
                index += 1;
            }
        }

        private void OnDestroy()
        {
            _activeWeapon.OnFire -= CheckForUpgradeType;
        }
    }
}