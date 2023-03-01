using System.Collections;
using UnityEngine;
using Random = System.Random;

namespace _ProjectSurvival.Scripts.Enemies.EnemySpawner
{
    public class EnemySpawnSystem : MonoBehaviour
    {
        [Header("Drag player from scene to transform point to spawn enemies around him")]
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private float _spawnDistance = 20;
        [SerializeField] private Wave[] _waves;

        private int _currentWave;
        private Transform _spawn;
        private Random _rand = new Random();
        private int _enemiesSpawnThisWave;

        private void Start()
        {
            _spawn = gameObject.transform;
            SpawnEnemiesManager();
        }

        private void Update()
        {
            CheckWaveStatus();
        }

        private void CheckWaveStatus()
        {
            if (_currentWave == _waves.Length - 1 && _waves[_currentWave].TimerSecondsThisWave <= 0 &&
                _enemiesSpawnThisWave <= 0)
            {
                StopAllCoroutines();
                Destroy(gameObject); 
                return;
            }

            _waves[_currentWave].TimerSecondsThisWave -= Time.deltaTime;

            if (_waves[_currentWave].TimerSecondsThisWave <= 0 && _currentWave < _waves.Length - 1 &&
                _enemiesSpawnThisWave <= 0)
            {
                StartNewWave();
            }
        }

        private void StartNewWave()
        {
            _currentWave += 1;
            SpawnEnemiesManager();
        }

        private void SpawnEnemiesManager()
        {
            for (int enemyBatch = 0; enemyBatch < _waves[_currentWave].Batches.Count; enemyBatch++)
            {
                StartCoroutine(EnemyBatchesManager(_waves[_currentWave].Batches[enemyBatch].SpawnSpeedSecondsThisWave,
                    enemyBatch, _currentWave));
                _enemiesSpawnThisWave += (_waves[_currentWave].Batches[enemyBatch].SpawnAmountThisWave) *
                                         (int)(_waves[_currentWave].TimerSecondsThisWave / _waves[_currentWave]
                                             .Batches[enemyBatch].SpawnSpeedSecondsThisWave);
            }
        }


        private IEnumerator EnemyBatchesManager(float spawnTimer, int enemyBatch, int currentWave)
        {
            yield return new WaitForSeconds(spawnTimer);

            if (_waves[currentWave].Batches[enemyBatch] == null) yield break;

            var spawnAmount =
                _waves[currentWave].Batches[enemyBatch]
                    .SpawnAmountThisWave;
            
            for (var enemiesSpawnAmount = 0; enemiesSpawnAmount < spawnAmount; enemiesSpawnAmount++)
            {
                if (_enemiesSpawnThisWave <= 0 || _currentWave != currentWave)
                {
                    yield break;
                }

                _enemiesSpawnThisWave -= 1;
                SpawnEnemies(enemyBatch, currentWave);

                if (_enemiesSpawnThisWave <= 0 || _currentWave != currentWave)
                {
                    yield break;
                }
            }

            if (_enemiesSpawnThisWave > 0 && _currentWave == currentWave)
            {
                StartCoroutine(EnemyBatchesManager(spawnTimer, enemyBatch, currentWave));
            }
        }

        private void SpawnEnemies(int enemyBatch, int currentWave)
        {
            var random = new Vector2(Mathf.Sin(_rand.Next(0, 360)),  Mathf.Cos(_rand.Next(0, 360)));
            var position =
                new Vector2(_spawnPoint.position.x, _spawnPoint.position.y);
            var dir = random.normalized;
            _spawn.transform.position = position + (dir * _spawnDistance);

            var enemy = Instantiate(
                _waves[currentWave].Batches[enemyBatch].EnemiesToSpawnThisWave[
                    _rand.Next(0, _waves[currentWave].Batches[enemyBatch].EnemiesToSpawnThisWave.Length)],
                new Vector2(_spawn.transform.position.x,_spawn.transform.position.y), Quaternion.identity);

            //if (enemy.GetComponentInChildren<Enemy>() == null) return;
            //var enemyToConstruct = enemy.GetComponentInChildren<Enemy>();
            //enemyToConstruct.ConstructDetectRadius(99999);
        }
    }
}
