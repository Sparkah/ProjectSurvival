using System;
using _ProjectSurvival.Scripts.Stats;
using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Player
{
    public class PlayerMover : MonoBehaviour
    {
        public Vector2 MovementDirection => _movementDirection;
        
        [SerializeField] private Transform _playerSpriteTransform;
        [SerializeField] private float _speed = 0.5f;
        //[SerializeField] private float _rotationSpeed = 2f;
        
        private Rigidbody2D _agent;
        private Animator _animator;
        private const int _speedIncreaseConstant = 10000;
        private Vector2 _movementDirection;
        private ActiveStats _activeStats;
        private float _initialSpeed;
        private Vector2 _input;

        [Inject]
        private void Construct(ActiveStats activeStats)
        {
            _initialSpeed = _speed;
            _activeStats = activeStats;
            _activeStats.OnWalkSpeedStatChanged += UpgradeMoveSpeed;
        }

        void Start()
        {
            _agent = gameObject.GetComponent<Rigidbody2D>();
            //Чтобы задать навпрление стрельбы при старте игры
            //Right - ибо тестовый персонаж при старте смотрит вправо
            //Можно вынести в инспектор?
            _movementDirection = transform.right;
        }

        void Update()
        {
            _input = GetInput();
            //Move(input);
            Rotate(_input);
            if (_input.magnitude != 0)
                _movementDirection = _input;
        }

        private void FixedUpdate()
        {
            Move(_input);
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
            _agent.AddForce(targetVector * (speed * _speedIncreaseConstant));
        }

        private void Rotate(Vector3 input)
        {
            if (input.x != 0)
            {
                Vector3 playerTransformScale = _playerSpriteTransform.localScale;
                playerTransformScale.x = Mathf.Abs(playerTransformScale.x);
                playerTransformScale.x *= input.x < 0 ? -1 : 1;
                _playerSpriteTransform.localScale = playerTransformScale;
            }
        }

        private void UpgradeMoveSpeed(float percentage)
        {
            _speed = _initialSpeed + (_initialSpeed*percentage)/100;
            //Debug.Log(_speed);
        }

        private void OnDestroy()
        {
            _activeStats.OnWalkSpeedStatChanged -= UpgradeMoveSpeed;
        }
    }
}