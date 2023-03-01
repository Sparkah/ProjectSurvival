using UnityEngine;
using UnityEngine.AI;

namespace _ProjectSurvival.Scripts.Player
{
    public class PlayerMover : MonoBehaviour
    {
        [SerializeField] private float _speed = 150f;
        [SerializeField] private float _rotationSpeed = 2f;
    
        //private NavMeshAgent _agent;
        private Rigidbody2D _agent;
        private Animator _animator;
        private string _animatorMoveParam = "AnimationPar";
        private DeviceType _playerDevice;
        private const int _speedIncreaseConstant =10000;

        void Start()
        {
            _agent = gameObject.GetComponent<Rigidbody2D>();
            //_animator = GetComponentInChildren<Animator>();
            _playerDevice = SystemInfo.deviceType;
        }

        void Update()
        {
            var input = GetInput();
            Move(input);
            Rotate(input);
        }
    
        private Vector2 GetInput()
        {
            var horizontal = 0.0f;
            var vertical = 0.0f;
        
            horizontal = UltimateJoystick.GetHorizontalAxis("Movement");
            vertical = UltimateJoystick.GetVerticalAxis("Movement");
            
            /*switch (_playerDevice)
            {
                case DeviceType.Desktop:
                    horizontal = Input.GetAxis("Horizontal");
                    vertical = Input.GetAxis("Vertical");
                    break;
                case DeviceType.Handheld:
                    horizontal = UltimateJoystick.GetHorizontalAxis("Movement");
                    vertical = UltimateJoystick.GetVerticalAxis("Movement");
                    break;
            }*/

            var input = new Vector2(horizontal, vertical);
            return input;
        }
    
        private void Move(Vector2 input)
        {
            var moveDirection = new Vector2(input.x, input.y);
            //_animator.SetInteger(_animatorMoveParam, input.magnitude > 0 ? 1 : 0);
            
            Debug.Log(moveDirection);
            MoveTowardTarget(moveDirection);
        }

        private void MoveTowardTarget(Vector3 targetVector)
        {
            var speed = _speed * Time.deltaTime;
            //_agent.Move(targetVector.normalized * (speed * Time.deltaTime));
            _agent.AddForce(targetVector * (speed * _speedIncreaseConstant * Time.deltaTime));
            //Debug.Log("moving agent" + targetVector.normalized);
        }

        private void Rotate(Vector3 input)
        {
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