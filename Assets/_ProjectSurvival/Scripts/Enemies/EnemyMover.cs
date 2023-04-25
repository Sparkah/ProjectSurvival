using UnityEngine;

namespace _ProjectSurvival.Scripts.Enemies
{
    public class EnemyMover : MonoBehaviour
    {
        private const float AxisChangeSideEdge = 0.4f;
        [SerializeField] private ObjectAppearance _objectAppearance;
        private Transform _player;
        private Rigidbody2D _rigidbody;

        private float _speed;
        private Vector2 _direction;
    
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
            _direction = _player.position - transform.position;
            _direction.Normalize();
            _objectAppearance.ChangeSide(_direction.y > AxisChangeSideEdge, _direction.x > 0);
        }

        private void FixedUpdate()
        {
            _rigidbody.AddForce(_direction*_speed);
        }

        public void SetupSpeed(float speed)
        {
            _speed = speed;
        }
    }
}
