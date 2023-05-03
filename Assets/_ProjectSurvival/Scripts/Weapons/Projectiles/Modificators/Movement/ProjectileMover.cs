using _ProjectSurvival.Scripts.DamageSystem;
using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Weapons.Projectiles.Modificators.Movement
{
    public class ProjectileMover : MonoBehaviour
    {
        [SerializeField] private WeaponProjectile _weaponProjectile;
        [SerializeField] private bool _autoAim;

        private bool _markClosestEnemy;
        private DamagableObject _closestEnemy;
        private bool _flyStraight;

        [Inject] private ClosestEnemyFinder _enemyFinder;

        private void FixedUpdate()
        {
            if (!_autoAim)
            {
                ShootStraight();
            }

            if (_autoAim)
            {
                TryAimAtEnemy();
            }
        }

        private void TryAimAtEnemy()
        {
            if (_enemyFinder.ReturnClosestEnemy(out Transform closestEnemy) && !_markClosestEnemy)
            {
                _markClosestEnemy = true;
                closestEnemy.TryGetComponent(out DamagableObject closestEnemyDamagableObject);
                {
                    _closestEnemy = closestEnemyDamagableObject;
                }
            }

            if (_closestEnemy && _closestEnemy.gameObject.activeInHierarchy && !_flyStraight)
            {
                Vector3 movement = Vector3.MoveTowards(transform.position, _closestEnemy.transform.position,
                    _weaponProjectile.Speed * Time.fixedDeltaTime);

                transform.position = movement;
                transform.LookAt(_closestEnemy.gameObject.transform);

                if (transform.position == _closestEnemy.transform.position)
                {
                    _flyStraight = true;
                }
                
                return;
            }

            _flyStraight = true;
            ShootStraight();
        }

        private void OnDisable()
        {
            if (!_autoAim) return;
            _markClosestEnemy = false;
            _closestEnemy = null;
            _flyStraight = false;
        }

        private void ShootStraight()
        {
            transform.position += transform.forward * _weaponProjectile.Speed * Time.fixedDeltaTime;
        }
    }
}