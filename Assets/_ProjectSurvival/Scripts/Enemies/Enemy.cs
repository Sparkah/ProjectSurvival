using _ProjectSurvival.Scripts.DamageSystem;
using _ProjectSurvival.Scripts.Enemies.Evolution;
using _ProjectSurvival.Scripts.Experience;
using _ProjectSurvival.Scripts.Gold;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace _ProjectSurvival.Scripts.Enemies
{
    public class Enemy : MonoBehaviour, IPoolableObject<Enemy>
    {
        [Inject] private EnemiesEvolutionTracker _enemiesEvolutionTracker;
        [SerializeField] private DamagableObject _damagableObject;
        [SerializeField] private EnemyMover _enemyMover;
        [SerializeField] private ObjectAppearance _enemyAppearance;
        [SerializeField] private ExperienceHolder _experienceHolder;
        [SerializeField] private GoldHolder _goldHolder;
        [SerializeField] private DamageDealer _damageDealer;
        
        private float _damage;
        private IObjectPool<Enemy> _pool;
        private EnemyTypeSO _enemyTypeSO;

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

        public void DefineType(EnemyTypeSO enemyType)
        {
            _enemyTypeSO = enemyType;

            int currentEvolutionLevel = _enemiesEvolutionTracker.GetEnemyEvolutionLevel(_enemyTypeSO);
            EnemyLevelData currentLevelData = _enemyTypeSO.GetEnemyLevelData(currentEvolutionLevel);
            SetupEnemy(currentLevelData);
        }

        public void Restore(Vector3 appearPoint, Transform target)
        {
            transform.position = appearPoint;
            _damagableObject.RestoreDurability();
            _enemyMover.Construct(target);
        }

        public void ReturnToPool()
        {
            if (isActiveAndEnabled)
            {
                Debug.Log(name + " defeated - return to pool");
                _goldHolder.DropGold();
                _experienceHolder.DropExperiencePoint(_enemyTypeSO);
                _pool.Release(this);
            }
        }

        private void ReturnToPoolOnPlayerTouched(IDamagable go)
        {
            if (isActiveAndEnabled)
            {
                go.TakeDamage(_damage);
                _pool.Release(this);
            }
        }

        private void SetupEnemy(EnemyLevelData levelData)
        {
            _damagableObject.SetupHealth(levelData.BaseHealth);
            _enemyMover.SetupSpeed(levelData.BaseSpeed);
            _enemyAppearance.SetupSprite(levelData.AppearanceSpriteFront);
            _damage = levelData.BaseDamage;
            _experienceHolder.SetupExperience(levelData.BaseExperience);
            _goldHolder.SetUp(levelData.BaseGold);
        }
    }
}