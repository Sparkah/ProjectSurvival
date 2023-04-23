using System.Collections;
using UnityEngine;

namespace _ProjectSurvival.Scripts.Weapons.Projectiles.Modificators
{
    public class ProjectileDestroyer : MonoBehaviour
    {
        [SerializeField] private WeaponProjectile _projectile;
        [SerializeField] private float _timeUntilDestroy;

        private void OnEnable()
        {
            StartCoroutine(RemoveBullet());
        }

        private IEnumerator RemoveBullet()
        {
            yield return new WaitForSeconds(_timeUntilDestroy);
            _projectile.ReturnToPool();
        }
    }
}