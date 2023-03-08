using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour, IPoolableObject<Enemy>
{
    [SerializeField] private DamagableObject _damagableObject;
    [SerializeField] private EnemyMover _enemyMover;
    private IObjectPool<Enemy> _pool;

    public void Init(IObjectPool<Enemy> pool)
    {
        _pool = pool;
        _damagableObject.OnDefeat += ReturnToPool;
    }

    public void Destroy()
    {
        _damagableObject.OnDefeat -= ReturnToPool;
    }

    public void ReturnToPool()
    {
        Debug.Log(name + " defeated - return to pool");
        _pool.Release(this);
    }

    public void DefineType(EnemyTypeSO enemyType)
    {
        _damagableObject.SetupHealth(enemyType.BaseHealth);
        _enemyMover.SetupSpeed(enemyType.BaseSpeed);
    }

    public void Restore(Vector3 appearPoint, Transform target)
    {
        transform.position = appearPoint;
        _damagableObject.RestoreDurability();
        _enemyMover.Construct(target);
    }
}