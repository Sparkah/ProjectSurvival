using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMover : MonoBehaviour
{
    private Transform _player;
    private Rigidbody2D _rigidbody;

    [SerializeField] private float _speed;
    
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
        transform.LookAt(_player);
        //transform.rotation = Quaternion.Euler(new Vector2(transform.rotation.x, 0));
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(transform.forward*_speed);
    }
}
