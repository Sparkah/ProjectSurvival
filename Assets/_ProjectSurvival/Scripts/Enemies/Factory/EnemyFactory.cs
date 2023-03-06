using System.Collections.Generic;
using _ProjectSurvival.Scripts.Enemies;
using UnityEngine;
using Zenject;

public class EnemyFactory : MonoBehaviour
{
    [Inject] DiContainer _diContainer;
    [Inject(Id = "Player")] private Transform _player;
    [SerializeField] private EnemiesPool _enemiesPoolPrefab;
    private Transform _chasingTarget;
    private Dictionary<EnemyTypeSO, EnemiesPool> _enemiesPools = new Dictionary<EnemyTypeSO, EnemiesPool>();

    public void PreparePools(EnemyTypeSO[] enemyTypes, Transform chasingTarget)
    {
        _chasingTarget = chasingTarget;

        List<EnemyTypeSO> requiredTypes = new List<EnemyTypeSO>(enemyTypes);
        List<EnemyTypeSO> existingTypes = new List<EnemyTypeSO>(_enemiesPools.Keys);

        //Add new (if doesn't exist)
        for (int i = 0; i < requiredTypes.Count; i++)
        {
            if (!existingTypes.Contains(requiredTypes[i]))
                _enemiesPools.Add(requiredTypes[i], CreatePool(requiredTypes[i]));
        }

        //Remove previous (if not in use)
        for (int i = 0; i < existingTypes.Count; i++)
        {
            if (!requiredTypes.Contains(existingTypes[i]))
            {
                _enemiesPools[existingTypes[i]].MarkForRemoving();
                _enemiesPools.Remove(existingTypes[i]);
            }
        }
    }

    public void SpawnEnemy(EnemyTypeSO enemyType, Vector2 spawnPosition)
    {
        if (_enemiesPools.ContainsKey(enemyType))
        {
            Enemy enemy = _enemiesPools[enemyType].Pool.Get();
            enemy.DefineType(enemyType);
            enemy.Restore(spawnPosition, _chasingTarget.transform);
            enemy.SetPlayer(_player);
        }
    }

    private EnemiesPool CreatePool(EnemyTypeSO enemyType)
    {
        EnemiesPool enemiesPool = _diContainer
                    .InstantiatePrefabForComponent<EnemiesPool>(_enemiesPoolPrefab, transform);
        enemiesPool.InitPool(enemyType.EnemyPrefab);
        enemiesPool.name = string.Concat(enemyType.name, " Pool");
        return enemiesPool;
    }
}
