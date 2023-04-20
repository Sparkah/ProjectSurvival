using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Weapons.Projectiles.Modificators.Movement
{
    public class PlayerFollower : MonoBehaviour
    {
        [Inject(Id = "Player")] private Transform _playerTransform;

        private void FixedUpdate()
        {
            transform.position = _playerTransform.position;
        }
    }
}
