using UnityEngine;
using UnityEngine.AI;

namespace _ProjectSurvival.Scripts.Player
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float _speed = 150f;
        [SerializeField] private float _rotationSpeed = 2f;
    
        //private NavMeshAgent _agent; // возможно придется вернуть навмеш
        private Rigidbody2D _agent;
        private Animator _animator;
        private const int _speedIncreaseConstant = 10000;
        private Vector2 _movementDirection;

        public Vector2 MovementDirection => _movementDirection;

        void Start()
        {
            _agent = gameObject.GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            var input = GetInput();
            Move(input);
            Rotate(input);
            if (input.magnitude != 0)
                _movementDirection = input;
        }
    
        private Vector2 GetInput()
        {
            var horizontal = 0.0f;
            var vertical = 0.0f;
        
            horizontal = UltimateJoystick.GetHorizontalAxis("Movement");
            vertical = UltimateJoystick.GetVerticalAxis("Movement");

            var input = new Vector2(horizontal, vertical);
            return input;
        }
    
        private void Move(Vector2 input)
        {
            var moveDirection = new Vector2(input.x, input.y);
            
            MoveTowardTarget(moveDirection);
        }

        private void MoveTowardTarget(Vector3 targetVector)
        {
            var speed = _speed * Time.deltaTime;
            _agent.AddForce(targetVector * (speed * _speedIncreaseConstant * Time.deltaTime));
        }

        private void Rotate(Vector3 input)
        {
            //Приделать сюда повороты
            //var targetVector = new Vector3(input.x, 0, input.z);
            //RotateTowardMovementVector(targetVector);
        }

        private void RotateTowardMovementVector(Vector3 movementDirection)
        {
            if (movementDirection.magnitude == 0)
            {
                return;
            }
        
            var rotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _rotationSpeed);
        }
    }
}