using System;
using UnityEngine;

namespace _ProjectSurvival.Scripts.Enemies
{
    public class EnemyMover : MonoBehaviour
    {
        private const float AxisChangeSideEdge = 0.4f;
        [SerializeField] private ObjectAppearance _objectAppearance;
        [SerializeField] private float startMovementDelay = 1f;
        private float timeSinceSpawned = 0f;
        private bool startMoving = false;
        private Transform _player;
        private Rigidbody2D _rigidbody;

        private float _speed;
        private Vector2 _direction;
        private Transform _parentPos;

        public void Construct(Transform player)
        {
            _player = player;
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            timeSinceSpawned += Time.deltaTime;
            if (timeSinceSpawned >= startMovementDelay)
            {
                startMoving = true;
            }

            if (startMoving)
            {
                _direction = _player.position - transform.position;
                _direction.Normalize();
                _objectAppearance.ChangeSide(_direction.y > AxisChangeSideEdge, _direction.x > 0);
            }
        }

        private void FixedUpdate()
        {
            if (startMoving)
            {
                _rigidbody.AddForce(_direction * _speed);
            }
        }

        public void SetupSpeed(float speed, Transform parentPos)
        {
            _parentPos = parentPos;
            _speed = speed;
        }

        private void OnDestroy()
        {
            startMoving = false;
            transform.position = _parentPos.position;
        }
    }
}
