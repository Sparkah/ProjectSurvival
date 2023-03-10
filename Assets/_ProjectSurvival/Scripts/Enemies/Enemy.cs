using _ProjectSurvival.Scripts.DamageSystem;
using _ProjectSurvival.Scripts.Experience;
using _ProjectSurvival.Scripts.Gold;
using UnityEngine;
using UnityEngine.Pool;

namespace _ProjectSurvival.Scripts.Enemies
{
    public class Enemy : MonoBehaviour, IPoolableObject<Enemy>
    {
        [SerializeField] private DamagableObject _damagableObject;
        [SerializeField] private EnemyMover _enemyMover;
        [SerializeField] private ObjectAppearance _enemyAppearance;
        [SerializeField] private ExperienceHolder _experienceHolder;
        [SerializeField] private GoldHolder _goldHolder;
        [SerializeField] private DamageDealer _damageDealer;
        
        private float _damage;
        private IObjectPool<Enemy> _pool;

        public void Init(IObjectPool<Enemy> pool)
        {
            _pool = pool;
            _damagableObject.OnDefeat += ReturnToPool;
            _damageDealer.OnDamagableTouched += ReturnToPoolOnPlayerTouched;
        }

        public void Destroy()
        {
            _damagableObject.OnDefeat -= ReturnToPool;
            _damageDealer.OnDamagableTouched -= ReturnToPoolOnPlayerTouched;
        }

        public void ReturnToPool()
        {
            Debug.Log(name + " defeated - return to pool");
            _goldHolder.DropGold();
            _experienceHolder.DropExperiencePoint();
            _pool.Release(this);
        }

        public void DefineType(EnemyTypeSO enemyType)
        {
            _damagableObject.SetupHealth(enemyType.BaseHealth);
            _enemyMover.SetupSpeed(enemyType.BaseSpeed);
            _enemyAppearance.SetupSprite(enemyType.AppearanceSpriteFront);
            _damage = enemyType.BaseDamage;
            _experienceHolder.SetupExperience(enemyType.BaseExperience);
            _goldHolder.SetUp(enemyType.BaseGold);
        }

        public void Restore(Vector3 appearPoint, Transform target)
        {
            transform.position = appearPoint;
            _damagableObject.RestoreDurability();
            _enemyMover.Construct(target);
        }

        private void ReturnToPoolOnPlayerTouched(IDamagable go)
        {
            go.TakeDamage(_damage);
            _pool.Release(this);
        }
    }
}