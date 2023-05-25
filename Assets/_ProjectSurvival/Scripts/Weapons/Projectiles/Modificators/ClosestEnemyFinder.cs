using System.Collections.Generic;
using _ProjectSurvival.Scripts.DamageSystem;
using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Weapons.Projectiles.Modificators
{
    public class ClosestEnemyFinder : MonoBehaviour
    {
        private List<DamagableObject> _enemiesInRange = new List<DamagableObject>();
        private Transform _closestEnemy;

        [Inject(Id = "Player")] private Transform _playerTransform;

        public bool ReturnClosestEnemy(out Transform enemy)
        {
            float distanceToClosestEnemy = 999;
            
            if (_enemiesInRange.Count > 0)
            {
                foreach (var closestEnemy in _enemiesInRange)
                {
                    if (closestEnemy == null || !closestEnemy.enabled)
                    {
                        _enemiesInRange.Remove(closestEnemy);
                    }

                    if (closestEnemy != null)
                    {
                        var distanceToCurrentEnemy = Vector3.Distance(transform.position,
                            closestEnemy.gameObject.transform.position);
                        if (!(distanceToCurrentEnemy < distanceToClosestEnemy)) continue;
                        distanceToClosestEnemy = distanceToCurrentEnemy;
                        _closestEnemy = closestEnemy.gameObject.transform;
                    }
                }
                enemy = _closestEnemy;
                return true;
            }

            enemy = default;
            return false;
        }

        private void FixedUpdate()
        {
            transform.position = _playerTransform.position;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(out DamagableObject damagableObject)) return;
            _enemiesInRange.Add(damagableObject);
            damagableObject.OnDefeatDamagable += RemoveEnemyFromList;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.TryGetComponent(out DamagableObject damagableObject)) return;
            _enemiesInRange.Remove(damagableObject);
            damagableObject.OnDefeatDamagable -= RemoveEnemyFromList;
        }

        private void RemoveEnemyFromList(DamagableObject enemy)
        {
            _enemiesInRange.Remove(enemy);
        }
    }
}
