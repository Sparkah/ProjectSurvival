using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Weapons.Projectiles.Modificators.Movement
{
    public class ProjectileCircularMover : MonoBehaviour
    {
        [Inject(Id = "Player")] private Transform _playerTransform;
        
        public float Speed = 2f;
        
        [SerializeField] private float _radius = 2f;
        
        private float _elapsedTime;
        private bool _moveProjectileInCircles;
        private float _correctionID;
        private int _arrayLength;

        void Update()
        {
            _elapsedTime += Time.deltaTime * Speed;
            
            if (!_moveProjectileInCircles) return;
            
            float x = _playerTransform.position.x + _radius * Mathf.Cos(_elapsedTime + _correctionID);
            float y = _playerTransform.position.y + _radius * Mathf.Sin(_elapsedTime + _correctionID);
            
            transform.position = new Vector3(x, y, transform.position.z);
        }
        
        public void SetUpProjectiles(int id, int arrayLength)
        {
            if(_arrayLength==arrayLength) return;
            _arrayLength = arrayLength;
            _moveProjectileInCircles = false;
            var circleID = 360 / arrayLength;
            var positionID = circleID * id;
            
            UpdatePosition(positionID);
        }

        private void UpdatePosition(float positionID)
        {
            _correctionID = positionID * Mathf.Deg2Rad;

            float x = _playerTransform.position.x + _radius * Mathf.Cos(_correctionID);
            float y = _playerTransform.position.y + _radius * Mathf.Sin(_correctionID);
            
            transform.position = new Vector3(x, y, transform.position.z);
            _elapsedTime = 0;
            _moveProjectileInCircles = true;
        }
    }
}
