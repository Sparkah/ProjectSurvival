using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Weapons.Projectiles.Modificators
{
    public class ClosestEnemyFinder : MonoBehaviour
    {
        private List<Collider2D> _enemiesInRange = new List<Collider2D>();
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
                    var distanceToCurrentEnemy = Vector3.Distance(transform.position, closestEnemy.gameObject.transform.position);
                    if (!(distanceToCurrentEnemy < distanceToClosestEnemy)) continue;
                    distanceToClosestEnemy = distanceToCurrentEnemy;
                    _closestEnemy = closestEnemy.gameObject.transform;
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
            if (!other.TryGetComponent(out Collider2D enemy)) return;
            _enemiesInRange.Add(enemy);
//            Debug.Log("enemy added");
            //enemyNavMesh.GetComponentInChildren<Enemy>().OnEnemyDeath += RemoveEnemyFromList;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.TryGetComponent(out Collider2D enemy)) return;
           // Debug.Log("enemy removed");
            // Debug.Log("removing enemy from list" + _enemiesInRange.Count + "name " + gameObject.name);
            _enemiesInRange.Remove(enemy);
        }

        private void RemoveEnemyFromList(Collider2D enemy)
        {
            _enemiesInRange.Remove(enemy);
            //Debug.Log("removing enemy from list _enemiesCoun = " + _enemiesInRange.Count + " name " + gameObject.name);
        }
    }
}
