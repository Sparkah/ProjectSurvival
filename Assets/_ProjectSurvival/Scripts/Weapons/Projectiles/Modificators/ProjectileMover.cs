using UnityEngine;

namespace _ProjectSurvival.Scripts.Weapons.Projectiles
{
    public class ProjectileMover : MonoBehaviour
    {
        [SerializeField] private WeaponProjectile _weaponProjectile;

        private void FixedUpdate()
        {
            transform.position += transform.forward * _weaponProjectile.Speed * Time.fixedDeltaTime;
        }
    }
}
