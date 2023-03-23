using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _ProjectSurvival.Scripts.Enemies.Evolution.UI
{
    public class LevelEvolutionUI : MonoBehaviour
    {
        [Inject] private EnemiesEvolutionTracker _enemiesEvolutionTracker;
        [SerializeField] private EvolutionBarUI[] _evolutionBars;
        private int _initedCount;

        private void Start()
        {
            Init();
            _enemiesEvolutionTracker.OnEnemyAdded += UnlockEvolutionBarFor;
        }

        private void OnDestroy()
        {
            _enemiesEvolutionTracker.OnEnemyAdded -= UnlockEvolutionBarFor;
        }

        private void Init()
        {
            List<EnemyTypeSO> evolvingEnemies = _enemiesEvolutionTracker.GetEvolvingEnemies();
            for (int i = 0; i < _evolutionBars.Length; i++)
            {
                if (i < evolvingEnemies.Count)
                    UnlockEvolutionBarFor(evolvingEnemies[i]);
                else
                    _evolutionBars[i].Init();
            }
        }

        private void UnlockEvolutionBarFor(EnemyTypeSO enemyType)
        {
            if (!HasEmptyBar())
                return;

            _evolutionBars[_initedCount]
                .Init(enemyType, _enemiesEvolutionTracker.GetEnemyLeveling(enemyType));

            _initedCount++;
        }

        private bool HasEmptyBar()
        {
            return _initedCount < _evolutionBars.Length;
        }
    }
}
