using UnityEngine;

namespace _ProjectSurvival.Scripts.Weapons.Projectiles.Modificators.Movement
{
    public class EnemyFollower : MonoBehaviour
    {
        [SerializeField] private Transform _gameobjectToFollow;
        public Transform Position => _gameobjectToFollow;

        private void FixedUpdate()
        {
            transform.position = _gameobjectToFollow.position;
        }
    }
}