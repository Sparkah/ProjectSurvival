using UnityEngine;
using UnityEngine.Pool;

public abstract class WeaponProjectile : MonoBehaviour, IPoolableObject<WeaponProjectile>
{
    [SerializeField] private DamagableObject _projectileDurability;
    [SerializeField] private DamageDealer _damageDealer;
    private IObjectPool<WeaponProjectile> _pool;

    public abstract void PrepareForLaunch(ProjectileSettings projectileSettings);
    public abstract void Launch(Vector3 launchPosition, Vector3 forwardDirection);
    protected abstract float CalculateDamage();
    protected abstract void PrepareForReturningToPool();

    public void Init(IObjectPool<WeaponProjectile> pool)
    {
        _damageDealer.OnDamagableTouched += DoDamage;
        _damageDealer.OnNotDamagableTouched += ReturnToPool;
        _projectileDurability.OnDefeat += ReturnToPool;
        _pool = pool;
    }

    public void ReturnToPool()
    {
        PrepareForReturningToPool();
        _projectileDurability.RestoreDurability();
        _pool.Release(this);
    }

    public void Destroy()
    {
        _damageDealer.OnDamagableTouched -= DoDamage;
        _damageDealer.OnNotDamagableTouched -= ReturnToPool;
        _projectileDurability.OnDefeat -= ReturnToPool;
    }

    protected void SetDurability(int durability)
    {
        _projectileDurability.SetupHealth(durability);
    }

    private void DoDamage(IDamagable damagable)
    {
        damagable.TakeDamage(CalculateDamage());
        _projectileDurability.TakeDamage(1); //How many enemies can projectile hit (pass through) before destroy
    }
}
