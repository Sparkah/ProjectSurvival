using _ProjectSurvival.Scripts.DamageSystem;
using _ProjectSurvival.Scripts.Pool;
using UnityEngine;
using UnityEngine.Pool;

namespace _ProjectSurvival.Scripts.Weapons.Projectiles
{
    public abstract class WeaponProjectile : MonoBehaviour, IPoolableObject<WeaponProjectile>
    {
        [SerializeField] private DamagableObject _projectileDurability;
        [SerializeField] private DamageDealer _damageDealer;
        [SerializeField] private ProjectileAngle _projectileAngle;
        [SerializeField] private bool _calculateAngle = true;
        private IObjectPool<WeaponProjectile> _pool;
        private float _speed;
        private float _baseDamage;
        private float _size;

        public float Speed => _speed;
        public float Size => _size;
        public float BaseDamage => _baseDamage;

        public abstract void CustomPrepareForLaunch(ProjectileSettings projectileSettings);
        public abstract void CustomLaunch(Vector3 launchPosition, Vector3 forwardDirection);
        protected abstract float CalculateDamage();
        protected abstract void PrepareForReturningToPool();

        public void Init(IObjectPool<WeaponProjectile> pool)
        {
            _damageDealer.OnDamagableTouched += DoDamage;
            _damageDealer.OnDestructionTouched += ReturnToPool;
            _projectileDurability.OnDefeat += ReturnToPool;
            _pool = pool;
        }

        public void ReturnToPool()
        {
            PrepareForReturningToPool();
            _projectileDurability.RestoreDurability();
            _pool.Release(this);
        }

        public void PrepareForLaunch(ProjectileSettings projectileSettings, float damageIncreasePercent)
        {
            _baseDamage = projectileSettings.Damage + projectileSettings.Damage*(damageIncreasePercent/100f);
            _speed = projectileSettings.Speed;
            _size = projectileSettings.Size;
            SetDurability(projectileSettings.Durability);
            CustomPrepareForLaunch(projectileSettings);
        }

        public void Launch(Vector3 launchPosition, Vector3 forwardDirection)
        {
            transform.position = launchPosition;
            if (forwardDirection.x == 0)
                forwardDirection.x = 0.001f;
            if (_calculateAngle)
                transform.rotation = _projectileAngle.CalculateAngle(forwardDirection);
            
            CustomLaunch(launchPosition, forwardDirection);
        }

        public void Destroy()
        {
            _damageDealer.OnDamagableTouched -= DoDamage;
            _damageDealer.OnDestructionTouched -= ReturnToPool;
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
}
