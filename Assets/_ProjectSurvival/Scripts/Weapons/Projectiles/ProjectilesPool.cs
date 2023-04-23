using _ProjectSurvival.Scripts.Pool;
using _ProjectSurvival.Scripts.Weapons.Projectiles.Types;

namespace _ProjectSurvival.Scripts.Weapons.Projectiles
{
    public class ProjectilesPool : GameObjectPool<WeaponProjectile>
    {
        private void Start()
        {
            var projectiles = GetComponentsInChildren<CircularProjectile>();
            if (projectiles!=null)
            {
                SetUpCircualarProjectiles(projectiles);
            }
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
    }
}
