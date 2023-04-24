using _ProjectSurvival.Scripts.DamageSystem;
using _ProjectSurvival.Scripts.Enemies.Abilities;
using _ProjectSurvival.Scripts.Enemies.Evolution;
using _ProjectSurvival.Scripts.Enemies.Types;
using _ProjectSurvival.Scripts.Experience;
using _ProjectSurvival.Scripts.GameFlow.Statistics;
using _ProjectSurvival.Scripts.Gold;
using _ProjectSurvival.Scripts.Pool;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace _ProjectSurvival.Scripts.Enemies
{
    public class Enemy : MonoBehaviour, IPoolableObject<Enemy>
    {
        [Inject] private EnemiesEvolutionTracker _enemiesEvolutionTracker;
        [Inject] private SessionStatistics _gameStatistics;
        [SerializeField] private DamagableObject _damagableObject;
        [SerializeField] private EnemyMover _enemyMover;
        [SerializeField] private ObjectAppearance _enemyAppearance;
        [SerializeField] private ExperienceHolder _experienceHolder;
        [SerializeField] private GoldHolder _goldHolder;
        [SerializeField] private DamageDealer _damageDealer;
        [SerializeField] private EnemyAbilityAction _abilityAction;
        
        private float _damage;
        private IObjectPool<Enemy> _pool;
        private EnemyTypeSO _enemyTypeSO;
        private int _currentEvolutionLevel;

        public DamagableObject EnemyDamagable => _damagableObject;

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

            _currentEvolutionLevel = _enemiesEvolutionTracker.GetEnemyEvolutionLevel(_enemyTypeSO);
            EnemyLevelData currentLevelData = _enemyTypeSO.GetEnemyLevelData(_currentEvolutionLevel);
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
                _goldHolder.DropGold();
                _experienceHolder.DropExperiencePoint(_enemyTypeSO);
                _gameStatistics.AddStatistic(StatisticType.DefeatedEnemy, _currentEvolutionLevel);
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
            _abilityAction.SetUpAbility(levelData.EnemyAbilities);
            _damagableObject.SetupHealth(levelData.BaseHealth);
            _enemyMover.SetupSpeed(levelData.BaseSpeed);
            _enemyAppearance.SetupSprite(levelData.AppearanceSpriteFront, levelData.AppearanceSpriteBack);
            _damage = levelData.BaseDamage;
            _experienceHolder.SetupExperience(levelData.BaseExperience);
            _goldHolder.SetUp(levelData.BaseGold);
        }
    }
}