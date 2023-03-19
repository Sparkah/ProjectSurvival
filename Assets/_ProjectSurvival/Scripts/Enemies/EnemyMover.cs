using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
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
        _spriteRenderer.flipX = _direction.x > 0;
        //transform.LookAt(_player);
        //transform.rotation = Quaternion.Euler(new Vector2(transform.rotation.x, 0));
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
