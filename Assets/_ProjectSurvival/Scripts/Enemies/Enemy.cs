using _ProjectSurvival.Scripts.Experience;
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
        
        private float _damage;
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
        }

        public void Restore(Vector3 appearPoint, Transform target)
        {
            transform.position = appearPoint;
            _damagableObject.RestoreDurability();
            _enemyMover.Construct(target);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out DamagableObject damagableObject))
            {
                damagableObject.TakeDamage(_damage);
                ReturnToPool();
            }
        }
    }
}
